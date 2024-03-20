using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
	public class Course
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Provide valid course name")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Provide valid Instuctor name")]
		public string? Instuctor { get; set; }

		[Required(ErrorMessage = "Provide valid date")]
		public DateTime Date { get; set; }

		[Required, RegularExpression(@"^[0-9][A-Z]\d{2}$", ErrorMessage = "Provide room number in this format : 1A23")]
		public string? RoomNumber { get; set; }

		public List<Student>? Students { get; set; }

		public int GetNumberOfStudents {  get; set; }

	}
}
