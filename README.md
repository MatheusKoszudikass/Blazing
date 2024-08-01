# Blazing

## 🧐 Visão Geral

Blazing é um aplicativo web desenvolvido em Blazor, com o objetivo de ser um software genérico para e-commerce, abrangendo todos os segmentos. Ele oferecerá funcionalidades para listar produtos de forma geral, utilizando uma abordagem abstrata para produtos, além de possuir carrinho de compras, formas de pagamento e integrações com APIs para geração de notas fiscais, como a Nuvem Fiscal.

O projeto também inclui uma estrutura de banco de dados para armazenar informações relevantes e utiliza técnicas de arquitetura limpa, com a intenção de escalar para vários segmentos, mantendo boas práticas de responsabilidade por camadas. Embora eu ainda esteja aprendendo o front-end, essas funcionalidades são previstas para a primeira etapa e podem ser ajustadas ao longo do desenvolvimento, sempre com foco em melhorias.

Este software será de código aberto, com o objetivo de ajudar a comunidade e abordar diferentes formas de programar. A ideia é proporcionar um projeto base que ofereça noções de desenvolvimento real e prático, indo além de um simples CRUD ou de cópias de projetos comumente vendidos em cursos. É uma oportunidade para mostrar os passos de um projeto completo e complexo, algo que não encontrei em outros lugares, especialmente no início da minha jornada.



## ⚙️ Funcionalidades Principais

1. **Listagem de Pizzas Especiais:**
   - A aplicação permite visualizar todas as pizzas especiais disponíveis na pizzaria.

2. **Listagem de Bebidas:**
   - Além das pizzas, é possível visualizar as bebidas disponíveis para compra.

3. **Carrinho de Compras:**
   - Os usuários podem adicionar pizzas ao carrinho de compras e realizar pedidos.

4. **Filtragem por Nome:**
   - Existe a opção de filtrar pizzas por nome, facilitando a busca para os clientes.

## 🔋 Status do Projeto

O projeto está em fase inicial, focando na estruturação de testes antes das implementações por exemplo banco de dados . Atualmente, estou trabalhando na base do projeto com operações básicas CRUD, sempre buscando uma abordagem mais performática para a aplicação. Além disso, estamos utilizando a biblioteca xUnit para testes unitarios durante a fase de implementação. Utilizando DTO(Data Transfer Object) para transferencia de dados. Seu feedback e sempre bem-vindo,
## 🆕 Atualizações Recentes

Mudança significativas de toda base para uma arquitetura limpa. 


## Roadmap

### 📖 Próximas Releases

1. **API:**
   - ✔️ Teste unitário e implementações CRUD product.
   - ✔️ Arquitetura limpa.
   - ⏳ Teste nos controllers.
   - ⏳ Teste unitário e implementações CRUD para category. 
   - ⏳ Criação das migrations.

2. **Cadastro de Usuário:**
   - Implementar a página de cadastro de usuário para facilitar o processo de compra.

### 🚀 Futuras Implementações

1. **Aplicativo de Gerenciamento Interno:**
   - Desenvolver um aplicativo separado para gerenciar adições de pizzas, bebidas e monitoramento de cadastros de usuários no site principal.

## 🤓 Boas Práticas

O projeto BlazingPizzaria segue boas práticas de desenvolvimento, como:
- Separação clara de responsabilidades entre componentes.
- Uso de técnicas de armazenamento em cache para melhorar o desempenho.
- Utilização de padrões de nomenclatura consistentes e legíveis.

## 💬 Contribuindo

Contribuições para o projeto são bem-vindas! Se você deseja contribuir, siga estes passos:
1. Faça um fork do repositório.
2. Crie uma branch para sua contribuição (`git checkout -b feature/nome-da-feature`).
3. Faça as alterações necessárias e adicione testes, se aplicável.
4. Envie um pull request para revisão.

## 💻 Requisitos do Ambiente

Certifique-se de que o seu ambiente atende aos seguintes requisitos antes de iniciar o desenvolvimento no projeto BlazingPizzaria:

- **.NET SDK:** Versão 8.0 ou superior
- **Entity Framework Core:** Versão 8.0.6 ou superior
- **Entity Framework Core Design:** Versão 8.0.6 ou superior
- **Entity Framework Core Tools:** Versão 8.0.6 ou superior
- **Entity Framework SqlServer:** Versão 8.0.6 ou superior
- **SQL Server:** Versão 2019 ou superior (ou outro banco de dados compatível com Entity Framework Core)

## 📚 Bibliotecas Utilizadas

O projeto BlazingPizzaria faz uso das seguintes bibliotecas e ferramentas:

- **Microsoft.AspNetCore.Blazor:** Versão 8.0.0
- **Microsoft.EntityFrameworkCore:** Versão 8.0.6
- **Newtonsoft.Json:** Versão 13.0.1
- **Bootstrap:** Versão 5.2.2
- **IJSRuntime:** Biblioteca padrão do Blazor
- **Radzen.Blazor:** Versão 4.32.9
- **Swashbuckle.AspNetCore:** Versão 6.2.3 (para documentação com Swagger)
- **AutoMapper:** Versão 13.0.1 (para mapeamento de entidades para DTOs)

## ⚙️ Configuração do Ambiente de Desenvolvimento

1. **Instalação do .NET SDK:**
   - Baixe e instale o .NET SDK 8.0 a partir do [site oficial da Microsoft](https://dotnet.microsoft.com/download).

2. **Layout e componentes:**
   - Execute o seguinte comando no terminal no diretório do projeto:
     ```
     dotnet add package Radzen.Blazor --version 4.32.9
     ```

3. **Configuração do Banco de Dados:**
   - Utilize o SQL Server Management Studio 2022 ou ferramenta similar para configurar o banco de dados conforme o arquivo de migração disponível no projeto.

4. **Execução do Projeto:**
   - Abra o terminal na pasta do projeto e execute o seguinte comando para iniciar a aplicação:
     ```
     dotnet watch run
     ```

## 📧 Contato

Para mais informações ou suporte, entre em contato com a equipe de desenvolvimento em [matheusprgc@gmail.com](mailto:matheusprgc@gmail.com).
