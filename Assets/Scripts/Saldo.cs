using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Saldo : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI txtSaldoPlayer; //Componente que irá exibir o saldo do jogador
    [SerializeField] private TextMeshProUGUI txtPatrimonioPlayer; //Componente que irá exibir o patrimonio do jogador
    [SerializeField] private TextMeshProUGUI txtPatrimonioIdent; //Texto que identifica o patrimônio

    [SerializeField] public float saldoPlayer = 0f; //Saldo do player
    [SerializeField] public float patrimonioPlayer = 0f; //Patrimonio do player
    [SerializeField] public float valorDebitoCredito = 0f; //Valor que irá alterar o saldo do player
    [HideInInspector] public long idJogador = 0L; //Identificação do jogador

    private bool coroutineSaldo = true; //Define se é para atualizar o saldo

    void Start(){

        //Previne que o saldo e patrimonio comecem negativos
        if(saldoPlayer <= 0f) {
            saldoPlayer = 1500000f;
        }
        if(patrimonioPlayer < 0f) {
            patrimonioPlayer = 0f;
        }
    }

    void Update(){
        //Atualiza os saldos conforme o id do Jogador
        if (idJogador != 0L) {
            ExibeSaldos();
            ExibePatrimonios();

            if (coroutineSaldo && valorDebitoCredito != 0f) {
                StartCoroutine(AtualizaSaldos(valorDebitoCredito));
            }

        }
    }

    //Exibe o valor dos saldos na tela
    private void ExibeSaldos() {
        txtSaldoPlayer.text = "R$ " + saldoPlayer.ToString("#,#");
    }

    //Exibe o valor dos patrimônios na tela
    private void ExibePatrimonios() {
        if (patrimonioPlayer > 0d) {
            txtPatrimonioPlayer.gameObject.SetActive(true);
            txtPatrimonioIdent.gameObject.SetActive(true);
            txtPatrimonioPlayer.text = "R$ " + saldoPlayer.ToString("#,#");
        } else {
            txtPatrimonioPlayer.gameObject.SetActive(false);
            txtPatrimonioIdent.gameObject.SetActive(false);
        }
    }

    //Coroutine que atualiza o valor do saldo e realiza animação dos números
    public IEnumerator AtualizaSaldos(float valorAlteracao) {

        coroutineSaldo = false; 

        float valorDesejado = saldoPlayer + valorAlteracao;

        if (saldoPlayer != valorDesejado) {
            if(saldoPlayer < valorDesejado) {
                for (float i = saldoPlayer; i < valorDesejado; i = saldoPlayer) {
                    saldoPlayer += 1000.0f;
                    ExibeSaldos();

                    yield return new WaitForSecondsRealtime(0.00005f);

                }
            } else {
                for (float i = saldoPlayer; i > valorDesejado; i = saldoPlayer) {
                    saldoPlayer -= 1000.0f;
                    ExibeSaldos();

                    yield return new WaitForSeconds(0.00005f);

                }
            }
        }

        valorDebitoCredito = 0f;
        coroutineSaldo = true;
    }
}
