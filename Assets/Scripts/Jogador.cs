using System.Globalization;
using Unity.Collections;
using UnityEngine;
using TMPro;

public class Jogador : MonoBehaviour {

    [SerializeField] public long idJogador = 0;
    public Transform[] waypoints; 

    [SerializeField] public float velocidadeMovimento = 5f; 
    public bool movimentoPermitido = false; 

    [SerializeField] public int posicaoAtual = 0; 
    [HideInInspector] public int ultimaPosicao = 0;


    [SerializeField] public TextMeshProUGUI txtVezJogador; 
    public bool vezJogador = false; 

    public bool preso = false; 

    [HideInInspector] public Saldo saldo;
    [HideInInspector] public bool aluguelPago = false;
    [HideInInspector] public bool cartaSelecionada = false;

    private void Start () {
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
            aluguelPago = false;
            cartaSelecionada = false;
            MoveJogador();
        }

        if (!movimentoPermitido && posicaoAtual == 30) {
            preso = true;
        }

    }

    private void MoveJogador(){
        int tamanhoTabuleiro = waypoints.Length - 1;

        if (posicaoAtual > tamanhoTabuleiro) {
            posicaoAtual = 0;
            saldo.valorDebitoCredito = 200000f;
        }

        transform.position = Vector2.MoveTowards(transform.position,
                                                waypoints[posicaoAtual].transform.position,
                                                velocidadeMovimento * Time.deltaTime);

        if (transform.position == waypoints[posicaoAtual].transform.position) {
            posicaoAtual += 1;
        }
    }
}
