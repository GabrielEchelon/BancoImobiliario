using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoteControl : MonoBehaviour {

    [SerializeField] private GameControl gameControl;
    [HideInInspector] private Jogador jogador;
    [HideInInspector] public Carta carta;

    [SerializeField] private TextMeshProUGUI txtLoteTitulo;

    [SerializeField] private TextMeshProUGUI txtProprietario;
    [SerializeField] private TextMeshProUGUI txtValorCompra;
    [SerializeField] private TextMeshProUGUI txtValorVenda;
    [SerializeField] private TextMeshProUGUI txtValorContratucaoCasa;
    [SerializeField] private TextMeshProUGUI txtValorContratucaoHotel;
    [SerializeField] private TextMeshProUGUI txtValorAluguel;

    [SerializeField] private TextMeshProUGUI txtLoteDescricao;
    [SerializeField] private TextMeshProUGUI txtPagamento;
    [SerializeField] private TextMeshProUGUI valorProprietario;
    [SerializeField] private TextMeshProUGUI valorCompra;
    [SerializeField] private TextMeshProUGUI valorVenda;
    [SerializeField] private TextMeshProUGUI valorConstrucaoCasa;
    [SerializeField] private TextMeshProUGUI valorConstrucaoHotel;
    [SerializeField] private TextMeshProUGUI valorAluguel;

    [SerializeField] private Button botaoConstruir;
    [SerializeField] private Button botaoComprarVender;
    [SerializeField] private TextMeshProUGUI txtComprarVender;

    [HideInInspector] public bool sorteReves = false;
    [HideInInspector] public bool cartasSorteadas = false;

    void Start(){
        jogador = gameControl.JogadorDaVez();
    }

    
    void Update(){
        jogador = gameControl.JogadorDaVez();
        if (!jogador.movimentoPermitido) {
            AtualizaInfoCasa();
        }
    }


    private void AtualizaInfoCasa() {
        Lote lote = jogador.waypoints[jogador.posicaoAtual].GetComponent<Lote>();

        //cartasSorteadas = false;

        AtualizaSorteReves(lote);
        AtualizaTxts(lote);
        AtualizaBotões(lote);
    }

    private void AtualizaSorteReves(Lote lote) {
        sorteReves = lote.sorteReves;
    }

    private void AtualizaBotões(Lote lote) {

        botaoComprarVender.gameObject.SetActive(false);
        botaoConstruir.gameObject.SetActive(false);

        if (lote.LoteCompravel()) {
            txtComprarVender.text = "Comprar Lote";
            botaoComprarVender.gameObject.SetActive(true);
        
        } else if (lote.dono == jogador.idJogador) {
            botaoConstruir.gameObject.SetActive(true);

            txtComprarVender.text = "Vender Lote";
            botaoComprarVender.gameObject.SetActive(true);            
        }

    }

    private void AtualizaTxts(Lote lote) {
        txtLoteTitulo.text = lote.nome;

        txtLoteDescricao.gameObject.SetActive(false);
        txtPagamento.gameObject.SetActive(false);
        DesativaTextos();

        if (lote.descritivo) {
            if (cartasSorteadas) {
                txtLoteDescricao.text = carta.descricao;

                txtPagamento.text = (carta.valor >= 0) ? "Receba: R$" + carta.valor.ToString("#,#") : "Pague: R$" + (carta.valor * -1).ToString("#,#");

                txtPagamento.gameObject.SetActive(true);
            } else {
                txtLoteDescricao.text = lote.descricao;
            }

            txtLoteDescricao.gameObject.SetActive(true);
        } else { 
            valorProprietario.gameObject.SetActive(true);

            if (lote.dono == 0) {
                valorProprietario.text = "Ninguém";
            } else {
                valorProprietario.text = "Jogador " + lote.dono;
            }

            valorCompra.text = "R$ " + lote.valorCompra;
            valorVenda.text = "R$ " + lote.valorVenda;
            valorConstrucaoCasa.text = "R$ " + lote.valorConstrucaoCasa;
            valorConstrucaoHotel.text = "R$ " + lote.valorConstrucaoHotel;
            valorAluguel.text = "R$ " + lote.valorAluguel;

            AtivaTextos();
        }
    }

    private void DesativaTextos() {
        txtProprietario.gameObject.SetActive(false);
        txtValorCompra.gameObject.SetActive(false);
        txtValorVenda.gameObject.SetActive(false);
        txtValorContratucaoCasa.gameObject.SetActive(false);
        txtValorContratucaoHotel.gameObject.SetActive(false);
        txtValorAluguel.gameObject.SetActive(false);

        valorProprietario.gameObject.SetActive(false);
        valorCompra.gameObject.SetActive(false);
        valorVenda.gameObject.SetActive(false);
        valorConstrucaoCasa.gameObject.SetActive(false);
        valorConstrucaoHotel.gameObject.SetActive(false);
        valorAluguel.gameObject.SetActive(false);
    }

    private void AtivaTextos() {
        txtProprietario.gameObject.SetActive(true);
        txtValorCompra.gameObject.SetActive(true);
        txtValorVenda.gameObject.SetActive(true);
        txtValorContratucaoCasa.gameObject.SetActive(true);
        txtValorContratucaoHotel.gameObject.SetActive(true);
        txtValorAluguel.gameObject.SetActive(true);

        valorProprietario.gameObject.SetActive(true);
        valorCompra.gameObject.SetActive(true);
        valorVenda.gameObject.SetActive(true);
        valorConstrucaoCasa.gameObject.SetActive(true);
        valorConstrucaoHotel.gameObject.SetActive(true);
        valorAluguel.gameObject.SetActive(true);

    }
}
