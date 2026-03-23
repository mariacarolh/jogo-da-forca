using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_forca {
    internal class Program {

        static Random random = new Random();

        static List<string> bancoPalavras = new List<string>()
        {
            "COMPUTADOR",
            "PROGRAMADOR",
            "CAIXA",
            "RECEBIMENTO",
            "CONCLUIR",
            "DESENVOLVER",
            "ADICIONAR",
        };


        static void Main(string[] args) {
            bool sair = false;

            do {
                Console.Clear();
                Console.WriteLine("===== JOGO DA FORCA =====");
                Console.WriteLine("1 - MultiPlayer");
                Console.WriteLine("2 - SinglePlayer");
                Console.WriteLine("3 - Sair");
                Console.WriteLine();
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
            Console.WriteLine("Bem-vindo ao jogo da forca!");
            Console.WriteLine();
            Console.WriteLine("Digite uma palavra e pressione ENTER.");
            Console.WriteLine();
            Console.Write("Palavra: ");
            string Palavra = Console.ReadLine().ToUpper();
            //Console.Clear();
            Jogar(Palavra, 2);
        }

        static void SinglePlayer() {
            Console.Clear();
            string Palavra = bancoPalavras[random.Next(bancoPalavras.Count)];
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
                DesenharForca(numeroVidas);
                Console.WriteLine();
                Console.WriteLine("Vidas restantes: " + numeroVidas);
                Console.Write("Letras usadas: ");

                foreach (string letra in letrasUsadas)
                {
                    Console.Write(letra.ToUpper() + " ");
                }

                Console.WriteLine();
                Console.Write("A palavra escolhida é: ");

                // percorre cada posição da palavra
                for (int i = 0; i < letrasIdentificadas.Length; i++) {
                    // pega a letra que foi descoberta na posição 
                    string letraAtual = letrasIdentificadas[i];

                    if (letraAtual == null) {
                        // exibe _ quando não encontrar a letra
                        Console.Write("_ ");
                    } else {
                        // exibe a letra se ela tiver sido encontrada
                        Console.Write(letraAtual);
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Escolha uma letra");
                Console.WriteLine();

                string letraEscolhida;

                while (true) {
                    Console.Write("A letra escolhida é: ");
                    string entrada = Console.ReadLine().ToUpper();

                    if (entrada.Length == 1 && Char.IsLetter(entrada[0])) {
                        letraEscolhida = entrada;
                        break;
                    }

                    Console.WriteLine("Digite apenas UMA letra válida!");
                }
                if (letrasUsadas.Contains(letraEscolhida)) {
                    Console.WriteLine($"A letra {letraEscolhida} já foi usada!");
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    continue;
                }

                letrasUsadas.Add(letraEscolhida);

                bool letraEncontrada = false;
                Console.WriteLine(letraEscolhida);

                for (int i = 0; i < tamanhoPalavra; i++) {
                    string letraAtual = Palavra[i].ToString();

                    if (letraAtual == letraEscolhida) {
                        letrasIdentificadas[i] = letraAtual + " ";
                        letraEncontrada = true;
                    }
                }

                if (letraEncontrada == true) {
                    bool palavraDescoberta = true;
                    for (int i = 0; i < letrasIdentificadas.Length; i++) {
                        string letraAtual = letrasIdentificadas[i];

                        if (letraAtual == null) {
                            palavraDescoberta = false;
                        }
                    }

                    if (palavraDescoberta == true) {
                        Console.WriteLine();
                        Console.WriteLine("Parabéns, você ganhou! A palavra secreta era " + Palavra + ".");
                        jogoEmAndamento = false;
                    }
                } else {
                    numeroVidas--;
                    Console.WriteLine("A letra " + letraEscolhida + " não pertence a PALAVRA SECRETA!!!");
                    Console.WriteLine();
                    Console.WriteLine("Vidas restantes: " + numeroVidas);
                    Console.WriteLine();
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();

                    if (numeroVidas == 0) {
                        Console.Clear();
                        DesenharForca(numeroVidas);
                        Console.WriteLine();
                        Console.WriteLine("Você perdeu! A palavra era: " + Palavra);
                        jogoEmAndamento = false;
                    }
                }
            } while (jogoEmAndamento);

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla");
            Console.ReadKey();
        }

        static void DesenharForca(int vidas) {
            Console.WriteLine("  _______");
            Console.WriteLine(" |/      |");

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
            Console.WriteLine();
        }
    }
}