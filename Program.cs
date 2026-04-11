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

                Console.WriteLine(" 1) Multiplayer");
                Console.WriteLine(" 2) Single Player");
                Console.WriteLine(" 3) Sair");

                UI.Linha();
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

            Console.Write("PALAVRA: ");
            string palavra = Console.ReadLine().ToUpper();

            Console.Clear();

            new Jogo().Jogar(palavra);
        }

        static void SinglePlayer() {
            Console.Clear();

            UI.Titulo("Modo Single Player");
            UI.Linha();

            Console.WriteLine("1) Frutas");
            Console.WriteLine("2) Objetos");
            Console.WriteLine("3) Cores");
            Console.WriteLine("4) Animais");
            Console.WriteLine("5) Paises");
            Console.WriteLine("0) Voltar");

            UI.Linha();
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

            // Sorteia uma palavra aleatória da lista com o RAMDOM e inicia o jogo
            string palavra = palavras[random.Next(palavras.Count)];
            new Jogo().Jogar(palavra);
        }
    }
}