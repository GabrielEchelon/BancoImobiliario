using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour {

    //Variaveis de Movimentação
    [SerializeField] private float velocidadeMovimento = 5f; //Velocidade padrão de movimentação
    [SerializeField] public int posicaoAtual = 0; //Casa do jogador
    public Transform[] waypoints; //Define quantidade de casas do tabuleiro
    public bool movimentoPermitido = false; //Define se o jogar esta na sua vez ou não

    //Variaveis de Saldo e Patrimonio
    private static GameObject txtSaldoPlayer1, txtSaldoPlayer2;
    public double saldoConta1, saldoConta2;
    public double patrimonio = 0d;

    //Variaveis Controle de Prisão
    public int posicaoPrisao = 10;
    public int enviaPrisao = 30;
    public bool estaPrisao = false;

	private void Start () {
        //Pega a posição da casa que está definida no index
        transform.position = waypoints[posicaoAtual].transform.position;

        saldoConta1 = 1500000d;
        saldoConta2 = 1500000d;

        txtSaldoPlayer1 = GameObject.Find("SaldoPlayer1");
        txtSaldoPlayer2 = GameObject.Find("SaldoPlayer2");

    }
	 
	private void Update () {
        if (movimentoPermitido){ 
            Move();
        }

        txtSaldoPlayer1.GetComponent<Text>().text = "R$ " + saldoConta1.ToString("#,#");
        txtSaldoPlayer2.GetComponent<Text>().text = "R$ " + saldoConta2.ToString("#,#");
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
