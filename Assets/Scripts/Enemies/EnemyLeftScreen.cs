using UnityEngine;

public class EnemyLeftScreen : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Enemy Medium")) 
        {
            other.gameObject.SetActive(false);
        }
    }
}