namespace Xadrez.Tab
{
    public abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int Movimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            Movimentos = 0;
        }

        public void IncrementarMovimento()
        {
            Movimentos++;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();

            for (int l = 0; l < Tabuleiro.Linha; l++)
            {
                for (int c = 0; c < Tabuleiro.Coluna; c++)
                {
                    if (mat[l, c] )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool PodeMoverPara(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
