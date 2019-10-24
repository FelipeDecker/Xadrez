using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.Tab;

namespace Xadrez.Xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "C";
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            Posicao posicao = new Posicao(0, 0);

            // superior esquerda

            posicao.DefinirValor(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            // superior direita

            posicao.DefinirValor(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            // inferior esquerda

            posicao.DefinirValor(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            // inferior direita

            posicao.DefinirValor(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            // superior esquerda s

            posicao.DefinirValor(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            // superior direita s 

            posicao.DefinirValor(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            // inferior esquerda

            posicao.DefinirValor(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            // inferior direita

            posicao.DefinirValor(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            return mat;
        }
    }
}
