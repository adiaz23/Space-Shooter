using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject winUI;
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
        Time.timeScale = 0f;
    }

    public void Win(){
        winUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
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
        if(gameOverUI.activeSelf == false){
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }    
    }

    public void Quit(){
        Application.Quit();
    }
}
