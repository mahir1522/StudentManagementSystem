using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentManagementSystem.Models;
using System.Net;
using System.Net.Mail;

namespace StudentManagementSystem.Controllers
{
	public class CourseController : Controller
	{
		private CourseContext _context { get; set; }
		public CourseController(CourseContext ctx) => _context = ctx;

		public IActionResult All()
		{
			var courses = _context.Courses.ToList();

			foreach(var course in courses)
			{
				course.GetNumberOfStudents = _context.Students.Count(s => s.CourseId == course.Id);
			}

			return View(courses);
		}

		[HttpGet]
		public IActionResult Add()
		{
			ViewBag.Action = "Add";
			return View("Edit", new Course());
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			ViewBag.Action = "Edit";
			var course = _context.Courses.Find(id);
			return View(course);
		}
		[HttpPost]
		public IActionResult Edit(Course course)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (course.Id == 0)
						_context.Courses.Add(course);
					else
						_context.Courses.Update(course);
					_context.SaveChanges();
					return RedirectToAction("Manage", new { id = course.Id });
				}
				else
				{
					ViewBag.Action = (course.Id == 0) ? "Add" : "Edit";
					return View(course);
				}
			}
            catch (Exception ex)
            {

                return View("Error");
            }
        }
		[HttpGet]
		public IActionResult Manage(int id)
		{
			var course = _context.Courses.Include(c => c.Students).FirstOrDefault(c => c.Id == id);
			var viewModel = new CourseInfoViewModel
			{
				Course = course,
				NewStudent = new Student()
			};
			return View(viewModel);
		}
		[HttpPost]
		public IActionResult Manage(int id, CourseInfoViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (viewModel.NewStudent != null)
					{

						viewModel.NewStudent.CourseId = id;
						_context.Students.Add(viewModel.NewStudent);
						_context.SaveChanges();
					}
					return RedirectToAction("Manage", new { id });
				}
				else
				{
					viewModel.Course = _context.Courses.Include(c => c.Students).FirstOrDefault(c => c.Id == id);
					return View(viewModel);
				}
			}
            catch (Exception ex)
            {

                return View("Error");
            }
        }

		public IActionResult Emailconfirmationform(int courseId, int studentId)
		{
			var course = _context.Courses.Include(c=>c.Students).FirstOrDefault(c=>c.Id == courseId);
			var student = _context.Students.FirstOrDefault(c=>c.S_Id == studentId);	

			CourseInfoViewModel model = new CourseInfoViewModel
			{
				Course = course,
				NewStudent = student
			};

			return View("Email-confirmation-form", model);
		}

		public async Task<IActionResult> SendEmailToAll(int courseId)
		{
			try
			{
				var course = _context.Courses.Include(c => c.Students).FirstOrDefault(c => c.Id == courseId);

				if (course != null)
				{
					foreach (var student in course.Students)
					{
						string sub = $"Enrollment confirmation for {course.Name} required";
						string Confirmationlink = Url.Action("Emailconfirmationform", "Course", new { courseId = course.Id, studentId = student.S_Id }, HttpContext.Request.Scheme);

						string body = $"<b>Hello {student.Name}:</b><br  /> Your request to enroll in the course {course.Name} so if you could <br />{Confirmationlink}<br /> as soon as possible that would be appriciated! <br /> Sincerely,<br />The Course Manager App";
						await SendEmailAsync(student.Email, sub, body);

						student.Status = Student.EnrollmentStatus.ConfirmationMessageSent;
						_context.Students.Update(student);
						_context.SaveChanges();
					}
				}
				return RedirectToAction("Manage", new { id = courseId });
			}
			catch (Exception ex)
			{

				return View("Error");
			}
        }


		public async Task SendEmailAsync(string to, string subject, string body)
		{
			using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
			{
				client.Port = 587;
				client.Credentials = new NetworkCredential("add_your_email", "add_your_password");
				client.EnableSsl = true;

				MailMessage message = new MailMessage
				{
					From = new MailAddress("add_your_email"),
					Subject = subject,
					Body = body,
					IsBodyHtml = true,
				};

				message.To.Add(to);
				await client.SendMailAsync(message);
			}

		}
		[HttpPost]
		public IActionResult SubmitEnrollment(int courseId,int studentId, string enrollmentStatus)
		{
			try
			{
				var student = _context.Students.FirstOrDefault(c => c.S_Id == studentId);

				if (enrollmentStatus == "Yes")
				{
					student.Status = Student.EnrollmentStatus.EnrollmentConfirmed;
				}
				else if (enrollmentStatus == "No")
				{
					student.Status = Student.EnrollmentStatus.EnrollmentDeclined;
				}
				_context.Students.Update(student);
				_context.SaveChanges();


				return View("Thankyou");
			}
            catch (Exception ex)
            {
                return View("Error");
            }
        }

	}
}
