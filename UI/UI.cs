using System;
using System.Collections.Generic;
using System.Threading;

namespace jogo_forca {
    public static class UI {
        public static void DesenharForca(int vidas, List<string> letrasUsadas) {
            string coracoes = GerarCoracoes(vidas);

            Console.WriteLine("  _______      Vidas: " + coracoes);
            Console.Write(" |/      |     Letras usadas: ");
            GerarHistoricoLetras(letrasUsadas);

            if (vidas <= 5) {
                Console.WriteLine(" |      (_)");
            } else  {
                Console.WriteLine(" |");
            }

            if (vidas == 4) { 
                Console.WriteLine(" |       |");  
            }

            else if (vidas == 3) {
                Console.WriteLine(" |      \\|");
            }

            else if (vidas <= 2) {
                Console.WriteLine(" |      \\|/");
            }

            else {
                Console.WriteLine(" |");
            }


            if (vidas <= 2) {
                Console.WriteLine(" |       |");
            } else {
                Console.WriteLine(" |");
            }


            if (vidas == 1) {
                Console.WriteLine(" |      / ");
            }

            else if (vidas == 0)
            {
                Console.WriteLine(" |      / \\");
            }

            else
            {
                Console.WriteLine(" |");
            }


            Console.WriteLine(" |");
            Console.WriteLine("_|___");
        }

        public static string GerarCoracoes(int vidas) {
            string coracoes = "";
            for (int i = 0; i < vidas; i++) {
                coracoes += "♥ ";
            }
            return coracoes;
        }

        public static void GerarHistoricoLetras(List<string> letrasUsadas) {
            Console.ForegroundColor = ConsoleColor.Yellow;

            foreach (var letra in letrasUsadas) {
                Console.Write(letra + " ");
            }

            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(texto);
            Console.ResetColor();
        }

        public static void Linha() {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('─', 40));
            Console.ResetColor();
        }
    }
}