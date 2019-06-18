using System.Collections.Generic;
using Xadrez.Tab;

namespace Xadrez.Xadrez
{
    public class Partida
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor CorAtual { get; private set; }
        public bool Terminada { get; set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public Partida()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            CorAtual = Cor.Branco;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            Peca peca = Tabuleiro.RetirarPeca(posicaoOrigem);
            peca.IncrementarMovimento();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(posicaoDestino);
            Tabuleiro.ColocarPeca(peca, posicaoDestino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
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

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        private void  ColocarPecas()
        {
            ColocarNovaPeca('C', 1, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('C', 2, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('D', 2, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('E', 1, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('E', 2, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('D', 1, new Rei(Tabuleiro, Cor.Branco));

            ColocarNovaPeca('C', 8, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('C', 7, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('D', 7, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('E', 8, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('E', 7, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('D', 8, new Rei(Tabuleiro, Cor.Preto));
        }
    }
}
