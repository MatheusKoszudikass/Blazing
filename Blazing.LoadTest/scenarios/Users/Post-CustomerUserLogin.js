import http from 'k6/http';
import { check, sleep } from 'k6';

let token;

export const options = {
    vus: 10,
    duration: '30s',
  };
  
// Função de configuração que será executada uma vez antes do teste
export default function () {
    // URL do endpoint de login
    let loginUrl = 'https://api.ativobyte.com.br/api/User/Login';

    // Corpo da requisição de login
    let loginPayload = JSON.stringify({
        LoginIdentifier: 'johndoe@example.com',
        Password: 'hasheSpassword123',
        TwoFactorCode: 'string',
        TwoFactorRecoveryCode: 'string',
        RememberMe: false
    });

    // Cabeçalhos da requisição
    let headers = {
        'Content-Type': 'application/json'
    };

    // Enviar a requisição POST para o endpoint de login
    let loginRes = http.post(loginUrl, loginPayload, { headers: headers });

    // Verificar a resposta de login
    check(loginRes, { 'login successful': (r) => r.status === 200 });

    // Extrair o token da resposta
    token = loginRes.json('token'); // Ajuste o caminho para o token conforme a resposta da API
}