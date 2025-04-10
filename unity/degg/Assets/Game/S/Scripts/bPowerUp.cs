using UnityEngine;

public class bPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("Player"))
            Destroy(gameObject);
    }
}
