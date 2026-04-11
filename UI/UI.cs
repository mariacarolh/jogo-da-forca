using System;
using System.Collections.Generic;
using System.Threading;

namespace jogo_forca {
    public static class UI {
        public static void DesenharForca(int vidas, List<string> letrasUsadas) {
            Console.WriteLine();

            string coracoes = GerarCoracoes(vidas);

            // Cor da forca muda conforme as vidas
            ConsoleColor corForca = vidas > 3 ? ConsoleColor.White : vidas > 1 ? ConsoleColor.Yellow : ConsoleColor.Red;

            string cabeca  = vidas <= 5 ? "(_)" : "   ";
            string tronco1 = vidas == 4 ? " | " : vidas == 3 ? "\\| " : vidas <= 2 ? "\\|/" : "   ";
            string tronco2 = vidas <= 2 ? " | " : "   ";
            string pernas  = vidas == 1 ? "/  " : vidas == 0 ? "/ \\" : "   ";

            Console.ForegroundColor = corForca;
            Console.WriteLine("   _______");
            Console.WriteLine("  |/      |");
            Console.WriteLine("  |      " + cabeca);
            Console.WriteLine("  |      " + tronco1);
            Console.WriteLine("  |      " + tronco2);
            Console.WriteLine("  |      " + pernas);
            Console.WriteLine("  |");
            Console.WriteLine("__|___");
            Console.ResetColor();

            Console.WriteLine();

            // Vidas com cor dinâmica
            Console.Write("  Vidas : ");
            Console.ForegroundColor = vidas > 3 ? ConsoleColor.Green : vidas > 1 ? ConsoleColor.Yellow : ConsoleColor.Red;
            Console.WriteLine(coracoes);
            Console.ResetColor();

            // Letras usadas
            Console.Write("  Letras: ");
            GerarHistoricoLetras(letrasUsadas);
        }

        public static string GerarCoracoes(int vidas) {
            string cheios  = new string('♥', vidas).Replace("♥", "♥ ");
            string vazios  = new string('♡', 6 - vidas).Replace("♡", "♡ ");
            return cheios + vazios;
        }

        public static void GerarHistoricoLetras(List<string> letrasUsadas) {
            if (letrasUsadas.Count == 0) {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("nenhuma ainda");
                Console.ResetColor();
                return;
            }

            foreach (var letra in letrasUsadas) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"[{letra}]");
                Console.ResetColor();
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        public static void PiscarMensagem(string mensagem, ConsoleColor cor, int piscadas) {
            int linha = Console.CursorTop;

            for (int i = 0; i < piscadas; i++) {
                Console.SetCursorPosition(0, linha);
                Console.ForegroundColor = cor;
                Console.Write(mensagem);

                Thread.Sleep(250);

                // Apaga a mensagem (sobrescreve com espaços)
                Console.SetCursorPosition(0, linha);
                Console.Write(new string(' ', mensagem.Length));

                Thread.Sleep(150);
            }


            // Deixa a mensagem visível após o efeito
            Console.SetCursorPosition(0, linha);
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

        public static void Titulo(string texto) {
            int largura = 55;
            int padding = Math.Max(0, (largura - texto.Length) / 2);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('═', largura));
            Console.WriteLine(new string(' ', padding) + texto);
            Console.WriteLine(new string('═', largura));
            Console.ResetColor();
        }

        public static void Linha() {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('─', 55));
            Console.ResetColor();
        }

        public static void ExibirIntegrantes(string[] nomes, string[] RAs) {
            Console.WriteLine();

            for (int i = 0; i < nomes.Length; i++) {
                Console.Write("  ");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(nomes[i]);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("  —  RA: ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(RAs[i]);

                Console.ResetColor();
            }

            Console.WriteLine();
        }

        public static string LerSenha() {
            string entrada = "";

            while (true) {
                // Lê a tecla sem exibir nada na tela com intercepttrue
                ConsoleKeyInfo tecla = Console.ReadKey(intercept: true);

                // Enter encerra a leitura
                if (tecla.Key == ConsoleKey.Enter)
                    break;

                // Backspace remove o último caractere da string e apaga o * da tela
                if (tecla.Key == ConsoleKey.Backspace) {
                    if (entrada.Length > 0) {
                        entrada = entrada.Substring(0, entrada.Length - 1);
                        Console.Write("\b \b"); // volta um, escreve espaço, volta de novo
                    }
                    continue;
                }

                if (Char.IsLetter(tecla.KeyChar)) {
                    entrada += tecla.KeyChar;
                    Console.Write("*"); // exibe * no lugar da letra digitada
                }
            }

            Console.WriteLine();
            return entrada;
        }
    }
}