import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import HttpGet from '../services/HttpGet';
import HttpPut from '../services/HttpPut';
import HttpDelete from '../services/HttpDelete';

const FabricanteEditPage = () => {
    const { id } = useParams();
    const [fabricante, setFabricante] = useState({ id: '', nome: '' });
    const [mensagem, setMensagem] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        const carregarFabricante = async () => {
            try {
                const response = await HttpGet(`https://localhost:7200/api/Fabricante/${id}`);
                if (response && Object.keys(response).length !== 0) {
                    setFabricante(response); // Atualiza o estado com os dados do fabricante retornado pela API
                }
            } catch (error) {
                console.error('Erro ao carregar dados do fabricante', error);
            }
        };

        if (id) {
            carregarFabricante(); // Carrega os dados do fabricante apenas se houver um ID
        }
    }, [id]);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const { id, nome } = fabricante;

        const sucesso = await HttpPut(`https://localhost:7200/api/Fabricante/${id}`, { nome });

        if (sucesso) {
            setMensagem('Dados atualizados com sucesso!');
            setTimeout(() => {
                navigate('/FabricantesPage');
            }, 1000);
        } else {
            setMensagem('Falha ao atualizar dados.');
        }
    };

    const handleDelete = async () => {
        const confirmacao = window.confirm('Tem certeza que deseja excluir este fabricante?');

        if (confirmacao) {
            const { id } = fabricante;
            const sucesso = await HttpDelete(`https://localhost:7200/api/Fabricante/${id}`);

            if (sucesso) {
                setMensagem('Fabricante excluído com sucesso!');
                setTimeout(() => {
                    navigate('/FabricantesPage');
                }, 1000);
            } else {
                setMensagem('Falha ao excluir fabricante.');
            }
        }
    };

    const handleVoltar = () => {
        navigate('/FabricantesPage');
    };

    return (
        <div className="create">
            <h2>Editar ou excluir fabricante</h2>
            <form onSubmit={handleSubmit}>
                <label>ID do fabricante:</label>
                <input
                    type="text"
                    value={fabricante.id}
                    readOnly
                />
                <label>Nome do fabricante:</label>
                <input
                    type="text"
                    required
                    value={fabricante.nome}
                    onChange={(e) => setFabricante({ ...fabricante, nome: e.target.value })}
                />
                <div style={{ display: 'flex', gap: '8px' }}>
                    <button style={{ flex: 1 }}>Atualizar</button>
                    <button style={{ flex: 1 }} onClick={handleDelete}>Excluir</button>
                    <button style={{ flex: 1 }} onClick={handleVoltar}>Voltar</button>
                </div>
            </form>
            <p className={mensagem ? (mensagem.includes('sucesso') ? 'success' : 'error') : ''}>{mensagem}</p>
        </div>
    );
}

export default FabricanteEditPage;
