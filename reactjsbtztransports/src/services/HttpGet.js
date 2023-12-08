import { useState, useEffect } from 'react';

const HttpGet = (url) => {
    const [data, setData] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const abortCont = new AbortController();

        fetch(url, { signal: abortCont.signal })
            .then(res => {
                if (!res.ok) {
                    throw Error('Não foi possível buscar dados no servidor');
                }
                return res.json();
            })
            .then(data => {
                setIsLoading(false);
                setData(data);
                setError(null);
            })
            .catch(err => {
                if (err.name === 'AbortError') {
                    console.log('fetch abortado')
                } else {
                    setIsLoading(false);
                    setError(err.message);
                }
            })
        return () => abortCont.abort();
    }, [url])

    return { data, isLoading, error };
}

export default HttpGet;