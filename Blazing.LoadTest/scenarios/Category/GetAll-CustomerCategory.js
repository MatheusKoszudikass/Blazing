import http from 'k6/http';
import { sleep } from 'k6';
import { Trend, Rate } from 'k6';
import { check, fail } from 'k6';


export default function () {


    let page = 1
    let pageSize = Math.floor(Math.random() * (50 - 20 + 1)) + 20;
    // Fazer a requisição com valores aleatórios
    let res = http.get(`http://192.168.0.2:7239/api/Category?page=${page}&pageSize=${pageSize}`);

    // Verificar se a resposta tem uma duração máxima de 1000ms
    let durationMsg = `Max Duration ${1000 / 1000}s`;

    let result = check(res, {
        'max duration': (r) => r.timings.duration < 1000,
    });

    if (!result) {
        fail(durationMsg); // Falha se a duração for maior que 1000ms
    }

    sleep(1); // Pausa de 1 segundo entre as requisições

}
