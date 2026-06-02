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

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            if (thisPower == Power.Bomba) 
            {
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
                ShootLaser.instance.heatTimer -= 2f;
            }

            Destroy(this.gameObject);
        }
    }
}