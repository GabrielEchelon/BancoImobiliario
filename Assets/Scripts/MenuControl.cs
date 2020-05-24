using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {
    
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
