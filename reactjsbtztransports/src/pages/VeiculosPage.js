import HttpGet from "../services/HttpGet";
import MostrarLista from "../components/MostrarLista";


const VeiculosPage = () => {
    const { error, isLoading, data: lista } = HttpGet('https://localhost:7200/api/Veiculo')
    const page = 'Veiculo';
    const titulo = 'Lista de Ve' + String.fromCharCode(237) +'culos';

    return (
        <div className="home">
            {error && <div>{error}</div>}
            {isLoading && <div>Atualizando...</div>}
            {lista && <MostrarLista titulo={titulo} page={page} lista={lista} />}
        </div>
    );
}

export default VeiculosPage;