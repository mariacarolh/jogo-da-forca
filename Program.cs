using System.IO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace jogo_forca {
    internal class Program {

        static Random random = new Random();

        static void Main(string[] args) {
            bool sair = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            do {
                Console.Clear();

                Titulo("JOGO DA FORCA");
                Linha();

                Console.WriteLine(" 1) Multiplayer");
                Console.WriteLine(" 2) Single Player");
                Console.WriteLine(" 3) Sair");

                Linha();
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

            Titulo("Modo Multiplayer");
            Linha();

            Console.WriteLine("Digite uma palavra e pressione ENTER.");
            Console.WriteLine();

            Console.Write("PALAVRA: ");
            string Palavra = Console.ReadLine().ToUpper();

            Console.Clear();

            Jogar(Palavra, 2);
        }

        static void SinglePlayer() {
            Console.Clear();

            Titulo("Modo Single Player");
            Linha();

            Console.WriteLine("1) Frutas");
            Console.WriteLine("2) Objetos");
            Console.WriteLine("3) Cores");
            Console.WriteLine("4) Animais");
            Console.WriteLine("5) Paises");
            Console.WriteLine("0) Voltar");

            Linha();
            Console.Write("Escolha uma categoria: ");

            string opcao = Console.ReadLine();

            List<string> palavras = new List<string>();

            switch (opcao) { 
            
                case "1":
                    palavras = CarregarPalavras("frutas.txt");
                    break;

                case "2":
                    palavras = CarregarPalavras("objetos.txt");
                    break;

                case "3":
                    palavras = CarregarPalavras("cores.txt");
                    break;

                case "4":
                    palavras = CarregarPalavras("animais.txt");
                    break;

                case "5":
                    palavras = CarregarPalavras("paises.txt");
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

            string Palavra = palavras[random.Next(palavras.Count)];
            Jogar(Palavra, 1);
    }

        static void Jogar(string Palavra, int modo) {

            string[] letrasIdentificadas = new string[Palavra.Length]; // identifica a quantidade de letras da palavra escolhida e cria um array com a mesma quantidade de posições com o valor null
            int tamanhoPalavra = Palavra.Length;

            List<string> letrasUsadas = new List<string>();

            bool jogoEmAndamento = true;
            int numeroVidas = 6;

            do {
                Console.Clear();

                DesenharForca(numeroVidas, letrasUsadas);

                Linha();

                Console.Write("PALAVRA: ");

                // percorre cada posição da palavra
                for (int i = 0; i < letrasIdentificadas.Length; i++) {
                    // pega a letra que foi descoberta na posição 
                    string letraAtual = letrasIdentificadas[i];

                    if (letraAtual == null) {
                        Console.Write("_ ");
                    } else {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(letraAtual + " ");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine();
                Console.WriteLine();

                string letraEscolhida;

                while (true) {
                    Console.Write("Digite uma letra: ");
                    string entrada = Console.ReadLine().ToUpper();

                    if (entrada.Length == 1 && Char.IsLetter(entrada[0])) {
                        letraEscolhida = entrada;
                        break;
                    }

                    Console.WriteLine("Digite apenas UMA letra válida!");
                }

                if (letrasUsadas.Contains(letraEscolhida)) {
                    Console.WriteLine($"A letra {letraEscolhida} já foi usada!");
                    Console.ReadKey();
                    continue;
                }

                letrasUsadas.Add(letraEscolhida);

                bool letraEncontrada = false;

                for (int i = 0; i < tamanhoPalavra; i++) {
                    string letraAtual = Palavra[i].ToString();

                    if (letraAtual == letraEscolhida) {
                        letrasIdentificadas[i] = letraAtual;
                        letraEncontrada = true;
                    }
                }

                if (letraEncontrada) {
                    bool palavraDescoberta = true;

                    for (int i = 0; i < letrasIdentificadas.Length; i++) {
                        if (letrasIdentificadas[i] == null)  {
                            palavraDescoberta = false;
                        }
                    }

                    if (palavraDescoberta) {
                        Linha();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("VOCÊ GANHOU! :D");
                        Console.ResetColor();

                        Console.Write("A palavra secreta era: ");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(Palavra);
                        Console.ResetColor();

                        jogoEmAndamento = false;
                    }
                } else {
                    numeroVidas--;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("A letra ");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(letraEscolhida);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" não está na palavra!");
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.WriteLine("Vidas restantes: " + numeroVidas);

                    Console.ReadKey();

                    if (numeroVidas == 0) {
                        Console.Clear();
                        DesenharForca(numeroVidas, letrasUsadas);

                        Linha();

                        PiscarMensagem("GAME OVER", ConsoleColor.Red, 4);

                        Console.WriteLine();
                        Console.Write("A palavra era: ");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(Palavra);
                        Console.ResetColor();

                        jogoEmAndamento = false;
                    }
                }

            } while (jogoEmAndamento);

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }

        static void DesenharForca(int vidas, List<string> letrasUsadas) {
            string coracoes = GerarCoracoes(vidas);

            Console.WriteLine("  _______      Vidas: " + coracoes);
            Console.Write(" |/      |     Letras usadas: ");
            GerarHistoricoLetras(letrasUsadas);

            if (vidas <= 5) {
                Console.WriteLine(" |      (_)");
            } else {
                Console.WriteLine(" |");
            }

            if (vidas == 4) {
                Console.WriteLine(" |       |");
            } else if (vidas == 3) {
                Console.WriteLine(" |      \\|");
            } else if (vidas <= 2) {
                Console.WriteLine(" |      \\|/");
            } else {
                Console.WriteLine(" |");
            }

            if (vidas <= 2) {
                Console.WriteLine(" |       |");
            } else {
                Console.WriteLine(" |");
            }

            if (vidas == 1) {
                Console.WriteLine(" |      / ");
            } else if (vidas == 0) {
                Console.WriteLine(" |      / \\");
            } else {
                Console.WriteLine(" |");
            }

            Console.WriteLine(" |");
            Console.WriteLine("_|___");
        }

        static string GerarCoracoes(int vidas) {
            string coracoes = "";

            for (int i = 0; i < vidas; i++) {
                coracoes += "♥ ";
            }

            return coracoes;
        }

        static void GerarHistoricoLetras(List<string> letrasUsadas) {
            Console.ForegroundColor = ConsoleColor.Yellow;

            foreach (string letra in letrasUsadas) {
                Console.Write(letra + " ");
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        static void PiscarMensagem(string mensagem, ConsoleColor cor, int piscadas) {
            int linha = Console.CursorTop;

            for (int i = 0; i < piscadas; i++) {
                Console.SetCursorPosition(0, linha);

                Console.ForegroundColor = cor;
                Console.Write(mensagem);

                Thread.Sleep(250);

                Console.SetCursorPosition(0, linha);
                Console.Write(new string(' ', mensagem.Length));

                Thread.Sleep(150);
            }

            Console.SetCursorPosition(0, linha);

            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

        static void Titulo(string texto) {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(texto);
            Console.ResetColor();
        }

        static void Linha() {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('─', 40));
            Console.ResetColor();
        }

        static List<string> CarregarPalavras(string arquivo) {
            if (!File.Exists(arquivo))
                return new List<string>();

            return File.ReadAllLines(arquivo)
                       .Where(l => !string.IsNullOrWhiteSpace(l))
                       .Select(l => l.Trim().ToUpper())
                       .ToList();
        }
    }
}