create database biblioteca;

CREATE TABLE usuarios(
id INT AUTO_INCREMENT PRIMARY KEY,
nome VARCHAR(100) NOT NULL,
email VARCHAR(100) NOT NULL,
tipo ENUM('Aluno','Professor') NOT NULL
);

CREATE TABLE livros(
id INT AUTO_INCREMENT PRIMARY KEY,
titulo VARCHAR(150) NOT NULL,
autor VARCHAR(100) NOT NULL,
ano INT NOT NULL,
disponivel BOOLEAN DEFAULT TRUE
);

CREATE TABLE emprestimos(
id INT AUTO_INCREMENT PRIMARY KEY,

id_usuario INT NOT NULL,
id_livro INT NOT NULL,

data_emprestimo DATE NOT NULL,
data_prevista DATE NOT NULL,
data_devolucao DATE NULL,

FOREIGN KEY(id_usuario) REFERENCES usuarios(id),

FOREIGN KEY(id_livro) REFERENCES livros(id)
);

select * from usuarios;
