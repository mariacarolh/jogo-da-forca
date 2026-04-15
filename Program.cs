using System;
using System.Collections.Generic;

namespace jogo_forca {
    internal class Program {

        static Random random = new Random();

        static void Main(string[] args) {
            bool sair = false;

            // Formatação para caracteres especiais
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            do {
                Console.Clear();

                UI.Titulo("JOGO DA FORCA");
                UI.Linha();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" [1] Multiplayer");
                Console.WriteLine(" [2] Single Player");
                Console.WriteLine(" [3] Ranking");
                Console.WriteLine(" [4] Detalhes");
                Console.WriteLine(" [5] Sair");

                UI.Linha();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        MultiPlayer();
                        break;

                    case "2":
                        SinglePlayer();
                        break;

                    case "3":
                        Ranking.ExibirRanking();
                        break;

                    case "4":
                        ExibirDetalhes();
                        break;
  
                    case "5":
                        sair = true;
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }

            } while (!sair);
        }

        static void MultiPlayer() {
            Console.Clear();

            UI.Titulo("Modo Multiplayer");
            UI.Linha();

            // Pede o nome do jogador para registrar no ranking
            Console.Write("Nome do jogador: ");
            string nomeJogador = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(nomeJogador))
                nomeJogador = "ANÔNIMO";

            Console.Write("PALAVRA: ");
            string palavra = UI.LerSenha().ToUpper();

            Console.Clear();

            // Registra o resultado da partida no ranking após o jogo terminar
            bool venceu = new Jogo().Jogar(palavra);
            Ranking.RegistrarResultado(nomeJogador, venceu);
        }

        static void SinglePlayer() {
            Console.Clear();

            UI.Titulo("Modo Single Player");
            UI.Linha();

            // Pede o nome do jogador para registrar no ranking
            Console.Write("Nome do jogador: ");
            string nomeJogador = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(nomeJogador))
                nomeJogador = "ANÔNIMO";

            Console.Clear();

            UI.Titulo("Modo Single Player");
            UI.Linha();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[1] Frutas");
            Console.WriteLine("[2] Objetos");
            Console.WriteLine("[3] Cores");
            Console.WriteLine("[4] Animais");
            Console.WriteLine("[5] Paises");
            Console.WriteLine("[0] Voltar");

            UI.Linha();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Escolha uma categoria: ");

            string opcao = Console.ReadLine();

            List<string> palavras = new List<string>();

            switch (opcao) {

                case "1":
                    palavras = Arquivo.CarregarPalavras("Data/frutas.txt");
                    break;

                case "2":
                    palavras = Arquivo.CarregarPalavras("Data/objetos.txt");
                    break;

                case "3":
                    palavras = Arquivo.CarregarPalavras("Data/cores.txt");
                    break;

                case "4":
                    palavras = Arquivo.CarregarPalavras("Data/animais.txt");
                    break;

                case "5":
                    palavras = Arquivo.CarregarPalavras("Data/paises.txt");
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    return;
            }

            if (palavras.Count == 0) {
                Console.WriteLine("Arquivo vazio ou não encontrado!");
                Console.ReadKey();
                return;
            }

            // Sorteia uma palavra aleatória da lista com o RANDOM e inicia o jogo
            string palavra = palavras[random.Next(palavras.Count)];

            // Registra o resultado da partida no ranking após o jogo terminar
            bool venceu = new Jogo().Jogar(palavra);
            Ranking.RegistrarResultado(nomeJogador, venceu);
        }

        static void ExibirDetalhes() {
            Console.Clear();

            UI.Titulo("SOBRE O JOGO");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Desenvolvido para a disciplina de Prática Profissional");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Linguagem: C#  |  Plataforma: .NET  |  Interface: Console");
            Console.ResetColor();

            Console.WriteLine();
            UI.Linha();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  FUNCIONALIDADES");
            Console.ResetColor();
            Console.WriteLine();

            string[] funcionalidades = {
                "Modo Multiplayer — um jogador digita a palavra para o outro adivinhar",
                "Modo Single Player — palavra sorteada aleatoriamente por categoria",
                "Categorias: Frutas, Objetos, Cores, Animais e Países",
                "Ranking com histórico de vitórias e derrotas",
                "Forca animada com 6 tentativas"
            };

            foreach (var item in funcionalidades) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("  • ");
                Console.ResetColor();
                Console.WriteLine(item);
            }

            Console.WriteLine();
            UI.Linha();

            UI.Titulo("INTEGRANTES DO GRUPO");

            string[] nomes = {
                "Cesar Augusto Silva",
                "Maria Carolina Pereira",
                "Sara Hadassa Bruzamolin Carvalho"
            };

            string[] RAs = {
                "2026109330",
                "2022202695",
                "2022101542"
            };

            UI.ExibirIntegrantes(nomes, RAs);

            UI.Linha();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}