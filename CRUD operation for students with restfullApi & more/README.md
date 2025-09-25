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
er UI will be available...
