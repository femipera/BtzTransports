import HttpGet from "../services/HttpGet";
import MostrarLista from "../components/MostrarLista";


const UsuariosPage = () => {
    const { error, isLoading, data: lista } = HttpGet('https://localhost:7200/api/Usuario')
    const page = 'Us' + String.fromCharCode(225) + 'rio';
    const titulo = 'Lista de usuarios';

    return (
        <div className="home">
            {error && <div>{error}</div>}
            {isLoading && <div>Atualizando...</div>}
            {lista && <MostrarLista titulo={titulo} page={page} lista={lista} />}
        </div>
    );
}

export default UsuariosPage;