using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace jogo_forca {
    public static class Arquivo {

        // Lê o arquivo .txt, ignora linhas vazias e retorna as palavras em maiúsculo
        public static List<string> CarregarPalavras(string caminho) {

            // Eetorna uma lista vazia
            if (!File.Exists(caminho)) {
                return new List<string>();
            }

            return File.ReadAllLines(caminho)
                       .Where(l => !string.IsNullOrWhiteSpace(l))
                       .Select(l => l.Trim().ToUpper())
                       .ToList();
        }

        // Monta o caminho absoluto na raiz do projeto
        private static string CaminhoRanking() {
            string raiz = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\"));
            return Path.Combine(raiz, "Data", "ranking.txt");
        }

        // Lê o arquivo de ranking e retorna um dicionário
        public static Dictionary<string, (int Vitorias, int Derrotas)> CarregarRanking(string caminho) {
            var ranking = new Dictionary<string, (int, int)>();

            string caminhoAbsoluto = CaminhoRanking();

            if (!File.Exists(caminhoAbsoluto)) {
                return ranking;
            }


            foreach (string linha in File.ReadAllLines(caminhoAbsoluto)) {
                string[] partes = linha.Split(';'); // Separa por ';'

                if (partes.Length == 3 && int.TryParse(partes[1], out int v) && int.TryParse(partes[2], out int d))
                {
                    ranking[partes[0].Trim()] = (v, d);
                }

            }

            return ranking;
        }

        // Salva o dicionário de ranking no arquivo, sobrescrevendo o conteúdo anterior
        public static void SalvarRanking(string caminho, Dictionary<string, (int Vitorias, int Derrotas)> ranking) {

            string caminhoAbsoluto = CaminhoRanking();
            string diretorio = Path.GetDirectoryName(caminhoAbsoluto);

            if (!string.IsNullOrEmpty(diretorio)) {
                Directory.CreateDirectory(diretorio);
            }

            // Escreve cada entrada no formato: NOME;VITORIAS;DERROTAS
            File.WriteAllLines(caminhoAbsoluto, ranking.Select(r => $"{r.Key};{r.Value.Vitorias};{r.Value.Derrotas}"));
        }
    }
}