using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casa : MonoBehaviour
{

    [SerializeField] private int idCasa = 0;
    [SerializeField] private string nome = "";
    
    [SerializeField] private string corTexto = "";
    [SerializeField] private string corPrimaria = "";
    [SerializeField] private string corSecundaria = "";

    [SerializeField] private bool compravel = true;
    [SerializeField] private bool empresa = false;
    [SerializeField] private bool sorteReves = false;

    [SerializeField] private float valorInicial = 0.0f;
    [SerializeField] private float valorVenda = 0.0f;
    [SerializeField] private float valorConstrucaoCasa = 0.0f;

    [SerializeField] private int dono = 0;
    void Start(){

        
    }

    void Update(){


    }
}
