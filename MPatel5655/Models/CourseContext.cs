using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Models
{
	public class CourseContext : DbContext
	{
		public CourseContext(DbContextOptions<CourseContext> options) : base(options) { }

		public DbSet<Course> Courses { get; set; }
		public DbSet<Student> Students { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Course>().HasData(

				new Course
				{
					Id = 1,
					Name = "Test",
					Instuctor = "Holl",
					Date = new DateTime(2004, 10, 03),
					RoomNumber = "2G12",
				},

				new Course
				{
					Id = 2,
					Name = "Test2",
					Instuctor = "Moll",
					Date = new DateTime(1984, 3, 13),
					RoomNumber = "4F31"
				},
				new Course
				{
					Id=3,
					Name = "test3",
					Instuctor = "helo",
					Date = new DateTime(2020,09,12),
					RoomNumber = "5G12"
				}
				);

			modelBuilder.Entity<Student>()
				.HasKey(s => s.S_Id);

			modelBuilder.Entity<Student>().HasData(

				new Student
				{
					S_Id = 1,
					Name = "John Mohn",
					Email = "john@example.com",
					CourseId = 1,
				},
				new Student
				{
					S_Id = 2,
					Name = "Smith will",
					Email = "Smithwill@example.com",
					CourseId = 2,
				},
				new Student
				{
					S_Id= 3,
					Name = "Test3",
					Email = "Testing@gmail.com",
					CourseId = 3,
				},
				new Student
				{
					S_Id = 4,
					Name = "test4",
					Email = "Test4@gmail.com",
					CourseId = 3,
				}
				);

			modelBuilder.Entity<Student>()
				.HasOne(s => s.Course)
				.WithMany(c => c.Students)
				.HasForeignKey(s => s.CourseId);
		}
	}
}
