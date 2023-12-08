import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import HttpPost from '../services/HttpPost';

const CombustivelCreatePage = () => {
    const [nome, setNome] = useState('');
    const [preco, setPreco] = useState('');
    const [mensagem, setMensagem] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        const combustivel = { nome, preco };

        const sucesso = await HttpPost('https://localhost:7200/api/Combustivel', combustivel);

        if (sucesso) {
            setMensagem('Dados adicionados com sucesso!');
            setTimeout(() => {
                navigate('/CombustiveisPage');
            }, 1000);
        } else {
            setMensagem('Falha ao adicionar dados.');
        }
    }

    return (
        <div className="create">
            <h2>Adicionar um novo combustivel</h2>
            <form onSubmit={handleSubmit}>
                <label>Nome do combustivel:</label>
                <input
                    type="text"
                    required
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                />
                <label>Preço do combustível:</label>
                <input
                    type="number"
                    step="0.01"
                    required
                    value={preco}
                    onChange={(e) => setPreco(e.target.value)}
                />
                <button>Incluir</button>
            </form>
            <p className={mensagem ? (mensagem.includes('sucesso') ? 'success' : 'error') : ''}>{mensagem}</p>
        </div>
    );
}

export default CombustivelCreatePage;
