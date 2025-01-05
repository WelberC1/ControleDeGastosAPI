document.getElementById('registerForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const name = document.getElementById('name').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    if (!name || !email || !password || !confirmPassword) {
        document.getElementById('errorMessage').innerText = 'Todos os campos são obrigatórios.';
        document.getElementById('errorMessage').style.display = 'block';
        return;
    }

    if (password !== confirmPassword) {
        document.getElementById('errorMessage').innerText = 'As senhas não coincidem.';
        document.getElementById('errorMessage').style.display = 'block';
        return;
    }

    try {
        const response = await fetch('/api/user/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ name, email, password })
        });

        if (response.ok) {
            document.getElementById('successMessage').innerText = 'Conta criada com sucesso! Redirecionando...';
            document.getElementById('successMessage').style.display = 'block';
            setTimeout(() => window.location.href = '/Pages/Index', 3000);
        } else {
            const data = await response.json();
            document.getElementById('errorMessage').innerText = data.message;
            document.getElementById('errorMessage').style.display = 'block';
        }
    } catch (error) {
        document.getElementById('errorMessage').innerText = 'Erro de conexão: ' + error.message;
        document.getElementById('errorMessage').style.display = 'block';
    }
});