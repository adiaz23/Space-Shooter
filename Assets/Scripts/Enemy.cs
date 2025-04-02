using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject shootPrefab;

    [SerializeField] private GameObject spawn1Prefab;

    [SerializeField] private GameObject spawn2Prefab;

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
        Instantiate(shootPrefab, spawn1Prefab.transform.position, Quaternion.identity);
        Instantiate(shootPrefab, spawn2Prefab.transform.position, Quaternion.identity);
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
