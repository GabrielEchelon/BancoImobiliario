using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisao : MonoBehaviour {

    private static Jogador jogador1, jogador2;
    private int posicaoPrisao = 10;
    private bool preso1, preso2;

    void Start(){
        jogador1 = GameObject.Find("Player1").GetComponent<Jogador>();
        jogador2 = GameObject.Find("Player2").GetComponent<Jogador>();

    }

    void Update(){
        verificaJogadores();
    }

    private void verificaJogadores() {
        if (jogador1.preso) {
            executaPrisao(jogador1);

        } else if (jogador2.preso) {
            executaPrisao(jogador2);

        }
    }

    private void executaPrisao(Jogador jogador) {
        jogador.transform.position = Vector2.MoveTowards(transform.position,
                                                    jogador.waypoints[posicaoPrisao].transform.position,
                                                    jogador.velocidadeMovimento * Time.deltaTime);


    }
}
