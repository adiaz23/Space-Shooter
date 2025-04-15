using UnityEngine;

public class LimitWalls : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
    }
}
