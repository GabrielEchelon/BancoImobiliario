using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisao : MonoBehaviour {

    private static Jogador jogador1, jogador2;
    private int posicaoPrisao = 10;
    private bool preso1, preso2;

    void Start(){
        jogador1 = GameObject.Find("Jogador1").GetComponent<Jogador>();
        jogador2 = GameObject.Find("Jogador2").GetComponent<Jogador>();

    }

    void Update(){
        VerificaJogadores();
    }

    private void VerificaJogadores() {
        if (jogador1.preso) {
            ExecutaPrisao(jogador1);

        } else if (jogador2.preso) {
            ExecutaPrisao(jogador2);

        }
    }

    private void ExecutaPrisao(Jogador jogador) {
        jogador.transform.position = Vector2.MoveTowards(jogador.transform.position,
                                                jogador.waypoints[posicaoPrisao].transform.position,
                                                jogador.velocidadeMovimento * Time.deltaTime);

        jogador.vezJogador = false;

    }
}
