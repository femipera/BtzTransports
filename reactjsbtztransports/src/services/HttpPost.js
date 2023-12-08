const HttpPost = async (url, dados) => {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(dados)
        });

        if (!response.ok) {
            throw new Error('Erro ao adicionar dados no servidor');
        }

        console.log('Novo dado adicionado com sucesso');
        return true;
    } catch (error) {
        console.error('Erro:', error);
        return false;
    }
};

export default HttpPost;