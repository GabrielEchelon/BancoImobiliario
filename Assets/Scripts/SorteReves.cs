using UnityEngine;

public class SorteReves : MonoBehaviour {

    [SerializeField] private Carta[] cartas;
    [SerializeField] private LoteControl loteControl;

    private bool coroutineDisponivel = true;

    private void OnMouseDown() {
        if (coroutineDisponivel && loteControl.sorteReves && !loteControl.jogador.cartaSelecionada) {
            PegarCarta();
        }
    }

    private void PegarCarta() {
        coroutineDisponivel = false;

        Carta carta = cartas[Random.Range(0, cartas.Length)];

        loteControl.carta = carta;

        Saldo saldo = loteControl.jogador.GetComponent<Saldo>();
        saldo.valorDebitoCredito = carta.valor;

        loteControl.jogador.cartaSelecionada = true;
        loteControl.sorteReves = false;
        coroutineDisponivel = true;

    }

}
