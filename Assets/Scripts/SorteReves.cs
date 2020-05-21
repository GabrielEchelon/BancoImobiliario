using UnityEngine;

public class SorteReves : MonoBehaviour {

    [SerializeField] private Carta[] cartas;
    [SerializeField] private LoteControl loteControl;

    private bool coroutineDisponivel = true;

    private void OnMouseDown() {
        if (coroutineDisponivel && loteControl.sorteReves) {
            PegarCarta();
        }
    }

    private void PegarCarta() {
        coroutineDisponivel = false;

        loteControl.carta = cartas[Random.Range(0, cartas.Length)];

        loteControl.cartasSorteadas = true;
        coroutineDisponivel = true;

    }

}
