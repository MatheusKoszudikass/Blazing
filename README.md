# 🍕 BlazingPizzaria

## 🧐 Visão Geral

BlazingPizzaria é um aplicativo web desenvolvido em Blazor, focado na gestão de uma pizzaria. Ele oferece funcionalidades para listar pizzas especiais, bebidas e carrinho de compras para as pizzas. O projeto também inclui a estrutura de banco de dados para armazenar informações relevantes e utiliza técnicas de cache para otimizar o desempenho da aplicação. Recentemente, foi adicionado um projeto de API que o site Blazor utiliza para puxar os dados dos produtos, utilizando classes DTO para a transferência de dados.

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

O projeto encontra-se em fase inicial, com a estrutura do banco de dados finalizada e a funcionalidade de listar pizzas especiais implementada. Atualmente, está sendo trabalhado o armazenamento em cache dos dados para melhorar o desempenho da aplicação. Além disso, está sendo utilizada a biblioteca Bunit para testes durante a fase de implementação. Agora, com a adição do projeto de API, os dados dos produtos são puxados via API utilizando classes DTO para a transferência de dados.

## 🆕 Atualizações Recentes

Foi adicionado um projeto de API em .NET 8.0, que é consumido pelo site Blazor. A API utiliza Swagger para documentação e o AutoMapper para mapear as entidades para as classes DTOs.

## Roadmap

### 📖 Próximas Releases

1. **Páginas de Pizza List:**
   - Adicionar carrinho de compra.
   - Cria uma politica de testes unitarios e dos componentes da pagina.
   - Explorando mais a fundo questões de armazenamento em cache do navegador.

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
