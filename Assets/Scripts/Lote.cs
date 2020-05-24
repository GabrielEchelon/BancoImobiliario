using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lote : MonoBehaviour {

    [SerializeField] private int idCasa = 0;
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

    [HideInInspector] public bool[] construcoes;

    private void Start() {

        /*for (int i = 0; i < 5; i++) {
            construcoes[i] = false;
        }*/

        if (compravel && !empresa && !sorteReves) {
            valorVenda = valorCompra / 2;
            valorConstrucaoCasa = valorCompra * 0.2f;
            valorConstrucaoHotel = valorCompra * 0.4f;
            valorAluguel = valorCompra * 0.01f;
        }
    }

    public bool LoteCompravel() {
        return dono == 0L && compravel;
    }

    /*private void CompraConstrucao() {
        for(int i = 0; i < construcoes.Length; i++) {
            if (!construcoes[i]) {
                construcoes[i] = true;
                break;
            }
        }
    }
    */
    private void AtualizaValores() {
        
    }

}
