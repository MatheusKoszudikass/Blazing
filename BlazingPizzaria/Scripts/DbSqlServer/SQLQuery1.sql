Use Pizzaria;


DELETE FROM pizzaSpecials;


INSERT INTO pizzaSpecials (Name, BasePrice, Description, ImageUrl)
VALUES
    ('Margherita', 29.99, 'Pizza clássica de queijo e tomate', 'img/pizzas/margherita.jpg'),
    ('Pepperoni', 34.99, 'Pizza de pepperoni com queijo mussarela', 'img/pizzas/pepperoni.jpg'),
    ('Vegetariana', 31.99, 'Pizza carregada de vegetais', 'img/pizzas/vegetariana.jpg'),
    ('Bacon', 39.99, 'Pizza clássica com bacon crocante e queijo', 'img/pizzas/bacon.jpg'),
    ('Quatro Queijos', 36.99, 'Pizza com uma mistura de quatro queijos deliciosos', 'img/pizzas/quatroqueijos.jpg'),
    ('Frango com Catupiry', 38.99, 'Pizza de frango desfiado com catupiry', 'img/pizzas/frangocatupiry.jpg'),
    ('Calabresa', 33.99, 'Pizza de calabresa com cebolas', 'img/pizzas/calabresa.jpg'),
    ('Portuguesa', 37.99, 'Pizza de presunto, ovos e azeitonas', 'img/pizzas/portuguesa.jpg'),
    ('Havaiana', 32.99, 'Pizza com presunto e abacaxi', 'img/pizzas/havaiana.jpg'),
    ('Mexicana', 34.99, 'Pizza com carne moída, pimentão, cebola e jalapeños', 'img/pizzas/mexicana.jpg'),
    ('Quatro Estações', 35.99, 'Pizza com quatro sabores diferentes em uma só', 'img/pizzas/quatroestacoes.jpg'),
    ('Alho e Óleo', 30.99, 'Pizza de alho e óleo com queijo', 'img/pizzas/alhoeoleo.jpg'),
    ('Napolitana', 33.99, 'Pizza com molho de tomate, queijo e anchovas', 'img/pizzas/napolitana.jpg'),
    ('Rúcula com Tomate Seco', 36.99, 'Pizza com rúcula, tomate seco e queijo', 'img/pizzas/ruculatomateseco.jpg'),
    ('Atum', 34.99, 'Pizza com atum, cebola e azeitonas', 'img/pizzas/atum.jpg'),
    ('Pesto', 35.99, 'Pizza com molho pesto, queijo e tomate', 'img/pizzas/pesto.jpg'),
    ('Carne Seca com Catupiry', 37.99, 'Pizza de carne seca desfiada com catupiry', 'img/pizzas/carneseca.jpg'),
    ('Zucchini', 31.99, 'Pizza de abobrinha grelhada com queijo', 'img/pizzas/zucchini.jpg'),
    ('Caprese', 33.99, 'Pizza com tomate, mussarela de búfala e manjericão', 'img/pizzas/caprese.jpg'),
    ('Provolone', 34.99, 'Pizza de queijo provolone e tomate', 'img/pizzas/provolone.jpg'),
    ('Gorgonzola', 36.99, 'Pizza com queijo gorgonzola e nozes', 'img/pizzas/gorgonzola.jpg'),
    ('Marinara', 28.99, 'Pizza simples com molho de tomate, alho e orégano', 'img/pizzas/marinara.jpg');


-- Populando a tabela addresses
INSERT INTO addresses (Name, Line1, Line2, City, Region, PostalCode)
VALUES
    ('João Silva', 'Rua Principal, 123', 'Apto 4B', 'São Paulo', 'SP', '01001-000'),
    ('Maria Oliveira', 'Rua das Flores, 456', NULL, 'Rio de Janeiro', 'RJ', '20000-000'),
    ('Carlos Souza', 'Av. das Palmeiras, 789', 'Sala 300', 'Belo Horizonte', 'MG', '30000-000');





-- Populando a tabela toppings
INSERT INTO toppings (Name, Price)
VALUES
    ('Queijo Extra', 5.00),
    ('Cogumelos', 3.00),
    ('Azeitonas', 3.00),
    ('Pepperoni', 5.00),
    ('Cebolas', 2.50);

-- Populando a tabela userInfos
INSERT INTO userInfos (IsAuthenticated, Name)
VALUES
    (1, 'João Silva'),
    (1, 'Maria Oliveira'),
    (0, 'Convidado');

-- Populando a tabela orders
INSERT INTO orders (UserInfo, CreatedTime, Address, TotalPreci)
VALUES
    (1, GETDATE(), 1, 79.99),
    (2, GETDATE(), 2, 89.99),
    (3, GETDATE(), 3, 45.99);

-- Populando a tabela orderWithStatuses
INSERT INTO orderWithStatuses (OrdemId, StatusText)
VALUES
    (1, 'Preparando'),
    (2, 'A caminho'),
    (3, 'Entregue');

	-- Populando a tabela pizza
INSERT INTO pizza (Name, Description, Price, Vegetarian, Vegan, OrderIdOrder)
VALUES
    ('Margherita', 'Pizza clássica de queijo e tomate', 29.99, 1, 0, 1),
    ('Pepperoni', 'Pizza de pepperoni com queijo mussarela', 34.99, 0, 0, 2),
    ('Vegetariana', 'Pizza carregada de vegetais', 31.99, 1, 1, 3)


	INSERT INTO pizzaToppings ([Pizza-Id], [Topping-Id])
VALUES
    (1, 1), -- Queijo Extra na Pizza Margherita
    (1, 2), -- Cogumelos na Pizza Margherita
    (2, 4), -- Pepperoni na Pizza Pepperoni
    (3, 2), -- Cogumelos na Pizza Vegetariana
    (3, 3); -- Azeitonas na Pizza Vegetariana





	-- Populando a tabela pizzaSpecials
INSERT INTO pizzaSpecials (Name, BasePrice, Description, ImageUrl)
VALUES
    ('Bacon', 39.99, 'Pizza clássica com bacon crocante e queijo', 'img/pizzas/bacon.jpg'),
    ('brit', 49.99, 'Pizza estilo britânico com pepperoni e queijo mussarela', 'img/pizzas/brit.jpg'),
    ('Cogumelo', 59.99, 'Pizza carregada com cogumelos frescos e queijo', 'img/pizzas/mushroom.jpg'),
    ('Carne', 45.99, 'Pizza com uma generosa quantidade de carnes variadas', 'img/pizzas/meaty.jpg'),
	('Bacon', 39.99, 'Pizza clássica com bacon crocante e queijo', 'img/pizzas/bacon.jpg'),
    ('brit', 49.99, 'Pizza estilo britânico com pepperoni e queijo mussarela', 'img/pizzas/brit.jpg'),
    ('Cogumelo', 59.99, 'Pizza carregada com cogumelos frescos e queijo', 'img/pizzas/mushroom.jpg'),
    ('Carne', 45.99, 'Pizza com uma generosa quantidade de carnes variadas', 'img/pizzas/meaty.jpg');



UPDATE pizzaSpecials
SET ImageUrl = 'img/pizzas/salad.jpg'
WHERE IdPizzaSpecial = '3';


INSERT INTO pizzaSpecials (Name, BasePrice, Description, ImageUrl)
VALUES
    ('Bacon', 39.99, 'Pizza clássica com bacon crocante e queijo', 'img/pizzas/bacon.jpg'),
    ('Brit', 49.99, 'Pizza estilo britânico com pepperoni e queijo mussarela', 'img/pizzas/brit.jpg'),
    ('Cogumelo', 59.99, 'Pizza carregada com cogumelos frescos e queijo', 'img/pizzas/mushroom.jpg'),
    ('Carne', 45.99, 'Pizza com uma generosa quantidade de carnes variadas', 'img/pizzas/meaty.jpg'),
    ('Margherita', 29.99, 'Pizza clássica de queijo e tomate', 'https://upload.wikimedia.org/wikipedia/commons/thumb/d/d1/Margarita_Real.jpg/800px-Margarita_Real.jpg'),
    ('Pepperoni', 34.99, 'Pizza de pepperoni com queijo mussarela', 'https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Hot_pizza.jpg/800px-Hot_pizza.jpg'),
    ('Vegetariana', 31.99, 'Pizza carregada de vegetais', 'https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Green_Vegetarian_Pizza.jpg/800px-Green_Vegetarian_Pizza.jpg'),
    ('Quatro Queijos', 36.99, 'Pizza com uma mistura de quatro queijos deliciosos', 'https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Four_cheese_pizza.jpg/800px-Four_cheese_pizza.jpg'),
    ('Frango com Catupiry', 38.99, 'Pizza de frango desfiado com catupiry', 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/2d/Pizza_Frango_Catupiry.jpg/800px-Pizza_Frango_Catupiry.jpg'),
    ('Calabresa', 33.99, 'Pizza de calabresa com cebolas', 'https://upload.wikimedia.org/wikipedia/commons/thumb/7/73/Pizza_de_Calabresa.jpg/800px-Pizza_de_Calabresa.jpg'),
    ('Portuguesa', 37.99, 'Pizza de presunto, ovos e azeitonas', 'https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Pizza_portuguesa.jpg/800px-Pizza_portuguesa.jpg'),
    ('Havaiana', 32.99, 'Pizza com presunto e abacaxi', 'https://upload.wikimedia.org/wikipedia/commons/thumb/1/11/Hawaiian_pizza.jpg/800px-Hawaiian_pizza.jpg'),
    ('Mexicana', 34.99, 'Pizza com carne moída, pimentão, cebola e jalapeños', 'https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/Mexican_pizza.jpg/800px-Mexican_pizza.jpg'),
    ('Quatro Estações', 35.99, 'Pizza com quatro sabores diferentes em uma só', 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Pizza_quatro_estacoes.jpg/800px-Pizza_quatro_estacoes.jpg'),
    ('Alho e Óleo', 30.99, 'Pizza de alho e óleo com queijo', 'https://upload.wikimedia.org/wikipedia/commons/thumb/f/f9/Garlic_Pizza.jpg/800px-Garlic_Pizza.jpg'),
    ('Napolitana', 33.99, 'Pizza com molho de tomate, queijo e anchovas', 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Napolitana_pizza.jpg/800px-Napolitana_pizza.jpg'),
    ('Rúcula com Tomate Seco', 36.99, 'Pizza com rúcula, tomate seco e queijo', 'https://upload.wikimedia.org/wikipedia/commons/thumb/1/17/Rucula_Pizza.jpg/800px-Rucula_Pizza.jpg'),
    ('Atum', 34.99, 'Pizza com atum, cebola e azeitonas', 'https://upload.wikimedia.org/wikipedia/commons/thumb/0/05/Tuna_Pizza.jpg/800px-Tuna_Pizza.jpg'),
    ('Pesto', 35.99, 'Pizza com molho pesto, queijo e tomate', 'https://upload.wikimedia.org/wikipedia/commons/thumb/b/b9/Pesto_Pizza.jpg/800px-Pesto_Pizza.jpg'),
    ('Carne Seca com Catupiry', 37.99, 'Pizza de carne seca desfiada com catupiry', 'https://upload.wikimedia.org/wikipedia/commons/thumb/6/63/Carne_Seca_Pizza.jpg/800px-Carne_Seca_Pizza.jpg'),
    ('Zucchini', 31.99, 'Pizza de abobrinha grelhada com queijo', 'https://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Zucchini_Pizza.jpg/800px-Zucchini_Pizza.jpg'),
    ('Caprese', 33.99, 'Pizza com tomate, mussarela de búfala e manjericão', 'https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Caprese_Pizza.jpg/800px-Caprese_Pizza.jpg'),
    ('Provolone', 34.99, 'Pizza de queijo provolone e tomate', 'https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/Provolone_Pizza.jpg/800px-Provolone_Pizza.jpg'),
    ('Gorgonzola', 36.99, 'Pizza com queijo gorgonzola e nozes', 'https://upload.wikimedia.org/wikipedia/commons/thumb/8/8c/Gorgonzola_Pizza.jpg/800px-Gorgonzola_Pizza.jpg'),
    ('Marinara', 28.99, 'Pizza simples com molho de tomate, alho e orégano', 'https://upload.wikimedia.org/wikipedia/commons/thumb/1/1e/Marinara_Pizza.jpg/800px-Marinara_Pizza.jpg');