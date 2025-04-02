using UnityEngine;

public class Shoots : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;	

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement(){
        transform.Translate(speed * Time.deltaTime * direction);
    }
}
