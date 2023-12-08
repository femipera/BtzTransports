import React from 'react';

const Home = () => {
    const endereco = 'Endereço: Av. das Palmeiras, 40 – Maring&atilde; – PR';
    const contato = 'Contato: (44) 3246-4144 / (44) 99999-8888 (whats) - sac@btztransports.com.br';
    const atendimento = 'Hor&atilde;rio de atendimento: seg/sex – 08h00 – 22h00';

    return (
        <div>
            <h2>Home</h2>
            <p>{endereco}</p>
            <p>{contato}</p>
            <p>{atendimento} </p>
        </div>
    );
};

export default Home;