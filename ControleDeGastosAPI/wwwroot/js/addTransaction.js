document.getElementById('addTransactionForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const userUUID = sessionStorage.getItem('userUUID');

    if (!userUUID) {
        document.getElementById('errorMessage').innerText = 'Usuário não autenticado.';
        document.getElementById('errorMessage').style.display = 'block';
        return;
    }

    const transactionType = document.getElementById('transactionType').value;
    const amount = parseFloat(document.getElementById('amount').value);
    const description = document.getElementById('description').value;
    const category = document.getElementById('category').value;

    if (!transactionType || isNaN(amount) || !description || !category) {
        document.getElementById('errorMessage').innerText = 'Todos os campos são obrigatórios.';
        document.getElementById('errorMessage').style.display = 'block';
        return;
    }

    const transactionTypeMap = {
        'EXPENSE': 1,
        'INCOME': 0
    };

    const transactionCategoryMap = {
        'FOOD': 0,
        'TRANSPORTATION': 1,
        'ENTERTAINMENT': 2,
        'HEALTH': 3,
        'BILLS': 4,
        'SALARIES':5,
        'MISCELLANEOUS':6
    };

    const typeTransaction = transactionTypeMap[transactionType.toUpperCase()];
    const transactionCategory = transactionCategoryMap[category.toUpperCase()];

    if (typeTransaction === undefined || transactionCategory === undefined) {
        document.getElementById('errorMessage').innerText = 'Tipo de transação ou categoria inválidos.';
        document.getElementById('errorMessage').style.display = 'block';
        return;
    }

    const transactionData = {
        userUUID: userUUID,
        TypeTransaction: typeTransaction,
        Amount: amount,
        Description: description,
        TransactionCategory: transactionCategory
    };

    console.log("Dados da requisição:", JSON.stringify(transactionData, null, 2));

    try {
        const response = await fetch('/api/transaction', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(transactionData)
        });

        if (response.ok) {
            document.getElementById('successMessage').innerText = 'Transação adicionada com sucesso!';
            document.getElementById('successMessage').style.display = 'block';
            document.getElementById('addTransactionForm').reset();
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
