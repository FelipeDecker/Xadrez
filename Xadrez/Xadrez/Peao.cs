using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.Tab;

namespace Xadrez.Xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return Tabuleiro.Peca(posicao) != null && peca.Cor != Cor;
        }

        public bool Livre(Posicao posicao)
        {
            return Tabuleiro.Peca(posicao) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Branco)
            {
                Posicao.DefinirValor(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    mat[Posicao.Linha, Posicao.Coluna] = true;
                }

                Posicao.DefinirValor(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && Movimentos == 0)
                {
                    mat[Posicao.Linha, Posicao.Coluna] = true;
                }

                Posicao.DefinirValor(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    mat[Posicao.Linha, Posicao.Coluna] = true;
                }

                Posicao.DefinirValor(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    mat[Posicao.Linha, Posicao.Coluna] = true;
                }
            }
            else if (Cor == Cor.Preto)
            {
                Posicao.DefinirValor(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    mat[Posicao.Linha, Posicao.Coluna] = true;
                }

                Posicao.DefinirValor(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && Movimentos == 0)
                {
                    mat[Posicao.Linha, Posicao.Coluna] = true;
                }

                Posicao.DefinirValor(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    mat[Posicao.Linha, Posicao.Coluna] = true;
                }

                Posicao.DefinirValor(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    mat[Posicao.Linha, Posicao.Coluna] = true;
                }
            }
            else
            {
                throw new TabuleiroExeption("Não foi possivel identificar a cor da peca");
            }

            return mat;
        }
    }
}
