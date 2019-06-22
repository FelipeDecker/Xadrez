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
                Partida partida = new Partida();

                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Digite a origem: ");
                        Posicao origem = Tela.LerPosicao().toPosicao();
                        partida.ValidarOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tabuleiro.Peca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Digite o destino: ");
                        Posicao destino = Tela.LerPosicao().toPosicao();
                        partida.ValidarDestino(origem, destino);


                        partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroExeption ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Tela.ImprimirPartida(partida);
            }
            catch (TabuleiroExeption ex)
            {
                Console.WriteLine(ex.Message); 
            }

            Console.ReadLine();
        }
    }
}
