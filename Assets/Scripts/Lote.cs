using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lote : MonoBehaviour
{

    [SerializeField] private int idCasa = 0;
    [SerializeField] public string nome = "";
    [SerializeField] public string descricao = "";

    [SerializeField] public string corTexto = "";
    [SerializeField] public string corPrimaria = "";
    [SerializeField] public string corSecundaria = "";

    [SerializeField] private bool compravel = true;
    [SerializeField] public bool empresa = false;
    [SerializeField] public bool sorteReves = false;
    [SerializeField] public bool descritivo = false;

    [SerializeField] public float valorCompra = 0.0f;
    [SerializeField] public float valorVenda = 0.0f;
    [SerializeField] public float valorAluguel = 0.0f;
    [SerializeField] public float valorConstrucaoCasa = 0.0f;
    [SerializeField] public float valorConstrucaoHotel = 0.0f;

    [SerializeField] public int dono = 0;

    private void Start() {

        if(compravel && !empresa && !sorteReves) {
            valorVenda = valorCompra / 2;
            valorConstrucaoCasa = valorCompra * 0.2f;
            valorConstrucaoHotel = valorCompra * 0.4f;
            valorAluguel = valorCompra * 0.01f;
        }
    }

    public bool LoteCompravel() {
        return dono == 0 && compravel;
    }
}
