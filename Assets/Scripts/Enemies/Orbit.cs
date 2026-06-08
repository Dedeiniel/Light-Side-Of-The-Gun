using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;

    public float orbitSpeed = 45f;

    void Update()
    {
        transform.RotateAround(target.position, Vector3.forward, orbitSpeed * Time.deltaTime);        
    }
}
