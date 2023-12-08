import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import HttpPost from '../services/HttpPost';

const AbastecimentoCreatePage = () => {
    const [data, setData] = useState('');
    const [quantidadeAbastecida, setQuantidadeAbastecida] = useState('');
    const [motoristaId, setMotoristaId] = useState('');
    const [motoristas, setMotoristas] = useState([]);
    const [veiculoId, setVeiculoId] = useState('');
    const [veiculos, setVeiculos] = useState([]);
    const [combustivelId, setCombustivelId] = useState('');
    const [combustiveis, setCombustiveis] = useState([]);
    const [mensagem, setMensagem] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        fetch('https://localhost:7200/api/Motorista')
            .then(response => response.json())
            .then(data => setMotoristas(data))
            .catch(error => console.error('Erro ao buscar Motorista:', error));

        fetch('https://localhost:7200/api/Veiculo')
            .then(response => response.json())
            .then(data => setVeiculos(data))
            .catch(error => console.error('Erro ao buscar tipos de Veiculo:', error));

        fetch('https://localhost:7200/api/Combustivel')
            .then(response => response.json())
            .then(data => setCombustiveis(data))
            .catch(error => console.error('Erro ao buscar tipos de combustivel:', error));


    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();

        let mensagemErro = '';

        if (parseInt(quantidadeAbastecida, 10) <= 0) {
            mensagemErro += 'Quantidade abastecida deve ser maior que 0 (zero).\n';
        }

        if (!combustivelId) {
            mensagemErro += 'Selecione um comubstivel.';
        }

        if (!motoristaId) {
            mensagemErro += 'Selecione um fabricante.';
        }

        if (!veiculoId) {
            mensagemErro += 'Selecione um veiculo.';
        }

        if (mensagemErro) {
            window.alert(mensagemErro);
            return;
        }

        const abastecimento = { motoristaId, veiculoId, combustivelId, data, quantidadeAbastecida };

        const sucesso = await HttpPost('https://localhost:7200/api/Abastecimento', abastecimento);

        if (sucesso) {
            setMensagem('Dados adicionados com sucesso!');
            setTimeout(() => {
                navigate('/AbastecimentosPage');
            }, 1000);
        } else {
            setMensagem('Falha ao adicionar dados.');
        }
    }

    useEffect(() => {
        const dataAtual = new Date().toISOString().slice(0, 16);
        setData(dataAtual);
    }, []); 

    return (
        <div className="create">
            <h2>Adicionar uma novo veiculo</h2>
            <form onSubmit={handleSubmit}>
                <label> Motorista </label>
                <select
                    value={motoristaId}
                    onChange={(e) => setMotoristaId(e.target.value)}>
                    <option value="">Selecione um Motorista</option>
                    {motoristas.map((motorista) => (
                        <option key={motorista.id} value={motorista.id}>
                            {motorista.nome}
                        </option>
                    ))}
                </select>
                <label> Veiculo </label>
                <select
                    value={veiculoId}
                    onChange={(e) => setVeiculoId(e.target.value)}>
                    <option value="">Selecione um Veiculo</option>
                    {veiculos.map((veiculo) => (
                        <option key={veiculo.id} value={veiculo.id}>
                            {veiculo.nome}
                        </option>
                    ))}
                </select>
                <label>Ano de fabricacao</label>
                <input
                    type="datetime-local"
                    value={data}
                    onChange={(e) => setData(e.target.value)}
                />
                <label> Combustivel </label>
                <select
                    value={combustivelId}
                    onChange={(e) => setCombustivelId(e.target.value)}>
                    <option value="">Selecione tipo de combustivel</option>
                    {combustiveis.map((combustivel) => (
                        <option key={combustivel.id} value={combustivel.id}>
                            {combustivel.nome}
                        </option>
                    ))}
                </select>
                <label>Quantidade abastecida</label>
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
                    value={quantidadeAbastecida}
                    onChange={(e) => setQuantidadeAbastecida(e.target.value)}
                />
                <button>Incluir</button>
            </form>
        </div>
    );
};

export default AbastecimentoCreatePage;