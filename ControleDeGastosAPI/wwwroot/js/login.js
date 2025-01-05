document.getElementById('loginForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    // Verifica se ambos os campos foram preenchidos
    if (!email || !password) {
        document.getElementById('errorMessage').innerText = 'Email e senha são obrigatórios.';
        document.getElementById('errorMessage').style.display = 'block';
        return;
    }

    try {
        // Envia os dados como um array de strings (conforme esperado pela API)
        const response = await fetch('/api/user/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify([email, password]) // Envia o email e senha como um array
        });

        // Verifica se a resposta foi bem-sucedida
        if (response.ok) {
            const data = await response.json();

            // Armazena o email no sessionStorage
            sessionStorage.setItem('userEmail', email);

            // Redireciona para a página Home
            window.location.href = '/Pages/Home';
        } else {
            const data = await response.json();
            document.getElementById('errorMessage').innerText = data.message || 'Erro de autenticação.';
            document.getElementById('errorMessage').style.display = 'block';
        }
    } catch (error) {
        // Exibe o erro se houver um problema de conexão
        document.getElementById('errorMessage').innerText = 'Erro de conexão: ' + error.message;
        document.getElementById('errorMessage').style.display = 'block';
    }
});