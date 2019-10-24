using Xadrez.Tab;

namespace Xadrez.Xadrez
{
    public class Rei : Peca
    {
        private Partida Partida { get; set; }

        public Rei(Tabuleiro tabuleiro, Cor cor, Partida partida) : base (tabuleiro, cor)
        {
            Partida = partida;
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        private bool testeTorreRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca is Torre && peca.Cor == Cor && peca.Movimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            Posicao posicao = new Posicao(0, 0);

            //acima

            posicao.DefinirValor(Posicao.Linha - 1, Posicao.Coluna);

            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //nordeste

            posicao.DefinirValor(Posicao.Linha - 1, Posicao.Coluna + 1);

            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //direita

            posicao.DefinirValor(Posicao.Linha, Posicao.Coluna + 1);

            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //suldeste

            posicao.DefinirValor(Posicao.Linha + 1, Posicao.Coluna + 1);

            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //abaixo

            posicao.DefinirValor(Posicao.Linha + 1, Posicao.Coluna);

            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //suldoeste

            posicao.DefinirValor(Posicao.Linha + 1, Posicao.Coluna - 1);

            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //esquerda

            posicao.DefinirValor(Posicao.Linha, Posicao.Coluna - 1);

            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //noroeste

            posicao.DefinirValor(Posicao.Linha - 1, Posicao.Coluna - 1);

            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //Jogada Especial - Roque

            if (Movimentos == 0 && !Partida.Xeque)
            {
                //Roque pequeno

                Posicao posicaoTorre1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (testeTorreRoque(posicaoTorre1)) 
                {
                    Posicao posicao1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao posicao2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if (Tabuleiro.Peca(posicao1) == null  && Tabuleiro.Peca(posicao2) == null )
                    {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true; 
                    }
                }

                //Roque grande

                Posicao posicaoTorre2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (testeTorreRoque(posicaoTorre2))
                {
                    Posicao posicao1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao posicao2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao posicao3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

                    if (Tabuleiro.Peca(posicao1) == null && Tabuleiro.Peca(posicao2) == null && Tabuleiro.Peca(posicao3) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
