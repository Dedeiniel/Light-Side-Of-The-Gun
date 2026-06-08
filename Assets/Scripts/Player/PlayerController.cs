using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 5f;

    private Rigidbody rb;

    public bool activateRotation = true;

    [Space(5)]
    public GameObject ExplosionPrefab;

    private void Awake() 
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(moveX, moveY);
            movement.Normalize();

            rb.linearVelocity = movement * moveSpeed;
            if (movement != Vector2.zero && activateRotation)
            {
                float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Enemy Medium") || other.CompareTag("Boss") || other.CompareTag("Bullet")) 
        {

            if (PowerUps.instance.SiLaser) 
            {
                PowerUps.instance.SiLaser = false;
            }
            else 
            {
                LifeManager.instance.Vidas--;
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}