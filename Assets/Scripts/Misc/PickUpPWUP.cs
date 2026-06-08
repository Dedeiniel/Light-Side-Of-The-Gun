using UnityEngine;

public class PickUpPWUP : MonoBehaviour
{
    public enum Power 
    {
        Bomba,
        Laser3,
        Hielo,
        Count
    }
    public Power thisPower;

    public GameObject BombaSprite;
    public GameObject Laser3Sprite;
    public GameObject HieloSprite;

    public float Speed = 5f;
    public float TimeToDestroyVertical = 5f;
    public float TimeToDestroyHorizontal = 7f;

    void OnEnable() 
    {
        thisPower = (Power)UnityEngine.Random.Range(0, (int)Power.Count);
        if (thisPower == Power.Bomba)
        {
            BombaSprite.SetActive(true);
        }
        else if (thisPower == Power.Laser3)
        {
            Laser3Sprite.SetActive(true);
        }
        else
        {
            HieloSprite.SetActive(true);
        }
    }

    void Update() 
    {
        if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.UP) 
        {
            float TrackY = transform.position.y;
            TrackY -= Speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, TrackY, transform.position.z);
            Destroy(this.gameObject, TimeToDestroyVertical);
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.DOWN) 
        {
            float TrackY = transform.position.y;
            TrackY += Speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, TrackY, transform.position.z);
            Destroy(this.gameObject, TimeToDestroyVertical);
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.RIGHT)
        {
            float TrackX = transform.position.x;
            TrackX -= Speed * Time.deltaTime;
            transform.position = new Vector3(TrackX, transform.position.y, transform.position.z);
            Destroy(this.gameObject, TimeToDestroyHorizontal);
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.LEFT)
        {
            float TrackX = transform.position.x;
            TrackX += Speed * Time.deltaTime;
            transform.position = new Vector3(TrackX, transform.position.y, transform.position.z);
            Destroy(this.gameObject, TimeToDestroyHorizontal);
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            if (thisPower == Power.Bomba) 
            {
                SoundManager.PlaySound(SoundType.BombPickup);
                if (PowerUps.instance.SiBomba) 
                {
                    ScoreManager.instance.Puntaje += 300;
                }
                else 
                {
                    PowerUps.instance.SiBomba = true;
                }
            }
            else if (thisPower == Power.Laser3) 
            {
                SoundManager.PlaySound(SoundType.PrismPickup);                
                if (PowerUps.instance.SiLaser)
                {
                    ScoreManager.instance.Puntaje += 300;
                }
                else
                {
                    PowerUps.instance.SiLaser = true;
                }
            }
            else 
            {
                SoundManager.PlaySound(SoundType.IcePickup);                
                ShootLaser.instance.heatTimer -= 2f;
            }

            Destroy(this.gameObject);
        }
    }
}