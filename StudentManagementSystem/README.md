# Student Management System

## Overview
This repository contains the code for a Student Management System developed using ASP.NET Core MVC. The system allows for the management of courses, student enrollments, and confirmation of enrollment messages.

## Functionality
- **View Courses:** View all courses along with the number of students enrolled in each course.
- **Add/Edit Courses:** Add new courses or edit existing ones.
- **Manage Course:** View course details, add new students to a course, and send enrollment confirmation emails.
- **Email Confirmation:** Students receive an email requesting confirmation of enrollment with a link to respond.
- **Submit Enrollment:** Students can submit their enrollment status via the provided link in the email.

## Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Installation
1. Clone this repository to your local machine.
2. Open the project in Visual Studio or Visual Studio Code.
3. Restore packages and dependencies.
4. Update the `EmailService.cs` file with your Gmail credentials for sending emails.

## Usage
1. Run the application.
2. Navigate to the course management interface.
3. Add/edit courses as needed.
4. Manage courses to add students and send enrollment confirmation emails.
5. Students receive enrollment confirmation emails and can respond via the provided link.

## Important Note
Ensure that you replace the placeholder values in the `EmailService.cs` file with your actual Gmail address and password. This is required for the email functionality to work properly.


## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments
- [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/core/)
- [Stack Overflow](https://stackoverflow.com/)
- [C# Corner](https://www.c-sharpcorner.com/)
