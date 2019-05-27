using System;
using Xadrez.Tab;

namespace Xadrez
{
    public class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int l = 0; l < tabuleiro.Linha; l++)
            {
                for (int c = 0; c < tabuleiro.Coluna; c++)
                {
                    if (tabuleiro.Peca(l, c) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tabuleiro.Peca(l, c) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
