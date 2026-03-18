using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_forca {
    internal class Program {
        static void Main(string[] args) {
            bool sair = false;

            do {
                Console.Clear();
                Console.WriteLine("===== JOGO DA FORCA =====");
                Console.WriteLine("1 - Jogar");
                Console.WriteLine("2 - vm faze ainda");
                Console.WriteLine("3 - Sair");
                Console.WriteLine();
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        Jogar();
                        break;

                    case "2":
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

        static void Jogar() {
            Console.Clear();
            Console.WriteLine("Bem-vindo ao jogo da forca!");
            Console.WriteLine();
            Console.WriteLine("Digite uma palavra e pressione ENTER.");
            Console.WriteLine();
            Console.Write("Palavra: ");

            string palavraEscolhida = Console.ReadLine();
            string[] letrasIdentificadas = new string[palavraEscolhida.Length]; // identifica a quantidade de letras da palavra escolhida e cria um array com a mesma quantidade de posições com o valor null
            int tamanhoPalavra = palavraEscolhida.Length;
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
                    Console.Write(letra + " ");
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

                Console.Write("A letra escolhida é: ");
                string letraEscolhida = Console.ReadLine();

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
                    string letraAtual = palavraEscolhida[i].ToString();

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
                        Console.WriteLine("Parabéns, você ganhou! A palavra secreta era " + palavraEscolhida + ".");
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
                        Console.WriteLine("Você perdeu! A palavra era: " + palavraEscolhida);
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
            }  else  {
                Console.WriteLine(" |");
            }

            Console.WriteLine(" |");
            Console.WriteLine("_|___");
            Console.WriteLine();
        }
    }
}