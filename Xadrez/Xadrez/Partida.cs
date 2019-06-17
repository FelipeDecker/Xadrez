using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.Tab;

namespace Xadrez.Xadrez
{
    public class Partida
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor CorAtual { get; private set; }
        public bool Terminada { get; set; }

        public Partida()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            CorAtual = Cor.Branco;
            Terminada = false;
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            Peca peca = Tabuleiro.RetirarPeca(posicaoOrigem);
            peca.IncrementarMovimento();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(posicaoDestino);
            Tabuleiro.ColocarPeca(peca, posicaoDestino);
        }

        public void RealizaJogada(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            ExecutarMovimento(posicaoOrigem, posicaoDestino);
            Turno++;
            MudaJogador();
        }

        public void ValidarOrigem(Posicao posicao)
        {
            if (Tabuleiro.Peca(posicao) == null)
            {
                throw new TabuleiroExeption("Não existe peça na posição de origem!");
            }
            if (CorAtual != Tabuleiro.Peca(posicao).Cor)
            {
                throw new TabuleiroExeption("A peça de origem não é sua!");
            }
            if (!Tabuleiro.Peca(posicao).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroExeption("Não existem movimentos possiveis para essa peça!");
            }
        }

        public void ValidarDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroExeption("Posição de destino invalida");
            }
        }

        private void MudaJogador()
        {
            if (CorAtual == Cor.Branco)
            {
                CorAtual = Cor.Preto;
            }
            else
            {
                CorAtual = Cor.Branco;
            }
        }

        private void  ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('C', 1).toPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('C', 2).toPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('D', 2).toPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('E', 1).toPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('E', 2).toPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Branco), new PosicaoXadrez('D', 1).toPosicao());

            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preto), new PosicaoXadrez('C', 7).toPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preto), new PosicaoXadrez('C', 8).toPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preto), new PosicaoXadrez('D', 7).toPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preto), new PosicaoXadrez('E', 7).toPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preto), new PosicaoXadrez('E', 8).toPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Preto), new PosicaoXadrez('D', 8).toPosicao());
        }
    }
}
