document.addEventListener('DOMContentLoaded', async function () {
    const userEmail = sessionStorage.getItem('userEmail');
    if (!userEmail) {
        window.location.href = '/Pages/Index';
        return;
    }

    try {
        const responseUser = await fetch(`/api/user/${userEmail}`);
        console.log('Resposta da API (usuário):', responseUser);
        if (responseUser.ok) {
            const user = await responseUser.json();

            document.getElementById('userName').innerText = `Bem-vindo (a), ${user.name}`;
            document.getElementById('userBalance').innerText = `Saldo Atual: R$ ${user.balance.toFixed(2)}`;

            const responseTransactions = await fetch(`/api/transaction/user/${user.uuid}`);
            console.log('Resposta da API (transações):', responseTransactions);
            if (responseTransactions.ok) {
                const transactions = await responseTransactions.json();

                if (transactions && transactions.length > 0) {
                    document.getElementById('transactionList').innerHTML = transactions
                        .map(t => {
                            let transactionType = '';
                            let transactionSpanClass = '';

                            if (t.typeTransaction === 0) {
                                transactionType = 'Entrada';
                                transactionSpanClass = 'bg-success';
                            } else if (t.typeTransaction === 1) {
                                transactionType = 'Saída';
                                transactionSpanClass = 'bg-danger';
                            }

                            return `<li class="list-group-item">Tipo: <span class="${transactionSpanClass} text-white px-2">${transactionType}</span> | Título: ${t.description} | Valor: R$ ${t.amount.toFixed(2)}</li>`;
                        })
                        .join('');
                } else {
                    document.getElementById('transactionList').innerHTML = '<li class="list-group-item text-center">Nenhuma transação encontrada.</li>';
                }
            } else {
                console.error('Erro ao carregar transações do usuário.');
            }
        } else {
            console.error('Erro ao carregar informações do usuário.');
        }
    } catch (error) {
        console.error('Erro de conexão:', error);
        console.log('Email do usuário:', userEmail);
    }
});