import HttpGet from "../services/HttpGet";
import MostrarLista from "../components/MostrarLista";


const CnhCategoriasPage = () => {
    const { error, isLoading, data: lista } = HttpGet('https://localhost:7200/api/CNHCategoria')
    const page = 'CategoriaCNH';
    const titulo = 'Lista de categorias da CNH';

    return (
        <div className="home">
            {error && <div>{error}</div>}
            {isLoading && <div>Atualizando...</div>}
            {lista && <MostrarLista titulo={titulo} page={page} lista={lista} />}
        </div>
    );
}

export default CnhCategoriasPage;