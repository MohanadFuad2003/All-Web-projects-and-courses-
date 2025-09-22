# 🎓 Student Management System

This project is a **multi-layered student management system** built with a clean architecture approach.  
It provides a full CRUD (Create, Read, Update, Delete) API for managing student records and a simple client-side interface.

---

## 📌 Project Structure

The solution is organized into multiple layers:

1. **Data Access Layer (DAL)**  
   - Implemented as a **Class Library (.NET Standard)**.  
   - Uses **ADO.NET** with `SqlConnection`, `SqlCommand`, and **Stored Procedures**.  
   - Responsible for direct communication with the **SQL Server** database.  
   - Example class: `StudentData`.

2. **Business Layer (BL)**  
   - Implemented as a **Class Library (.NET Standard)**.  
   - Contains business logic and acts as a bridge between the API and the DAL.  
   - Example class: `clsStudents`.

3. **API Layer (Web Layer)**  
   - Implemented as an **ASP.NET Core Web API**.  
   - Provides RESTful endpoints to interact with the system.  
   - Includes validation, error handling, and Swagger/OpenAPI integration.  
   - Main controller: `StudentAPIController`.

4. **Client Side**  
   - Built with **HTML, CSS, and JavaScript**.  
   - Provides a simple front-end UI to interact with the API.  
   - Supports viewing, adding, updating, and deleting students.  
   - Uses `fetch` API for asynchronous calls.

---

## 🗄️ Database

- **SQL Server Local Database**  
- Database: `StudentsDB`  
- Table: `Students`  

### Table Schema
```sql
CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Grade INT NOT NULL
);

-----------

Stored Procedures : 

SP_GetAllStudents

SP_GetPassedStudents

SP_GetAverageGrade

SP_GetStudentById

SP_AddStudent

SP_UpdateStudent

SP_DeleteStudent


------------------

API Endpoint : https://localhost:7189/api/Students

------------------

StudentAPIController

GET /GetAllStudents
Returns all students.

GET /GetPassedStudents
Returns students with grade >= 50.

GET /GetAverageGrade
Returns the average grade.

GET /GetStudentsWithMaxGrade
Returns students with the maximum grade.

GET /GetStudentById/{id}
Returns a student by ID.

POST /AddStudents
Adds a new student.

PUT /UpdateStudent/{id}
Updates an existing student.

PUT /DeleteStudent?id={id}
Deletes a student by ID.

💻 Client Features

Modern interface built with HTML, CSS, JavaScript.

Includes:

List all students

View passed students

View max grade students

View average grade

Search student by ID

Add new student

Update student

Delete student

Results are displayed dynamically inside the page.

Includes a live digital clock and styled notifications for success/error.

⚙️ Requirements

.NET 6/7 SDK

SQL Server LocalDB or SQL Server Express

Visual Studio 2022 or Visual Studio Code

Browser (for client UI)

📖 How to Run

Database Setup

Restore the StudentsDB database.

Create the Students table.

Add stored procedures listed above.

Backend (API)

Open the solution in Visual Studio.

Run the ApiWebLayer project.

Swagger UI will be available at:
