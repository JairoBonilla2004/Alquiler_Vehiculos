export async function ajax(props) {
    let { endpoint, _method, content_type, data, cbSuccess, cbError } = props;
    try {
        let config = {
            method: _method,
            headers: {
                "Content-Type": content_type,
            },
        };

        if (_method !== "GET" && _method !== "DELETE") {
            config.body = JSON.stringify(data);
        }
        let response = await fetch(endpoint, config);
        let json = await response.json();
        cbSuccess(json);
    } catch (err) {
        cbError(err);
    }
}