using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControl : MonoBehaviour {

    [SerializeField] private Prisao prisao; 

    [SerializeField] private Jogador[] jogadores; 
    private int posicaoAnterior = 0;

    [SerializeField] public long vezJogador = 1L; 

    public int valorDado = 0; 

    public bool dadosIguais = false;
    public bool dadosRodados = false;
    public int dadosPrisao = 0; 

    [SerializeField] public Button botaoEncerrarVez;
    [SerializeField] private TextMeshProUGUI txtToolTip;

    void Start() {
        SetaVezJogador();

        prisao.jogadorDaVez = JogadorDaVez();

    }

    void Update() {
        if (valorDado != 0) { 
            VerificaPrisao();
        }

        if (valorDado != 0) {
            MovePlayer();
        }

        AtualizaDado();

    }

    public void VerificaPrisao() {
        Jogador jogador = JogadorDaVez();
        if (!jogador.preso) {
            if ((jogador.posicaoAtual == prisao.posicaoVaParaPrisao && !jogador.movimentoPermitido) || (dadosPrisao >= 3)) {
                jogador.preso = true;
                jogador.movimentoPermitido = false;

                prisao.jogadorDaVez = jogador;
                dadosIguais = false;
                dadosPrisao = 0;

                FinalizaVezJogadorPreso(jogador);
            }
        } else {
            FinalizaVezJogadorPreso(jogador);
        }
    }

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
            ResetaJogador(jogador);
        }
    }

    private void ResetaJogador(Jogador jogador) {
        jogador.movimentoPermitido = false;

        posicaoAnterior = 0;
        valorDado = 0;
        botaoEncerrarVez.interactable = true;
    }

    public void FinalizaVezJogador() {

        AtualizaVezJogador();
        SetaVezJogador();

        botaoEncerrarVez.interactable = false;
        dadosRodados = false;
    }

    private void FinalizaVezJogadorPreso(Jogador jogador) {
        if (dadosIguais) {
            jogador.preso = false;
            prisao.jogadorDaVez = jogador;
            dadosIguais = false;
        } else {
            ResetaJogador(jogador);
            FinalizaVezJogador();
        }
    }

    public Jogador JogadorDaVez() {
        return Array.Find(jogadores, j => j.vezJogador);
    }

    private void SetaVezJogador() {
        foreach (Jogador jogador in jogadores) {
            jogador.vezJogador = (jogador.idJogador.Equals(vezJogador));
        }
    }

    public Jogador RetornaJogadorPorId(long idJogador) {
        return Array.Find(jogadores, j => j.idJogador.Equals(idJogador));
    }

    private void AtualizaVezJogador() {
        vezJogador++;
        dadosPrisao = 0;
        if (vezJogador > jogadores.Length) {
            vezJogador = 1L;
        }
    }

    private void AtualizaDado() {
        Jogador jogador = JogadorDaVez();
        if (jogador.posicaoAtual == 0 && posicaoAnterior > 0) {
            valorDado -= ((jogador.waypoints.Length - 1) - posicaoAnterior);
            posicaoAnterior = jogador.posicaoAtual - 1;
        }
    }
}