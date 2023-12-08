import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import HttpPost from '../services/HttpPost';

const MotoristaCreatePage = () => {
    const [nome, setNome] = useState('');
    const [cpf, setCpf] = useState('');
    const [numeroCnh, setNumeroCnh] = useState('');
    const [dataNascimento, setDataNascimento] = useState('');
    const [cnhCategoriaId, setCnhCategoriaId] = useState('');
    const [cnhCategorias, setCnhCategorias] = useState([]);
    const [mensagem, setMensagem] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        fetch('https://localhost:7200/api/CNHCategoria')
            .then(response => response.json())
            .then(data => setCnhCategorias(data))
            .catch(error => console.error('Erro ao buscar categorias da CNH:', error));

    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();

        let mensagemErro = '';

        if (nome.length < 3) {
            mensagemErro += 'Nome deve ter no minimo 3 caracteres.\n';
        }

        if (cpf.length !== 11) {
            mensagemErro += 'Cpf deve ter 11 digitos.\n';
        }

        if (numeroCnh.length !== 11) {
            mensagemErro += 'NUmero da CNH deve ter no 11 caracteres alfanumericos.\n';
        }

        if (!cnhCategoriaId) {
            mensagemErro += 'Categoria da CNH e obrigatorios.';
        }

        if (mensagemErro) {
            window.alert(mensagemErro);
            return;
        }

        const motorista = { nome, cpf, numeroCnh, dataNascimento, cnhCategoriaId };

        const sucesso = await HttpPost('https://localhost:7200/api/Motorista', motorista);

        if (sucesso) {
            setMensagem('Dados adicionados com sucesso!');
            setTimeout(() => {
                navigate('/MotoristasPage');
            }, 1000);
        } else {
            setMensagem('Falha ao adicionar dados.');
        }
    }

    useEffect(() => {
        const dataAtual = new Date().toISOString().slice(0, 16);
        setDataNascimento(dataAtual);
    }, []); 

    return (
        <div className="create">
            <h2>Adicionar um novo motorista </h2>
            <form onSubmit={handleSubmit}>
                <label>Nome:</label>
                <input
                    type="text"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)} />
                <label> N&uacute;mero do CPF </label>
                <input
                    type="number"
                    min="1"
                    pattern="[0-9]*"
                    inputMode="numeric"
                    onKeyDown={(e) => {
                        if (!((e.key >= '0' && e.key <= '9') || e.key === 'Backspace' || e.key === 'Delete' || e.key === 'ArrowLeft' || e.key === 'ArrowRight')) {
                            e.preventDefault();
                        }
                    }}
                    value={cpf}
                    onChange={(e) => setCpf(e.target.value)}
                />
                <label> N&uacute;mero da CNH </label>
                <input
                    type="text"
                    value={numeroCnh}
                    onChange={(e) => setNumeroCnh(e.target.value)}
                />
                <label>Data de nascimento</label>
                <input
                    type="datetime-local"
                    value={dataNascimento}
                    onChange={(e) => setDataNascimento(e.target.value)}
                />
                <label> Categoria da CNH </label>
                <select
                    value={cnhCategoriaId}
                    onChange={(e) => setCnhCategoriaId(e.target.value)}>
                    <option value="">Selecione uma categoria </option>
                    {cnhCategorias.map((cnhCategoria) => (
                        <option key={cnhCategoria.id} value={cnhCategoria.id}>
                            {cnhCategoria.nome}
                        </option>
                    ))}
                </select>
                <button>Incluir</button>
            </form>
        </div>
    );
};

export default MotoristaCreatePage;