import HttpGet from "../services/HttpGet";
import MostrarLista from "../components/MostrarLista";


const FabricantesPage = () => {
    const { error, isLoading, data: lista } = HttpGet('https://localhost:7200/api/Fabricante')
    const page = 'Fabricante';
    const titulo = 'Lista de fabricantes de ve' + String.fromCharCode(237) + 'culos';

    return (
        <div className="home">
            {error && <div>{error}</div>}
            {isLoading && <div>Atualizando...</div>}
            {lista && <MostrarLista titulo={titulo} page={page} lista={lista} />}
        </div>
    );
}

export default FabricantesPage;