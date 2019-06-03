using System;
using Xadrez.Tab;
using Xadrez.Xadrez;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tabuleiro = new Tabuleiro(8, 8);

                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preto), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preto), new Posicao(1, 5));
                tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preto), new Posicao(0, 4));

                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branco), new Posicao(2, 4));
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branco), new Posicao(3, 4));
                tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Branco), new Posicao(6, 4));


                Tela.ImprimirTabuleiro(tabuleiro);

                Console.ReadLine();
            }
            catch (Exception e )
            {
                Console.WriteLine(e.Message); 
            }

            Console.ReadLine();
        }
    }
}
