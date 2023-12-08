import HttpGet from "../services/HttpGet";
import MostrarLista from "../components/MostrarLista";


const CombustivelPage = () => {
    const { error, isLoading, data: lista } = HttpGet('https://localhost:7200/api/Combustivel')
    const page = 'Combustivel';
    const titulo = 'Lista de combust' + String.fromCharCode(237) + 'veis';

    return (
        <div className="home">
            {error && <div>{error}</div>}
            {isLoading && <div>Atualizando...</div>}
            {lista && <MostrarLista titulo={titulo} page={page} lista={lista} />}
        </div>
    );
}

export default CombustivelPage;