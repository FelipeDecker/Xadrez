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
        public bool Xeque { get; private set; }

        public Partida()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            CorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            Peca peca = Tabuleiro.RetirarPeca(posicaoOrigem);
            peca.IncrementarMovimento();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(posicaoDestino);
            Tabuleiro.ColocarPeca(peca, posicaoDestino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void DesfazerMovimento(Posicao posicaoOrigem, Posicao posicaoDestino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetirarPeca(posicaoDestino);
            peca.DecrementarMovimento();
            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, posicaoDestino);
                Capturadas.Remove(pecaCapturada);
            }

            Tabuleiro.ColocarPeca(peca, posicaoOrigem);
        }

        public void RealizaJogada(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            Peca pecaCapturada = ExecutarMovimento(posicaoOrigem, posicaoDestino);

            if (EstaEmXeque(CorAtual))
            {
                DesfazerMovimento(posicaoOrigem, posicaoDestino, pecaCapturada);
                throw new TabuleiroExeption("Não pode se colocar em xeque!");
            }

            if (EstaEmXeque(CorAdversaria(CorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (EstaEmXequeMate(CorAdversaria(CorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
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
            if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
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

        private Cor CorAdversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }

        private Peca PecaRei(Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }
            }

            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = PecaRei(cor);

            if (R == null)
            {
                throw new TabuleiroExeption("Não tem rei da cor "+ cor +" no tabuleiro");
            }

            foreach (Peca peca in PecasEmJogo(CorAdversaria(cor)))
            {
                bool[,] mat = peca.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool EstaEmXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] mat = peca.MovimentosPossiveis();

                for (int l = 0; l < Tabuleiro.Linha; l++)
                {
                    for (int c = 0; c < Tabuleiro.Coluna; c++)
                    {
                        if (mat[l, c])
                        {
                            Posicao oreigem = peca.Posicao;
                            Posicao destino = new Posicao(l, c);
                            Peca pecaCapturada = ExecutarMovimento(oreigem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazerMovimento(oreigem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            var colunaUp = coluna.ToString().ToUpper().ToCharArray();
            char novaColuna = colunaUp[0];
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(novaColuna, linha).toPosicao());
            Pecas.Add(peca);
        }

        private void  ColocarPecas()
        {
            ColocarNovaPeca('A', 1, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('B', 1, new Cavalo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('C', 1, new Bispo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('D', 1, new Dama(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('E', 1, new Rei(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('F', 1, new Bispo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('G', 1, new Cavalo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('H', 1, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('A', 2, new Peao(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('B', 2, new Peao(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('C', 2, new Peao(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('D', 2, new Peao(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('E', 2, new Peao(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('F', 2, new Peao(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('G', 2, new Peao(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('H', 2, new Peao(Tabuleiro, Cor.Branco));

            ColocarNovaPeca('A', 8, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('B', 8, new Cavalo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('C', 8, new Bispo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('D', 8, new Dama(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('E', 8, new Rei(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('F', 8, new Bispo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('G', 8, new Cavalo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('H', 8, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('A', 7, new Peao(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('B', 7, new Peao(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('C', 7, new Peao(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('D', 7, new Peao(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('E', 7, new Peao(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('F', 7, new Peao(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('G', 7, new Peao(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('H', 7, new Peao(Tabuleiro, Cor.Preto));
        }
    }
}
