using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    public bool activateRotation = true;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);
        movement.Normalize();

        rb.linearVelocity = movement * moveSpeed;
        if(movement != Vector2.zero && activateRotation) 
        {
            float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

}