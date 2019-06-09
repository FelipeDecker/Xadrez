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
                    Console.Clear();

                    Tela.ImprimirTabuleiro(partida.Tabuleiro);

                    Console.WriteLine();
                    Console.Write("Digite a origem: ");
                    Posicao origem = Tela.LerPosicao().toPosicao();
                    Console.Write("Digite o destino: ");
                    Posicao destino = Tela.LerPosicao().toPosicao();


                    partida.ExecutarMovimento(origem, destino);
                }

                Tela.ImprimirTabuleiro(partida.Tabuleiro);

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
