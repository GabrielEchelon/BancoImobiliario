using System.Collections;
using UnityEngine;

public class Dado : MonoBehaviour {

    private Sprite[] dadoFace;
    private SpriteRenderer render1, render2;
    private bool coroutineDisponivel = true;

    [SerializeField] private GameControl gameControl;

	private void Start () {
        dadoFace = Resources.LoadAll<Sprite>("dadoFace/"); 

        render1 = GameObject.Find("Dado1").GetComponent<SpriteRenderer>();
        render2 = GameObject.Find("Dado2").GetComponent<SpriteRenderer>(); 

        render1.sprite = dadoFace[5]; 
        render2.sprite = dadoFace[5];
	}

    private void OnMouseDown(){
        if (coroutineDisponivel && !gameControl.dadosRodados)
            StartCoroutine(RolarDado());
    }

    private IEnumerator RolarDado(){
        coroutineDisponivel = false;
        gameControl.dadosRodados = true;

        int faceDadoRandom1 = 0;
        int faceDadoRandom2 = 0;

        for (int i = 0; i <= 20; i++){ 
            faceDadoRandom1 = Random.Range(0, 6);
            render1.sprite = dadoFace[faceDadoRandom1];

            faceDadoRandom2 = Random.Range(0, 6);
            render2.sprite = dadoFace[faceDadoRandom2];

            yield return new WaitForSeconds(0.05f);
        }
        //VerificaDados(4, 4);
        VerificaDados((faceDadoRandom1 + 1), (faceDadoRandom2 + 1));
        gameControl.valorDado = (faceDadoRandom1 + 1) + (faceDadoRandom2 + 1);
        //gameControl.valorDado = 10;

        coroutineDisponivel = true;
    }

    private void VerificaDados(int valorDado1, int valorDado2) {
        if(valorDado1 == valorDado2) {
            gameControl.dadosIguais = true;
            gameControl.dadosPrisao += 1;
        } else {
            gameControl.dadosIguais = false;
            gameControl.dadosPrisao = 0;
        }   
    }
}
