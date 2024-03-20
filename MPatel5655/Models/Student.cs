using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
	public class Student
	{
		public int S_Id { get; set; }

		[Required(ErrorMessage = "Enter student name")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Enter email id")]
		[EmailAddress(ErrorMessage ="Valid email id")]
		public string? Email { get; set; }
		public EnrollmentStatus Status { get; set; } = EnrollmentStatus.ConfirmationMessageNotSent;

		public enum EnrollmentStatus
		{
			ConfirmationMessageNotSent,
			ConfirmationMessageSent,
			EnrollmentConfirmed,
			EnrollmentDeclined
		}

		public int CourseId { get; set; }

		public Course? Course { get; set; }
	}
}
