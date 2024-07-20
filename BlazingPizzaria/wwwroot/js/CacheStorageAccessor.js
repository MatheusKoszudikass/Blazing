
window.produtos = async function (produtos) {
    if (!window.indexedDB) {
        console.log("Seu navegador não suporta uma versão estável do IndexedDB. Alguns recursos não estarão disponíveis.");
        return;
    }

    const request = indexedDB.open("PizzariaDB", 1);

    request.onerror = (event) => {
        console.error("Erro ao abrir o banco de dados", event);
    };

    request.onsuccess = (event) => {
        const db = event.target.result;
        console.log("Banco de dados aberto com sucesso");

        adicionarProdutos(db, produtos);
    };

    request.onupgradeneeded = (event) => {
        const db = event.target.result;
        const objectStore = db.createObjectStore("produtos", { keyPath: "id", autoIncrement: true });
        objectStore.createIndex("nome", "nome", { unique: false });

        console.log("Objeto de armazenamento 'produtos' criado com sucesso");
    };

    function adicionarProdutos(db, produtos) {
        const transaction = db.transaction(["produtos"], "readwrite");

        transaction.oncomplete = () => {
            console.log("Todos os produtos foram adicionados com sucesso.");
        };

        transaction.onerror = (event) => {
            console.error("Erro ao adicionar produtos", event);
        };

        const objectStore = transaction.objectStore("produtos");

        produtos.forEach((produto) => {
            const request = objectStore.add(produto);

            request.onsuccess = () => {
                console.log(`Produto ${produto.nome} adicionado com sucesso.`);
            };
        });
    }
};
