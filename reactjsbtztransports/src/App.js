import SideBar from './components/SideBar';
import Home from './pages/Home';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import FabricanteCreatePage from './pages/FabricanteCreatePage';
import FabricanteEditPage from './pages/FabricanteEditPage';
import FabricantesPage from './pages/FabricantesPage';
import AbastecimentoCreatePage from './pages/AbastecimentoCreatePage';
import AbastecimentosPage from './pages/AbastecimentosPage';
import CnhCategoriasPage from './pages/CnhCategoriasPage';
import CnhCategoriaCreatePage from './pages/CnhCategoriaCreatePage';
import CombustiveisPage from './pages/CombustiveisPage';
import CombustivelCreatePage from './pages/CombustivelCreatePage';
import MotoristasPage from './pages/MotoristasPage';
import MotoristaCreatePage from './pages/MotoristaCreatePage';
import UsuariosPage from './pages/UsuariosPage';
import UsuarioCreatePage from './pages/UsuarioCreatePage';
import VeiculosPage from './pages/VeiculosPage';
import VeiculoCreatePage from './pages/VeiculoCreatePage';

function App() {
    return (
        <Router>
            <div className="App">
                <SideBar />
                <div className="content">
                    <Routes>
                        <Route exact path="/" element={<Home />} />
                        <Route path="/abastecimentoCreatePage" element={<AbastecimentoCreatePage />} />
                        <Route path="/abastecimentosPage" element={<AbastecimentosPage />} />
                        <Route path="/cnhCategoriasPage" element={<CnhCategoriasPage />} />
                        <Route path="/cnhCategoriaCreatePage" element={<CnhCategoriaCreatePage />} />
                        <Route path="/combustiveisPage" element={<CombustiveisPage />} />
                        <Route path="/combustivelCreatePage" element={<CombustivelCreatePage />} />
                        <Route path="/motoristasPage" element={<MotoristasPage />} />
                        <Route path="/motoristaCreatePage" element={<MotoristaCreatePage />} />
                        <Route path="/usuariosPage" element={<UsuariosPage />} />
                        <Route path="/usuarioCreatePage" element={<UsuarioCreatePage />} />
                        <Route path="/fabricanteCreatePage" element={<FabricanteCreatePage />} />
                        <Route path="/fabricanteEditPage/:id" element={<FabricanteEditPage />} />
                        <Route path="/fabricantesPage" element={<FabricantesPage />} />
                        <Route path="/veiculosPage" element={<VeiculosPage />} />
                        <Route path="/veiculoCreatePage" element={<VeiculoCreatePage />} />
                    </Routes>
                </div>
            </div>
        </Router>
    );
}

export default App;