import HttpGet from "../services/HttpGet";
import MostrarLista from "../components/MostrarLista";


const MotoristasPage = () => {
    const { error, isLoading, data: lista } = HttpGet('https://localhost:7200/api/Motorista')
    const page = 'Motorista';
    const titulo = 'Lista de motoristas';

    return (
        <div className="home">
            {error && <div>{error}</div>}
            {isLoading && <div>Atualizando...</div>}
            {lista && <MostrarLista titulo={titulo} page={page} lista={lista} />}
        </div>
    );
}

export default MotoristasPage;