using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shootRate;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private GameObject spawnPoint1;
    [SerializeField] private GameObject spawnPoint2;
    
    private float timer = 0.5f;
    private float lives = 100;
    
    void Update()
    {
        Move();
        DelimitMove();
        Shoot();
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

            Instantiate(shootPrefab, spawnPoint1.transform.position, Quaternion.identity);
            Instantiate(shootPrefab, spawnPoint2.transform.position, Quaternion.identity);
            timer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("EnemyProjectile") || other.gameObject.CompareTag("Enemy")){
            lives -= 20;
            Destroy(other.gameObject);
            if(lives <= 0)
                Destroy(gameObject);
        }
    }

}
