using UnityEngine;

public class Jogador : MonoBehaviour {

    [SerializeField] private float velocidadeMovimento = 5f; //Velocidade padrão de movimentação
    [SerializeField] public int posicaoAtual = 0; //Casa do jogador
    public Transform[] waypoints; //Define quantidade de casas do tabuleiro
    public bool movimentoPermitido = false; //Define se o jogar esta na sua vez ou não

    public int posicaoPrisao = 10;
    public int enviaPrisao = 30;
    public bool estaPrisao = false;

	private void Start () {
        transform.position = waypoints[posicaoAtual].transform.position; //Pega a posição da casa que está definida no index
	}
	 
	private void Update () {
        if (movimentoPermitido){ 
            Move();
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
