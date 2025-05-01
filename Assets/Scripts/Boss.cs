using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{

    private bool inPosition;
    private bool isMoving;
    private Vector2 target;

    [SerializeField] private float speed;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inPosition = false;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inPosition == false)
            MoveToInitialPosition();
        else if (isMoving == false){
            StartCoroutine(Move()); 
            isMoving = true;
        }
            
    }

    void MoveToInitialPosition(){
         target = new Vector2(4f, 0);
         float step = speed * Time.deltaTime;
         transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

 IEnumerator Move(){
        GameObject bossVisual = transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        SpriteRenderer bossSprite = bossVisual.GetComponent<SpriteRenderer>();

        Vector2 moveUp = new(4f, 2.5f);
        Vector2 moveDown = new(4f, -2.5f);

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StartBoss")){
            inPosition = true;
        }
    }

}
