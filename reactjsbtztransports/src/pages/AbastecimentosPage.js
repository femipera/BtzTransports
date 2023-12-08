import HttpGet from "../services/HttpGet";
import MostrarListaAbastecimento from "../components/MostrarListaAbastecimento";

const AbastecimentosPage = () => {
    const { error, isLoading, data: lista } = HttpGet('https://localhost:7200/api/Abastecimento')
    const page = 'Abastecimento';
    const titulo = 'Lista de abastecimentos';

    return (
        <div className="home">
            {error && <div>{error}</div>}
            {isLoading && <div>Atualizando...</div>}
            {lista && <MostrarListaAbastecimento titulo={titulo} page={page} lista={lista} />}
        </div>
    );
}

export default AbastecimentosPage;