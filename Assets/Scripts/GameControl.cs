using System;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    [SerializeField] private Text txtJogadorVencedor; //Texto que indica qual jogador ganhou a partida

    [SerializeField] private Prisao prisao; //Define as configuracoes da prisão

    [SerializeField] private Jogador[] jogadores; //Array de jogadores, caso queira mais de dois, é só por um novo icone e definir no gameControl (Dentro do unity)
    private int posicaoAnterior = 0;

    [SerializeField] public long vezJogador = 1L; //Define a vez de qual jogador, começando sempre pelo primeiro

    public int valorDado = 0; //Recebe o valor do dado para movimentação

    public bool dadosIguais = false; //Se o jogador tirar dados iguais, jogará novamente
    public int dadosPrisao = 0; //Se o jogador tirar dados iguais três vezes seguidas, jogador estará preso


    void Start() {
        //Define o jogador que irá começar o jogo
        SetaVezJogador();

        //Verificação necessária para a prisão não ficar com um jogador null
        prisao.jogadorDaVez = JogadorDaVez();

        //Mantem o texto de "Jogador x venceu" até segunda chamada
        txtJogadorVencedor.gameObject.SetActive(false);
    }

    void Update() {

        if (valorDado != 0) {
            VerificaPrisao(JogadorDaVez());
        }

        if (valorDado != 0) {
            MovePlayer();
        }

        AtualizaDado();

    }

    public void VerificaPrisao(Jogador jogador) {
        if (!jogador.preso) {
            if ((jogador.posicaoAtual == prisao.posicaoVaParaPrisao && !jogador.movimentoPermitido) || (dadosPrisao >= 3)) {
                jogador.preso = true;
                prisao.jogadorDaVez = jogador;
                dadosIguais = false;
                dadosPrisao = 0;
                FinalizaVezJogadorPreso(jogador);
            }
        } else {
            FinalizaVezJogadorPreso(jogador);
        }
    }

    //Inicia a movimentação do jogador, além de saber se ele concluiu o caminho ou não
    private void MovePlayer() {
        Jogador jogador = JogadorDaVez();
        if (!jogador.movimentoPermitido && posicaoAnterior == 0) {
            posicaoAnterior = jogador.posicaoAtual;
        }

        if (jogador.posicaoAtual <= posicaoAnterior + valorDado) {
            jogador.movimentoPermitido = true;
        } else {
            if (jogador.posicaoAtual != 0) {
                jogador.posicaoAtual -= 1;
            }
            FinalizaVezJogador(jogador);
        }
    }

    private void FinalizaVezJogador(Jogador jogador) {
        jogador.movimentoPermitido = false;

        posicaoAnterior = 0;
        valorDado = 0;

        if (!dadosIguais) {
            AtualizaVezJogador();
            SetaVezJogador();
        }
    }

    private void FinalizaVezJogadorPreso(Jogador jogador) {

        if (dadosIguais) {
            jogador.preso = false;
            prisao.jogadorDaVez = jogador;
            dadosIguais = false;
        } else {
            jogador.movimentoPermitido = false;

            posicaoAnterior = 0;
            valorDado = 0;

            AtualizaVezJogador();
            SetaVezJogador();
        }
    }

    //Pega qual jogador está na vez de jogada
    public Jogador JogadorDaVez() {
        return Array.Find(jogadores, j => j.vezJogador);
    }

    //Define a vez de jogada do jogador
    private void SetaVezJogador() {
        foreach (Jogador jogador in jogadores) {
            jogador.vezJogador = (jogador.idJogador.Equals(vezJogador));
        }
    }

    //Vai para o próximo jogador poder jogar
    private void AtualizaVezJogador() {
        vezJogador++;
        dadosPrisao = 0;
        if (vezJogador > jogadores.Length) {
            vezJogador = 1L;
        }
    }

    //Atualiza o valor do dado para o jogador poder continuar no tabuleiro depois de dar uma volta
    private void AtualizaDado() {
        Jogador jogador = JogadorDaVez();
        if (jogador.posicaoAtual == 0 && posicaoAnterior > 0) {
            valorDado -= ((jogador.waypoints.Length - 1) - posicaoAnterior);
            posicaoAnterior = jogador.posicaoAtual - 1;
        }
    }
}