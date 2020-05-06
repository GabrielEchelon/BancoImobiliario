using System.Collections;
using UnityEngine;

public class Dado : MonoBehaviour {

    private Sprite[] dadoFace;
    private SpriteRenderer render1, render2;
    private bool coroutineDisponivel = true;
    private int dadosIguais = 0;

    [SerializeField] private GameControl gameControl;

	private void Start () {
        //Carrega os arquivo da pasta dadoFace
        dadoFace = Resources.LoadAll<Sprite>("dadoFace/"); 

        //Carrega os componentes dos dados
        render1 = GameObject.Find("Dado1").GetComponent<SpriteRenderer>();
        render2 = GameObject.Find("Dado2").GetComponent<SpriteRenderer>(); 

        //Inicia os dados na face de número 6
        render1.sprite = dadoFace[5]; 
        render2.sprite = dadoFace[5];
	}

    //Método de quando o mouse clicar no dado
    private void OnMouseDown(){
        // Inicia a rotina "RolarDado"
        if (coroutineDisponivel)
            StartCoroutine(RolarDado());
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

        gameControl.valorDado = (faceDadoRandom1 + 1) + (faceDadoRandom2 + 1);
        VerificaDados((faceDadoRandom1 + 1), (faceDadoRandom2 + 1));

        coroutineDisponivel = true;
    }

    //Verifica quantas vezes os dados ficaram iguais seguidos
    private void VerificaDados(int valorDado1, int valorDado2) {
        if(valorDado1 == valorDado2) {
            dadosIguais++;
        }else {
            dadosIguais = 0;
        }

        if(dadosIguais >= 3) {

        }
    }
}
