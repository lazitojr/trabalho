using System;
using Trabalho_Final.DAO;

namespace Trabalho_Final
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            LivroDAO livroDAO = new LivroDAO();
            EmprestimoDAO emprestimoDAO = new EmprestimoDAO();

            bool sair = false;

            while (!sair)
            {
                Console.Clear();

                Console.WriteLine("//////////////////////////////////////");
                Console.WriteLine("      SISTEMA DE BIBLIOTECA");
                Console.WriteLine("//////////////////////////////////////");
                Console.WriteLine("1 - Cadastrar Usuário");
                Console.WriteLine("2 - Listar Usuários");
                Console.WriteLine("3 - Alterar Usuário");
                Console.WriteLine("4 - Excluir Usuário");

                Console.WriteLine("///////////////////////////////////////");


                Console.WriteLine("5 - Cadastrar Livro");
                Console.WriteLine("6 - Listar Livros");
                Console.WriteLine("7 - Alterar Livro");
                Console.WriteLine("8 - Excluir Livros");
                Console.WriteLine("9 - Buscar Livro");

                Console.WriteLine("///////////////////////////////////////");

                Console.WriteLine("10 - Realizar Empréstimo");
                Console.WriteLine("11 - Registrar Devolução");
                Console.WriteLine("12 - Listar Empréstimos");
                Console.WriteLine("13 - Sair");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out int opcao))
                {
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        Console.Clear();

                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();

                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Tipo (1-Aluno / 2-Professor): ");

                        int tipo = int.Parse(Console.ReadLine());

                        Pessoa usuario;

                        if (tipo == 1)
                            usuario = new Aluno();
                        else
                            usuario = new Professor();

                        usuario.Nome = nome;
                        usuario.Email = email;

                        usuarioDAO.Inserir(usuario);

                        Console.ReadKey();
                        break;

                    case 2:

                        Console.Clear();

                        foreach (Pessoa u in usuarioDAO.Listar())
                        {
                            Console.WriteLine("///////////////////////////////////////");
                            Console.WriteLine($"ID: {u.Id}");
                            Console.WriteLine($"Nome: {u.Nome}");
                            Console.WriteLine($"Email: {u.Email}");
                            Console.WriteLine($"Tipo: {u.GetType().Name}");
                        }

                        Console.ReadKey();
                        break;


                    case 3:

                        Console.Clear();

                        Console.Write("ID do usuário: ");
                        int idAlterarUsuario = int.Parse(Console.ReadLine());
                        usuario = usuarioDAO.BuscarPorId(idAlterarUsuario);

                        if (usuario == null)
                        {
                            Console.WriteLine("Usuário não encontrado!");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Novo Nome: ");
                        usuario.Nome = Console.ReadLine();

                        Console.Write("Novo Email: ");
                        usuario.Email = Console.ReadLine();

                        Console.Write("Tipo (1-Aluno / 2-Professor): ");
                        int tipoUsuario = int.Parse(Console.ReadLine());

                        if (tipoUsuario == 1)
                            usuario = new Aluno
                            {
                                Id = idAlterarUsuario,
                                Nome = usuario.Nome,
                                Email = usuario.Email
                            };
                        else
                            usuario = new Professor
                            {
                                Id = idAlterarUsuario,
                                Nome = usuario.Nome,
                                Email = usuario.Email
                            };

                        usuarioDAO.Atualizar(usuario);

                        Console.ReadKey();

                        break;



                    case 4:

                        Console.Clear();

                        Console.Write("ID do usuário: ");

                        int idExcluirUsuario = int.Parse(Console.ReadLine());

                        usuarioDAO.Excluir(idExcluirUsuario);

                        Console.ReadKey();

                        break;



                    case 5:

                        Console.Clear();

                        Livro livro = new Livro();

                        Console.Write("Título: ");
                        livro.Titulo = Console.ReadLine();

                        Console.Write("Autor: ");
                        livro.Autor = Console.ReadLine();

                        Console.Write("Ano: ");
                        livro.Ano = int.Parse(Console.ReadLine());

                        livro.Disponivel = true;

                        livroDAO.Inserir(livro);

                        Console.ReadKey();

                        break;

                    case 6:

                        Console.Clear();

                        foreach (Livro l in livroDAO.Listar())
                        {
                            Console.WriteLine("///////////////////////////////////////");
                            Console.WriteLine($"ID: {l.Id}");
                            Console.WriteLine($"Título: {l.Titulo}");
                            Console.WriteLine($"Autor: {l.Autor}");
                            Console.WriteLine($"Ano: {l.Ano}");
                            Console.WriteLine($"Disponível: {l.Disponivel}");
                        }

                        Console.ReadKey();

                        break;

                    case 7:

                        Console.Clear();

                        Console.Write("ID do livro: ");

                        int idLivroAlterar = int.Parse(Console.ReadLine());

                        Livro livroAlterar = livroDAO.BuscarPorId(idLivroAlterar);

                        if (livroAlterar == null)
                        {
                            Console.WriteLine("Livro não encontrado!");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Novo título: ");
                        livroAlterar.Titulo = Console.ReadLine();

                        Console.Write("Novo autor: ");
                        livroAlterar.Autor = Console.ReadLine();

                        Console.Write("Novo ano: ");
                        livroAlterar.Ano = int.Parse(Console.ReadLine());

                        livroDAO.Atualizar(livroAlterar);

                        Console.ReadKey();

                        break;



               
                    case 8:

                        Console.Clear();

                        Console.Write("ID do livro: ");

                        int idExcluirLivro = int.Parse(Console.ReadLine());

                        livroDAO.Excluir(idExcluirLivro);

                        Console.ReadKey();

                        break;


                    case 9:

                        Console.Clear();

                        Console.Write("Digite o título: ");

                        string titulo = Console.ReadLine();

                        var livros = livroDAO.BuscarPorTitulo(titulo);

                        foreach (Livro l in livros)
                        {
                            Console.WriteLine("///////////////////////////////");
                            Console.WriteLine($"ID: {l.Id}");
                            Console.WriteLine($"Título: {l.Titulo}");
                            Console.WriteLine($"Autor: {l.Autor}");
                            Console.WriteLine($"Ano: {l.Ano}");
                            Console.WriteLine($"Disponível: {l.Disponivel}");
                        }

                        Console.ReadKey();

                        break;

                    case 10:

                        Console.Clear();

                        Console.Write("ID do usuário: ");

                        int idUsuario = int.Parse(Console.ReadLine());

                        Console.Write("ID do livro: ");

                        int idLivro = int.Parse(Console.ReadLine());

                        emprestimoDAO.RealizarEmprestimo(idUsuario, idLivro);

                        Console.ReadKey();

                        break;

                    case 11:

                        Console.Clear();

                        Console.Write("ID do empréstimo: ");

                        int idEmprestimo = int.Parse(Console.ReadLine());

                        emprestimoDAO.RegistrarDevolucao(idEmprestimo);

                        Console.ReadKey();

                        break;

                    case 12:

                        Console.Clear();

                        foreach (Emprestimo e in emprestimoDAO.Listar())
                        {
                            Console.WriteLine("///////////////////////////////");
                            Console.WriteLine($"ID: {e.Id}");
                            Console.WriteLine($"Usuário: {e.Usuario.Nome}");
                            Console.WriteLine($"Livro: {e.Livro.Titulo}");
                            Console.WriteLine($"Empréstimo: {e.DataEmprestimo:d}");
                            Console.WriteLine($"Prevista: {e.DataPrevista:d}");

                            if (e.DataDevolucao == null)
                                Console.WriteLine("Situação: Em aberto");
                            else
                                Console.WriteLine($"Devolução: {e.DataDevolucao:d}");
                        }

                        Console.ReadKey();

                        break;

                    case 13:

                        sair = true;

                        break;

                    default:

                        Console.WriteLine("Opção inválida!");

                        Console.ReadKey();

                        break;
                }
            }
        }
    }
}