using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace jogo_forca {
    public static class Arquivo {

        // Lê o arquivo .txt, ignora linhas vazias e retorna as palavras em maiúsculo
        public static List<string> CarregarPalavras(string caminho) {

            // Se o arquivo não existir, retorna uma lista vazia
            if (!File.Exists(caminho))
                return new List<string>();

            return File.ReadAllLines(caminho)
                       .Where(l => !string.IsNullOrWhiteSpace(l))
                       .Select(l => l.Trim().ToUpper())
                       .ToList();
        }
    }
}