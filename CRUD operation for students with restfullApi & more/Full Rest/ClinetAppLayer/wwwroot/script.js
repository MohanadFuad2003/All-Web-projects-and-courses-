const apiUrl = "https://localhost:7189/api/Students"; 

// ---------- Get Methods ----------
async function getAllStudents() {
    fetchData(`${apiUrl}/GetAllStudents`, "All Students");
}

async function getPassedStudents() {
    fetchData(`${apiUrl}/GetPassedStudents`, "Passed Students");
}

async function getMaxGradeStudents() {
    fetchData(`${apiUrl}/GetStudentsWithMaxGrade`, "Top Students");
}

async function getAverageGrade() {
    try {
        const res = await fetch(`${apiUrl}/GetAverageGrade`);
        if (!res.ok) throw new Error(await res.text());
        const data = await res.json();
        document.getElementById("result").innerHTML =
            `<h3>Average Grade</h3>
             <div class="success">${data}</div>`;
    } catch (err) {
        showError(err);
    }
}

async function getStudentById() {
    const id = document.getElementById("studentIdSearch").value;
    if (!id) return alert("Enter an ID");
    fetchData(`${apiUrl}/GetStudentById/${id}`, `Student ${id}`);
}

// ---------- Post ----------
async function addStudent() {
    const newStudent = {
        name: document.getElementById("addName").value,
        age: parseInt(document.getElementById("addAge").value),
        grade: parseInt(document.getElementById("addGrade").value)
    };

    try {
        const res = await fetch(`${apiUrl}/AddStudents`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(newStudent)
        });
        if (!res.ok) throw new Error(await res.text());
        const data = await res.json();
        showSuccess(`Added: ${data.name} (ID: ${data.id})`);
    } catch (err) {
        showError(err);
    }
}

// ---------- Put ----------
async function updateStudent() {
    const id = document.getElementById("updateId").value;
    const updatedStudent = {
        id: parseInt(id),
        name: document.getElementById("updateName").value,
        age: parseInt(document.getElementById("updateAge").value),
        grade: parseInt(document.getElementById("updateGrade").value)
    };

    try {
        const res = await fetch(`${apiUrl}/UpdateStudent/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(updatedStudent)
        });
        if (!res.ok) throw new Error(await res.text());
        const data = await res.json();
        showSuccess(`Updated: ${data.name} (ID: ${data.id})`);
    } catch (err) {
        showError(err);
    }
}

// ---------- Delete ----------
async function deleteStudent() {
    const id = document.getElementById("deleteId").value;
    if (!id) return alert("Enter an ID");

    try {
        const res = await fetch(`${apiUrl}/DeleteStudent?id=${id}`, {
            method: "PUT" 
        });
        if (!res.ok) throw new Error(await res.text());
        showSuccess(`Student with ID ${id} deleted`);
    } catch (err) {
        showError(err);
    }
}

// ---------- Helpers ----------
async function fetchData(url, title) {
    try {
        const res = await fetch(url);
        if (!res.ok) throw new Error(await res.text());
        const data = await res.json();
        renderTable(data, title);
    } catch (err) {
        showError(err);
    }
}

function renderTable(data, title) {
    if (!data || (Array.isArray(data) && data.length === 0)) {
        document.getElementById("result").innerHTML =
            `<h3>${title}</h3><p>No data found.</p>`;
        return;
    }
    if (!Array.isArray(data)) {
        data = [data];
    }
    let html = `<h3>${title}</h3>
                <table>
                  <thead>
                    <tr>
                      <th>ID</th><th>Name</th><th>Age</th><th>Grade</th>
                    </tr>
                  </thead>
                  <tbody>`;
    data.forEach(stu => {
        html += `<tr>
                   <td>${stu.id}</td>
                   <td>${stu.name}</td>
                   <td>${stu.age}</td>
                   <td>${stu.grade}</td>
                 </tr>`;
    });
    html += "</tbody></table>";
    document.getElementById("result").innerHTML = html;
}

function showError(err) {
    document.getElementById("result").innerHTML =
        `<p class="error">❌ ${err}</p>`;
}

function showSuccess(msg) {
    document.getElementById("result").innerHTML =
        `<p class="success">✅ ${msg}</p>`;
}

function updateClock() {
    const now = new Date();
    document.getElementById("clock").innerText =
        now.toLocaleTimeString();
}
setInterval(updateClock, 1000);
updateClock();
