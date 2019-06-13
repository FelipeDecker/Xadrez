﻿using System;
using Xadrez.Tab;
using Xadrez.Xadrez;

namespace Xadrez
{
    public class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int l = 0; l < tabuleiro.Linha; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < tabuleiro.Coluna; c++)
                {
                    ImprimirPeca(tabuleiro.Peca(l, c));
                }

                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int l = 0; l < tabuleiro.Linha; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < tabuleiro.Coluna; c++)
                {
                    if (posicoesPossiveis[l, c])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    ImprimirPeca(tabuleiro.Peca(l, c));

                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = fundoOriginal;

        }

        public static PosicaoXadrez LerPosicao()
        {
            string posicaoDigitada = Console.ReadLine();

            posicaoDigitada.Split();

            string digitado = posicaoDigitada[0].ToString().ToUpper();

            char coluna = digitado[0];

            int linha = int.Parse(posicaoDigitada[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }

                Console.Write(" ");
            }
        }
    }
}
