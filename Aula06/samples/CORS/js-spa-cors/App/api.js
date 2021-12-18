function callApi(method, endpoint) {

    logMessage('Calling Web API ' + endpoint);
    const headers = new Headers();
    const options = {
        method: method,
        headers: headers
    };

    fetch(endpoint, options)
        .then(response => {
            if(response.status == 403)
            {
                logMessage('Acesso negado');
            }
            if(!response.ok)
                throw Error(response.statusText);
            return response;
        })
        .then(response => response.json())
        .then(data => {

            if (data) {
                logMessage('Web API responded with ' + data);
            }

            console.log('api success', data);
            
            return data;
        }).catch(error => {
            console.error('api error', error);
        });
}

function getDadosComCORS(path) {
    try {
        callApi("GET", "http://localhost:5000/api/"+ path);
    } catch(error) {
        console.warn(error); 
    }
}

function getDadosSemCORS(path) {
    try {
        callApi("GET", "http://localhost:4000/api/"+ path);
    } catch(error) {
        console.warn(error); 
    }
}