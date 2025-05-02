using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shootRate;
    [SerializeField] private Shoots shootPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private AudioClip clipExplosion;
    [SerializeField] private AudioClip clipLossHealth;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject effectsPrefab;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button shootButton;

    private AudioSource audioSource; 
    private GameObject  playerVisual; 
    private float timer = 0.5f;
    private readonly int lives = 100;
    private int currentLives;
    private readonly int damageTaken = 10;
    private bool isMobile;
    private bool isShooting;

    void Awake()
    {
        isMobile = Application.isMobilePlatform;
    }

    void Start(){
        if (isMobile){
            joystick.gameObject.SetActive(true);
            shootButton.gameObject.SetActive(true);
        }
        playerVisual = transform.GetChild(0).gameObject; 
        currentLives = lives;
        healthBar.SetMaxHealth(lives);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {   
        Move();
        DelimitMove();
        timer += 1 * Time.deltaTime;
        if(Input.GetKey(KeyCode.Space) || isShooting)
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
        audioSource.PlayOneShot(clipLossHealth);
        healthBar.SetHealth(currentLives);
    }

    private void Move(){
        float horizontalMovement;
        float verticalMovement;

       if(isMobile){
        horizontalMovement = joystick.Horizontal;
        verticalMovement = joystick.Vertical;
        transform.Translate(speed * Time.deltaTime * new Vector2(horizontalMovement, verticalMovement));
       } else {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        transform.Translate(speed * Time.deltaTime * new Vector2(horizontalMovement, verticalMovement).normalized);
       }
 
    }

    private void DelimitMove(){
        //Delimitation
        float horizontalDelimitation = Math.Clamp(transform.position.x, -8.19f, 8.19f);
        float verticalDelimitation = Math.Clamp(transform.position.y, -4.18f, 4.18f);
        transform.position = new Vector2(horizontalDelimitation, verticalDelimitation);
    }

    private void Shoot(){
        if(timer > shootRate){
            for(int counter = 0; counter < 2; counter++)
                Instantiate(shootPrefab, spawnPoints[counter].transform.position, Quaternion.identity);
            timer = 0;
        }
    }

    public void OnPointerDown(){
        isShooting = true;
    }

      public void OnPointerUp(){
        isShooting = false;
    }

    private void DestroyPlayer(){
        Instantiate(effectsPrefab, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(clipExplosion);
        playerVisual.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
    }

    private void DestroyEnemyProjectiles(Collider2D other){
       if(other.gameObject.CompareTag("EnemyProjectile")){}
            Destroy(other.gameObject);
    }

}
