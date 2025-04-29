using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Player player;

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
    }

    public void GoToOptionsMenu(){
        SceneManager.LoadScene("Options");
    }

    public void Quit(){
        Application.Quit();
    }
}
