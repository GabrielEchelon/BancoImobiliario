using System.Collections;
using UnityEngine;

public class Dado : MonoBehaviour {

    private static GameObject dado1, dado2;
    private Sprite[] dadoFace;
    private SpriteRenderer render1, render2;
    private int vezJogador = 1;
    private bool coroutineDisponivel = true;

	private void Start () {
        //Carrega os arquivo da pasta dadoFace
        dadoFace = Resources.LoadAll<Sprite>("dadoFace/"); 

        //Encontra os objetos "Dado1" e "Dado2"
        dado1 = GameObject.Find("Dado1"); 
        dado2 = GameObject.Find("Dado2"); 

        //Carrega os componentes dos dados
        render1 = dado1.GetComponent<SpriteRenderer>();
        render2 = dado2.GetComponent<SpriteRenderer>(); 

        //Inicia os dados na face de número 6
        render1.sprite = dadoFace[5]; 
        render2.sprite = dadoFace[5];
	}

    //Método de quando o mouse clicar no dado
    private void OnMouseDown(){
        if (coroutineDisponivel)
            // Inicia a rotina "RolarDado"
            StartCoroutine("RolarDado"); 
    }

    //Rotina de rolar o dado
    private IEnumerator RolarDado(){
        //Comando que da "lock" para não ser executado várias vezes ao mesmo tempo
        coroutineDisponivel = false; 

        int faceDadoRandom1 = 0;
        int faceDadoRandom2 = 0;

        //Roda 20x o dado, mantendo a face virada para cima por 0.05 segundos
        for (int i = 0; i <= 20; i++){ 
            faceDadoRandom1 = Random.Range(0, 6);
            render1.sprite = dadoFace[faceDadoRandom1];

            faceDadoRandom2 = Random.Range(0, 6);
            render2.sprite = dadoFace[faceDadoRandom2];

            yield return new WaitForSeconds(0.05f);
        }

        GameControl.valorDado = (faceDadoRandom1 + 1) + (faceDadoRandom2 + 1);
        if(vezJogador == 1){
            GameControl.MovePlayer(1);
        } else if(vezJogador == -1){
            GameControl.MovePlayer(2);
        }
        
        vezJogador *= -1;
        coroutineDisponivel = true;
    }
}
