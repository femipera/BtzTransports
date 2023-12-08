import React from 'react';
import { Link } from 'react-router-dom';

const MostrarLista = ({ titulo, page, lista }) => {
    const handleDeletar = (id) => {

        console.log(`Item com ID ${id} deletado.`);
    };

    return (
        <div className="lista">
            <div className="lista-titulo">
                <h3>{titulo}</h3>
            </div>
            <div className="lista-itens">
                <div className="lista-header">
                    <div className="lista-header-item">ID</div>
                    <div className="lista-header-item">Nome</div>
                    <div className="lista-header-item">A&ccedil;&otilde;es</div>
                </div>
                {lista.map((item, index) => (
                    <div
                        className={index % 2 === 0 ? 'item-zebrado' : 'item-normal'}
                        key={item.id}>
                        <div className="item">
                            <div className="item-info">
                                <span className="item-value">{item.id}</span>
                                <span className="item-value">{item.nome}</span>
                                <div className="acoes">
                                    <Link to={`/${page}DeletePage/${item.id}`}>Deletar</Link>
                                    <span>&nbsp;&nbsp;</span>
                                    <Link to={`/${page}EditPage/${item.id}`}>Editar</Link>
                                </div>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
            <Link to={`/${page}CreatePage`} className="adicionar-link">
                Novo Registro
            </Link>
        </div>
    );
};

export default MostrarLista;
