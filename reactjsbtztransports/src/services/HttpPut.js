const HttpPut = async (url, dados) => {
    try {
        const response = await fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dados)
        });

        if (!response.ok) {
            throw new Error('Erro ao atualizar dados no servidor');
        }

        console.log('Dados atualizados com sucesso');
        return true;
    } catch (error) {
        console.error('Erro:', error);
        return false;
    }
};

export default HttpPut;
