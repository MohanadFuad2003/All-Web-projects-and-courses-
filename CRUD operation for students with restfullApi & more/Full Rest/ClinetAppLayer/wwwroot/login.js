const apiUrl = "https://localhost:7189/api/Teachers";

async function login() {
    const username = document.getElementById("username").value.trim();
    const password = document.getElementById("password").value.trim();

    if (!username || !password) {
        document.getElementById("loginResult").innerHTML =
            `<p class="error">❌ Please enter username and password</p>`;
        return;
    }

    try {
        const res = await fetch(`${apiUrl}/Login?username=${username}&password=${password}`, {
            method: "POST"
        });

        if (!res.ok) throw new Error(await res.text());

        const data = await res.text();

        localStorage.setItem("teacherName", username);

        document.getElementById("loginResult").innerHTML =
            `<p class="success">${data}</p>`;

        setTimeout(() => {
            window.location.href = "index.html";
        }, 1000);

    } catch (err) {
        document.getElementById("loginResult").innerHTML =
            `<p class="error">❌ ${err}</p>`;
    }
}
