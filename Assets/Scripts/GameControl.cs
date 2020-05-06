using System;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    [SerializeField] private Text txtJogadorVencedor; //Texto que indica qual jogador ganhou a partida

    [SerializeField] private Jogador[] jogadores; //Array de jogadores, caso queira mais de dois, é só por um novo icone e definir no gameControl (Dentro do unity)
    private int posicaoAnterior = 0;

    [SerializeField] public long vezJogador = 1L; //Define a vez de qual jogador, começando sempre pelo primeiro

    public int valorDado = 0; //Recebe o valor do dado para movimentação


    void Start () {
        //Define o jogador que irá começar o jogo
        SetaVezJogador();

        //Mantem o texto de "Jogador x venceu" até segunda chamada
        txtJogadorVencedor.gameObject.SetActive(false);
    }

    void Update(){

        //Se o valor do dado mudar de zero, inicia o método de movimentação do jogador
        if(valorDado != 0) {
            MovePlayer();
        }

        AtualizaDado();

    }

    //Permite a movimentação do jogador, além de saber se ele concluiu o caminho ou não
    private void MovePlayer() {
        Jogador jogador = JogadorDaVez();
        if (!jogador.movimentoPermitido && posicaoAnterior == 0) {
            posicaoAnterior = jogador.posicaoAtual;
        }

        if(jogador.posicaoAtual <= posicaoAnterior + valorDado) {
            jogador.movimentoPermitido = true;
        } else {
            jogador.movimentoPermitido = false;

            posicaoAnterior = 0;
            valorDado = 0;

            AtualizaVezJogador();
            SetaVezJogador();

        }
    }

    //Pega qual jogador está na vez de jogada
    private Jogador JogadorDaVez() {
        Jogador jogadorRetorno = null;
        foreach(Jogador jogador in jogadores) {
            if (jogador.idJogador.Equals(vezJogador)) {
                jogadorRetorno = jogador;
            }
        }
        return jogadorRetorno;
    }

    //Define a vez de jogada do jogador
    private void SetaVezJogador() {
        foreach (Jogador jogador in jogadores) {
            if (jogador.idJogador.Equals(vezJogador)) {
                jogador.vezJogador = true;
            } else {
                jogador.vezJogador = false;
            }
        }
    }

    //Vai para o próximo jogador poder jogar
    private void AtualizaVezJogador() {
        vezJogador++;
        if (vezJogador > jogadores.Length) {
            vezJogador = 1L;
        }
    }

    //Atualiza o valor do dado para o jogador poder continuar no tabuleiro depois de dar uma volta
    private void AtualizaDado() {
        Jogador jogador = JogadorDaVez();
        if(jogador.posicaoAtual == 0 && posicaoAnterior > 0) {
            valorDado -= ((jogador.waypoints.Length - 1) - posicaoAnterior);
            posicaoAnterior = jogador.posicaoAtual - 1;
        }
    }
}
