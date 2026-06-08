using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed;
    public float BulletDuration = 3f;

    void Update()
    {
        if(FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
        {
            transform.position += transform.right * BulletSpeed * Time.deltaTime;
            if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.HUB)
            {
                Destroy(gameObject);
            }
            else { Destroy(gameObject, BulletDuration); }
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}