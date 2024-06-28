# .NET-Core-MVC--E-commerce-app
An E-commerce application using ASP.NET Core MVC, Entity Framework Core and ASP.NET Core Identity.
<img width="960" alt="Bulky Homepage" src="https://github.com/reggyshicky/.NET-Core-MVC--E-commerce-app/assets/122837010/5bb196da-6082-44b3-9c93-343b01aa8556">


# ASP.NET Core Identity with MVC Course Project
This repository contains the project I developed during the ASP.NET Core Identity with MVC course. The course focused on teaching important skills related to the new identity system for ASP.NET Core and provided hands-on experience in implementing authentication, authorization, and user management in ASP.NET Core MVC applications.

# Course Overview
The ASP.NET Core Identity with MVC course covered a wide range of topics, enabling me to build a comprehensive project that incorporates various aspects of identity management. The key concepts and features covered in the course include:

- ASP.NET Core Identity with MVC: Understanding the fundamentals of integrating identity management into MVC applications.
- Authorization with Roles: Implementing role-based authorization to control access to different parts of the application.
- Implementing Two-Factor Authentication: Enhancing security by adding two-factor authentication to user accounts.
- Implementing Sign Up and Sign-in with E-mail Confirmation: Creating a seamless user registration and login process with e-mail confirmation.
- User, Claims, and Role Management: Managing user accounts, defining custom claims, and handling user roles effectively.
- Scaffold Identity Library: Generating the necessary code and views for authentication and user management using the Identity Library.
- Two-Factor Authentication with MVC: Extending the implementation of two-factor authentication within the MVC framework.
- External Logins in MVC: Integrating external logins, such as social media accounts, for user authentication.
- Policy Management: Implementing advanced authorization techniques using policy-based authorization.
- Custom Handler and Requirements: Building custom handlers and requirements for fine-grained access control.

# Project Details
- The project developed during the course is a comprehensive MVC application that incorporates all the concepts covered. It provides a secure and user-friendly environment for user authentication, authorization, and management. The key features of the project include:

- User Registration and Login: A robust user registration and login system that supports e-mail confirmation and password security measures.

- Role-Based Authorization: Implementation of role-based authorization to restrict access to different sections and functionalities of the application based on user roles.

- Two-Factor Authentication: Integration of two-factor authentication to add an extra layer of security for user accounts.

- External Logins: Integration of external login options, allowing users to sign in using their social media accounts using Facebook and Gmail Account. You can add more at
  program.cs

 - builder.Services.AddAuthentication().
   AddFacebook(options =>
   {
       options.AppId = "<YOUR API KEY>";
       options.AppSecret = "<YOUR SECRET KEY>";
   }).AddGoogle(options =>  
   {
       options.ClientId = "<YOUR API KEY>";
       options.ClientSecret = "<YOUR SECRET KEY>";
   });
- User and Role Management: An intuitive user management system that enables administrators to manage user accounts, assign roles, and customize user permissions.

- Custom Policies: Implementation of custom policies to control access to specific resources and functionalities based on fine-grained authorization requirements.

# Getting Started
- To run the project locally and explore the implemented features, follow these steps:

- Clone the repository to your local machine using the following command:

- git clone https://github.com/Peter19995/ASP.NET-Core-Identity-with-MVC.git
- Navigate to the project directory and open the project solution in Visual Sutido.

- Install the necessary dependencies.

- Configure the database connection string in the appsettings.json file.

"ConnectionStrings": {
    "DefaultConnection": "Server=<Your Server Name>;Database=IdentityManagerDb;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true;User=sa;Password=<Your Password>;"
}
- Apply the database migrations by running the following command in Package Manager Console

dotnet add-migiration <YourMigrationName>
then

dotnet update-database
- Run your application in Visual Studio.

- Access the application in your web browser at http://localhost:<portnumber> or https://localhost:<portnumber>.

# Contributions and Feedback
- I am open to contributions and appreciate any feedback on the project. If you encounter any issues or have suggestions for improvement, please feel free to create an issue in the repository.

- Let's continue learning and building secure and robust web applications with ASP.NET Core Identity and MVC!

- Happy coding!
