using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour {

    //Variaveis de Movimentação
    [SerializeField] private long idJogador;
    [SerializeField] public float velocidadeMovimento = 5f; //Velocidade padrão de movimentação
    [SerializeField] public int posicaoAtual = 0; //Casa do jogador
    public Transform[] waypoints; //Define quantidade de casas do tabuleiro
    public bool movimentoPermitido = false; //Define se o jogar esta na sua vez ou não

    public bool preso = false;

	private void Start () {
        //Pega a posição da casa que está definida no index
        transform.position = waypoints[posicaoAtual].transform.position;
    }
	 
	private void Update () {
        if (movimentoPermitido){ 
            Move();
        }

        if(!movimentoPermitido && posicaoAtual == 30) {
            preso = true;
        }
    }

    //Método que irá mover o jogador para a casa
    private void Move(){
        int tamanhoTabuleiro = waypoints.Length - 1;

        if (posicaoAtual > tamanhoTabuleiro)
            posicaoAtual = 0;

        transform.position = Vector2.MoveTowards(transform.position,
                                                    waypoints[posicaoAtual].transform.position,
                                                    velocidadeMovimento * Time.deltaTime);

        if (transform.position == waypoints[posicaoAtual].transform.position)
            posicaoAtual += 1;
    }
}
