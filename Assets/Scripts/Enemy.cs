using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Shoots shootPrefab;

    [SerializeField] private Transform[] spawnPrefabs;

    void Start(){
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
        while(gameObject){
        for(int counter = 0; counter < spawnPrefabs.Length; counter++)
            Instantiate(shootPrefab, spawnPrefabs[counter].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Projectile")){
             Destroy(other.gameObject);
             Destroy(gameObject);
        }
    }
}
