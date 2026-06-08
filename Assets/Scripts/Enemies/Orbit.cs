using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;

    public float orbitSpeed = 45f;

    void Update()
    {
        if(FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
        {
            transform.RotateAround(target.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        }        
    }
}