use BlazingPizzariaApi

INSERT INTO Categorias (Nome)
VALUES ('Pizza');

INSERT INTO Produtos (Nome, Descricao, LocalizacaoEstoque, Marca, Moeda, Preco, QuantidadeEmEstoque, SKU, CategoriaId, DimensoesId, AtributosId, AvaliacaoId, ImagemId, DisponibilidadeId)
VALUES ('Marguerita', 'Pizza clássica de queijo e tomate', 'Localização Estoque 1', 'Blazing', 'BRL', 54.99, 10, 'SKU001', 1 , 1, 1, 1, 1, 1),
       ('Pepperoni', 'Pizza de pepperoni com queijo mussarela2', 'Localização Estoque 1', 'Blazing', 'BRL', 64.99, 20, 'SKU002', 1, 2, 2, 2, 2, 2),
       ('Vegetariana', 'Pizza carregada de vegetais', 'Localização Estoque 1', 'Blazing', 'BRL', 59.99, 15, 'SKU004', 1, 3, 3, 3, 3, 3),
	   ('Bacon', 'Pizza clássica com bacon crocante e queijo', 'Localização Estoque 1', 'Blazing', 'BRL', 59.99, 20, 'SKU005', 1, 4, 4, 4, 4, 4),
	   ('Quatro Queijos', 'Pizza com uma mistura de quatro queijos deliciosos', 'Localização Estoque 1', 'Blazing', 'BRL', 54.99, 15, 'SKU006', 1, 5, 5, 5, 5, 5);

INSERT INTO Atributos (Cor, Material, Modelo)
VALUES 
    ('Vermelho', 'Queijo e Tomate', 'Clássica'),
    ('Vermelho escuro', 'Pepperoni e Queijo Mussarela', 'Apimentada'),
    ('Variada', 'Vegetais', 'Carregada de Vegetais'),
    ('Marrom', 'Bacon e Queijo', 'Clássica com Bacon'),
    ('Amarelo', 'Quatro Queijos', 'Queijo');


INSERT INTO Avaliacaos (Media, NumeroDeAvaliacoes, RevisaoId)
VALUES 
    (4.5, 100, 1),  -- Avaliação para Marguerita
    (4.2, 150, 2),  -- Avaliação para Pepperoni
    (4.7, 120, 3),  -- Avaliação para Vegetariana
    (4.3, 90, 4),   -- Avaliação para Bacon
    (4.8, 110, 5);  -- Avaliação para Quatro Queijos


INSERT INTO Dimensoes (Altura, Largura, Profundidade, Peso, Unidade)
VALUES 
    (1.5, 30.0, 30.0, 0.5, 'cm'),  -- Marguerita
    (1.5, 30.0, 30.0, 0.55, 'cm'),  -- Pepperoni
    (1.5, 30.0, 30.0, 0.6, 'cm'),  -- Vegetariana
    (1.5, 30.0, 30.0, 0.65, 'cm'),  -- Bacon
    (1.5, 30.0, 30.0, 0.7, 'cm'); -- Quatro Queijos


INSERT INTO Disponibilidades (DataEstimadaDeEntrega, EstaDisponivel, ProdutoId)
VALUES 
    ('2024-07-01', 1, 1),  -- Disponibilidade para Marguerita
    ('2024-07-05', 0, 2),  -- Disponibilidade para Pepperoni
    ('2024-06-30', 1, 3),  -- Disponibilidade para Vegetariana
    ('2024-07-03', 1, 4),  -- Disponibilidade para Bacon
    ('2024-07-02', 1, 5);  -- Disponibilidade para Quatro Queijos


INSERT INTO Imagem(Url, TextoAlternativo, ProdutoId)
VALUES 
    ('img/pizzas/margherita.jpg', 'Pizza Marguerita', 1),
    ('img/pizzas/pepperoni.jpg', 'Pizza Pepperoni', 2),
    ('img/pizzas/salad.jpg', 'Pizza Vegetariana', 3),
    ('img/pizzas/bacon.jpg', 'Pizza Bacon', 4),
    ('img/pizzas/quatroQueijo.jpg', 'Pizza Quatro Queijos', 5);



INSERT INTO Revisaos (Comentario, Data, Usuario)
VALUES 
    ('Excelente pizza Marguerita, muito saborosa!', '2024-06-25', 'João'),
    ('Pizza Pepperoni deliciosa, recomendo!', '2024-06-28', 'Maria'),
    ('Pizza Vegetariana bem feita, ingredientes frescos.', '2024-06-30', 'Carlos'),
    ('Pizza Bacon boa, mas poderia ter mais bacon.', '2024-07-01', 'Ana'),
    ('Pizza Quatro Queijos fantástica, os queijos são incríveis!', '2024-07-02', 'Pedro');




