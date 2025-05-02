using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Shoots shootPrefab;
    [SerializeField] private Transform[] spawnPrefabs;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject effectsPrefab;

    private GameObject enemyVisual;
    private SpriteRenderer enemySprite;
    private Collider2D enemyCollider;
    private AudioSource audioSource;

    void Start(){
        enemyVisual = transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        enemySprite = enemyVisual.GetComponent<SpriteRenderer>();
        enemyCollider = gameObject.GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Shoot());
    }

    void Update()
    {
        Move();
    }

    private void Move(){
        transform.Translate(speed * Time.deltaTime * new Vector2(-1,0));
    }

    IEnumerator Shoot(){        
        while(enemySprite.enabled){
            for(int counter = 0; counter < spawnPrefabs.Length; counter++)
                Instantiate(shootPrefab, spawnPrefabs[counter].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if((other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Player")) && enemySprite.enabled){
             DestroyProjectile(other);
             audioSource.PlayOneShot(clip);
             enemySprite.enabled = false;
             enemyCollider.enabled = false;
             Instantiate(effectsPrefab, transform.position, Quaternion.identity);
             Destroy(gameObject, 1f);
        }
    }

    void DestroyProjectile(Collider2D other){
        if(other.gameObject.CompareTag("Projectile"))
            Destroy(other.gameObject);
    }
}
