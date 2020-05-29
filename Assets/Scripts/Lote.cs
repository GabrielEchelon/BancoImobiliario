using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lote : MonoBehaviour {

    [SerializeField] private int idCasa = 0;
    [SerializeField] public int idCor = 0;
    [SerializeField] public string nome = "";
    [SerializeField] public string descricao = "";

    [SerializeField] public Color corTexto;
    [SerializeField] public Color corPrimaria;
    [SerializeField] public Color corSecundaria;

    [SerializeField] public bool compravel = true;
    [SerializeField] public bool empresa = false;
    [SerializeField] public bool sorteReves = false;
    [SerializeField] public bool descritivo = false;
    [SerializeField] public bool creditoDebito = false;

    [SerializeField] public float valorCompra = 0.0f;
    [SerializeField] public float valorVenda = 0.0f;
    [SerializeField] public float valorAluguel = 0.0f;
    [SerializeField] public float valorConstrucaoCasa = 0.0f;
    [SerializeField] public float valorConstrucaoHotel = 0.0f;

    [SerializeField] public float valorCreditoDebito = 0.0f;

    [SerializeField] public long dono = 0L;

    [HideInInspector] public int casas = 0;
    [HideInInspector] public int hotel = 0;

    private void Update() {
        if (compravel && !empresa && !sorteReves) {
            AtualizaValores();
        }
        
    }

    private void Start() {
        if (compravel && !empresa && !sorteReves) {
            valorVenda = valorCompra / 2;
            valorConstrucaoCasa = valorCompra * 0.2f;
            valorConstrucaoHotel = valorCompra * 0.4f;
            valorAluguel = valorCompra * 0.01f;
        }
    }


    private void AtualizaValores() {
        if(casas > 0) {
            valorVenda = (valorCompra / 2) * (casas + 1);
            valorConstrucaoCasa = (valorCompra * 0.2f) * (casas + 1);
            valorAluguel = (valorCompra * 0.01f) * (casas + 1);
        }

        if(hotel > 0) {
            valorVenda = (valorCompra / 2) * (hotel + 5);
            valorConstrucaoCasa = (valorCompra * 0.2f) * (hotel + 5);
            valorAluguel = (valorCompra * 0.01f) * (hotel + 5);
        }
    }

    public bool LoteCompravel() {
        return dono == 0L && compravel;
    }

}
