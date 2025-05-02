using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Shoots shootPrefab;
    [SerializeField] private Transform[] spawnPrefabs;
    [SerializeField] private AudioClip clipExplosion;
    [SerializeField] private AudioClip clipDamage;
    [SerializeField] private GameObject effectsPrefab;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameManager gameManager;

    private AudioSource audioSource;
    private GameObject bossVisual;
    private Collider2D bossCollider;
    private SpriteRenderer bossSprite;
    private bool inPosition = false;
    private bool isMovingUpDown = false;
    private Vector2 target;
    private int lives = 500;
    private int remainLives;
    private readonly int damageTaken = 25;

    void Start()
    {
        bossVisual = transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        bossSprite = bossVisual.GetComponent<SpriteRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        bossCollider = gameObject.GetComponent<Collider2D>();
        remainLives = lives;
        healthBar.SetMaxHealth(lives);
        StartCoroutine(Shoot()); 
    }

    void Update()
    {
        if (inPosition == false)
            MoveToInitialPosition();
        else if (isMovingUpDown == false){
            StartCoroutine(Move()); 
            isMovingUpDown = true;
        }    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StartBoss")){
            inPosition = true;
            healthBar.gameObject.SetActive(true);
        }

        if((other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Player")) && bossSprite.enabled && isMovingUpDown){
            DestroyProjectile(other);
            TakeDamage(damageTaken);
            if (remainLives <= 0){
                audioSource.PlayOneShot(clipExplosion);
                bossSprite.enabled = false;
                bossCollider.enabled = false;
                Instantiate(effectsPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject, 1f);
                gameManager.Win();
            }   
        }
    }

    private void TakeDamage(int damage){
        remainLives -= damage;
        audioSource.PlayOneShot(clipDamage);
        healthBar.SetHealth(remainLives);
    }

    private void DestroyProjectile(Collider2D other)
    {
         if(other.gameObject.CompareTag("Projectile"))
            Destroy(other.gameObject);
    }

    void MoveToInitialPosition(){
         target = new Vector2(4f, 0);
         float step = speed * Time.deltaTime;
         transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    IEnumerator Move(){
        float verticalLimit = Camera.main.orthographicSize;
        float bossOffset = bossSprite.bounds.extents.y;

        float topY = verticalLimit - bossOffset;
        float bottomY = -verticalLimit + bossOffset;

        Vector2 moveUp = new(4f, topY);
        Vector2 moveDown = new(4f, bottomY);

        while(bossSprite.enabled){
         yield return StartCoroutine(MoveToPosition(moveUp));
         yield return new WaitForSeconds(1f); 
         yield return StartCoroutine(MoveToPosition(moveDown));
         yield return new WaitForSeconds(1f);
        }

    }

    IEnumerator MoveToPosition(Vector2 target){
        while((Vector2)transform.position != target){
              float step = speed * Time.deltaTime;
              transform.position = Vector2.MoveTowards(transform.position, target, step);
              yield return null;
        }
     
    }

    IEnumerator Shoot(){        
        while(bossSprite.enabled){
            for(int counter = 0; counter < spawnPrefabs.Length; counter++)
                Instantiate(shootPrefab, spawnPrefabs[counter].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
    }   

   }
}
