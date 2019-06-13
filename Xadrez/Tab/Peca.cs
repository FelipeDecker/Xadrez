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

        public abstract bool[,] MovimentosPossiveis();
    }
}
