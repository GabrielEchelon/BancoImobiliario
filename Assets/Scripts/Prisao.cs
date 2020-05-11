using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisao : MonoBehaviour {

    [HideInInspector] public Jogador jogadorDaVez;
    [SerializeField] public int posicaoPrisao = 10;
    [SerializeField] public int posicaoVaParaPrisao = 30;

    void Update(){
        if(jogadorDaVez.preso && !jogadorDaVez.movimentoPermitido && jogadorDaVez.posicaoAtual != posicaoPrisao) {
            ExecutaPrisao(jogadorDaVez);
        }
    }

    public void ExecutaPrisao(Jogador jogador) {
        jogador.transform.position = Vector2.MoveTowards(jogador.transform.position,
                                                jogador.waypoints[posicaoPrisao].transform.position,
                                                jogador.velocidadeMovimento * Time.deltaTime);

        if (jogador.transform.position == jogador.waypoints[posicaoPrisao].transform.position) {
            jogador.posicaoAtual = 10;
        }

        jogadorDaVez.preso = true;

    }
}
