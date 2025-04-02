using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float imageWidth;
    private Vector2 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
       float substract =  speed * Time.time % imageWidth;
       transform.position = initialPosition + substract * direction;
    }
}
