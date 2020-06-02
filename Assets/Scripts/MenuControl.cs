using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

    [SerializeField] private long idJogadorVitorioso = 0L;
    private TextMeshProUGUI txtVitoria;

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SairDoJogo();
        }

        if (idJogadorVitorioso != 0L) {
            txtVitoria = GameObject.Find("JogadorVencedor").GetComponent<TextMeshProUGUI>();
            if(txtVitoria != null) {
                txtVitoria.text = "Jogador " + idJogadorVitorioso + " Venceu!";
            }
        }
    }

    public void SairDoJogo() {
        Application.Quit();
    }

    public void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadWinScene() {
        SceneManager.LoadScene(2);
    }
}
