using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 5f;

    private Rigidbody rb;

    public bool activateRotation = true;

    private void Awake() 
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
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