// Variáveis globais
let cart = [];

// Função para abrir o modal do carrinho.
async function showCartModal() {
    const cartBtn = document.getElementById("cart-btn");
    const cartIcon = document.getElementById("cart-icon");
    const cartModal = document.getElementById("cart-modal");

    if (cartBtn && cartModal) {

        cartIcon.addEventListener("click", function () {
            cartModal.style.display = "flex"
        });
        cartBtn.addEventListener("click", function () {
            cartModal.style.display = "flex";
        });
    } else {
        console.error("Elementos não encontrados");
    }
}

// Função de fechar o modal do carrinho.
async function closeCartModal() {
    const cartBtnClose = document.getElementById("close-modal-btn");
    const cartModal = document.getElementById("cart-modal");

    if (cartBtnClose && cartModal) {
        cartBtnClose.addEventListener("click", function () {
            cartModal.style.display = "none"
        })
        cartModal.addEventListener("click", function (event) {
            if (event.target === cartModal) {
                cartModal.style.display = "none"
            }
        })
    } else {
        console.error("Elementos não encontrados")
    }
}

async function menu() {
    const menuIndex = document.getElementById("menu")
    if (menuIndex) {
        menuIndex.addEventListener("click", function (event) {

            let parentButton = event.target.closest(".add-to-cart-btn")
            if (parentButton) {
                const name = parentButton.getAttribute("data-name")
                const preco = parseFloat(parentButton.getAttribute("data-preco"))

                //adicionar no carrinho
                addToCart(name, preco)
            }
        });
    }
}


async function addToCart(name, preco) {
    const existingItem = cart.find(item => item.name == name)

    if (existingItem) {
        existingItem.quantity += 1;
    } else {
        cart.push({
            name,
            preco,
            quantity: 1,
        })
    }
    updateCartModal()
}

async function updateCartModal(id) {
    const cartNotificacaoQuantity = document.getElementById("cart-icon");
    console.log(cartNotificacaoQuantity);

    if (cartNotificacaoQuantity) {

        const cartLength = id;
        console.log(cartLength);

        // Atualiza o atributo 'data-quantity' com o tamanho do array 'cart'
        cartNotificacaoQuantity.setAttribute("data-quantity", cartLength.toString());

        // Verifique se o atributo foi atualizado corretamente
        const cartTopoQuantidade = cartNotificacaoQuantity.getAttribute("data-quantity");
        console.log(cartTopoQuantidade); // Deveria mostrar o tamanho do array
    } else {
        console.error("Elemento 'cart-icon' não encontrado");
    }
}