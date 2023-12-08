import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import HttpPost from '../services/HttpPost';

const VeiculoCreatePage = () => {
    const [nome, setNome] = useState('');
    const [placa, setPlaca] = useState('');
    const [anoFabricacao, setAnoFabricacao] = useState('');
    const [capacidadeMaximaTanque, setCapacidadeMaximaTanque] = useState('');
    const [observacao, setObservacao] = useState('');
    const [fabricanteId, setFabricanteId] = useState('');
    const [fabricantes, setFabricantes] = useState([]);
    const [combustivelId, setCombustivelId] = useState('');
    const [combustiveis, setCombustiveis] = useState([]);
    const [mensagem, setMensagem] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        fetch('https://localhost:7200/api/Fabricante')
            .then(response => response.json())
            .then(data => setFabricantes(data))
            .catch(error => console.error('Erro ao buscar fabricantes:', error));

        fetch('https://localhost:7200/api/Combustivel')
            .then(response => response.json())
            .then(data => setCombustiveis(data))
            .catch(error => console.error('Erro ao buscar tipos de combustivel:', error));
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();

        let mensagemErro = '';

        if (nome.length < 3) {
            mensagemErro += 'Nome deve ter no minimo 3 caracteres.\n';
        }

        if (placa.length !== 7) {
            mensagemErro += 'Placa deve ter 7 caracteres alfanumericos.\n';
        }

        if (capacidadeMaximaTanque.length < 0) {
            mensagemErro += 'Capacidade maxima do tanque deve ser maior que 0 (zero).\n';
        }

        if (!combustivelId) {
            mensagemErro += 'Selecione um comubstivel.';
        }

        if (!fabricanteId) {
            mensagemErro += 'Selecione um fabricante.';
        }

        if (mensagemErro) {
            window.alert(mensagemErro);
            return;
        }

        const veiculo = { nome, placa, anoFabricacao, capacidadeMaximaTanque, observacao, fabricanteId, combustivelId };

        const sucesso = await HttpPost('https://localhost:7200/api/Veiculo', veiculo);

        if (sucesso) {
            setMensagem('Dados adicionados com sucesso!');
            setTimeout(() => {
                navigate('/VeiculosPage');
            }, 1000);
        } else {
            setMensagem('Falha ao adicionar dados.');
        }
    }

    useEffect(() => {
        const dataAtual = new Date().toISOString().slice(0, 16);
        setAnoFabricacao(dataAtual);
    }, []); 

    return (
        <div className="create">
            <h2>Adicionar uma novo veiculo</h2>
            <form onSubmit={handleSubmit}>
                <label>Nome</label>
                <input
                    type="text"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                />
                <label>Placa</label>
                <input
                    type="text"
                    value={placa}
                    onChange={(e) => setPlaca(e.target.value)}
                />
                <label>Ano de fabricacao</label>
                <input
                    type="datetime-local"
                    value={anoFabricacao}
                    onChange={(e) => setAnoFabricacao(e.target.value)}
                />
                <label>Capacidade maxima do tanque</label>
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
                    value={capacidadeMaximaTanque}
                    onChange={(e) => setCapacidadeMaximaTanque(e.target.value)}
                />
                <label>Obsevacao</label>
                <textarea
                    type="text"
                    value={observacao}
                    onChange={(e) => setObservacao(e.target.value)}
                />           
                <label> Fabricante </label>
                <select
                    value={fabricanteId}
                    onChange={(e) => setFabricanteId(e.target.value)}>
                    <option value="">Selecione um fabricante</option>
                    {fabricantes.map((fabricante) => (
                        <option key={fabricante.id} value={fabricante.id}>
                            {fabricante.nome}
                        </option>
                    ))}
                </select>
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
                <button>Incluir</button>
            </form>
        </div>
    );
};

export default VeiculoCreatePage;