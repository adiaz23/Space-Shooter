using Unity.VisualScripting;
using UnityEngine;

public class LimitZone : MonoBehaviour
{

 void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Projectile")|| other.gameObject.CompareTag("EnemyProjectile"))
          Destroy(other.gameObject);
    }
}
