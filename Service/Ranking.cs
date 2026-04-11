using System;
using System.Collections.Generic;
using System.Linq;

namespace jogo_forca {
    public static class Ranking {

        // Caminho do arquivo de ranking, mesmo padrão dos outros arquivos de dados
        private const string CaminhoArquivo = "Data/ranking.txt";

        // Registra o resultado de uma partida para o jogador informado e persiste no arquivo
        public static void RegistrarResultado(string nomeJogador, bool venceu) {
            var ranking = Arquivo.CarregarRanking(CaminhoArquivo);

            // Se o jogador ainda não está no ranking, inicializa com zeros
            if (!ranking.ContainsKey(nomeJogador)) {
                ranking[nomeJogador] = (0, 0);
            }

            var (vitorias, derrotas) = ranking[nomeJogador];

            // Incrementa vitória ou derrota conforme o resultado da partida
            if (venceu) {
                ranking[nomeJogador] = (vitorias + 1, derrotas);
            } else {
                ranking[nomeJogador] = (vitorias, derrotas + 1);
            }

            Arquivo.SalvarRanking(CaminhoArquivo, ranking);
        }

        // Exibe o ranking atual ordenado por vitórias (maior para menor)
        public static void ExibirRanking() {
            Console.Clear();

            UI.Titulo("RANKING");
            UI.Linha();

            var ranking = Arquivo.CarregarRanking(CaminhoArquivo);

            if (ranking.Count == 0) {
                Console.WriteLine("Nenhuma partida registrada ainda!");
            } else {
                // Ordena por vitórias decrescente; em caso de empate, menos derrotas primeiro
                var ordenado = ranking.OrderByDescending(r => r.Value.Vitorias).ThenBy(r => r.Value.Derrotas).ToList();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" {"#",-4} {"JOGADOR",-24} {"VITÓRIAS",-13} {"DERROTAS",-13}");
                UI.Linha();

                for (int i = 0; i < ordenado.Count; i++) {
                    var entry = ordenado[i];

                    Console.Write($" {i + 1,-4} {entry.Key,-27} ");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{entry.Value.Vitorias,-14}");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{entry.Value.Derrotas,-13}");
                    Console.ResetColor();
                }
            }

            UI.Linha();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
    }
}