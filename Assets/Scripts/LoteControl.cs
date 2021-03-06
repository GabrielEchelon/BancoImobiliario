﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoteControl : MonoBehaviour {

    [SerializeField] private GameControl gameControl;
    [HideInInspector] public Jogador jogador;
    [HideInInspector] public Carta carta;
    [HideInInspector] public Lote lote;

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

    [SerializeField] private SpriteRenderer casaTitulo;
    [SerializeField] private SpriteRenderer casaDescricao;

    [SerializeField] public SpriteRenderer[] sprCasas;
    [SerializeField] public SpriteRenderer[] sprHoteis;

    [HideInInspector] public bool sorteReves = false;

    void Start(){
        jogador = gameControl.JogadorDaVez();
    }

    
    void Update(){
        jogador = gameControl.JogadorDaVez();
        if (!jogador.movimentoPermitido) {
            AtualizaInfoLote();
            VerificaAluguel();
            VerificaCreditoDebito();
            ExibeCasas();
        }
    }

    private void ExibeCasas() {
        if (lote.casas > 0) {
            for (int i = 0; i < lote.casas; i++) {
                sprCasas[i].gameObject.SetActive(true);
            }
        } else {
            foreach (SpriteRenderer spr in sprCasas) {
                spr.gameObject.SetActive(false);
            }
        }

        if (lote.hotel > 0) {
            for (int i = 0; i < lote.hotel; i++) {
                sprHoteis[i].gameObject.SetActive(true);
            }
        } else {
            foreach (SpriteRenderer spr in sprHoteis) {
                spr.gameObject.SetActive(false);
            }
        }
    }

    public void ComprarCasa() {
        if(lote.casas < 3) {
            jogador.AtualizaSaldoJogador(lote.valorConstrucaoCasa * -1f);
            lote.casas++;
        } else {
            ComprarHotel();
        }
    }

    private void ComprarHotel() {
        if(lote.hotel < 1) {
            jogador.AtualizaSaldoJogador(lote.valorConstrucaoHotel * -1f);
            lote.hotel++;
        }
    }

    private void VerificaCreditoDebito() {
        if (lote.creditoDebito && !jogador.creditoDebitoPago) {
            jogador.AtualizaSaldoJogador(lote.valorCreditoDebito);
            jogador.creditoDebitoPago = true;
        }
    }

    private void VerificaAluguel() {
        if(lote.dono != jogador.idJogador && lote.dono != 0L) {
            if (!jogador.creditoDebitoPago) {
                jogador.AtualizaSaldoJogador(lote.valorAluguel * -1f);
                gameControl.RetornaJogadorPorId(lote.dono).AtualizaSaldoJogador(lote.valorAluguel);
                jogador.creditoDebitoPago = true;
            }
        }
    }

    public void ComprarVenderLote() {
        if(lote.dono == jogador.idJogador) {
            lote.compravel = true;
            lote.dono = 0L;

            jogador.AtualizaSaldoJogador(lote.valorVenda);
        } else {
            lote.compravel = false;
            lote.dono = jogador.idJogador;

            jogador.AtualizaSaldoJogador(lote.valorCompra * -1f);
        }
        
    }

    private void AtualizaInfoLote() {
        lote = jogador.waypoints[jogador.posicaoAtual].GetComponent<Lote>();

        AtualizaSorteReves();
        AtualizaTxtsLote();
        AtualizaCores();
        AtualizaBotões();

    }

    private void AtualizaCores() {
        casaTitulo.color = lote.corPrimaria;
        casaDescricao.color = lote.corSecundaria;
    }

    private void AtualizaSorteReves() {
        sorteReves = lote.sorteReves;
    }

    private void AtualizaBotões() {

        botaoComprarVender.gameObject.SetActive(false);
        botaoConstruir.gameObject.SetActive(false);
        botaoConstruir.gameObject.SetActive(false);

        if (lote.LoteCompravel()) {
            txtComprarVender.text = "Comprar Lote";
            botaoComprarVender.gameObject.SetActive(true);

            botaoComprarVender.interactable = jogador.GetSaldoJogador() >= lote.valorCompra;

        } else if (lote.dono == jogador.idJogador) {
            botaoConstruir.gameObject.SetActive(true);

            txtComprarVender.text = "Vender Lote";
            botaoComprarVender.gameObject.SetActive(true);
            botaoConstruir.gameObject.SetActive(true);

            if (jogador.PossuiTodosLotes(lote.idCor)) {
                botaoConstruir.interactable = true;
            } else {
                botaoConstruir.interactable = false;
            }
        }

    }

    private void AtualizaTxtsLote() {
        txtLoteTitulo.text = lote.nome;

        txtLoteDescricao.gameObject.SetActive(false);
        txtPagamento.gameObject.SetActive(false);
        ExibeTextos(false);

        if (lote.descritivo) {
            if (jogador.cartaSelecionada) {
                txtLoteDescricao.text = carta.descricao;

                txtPagamento.text = (carta.valor >= 0) ? "Receba: R$" + carta.valor.ToString("#,#") : "Pague: R$" + (carta.valor * -1).ToString("#,#");

                txtPagamento.gameObject.SetActive(true);
            } else {
                txtLoteDescricao.text = lote.descricao;
            }

            txtLoteDescricao.gameObject.SetActive(true);
        } else if (lote.empresa) {
            valorProprietario.gameObject.SetActive(true);

            if (lote.dono == 0) {
                valorProprietario.text = "Ninguém";
            } else {
                valorProprietario.text = "Jogador " + lote.dono;
            }
            txtProprietario.gameObject.SetActive(true);

            valorCompra.text = "R$ " + lote.valorCompra;
            txtValorCompra.gameObject.SetActive(true);
            valorCompra.gameObject.SetActive(true);

            valorAluguel.text = "R$ " + lote.valorAluguel;
            txtValorAluguel.gameObject.SetActive(true);
            valorAluguel.gameObject.SetActive(true);

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

            ExibeTextos(true);
        }
    }

    private void ExibeTextos(bool trueFalse) {
        txtProprietario.gameObject.SetActive(trueFalse);
        txtValorCompra.gameObject.SetActive(trueFalse);
        txtValorVenda.gameObject.SetActive(trueFalse);
        txtValorContratucaoCasa.gameObject.SetActive(trueFalse);
        txtValorContratucaoHotel.gameObject.SetActive(trueFalse);
        txtValorAluguel.gameObject.SetActive(trueFalse);

        valorProprietario.gameObject.SetActive(trueFalse);
        valorCompra.gameObject.SetActive(trueFalse);
        valorVenda.gameObject.SetActive(trueFalse);
        valorConstrucaoCasa.gameObject.SetActive(trueFalse);
        valorConstrucaoHotel.gameObject.SetActive(trueFalse);
        valorAluguel.gameObject.SetActive(trueFalse);

    }
}
