using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    private static GameObject textoJogadorVencedor, textoVezPlayer1, textoVezPlayer2;
    private static GameObject playerObj1, playerObj2;
    public static int valorDado = 0;
    public static int posicaoPlayer1 = 0;
    public static int posicaoPlayer2 = 0;

    private Jogador jogador1;
    private Jogador jogador2;

    void Start () {

        textoJogadorVencedor = GameObject.Find("TextoJogadorVencedor");
        textoVezPlayer1 = GameObject.Find("TextoVezPlayer1");
        textoVezPlayer2 = GameObject.Find("TextoVezPlayer2");

        playerObj1 = GameObject.Find("Player1");
        playerObj2 = GameObject.Find("Player2");

        playerObj1.GetComponent<Jogador>().movimentoPermitido = false;
        playerObj2.GetComponent<Jogador>().movimentoPermitido = false;

        textoJogadorVencedor.gameObject.SetActive(false);
        textoVezPlayer1.gameObject.SetActive(true);
        textoVezPlayer2.gameObject.SetActive(false);
    }

    void Update(){

        jogador1 = playerObj1.GetComponent<Jogador>();
        jogador2 = playerObj2.GetComponent<Jogador>();

        AtualizaDado();
        SetaVezJogador();

    }

    public static void MovePlayer(int playerToMove){
        switch (playerToMove) { 
            case 1:
                playerObj1.GetComponent<Jogador>().movimentoPermitido = true;
                break;

            case 2:
                playerObj2.GetComponent<Jogador>().movimentoPermitido = true;
                break;
        }
    }

    // Método que verifica se é a vez de certo jogador ou não
    private void SetaVezJogador(){
        if (jogador1.posicaoAtual > posicaoPlayer1 + valorDado){
            jogador1.movimentoPermitido = false;
            textoVezPlayer1.gameObject.SetActive(false);
            textoVezPlayer2.gameObject.SetActive(true);
            posicaoPlayer1 = jogador1.posicaoAtual - 1;
        }

        if (jogador2.posicaoAtual > posicaoPlayer2 + valorDado){
            jogador2.movimentoPermitido = false;
            textoVezPlayer2.gameObject.SetActive(false);
            textoVezPlayer1.gameObject.SetActive(true);
            posicaoPlayer2 = jogador2.posicaoAtual - 1;
        }
    }
    
    //Atualiza o valor do dado após cruzar linha inicial
    private void AtualizaDado(){
        if (jogador1.posicaoAtual == 0 && posicaoPlayer1 > 0){
            valorDado = valorDado - ((jogador1.waypoints.Length - 1) - posicaoPlayer1);
            posicaoPlayer1 = jogador1.posicaoAtual;

        }else if (jogador2.posicaoAtual == 0 && posicaoPlayer2 > 0){
            valorDado = valorDado - ((jogador2.waypoints.Length - 1) - posicaoPlayer2);
            posicaoPlayer2 = jogador2.posicaoAtual;

        }
    }
}
