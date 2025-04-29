using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private BackgroundMusic bgMusic;

    void Awake(){

        if(bgMusic == null){
            Destroy(gameObject);
        } else {
            bgMusic = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game" && gameObject == enabled)
           Destroy(gameObject);
    }
}
