using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Player player;
    private static bool isPaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                 Resume();
            } else {
                Pause();
            }     
        }
    }
    public void GameOver(){
        gameOverUI.SetActive(true);
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame(){
         SceneManager.LoadScene("Game");
         Resume();
    }

    public void GoToOptionsMenu(){
        SceneManager.LoadScene("Options", LoadSceneMode.Additive);
    }

    public void Back(){
        SceneManager.UnloadSceneAsync("Options");
    }

    public void Resume(){
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause(){
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Quit(){
        Application.Quit();
    }
}
