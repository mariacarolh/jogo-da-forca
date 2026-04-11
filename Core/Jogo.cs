using System;
using System.Collections.Generic;

namespace jogo_forca {
    public class Jogo {

        // Executa uma partida com a palavra fornecida e retorna true se o jogador venceu
        public bool Jogar(string Palavra) {
            // Cria um array do tamanho da palavra, cada posição começa como null (letra não descoberta)
            string[] letrasIdentificadas = new string[Palavra.Length];
            int tamanhoPalavra = Palavra.Length;

            List<string> letrasUsadas = new List<string>();

            bool jogoEmAndamento = true;
            int numeroVidas = 6;

            // Controla se o jogador venceu para retornar ao final
            bool venceu = false;

            do {
                Console.Clear();

                // Redesenha a forca e o status a cada rodada
                UI.DesenharForca(numeroVidas, letrasUsadas);

                UI.Linha();

                Console.Write("PALAVRA: ");

                // Exibe as letras descobertas e "_" no lugar das ainda ocultas
                for (int i = 0; i < letrasIdentificadas.Length; i++) {
                    string letraAtual = letrasIdentificadas[i];

                    if (letraAtual == null)
                        Console.Write("_ ");
                    else {
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

                // Impede que o jogador repita uma letra já usada
                if (letrasUsadas.Contains(letraEscolhida)) {
                    Console.WriteLine($"A letra {letraEscolhida} já foi usada!");
                    Console.ReadKey();
                    continue;
                }

                letrasUsadas.Add(letraEscolhida);

                bool letraEncontrada = false;

                // Percorre a palavra procurando a letra e revela as posições encontradas
                for (int i = 0; i < tamanhoPalavra; i++) {
                    string letraAtual = Palavra[i].ToString();

                    if (letraAtual == letraEscolhida) {
                        letrasIdentificadas[i] = letraAtual;
                        letraEncontrada = true;
                    }
                }

                if (letraEncontrada) {
                    // Verifica se ainda existe alguma posição nula (letra não descoberta)
                    bool palavraDescoberta = true;

                    for (int i = 0; i < letrasIdentificadas.Length; i++) {
                        if (letrasIdentificadas[i] == null)
                            palavraDescoberta = false;
                    }

                    // Se não há mais posições nulas, o jogador venceu
                    if (palavraDescoberta) {
                        UI.Linha();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("VOCÊ GANHOU! :D");
                        Console.ResetColor();

                        Console.Write("A palavra secreta era: ");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(Palavra);
                        Console.ResetColor();

                        jogoEmAndamento = false;
                        venceu = true;
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

                    // Se zerou as vidas, exibe game over e encerra o jogo
                    if (numeroVidas == 0) {
                        Console.Clear();
                        UI.DesenharForca(numeroVidas, letrasUsadas);

                        UI.Linha();

                        UI.PiscarMensagem("GAME OVER", ConsoleColor.Red, 4);

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

            // Retorna se o jogador venceu para que o Program possa registrar no ranking
            return venceu;
        }
    }
}