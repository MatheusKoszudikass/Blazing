import PostUserloginTest from './scenarios/Users/Post-CustomerUserLogin.js';
import GetUsersTest from './scenarios/Users/Get-CustomerUsers.js';
import GetProductsTest from './scenarios/Products/GetAll-CustomerProducts.js';
import GetCategoryTest from './scenarios/Category/GetAll-CustomerCategory.js';
import { group, sleep } from 'k6';

export let options = {
    stages: [
      { duration: '1m', target: 10 },    // Ramp up para 10 usuários em 1 minuto
      { duration: '1m', target: 30 },    // Ramp up para 30 usuários em 1 minuto
      { duration: '1m', target: 50 },    // Ramp up para 50 usuários em 1 minuto
      { duration: '1m', target: 70 },    // Ramp up para 70 usuários em 1 minuto
      { duration: '1m', target: 90 },    // Ramp up para 90 usuários em 1 minuto
      { duration: '1m', target: 120 },   // Ramp up para 120 usuários em 1 minuto
      { duration: '1m', target: 150 },   // Ramp up para 150 usuários em 1 minuto
      { duration: '1m', target: 180 },   // Ramp up para 180 usuários em 1 minuto
      { duration: '1m', target: 220 },   // Ramp up para 220 usuários em 1 minuto
      { duration: '1m', target: 270 },   // Ramp up para 270 usuários em 1 minuto
      { duration: '1m', target: 320 },   // Ramp up para 320 usuários em 1 minuto
      { duration: '1m', target: 370 },   // Ramp up para 370 usuários em 1 minuto
      { duration: '1m', target: 420 },   // Ramp up para 420 usuários em 1 minuto
      { duration: '1m', target: 470 },   // Ramp up para 470 usuários em 1 minuto
      { duration: '1m', target: 500 },   // Ramp up para 500 usuários em 1 minuto
      { duration: '5m', target: 500 },   // Mantém 500 usuários por 5 minutos
      { duration: '2m', target: 0 }      // Ramp down para 0 usuários em 2 minutos // Ramp up para 10 usuários em 1 minuto
        // { duration: '1m', target: 20 },   // Ramp up para 20 usuários em 1 minuto
        // { duration: '1m', target: 30 },   // Ramp up para 30 usuários em 1 minuto
        // { duration: '1m', target: 40 },   // Ramp up para 40 usuários em 1 minuto
        // { duration: '1m', target: 50 },   // Ramp up para 50 usuários em 1 minuto
        // { duration: '1m', target: 60 },   // Ramp up para 60 usuários em 1 minuto
        // { duration: '1m', target: 70 },   // Ramp up para 70 usuários em 1 minuto
        // { duration: '1m', target: 80 },   // Ramp up para 80 usuários em 1 minuto
        // { duration: '1m', target: 90 },   // Ramp up para 90 usuários em 1 minuto
        // { duration: '1m', target: 100 },  // Ramp up para 100 usuários em 1 minuto
        // { duration: '1m', target: 200 },  // Ramp up para 200 usuários em 1 minuto
        // { duration: '1m', target: 300 },  // Ramp up para 300 usuários em 1 minuto
        // { duration: '1m', target: 400 },  // Ramp up para 400 usuários em 1 minuto
        // { duration: '1m', target: 500 },  // Ramp up para 500 usuários em 1 minuto
        // { duration: '1m', target: 600 },  // Ramp up para 600 usuários em 1 minuto
        // { duration: '1m', target: 700 },  // Ramp up para 700 usuários em 1 minuto
        // { duration: '1m', target: 800 },  // Ramp up para 800 usuários em 1 minuto
        // { duration: '1m', target: 900 },  // Ramp up para 900 usuários em 1 minuto
        // { duration: '1m', target: 1000 }, // Ramp up para 1000 usuários em 1 minuto
        // { duration: '2m', target: 1000 }, // Mantém 1000 usuários por 2 minutos
        // { duration: '1m', target: 800 },  // Ramp down para 800 usuários em 1 minuto
        // { duration: '1m', target: 600 },  // Ramp down para 600 usuários em 1 minuto
        // { duration: '1m', target: 400 },  // Ramp down para 400 usuários em 1 minuto
        // { duration: '1m', target: 200 },  // Ramp down para 200 usuários em 1 minuto
        // { duration: '1m', target: 0 },    // Ramp down para 0 usuários em 1 minuto
    ],
    thresholds: {
        'http_req_duration': ['p(95)<1000'], // 95% das requisições devem ser abaixo de 1000ms
    },
};

export default function () {
    group('Endpoint Get Customer - Controller product - Blazing.Api', function () {
        GetCategoryTest();
        GetProductsTest();
    });

    // group('Endpoint Get Customer - Controller category - Blazing.Api', function () {
       
    // });

    sleep(1); // Pausa por 1 segundo antes da próxima iteração
}
