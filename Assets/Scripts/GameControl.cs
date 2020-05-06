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
        SetaVezJogador();

        txtJogadorVencedor.gameObject.SetActive(false);
    }

    void Update(){

        if(valorDado != 0) {
            MovePlayer();
        }

        AtualizaDado();

    }

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

    private Jogador JogadorDaVez() {
        Jogador jogadorRetorno = null;
        foreach(Jogador jogador in jogadores) {
            if (jogador.idJogador.Equals(vezJogador)) {
                jogadorRetorno = jogador;
            }
        }
        return jogadorRetorno;
    }

    private void SetaVezJogador() {
        foreach (Jogador jogador in jogadores) {
            if (jogador.idJogador.Equals(vezJogador)) {
                jogador.vezJogador = true;
            } else {
                jogador.vezJogador = false;
            }
        }
    }

    private void AtualizaVezJogador() {
        vezJogador++;
        if (vezJogador > jogadores.Length) {
            vezJogador = 1L;
        }
    }

    private void AtualizaDado() {
        Jogador jogador = JogadorDaVez();
        if(jogador.posicaoAtual == 0 && posicaoAnterior > 0) {
            valorDado -= ((jogador.waypoints.Length - 1) - posicaoAnterior);
            posicaoAnterior = jogador.posicaoAtual - 1;
        }
    }
}
