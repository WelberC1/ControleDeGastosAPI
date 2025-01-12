document.getElementById('loginForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    if (!email || !password) {
        document.getElementById('errorMessage').innerText = 'Email e senha são obrigatórios.';
        document.getElementById('errorMessage').style.display = 'block';
        return;
    }

    try {
        const response = await fetch('/api/user/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify([email, password])
        });

        if (response.ok) {
            const data = await response.json();

            sessionStorage.setItem('userEmail', email);

            window.location.href = '/Pages/Home';
        } else {
            const data = await response.json();
            document.getElementById('errorMessage').innerText = data.message || 'Erro de autenticação.';
            document.getElementById('errorMessage').style.display = 'block';
        }
    } catch (error) {
        document.getElementById('errorMessage').innerText = 'Erro de conexão: ' + error.message;
        document.getElementById('errorMessage').style.display = 'block';
    }
});