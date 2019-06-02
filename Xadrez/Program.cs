using System;
using Xadrez.Tab;
using Xadrez.Xadrez;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez posicaoXadrez = new PosicaoXadrez('C', 7);

            Console.WriteLine(posicaoXadrez);

            Console.WriteLine(posicaoXadrez.toPosicao());

            Console.ReadLine();
        }
    }
}
