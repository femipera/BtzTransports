const HttpDelete = async (url) => {
    try {
        const response = await fetch(url, {
            method: 'DELETE',
        });

        if (!response.ok) {
            throw new Error('Erro ao excluir dados no servidor');
        }

        console.log('Dado excluído com sucesso');
        return true;
    } catch (error) {
        console.error('Erro:', error);
        return false;
    }
};

export default HttpDelete;