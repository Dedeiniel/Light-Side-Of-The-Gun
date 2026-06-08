using UnityEngine;

public class Arrows : MonoBehaviour
{
    public bool vertical;

    public float speed;
    public float limit;

    float StartingX;
    float StartingY;

    void Awake() 
    {
        StartingX = transform.position.x;
        StartingY = transform.position.y;
    }

    void Update()
    {
        if (vertical) 
        {
            float newY = StartingY + Mathf.PingPong(Time.time * speed, limit) - (limit / 2);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else 
        {
            float newX = StartingX + Mathf.PingPong(Time.time * speed, limit) - (limit / 2);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}