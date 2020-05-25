using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Saldo : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI txtSaldoPlayer; 

    [SerializeField] private float saldoPlayer = 0f; 
    [SerializeField] public float valorDebitoCredito = 0f; 
    [HideInInspector] public long idJogador = 0L; 

    private bool coroutineSaldo = true; 

    void Start(){
        if(saldoPlayer <= 0f) {
            saldoPlayer = 1500000f;
        }
    }

    void Update(){
        if (idJogador != 0L) {
            ExibeSaldos();

            if (coroutineSaldo && valorDebitoCredito != 0f) {
                StartCoroutine(AtualizaSaldos(valorDebitoCredito));
            }

        }
    }

    public float getSaldo() {
        return saldoPlayer;
    }

    private void ExibeSaldos() {
        txtSaldoPlayer.text = "R$ " + saldoPlayer.ToString("#,#");
    }

    public IEnumerator AtualizaSaldos(float valorAlteracao) {

        coroutineSaldo = false; 

        float valorDesejado = saldoPlayer + valorAlteracao;

        if (saldoPlayer != valorDesejado) {
            if(saldoPlayer < valorDesejado) {
                for (float i = saldoPlayer; i < valorDesejado; i = saldoPlayer) {
                    saldoPlayer += 500.0f;
                    ExibeSaldos();

                    yield return new WaitForSecondsRealtime(0.00005f);

                }
            } else {
                for (float i = saldoPlayer; i > valorDesejado; i = saldoPlayer) {
                    saldoPlayer -= 500.0f;
                    ExibeSaldos();

                    yield return new WaitForSeconds(0.00005f);

                }
            }
        }

        valorDebitoCredito = 0f;
        coroutineSaldo = true;
    }
}
