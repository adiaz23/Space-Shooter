using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shootRate;
    [SerializeField] private Shoots shootPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject effectsPrefab;
    [SerializeField] private HealthBar healthBar;

    private AudioSource audioSource;   
    private float timer = 0.5f;
    private int lives = 100;
    private int currentLives;
    private int damageTaken = 10;

    void Start(){
        currentLives = lives;
        healthBar.SetMaxHealth(lives);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {   
        Move();
        DelimitMove();
        Shoot();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("EnemyProjectile") || other.gameObject.CompareTag("Enemy")){
            DestroyEnemyProjectiles(other);
            TakeDamage(damageTaken);
            if(currentLives <= 0){
                gameManager.GameOver();
                DestroyPlayer();
            }
        }
    } 

    private void TakeDamage(int damage){
        currentLives -= damage;
        healthBar.SetHealth(currentLives);
    }

    private void Move(){
        //Vertical movement
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        transform.Translate(speed * Time.deltaTime * new Vector2(horizontalMovement, verticalMovement).normalized);
    }

    private void DelimitMove(){
        //Delimitation
        float horizontalDelimitation = Math.Clamp(transform.position.x, -8.19f, 8.19f);
        float verticalDelimitation = Math.Clamp(transform.position.y, -4.18f, 4.18f);
        transform.position = new Vector2(horizontalDelimitation, verticalDelimitation);
    }

    private void Shoot(){

        timer += 1 * Time.deltaTime;

        if(Input.GetKey(KeyCode.Space) && timer > shootRate){
            
            for(int counter = 0; counter < 2; counter++)
                Instantiate(shootPrefab, spawnPoints[counter].transform.position, Quaternion.identity);
            
            timer = 0;
        }

    }

    private void DestroyPlayer(){
        GameObject  playerVisual = transform.GetChild(0).gameObject;
        Instantiate(effectsPrefab, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(clip);
        playerVisual.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
    }

    private void DestroyEnemyProjectiles(Collider2D other){
       if(other.gameObject.CompareTag("EnemyProjectile")){}
            Destroy(other.gameObject);
    }

}
