using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saldo : MonoBehaviour {

    private static GameObject txtSaldoPlayer1, txtSaldoPlayer2;
    private static GameObject txtPatrimonioPlayer1, txtPatrimonioPlayer2;

    [SerializeField] public double saldoPlayer1 = 1500000d;
    [SerializeField] public double saldoPlayer2 = 1500000d;

    [SerializeField] public double patrimonioPlayer1 = 0d;
    [SerializeField] public double patrimonioPlayer2 = 0d;

    void Start(){
        //Encontra os objetos de saldo
        txtSaldoPlayer1 = GameObject.Find("SaldoPlayer1");
        txtSaldoPlayer2 = GameObject.Find("SaldoPlayer2");

        //Encontra os objetos de patrimonio
        txtPatrimonioPlayer1 = GameObject.Find("PatrimonioPlayer1");
        txtPatrimonioPlayer2 = GameObject.Find("PatrimonioPlayer2");

    }

    void Update(){
        exibeSaldos();
        exibePatrimonios();
    }

    //Exibe o valor dos saldos na tela
    private void exibeSaldos() {
        txtSaldoPlayer1.GetComponent<Text>().text = "R$ " + saldoPlayer1.ToString("#,#");
        txtSaldoPlayer2.GetComponent<Text>().text = "R$ " + saldoPlayer2.ToString("#,#");
    }

    //Exibe o valor dos patrimônios na tela
    private void exibePatrimonios() {
        if (patrimonioPlayer1 > 0d) {
            txtPatrimonioPlayer1.GetComponent<Text>().text = "R$ " + saldoPlayer1.ToString("#,#");
        }

        if (patrimonioPlayer2 > 0d) {
            txtPatrimonioPlayer2.GetComponent<Text>().text = "R$ " + saldoPlayer2.ToString("#,#");
        }
    }
}
