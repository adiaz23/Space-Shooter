using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private BackgroundMusic bgMusic;

    void Awake(){
        if(FindObjectsByType<BackgroundMusic>(FindObjectsSortMode.None).Length > 1)
           Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game")
           Destroy(gameObject);
    }
}
