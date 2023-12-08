import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import HttpPost from '../services/HttpPost';

const UsuarioCreatePage = () => {
    const [nome, setNome] = useState('');
    const [senha, setSenha] = useState('');
    const [mensagem, setMensagem] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        const usuario = { nome, senha };

        const sucesso = await HttpPost('https://localhost:7200/api/Usuario', usuario);

        if (sucesso) {
            setMensagem('Dados adicionados com sucesso!');
            setTimeout(() => {
                navigate('/UsuariosPage');
            }, 1000);
        } else {
            setMensagem('Falha ao adicionar dados.');
        }
    }

    return (
        <div className="create">
            <h2>Adicionar um novo usuario</h2>
            <form onSubmit={handleSubmit}>
                <label>login:</label>
                <input
                    type="text"
                    required
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                />
                <label>Senha:</label>
                <input
                    type="password"
                    required
                    value={senha}
                    onChange={(e) => setSenha(e.target.value)}
                />
                <button>Incluir</button>
            </form>
            <p className={mensagem ? (mensagem.includes('sucesso') ? 'success' : 'error') : ''}>{mensagem}</p>
        </div>
    );
}

export default UsuarioCreatePage;
