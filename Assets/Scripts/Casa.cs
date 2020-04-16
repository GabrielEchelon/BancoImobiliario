using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casa : MonoBehaviour
{

    [SerializeField] private int numero = 0;
    [SerializeField] private float valor = 0.0f;
    [SerializeField] private string nome = "";
    [SerializeField] private int dono = 0;
    [SerializeField] private bool compravel = true;
    [SerializeField] private bool empresa = false;
    [SerializeField] private bool sorteReves = false;

    private Jogador player1Mov;
    private Jogador player2Mov;

    private static GameObject player1, player2;
    
    void Start(){

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        
    }

    void Update(){

        player1Mov = player1.GetComponent<Jogador>();
        player2Mov = player2.GetComponent<Jogador>();

        if((player1Mov.posicaoAtual == numero && player1Mov.movimentoPermitido == false) 
        || (player2Mov.posicaoAtual == numero && player2Mov.movimentoPermitido == false)){
            Debug.Log(nome + ", " + numero + ", valor R$" + valor + ". Dono: " + dono);
            Debug.Log("Empresa: " + empresa + ". " + "Compravel: " + compravel + ". Sorte Ou Revés: " + sorteReves);
        }
    }
}
