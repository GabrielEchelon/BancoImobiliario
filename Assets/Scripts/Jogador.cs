using System.Globalization;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour {

    //Variaveis de Movimentação
    [SerializeField] public long idJogador = 0;//Identificação do Jogador
    public Transform[] waypoints; //Define quantidade de casas do tabuleiro

    [SerializeField] public float velocidadeMovimento = 5f; //Velocidade padrão de movimentação
    public bool movimentoPermitido = false;

    [SerializeField] public int posicaoAtual = 0; //Casa atual do jogador
    [SerializeField] public int ultimaPosicao = 0; //Casa anterior que o jogador estava


    [SerializeField] public Text txtVezJogador; //Texto que indica se é a vez do jogador
    public bool vezJogador = false; //Define se o jogar esta na sua vez ou não

    public bool preso = false; //Identifica se esta preso ou não

    private Saldo saldo;

    private void Start () {
        //Pega a posição da casa que está definida no index
        transform.position = waypoints[posicaoAtual].transform.position;

        saldo = GetComponent<Saldo>();
        saldo.idJogador = idJogador;
    }
	 
	private void Update () {

        if (vezJogador) {
            txtVezJogador.gameObject.SetActive(true);
        } else {
            txtVezJogador.gameObject.SetActive(false);
        }

        if(movimentoPermitido) {
            MoveJogador();
        }

    }

    //Método que irá mover o jogador para a casa
    private void MoveJogador(){
        int tamanhoTabuleiro = waypoints.Length - 1;

        if (posicaoAtual > tamanhoTabuleiro) {
            posicaoAtual = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position,
                                                waypoints[posicaoAtual].transform.position,
                                                velocidadeMovimento * Time.deltaTime);

        if (transform.position == waypoints[posicaoAtual].transform.position) {
            posicaoAtual += 1;
        }
    }
}
