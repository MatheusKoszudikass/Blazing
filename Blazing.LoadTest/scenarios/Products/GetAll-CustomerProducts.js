import http from 'k6/http';
import { sleep, check, fail } from 'k6';

// Enviar uma solicitação GET

export default function () {
    // Geração de valores aleatórios para 'page' e 'pageSize'
    let page = Math.floor(Math.random() * 20) + 1;
    let pageSize = Math.floor(Math.random() * (50 - 20 + 1)) + 20;

    // Faz a requisição HTTP GET
    let res = http.get(`http://192.168.0.2:7239/api/Product?page=${page}&pageSize=${pageSize}`);

    // Verificar se a resposta é válida e se o status é 200
    if (res === null || res === undefined) {
        fail('Request failed: response is null or undefined');
        return;
    }

    check(res, {
        'status is 200': (r) => r.status === 200,
    });

    // Verificar se o tempo de resposta está abaixo de 1000ms
    let durationMsg = `Max Duration ${1000 / 1000}s`;
    if (!check(res, {
        'max duration': (r) => r.timings.duration < 1000,
    })) {
        fail(durationMsg);
    }

    // Pausa o script por 1 segundo antes de repetir
    sleep(1);
}
