// script.js (modified to require login + show teacher name + logout)
const apiUrl = "https://localhost:7189/api/Students";

// --------- Authentication helpers ----------
function getLoggedTeacher() {
    return localStorage.getItem("teacherName");
}

function ensureAuthenticated() {
    const teacher = getLoggedTeacher();
    if (!teacher) {
        // not logged in -> redirect to login page
        window.location.href = "login.html";
        return false;
    }
    return true;
}

function logout() {
    localStorage.removeItem("teacherName");
    window.location.href = "login.html";
}

// show teacher name in header and add logout button
function decorateHeader() {
    const teacher = getLoggedTeacher();
    const headerH1 = document.querySelector("header h1");
    if (headerH1) {
        if (teacher) {
            headerH1.innerText = `🎓 Welcome ${teacher} - Students Management Dashboard`;
        } else {
            headerH1.innerText = `🎓 Students Management Dashboard`;
        }
    }

    // add logout button (if not already)
    let header = document.querySelector("header");
    if (header && teacher) {
        if (!document.getElementById("logoutBtn")) {
            const btn = document.createElement("button");
            btn.id = "logoutBtn";
            btn.textContent = "Logout";
            btn.style.marginLeft = "12px";
            btn.onclick = logout;
            // put it next to clock-container if exists, else append
            const clockContainer = document.getElementById("clock-container");
            if (clockContainer) clockContainer.appendChild(btn);
            else header.appendChild(btn);
        }
    }
}

// call on load
decorateHeader();

// redirect to login if not authenticated
// allow login.html itself to load (so this script must be used on index.html only)
if (!window.location.pathname.endsWith("login.html")) {
    ensureAuthenticated();
}

// ---------- Get Methods ----------
async function getAllStudents() {
    if (!ensureAuthenticated()) return;
    fetchData(`${apiUrl}/GetAllStudents`, "All Students");
}

async function getPassedStudents() {
    if (!ensureAuthenticated()) return;
    fetchData(`${apiUrl}/GetPassedStudents`, "Passed Students");
}

async function getMaxGradeStudents() {
    if (!ensureAuthenticated()) return;
    fetchData(`${apiUrl}/GetStudentsWithMaxGrade`, "Top Students");
}

async function getAverageGrade() {
    if (!ensureAuthenticated()) return;
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
    if (!ensureAuthenticated()) return;
    const id = document.getElementById("studentIdSearch").value;
    if (!id) return alert("Enter an ID");
    fetchData(`${apiUrl}/GetStudentById/${id}`, `Student ${id}`);
}

// ---------- Post ----------
async function addStudent() {
    if (!ensureAuthenticated()) return;
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
    if (!ensureAuthenticated()) return;
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
    if (!ensureAuthenticated()) return;
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
    const msg = (err && err.message) ? err.message : String(err);
    document.getElementById("result").innerHTML =
        `<p class="error">❌ ${escapeHtml(msg)}</p>`;
}

function showSuccess(msg) {
    document.getElementById("result").innerHTML =
        `<p class="success">✅ ${escapeHtml(msg)}</p>`;
}

// small escaping helper
function escapeHtml(unsafe) {
    return String(unsafe)
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}

function updateClock() {
    const now = new Date();
    document.getElementById("clock").innerText =
        now.toLocaleTimeString();
}
setInterval(updateClock, 1000);
updateClock();
