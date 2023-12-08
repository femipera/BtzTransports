import React from 'react';
import { Link } from 'react-router-dom';

const MostrarListaAbastecimento = ({ titulo, page, lista }) => {

    return (
        <div className="lista">
            <div className="lista-titulo">
                <h3>{titulo}</h3>
            </div>
            <div className="lista-itens">
                <div className="lista-header">
                    <div className="lista-header-item">Motorista</div>
                    <div className="lista-header-item">Veiculo</div>
                    <div className="lista-header-item">Combustivel</div>
                    <div className="lista-header-item">Qtd Litros</div>
                    <div className="lista-header-item">Preco R$</div>
                    <div className="lista-header-item">Total R$</div>
                    <div className="lista-header-item">Data</div>
                    <div className="lista-header-item">A&ccedil;&otilde;es</div>
                </div>
                {lista.map((item, index) => (
                    <div
                        className={index % 2 === 0 ? 'item-zebrado' : 'item-normal'}
                        key={item.id}>
                        <div className="item">
                            <div className="item-info">
                                <span className="item-value">{item.motorista?.nome}</span>
                                <span className="item-value">{item.veiculo?.nome}</span>
                                <span className="item-value">{item.combustivel?.nome}</span>
                                <span className="item-value">{item.data}</span>
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

export default MostrarListaAbastecimento;