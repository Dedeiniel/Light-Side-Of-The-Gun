using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public static PowerUps instance;

    public GameObject BombaIMG;
    public bool SiBomba;
    public GameObject Laser3IMG;
    public bool SiLaser;
    public GameObject BombParticles;

    void Awake() 
    {
        instance = this;
    }

    void Update() 
    {
        if (SiBomba) 
        {
            BombaIMG.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                BombParticles.SetActive(true);
                SoundManager.PlaySound(SoundType.BomExplosion);
                Bombastic();
            }
        }

        if (SiLaser) 
        {
            Laser3IMG.SetActive(true);
        }
        else 
        {
            Laser3IMG.SetActive(false);
        }
    }

    void Bombastic() 
    {
        foreach (GameObject enemy in EnemyManager.instance.BasicEnemies) 
        {
            if (enemy.activeInHierarchy) 
            {
                enemy.GetComponent<EnemyBasic>().EnemyHP = 0;
            }
        }
        foreach (GameObject enemy in EnemyManager.instance.MediumEnemies) 
        {
            if (enemy.activeInHierarchy)
            {
                enemy.GetComponent<EnemyMedium>().EnemyHP = 0;
            }
        }
        BombaIMG.SetActive(false);
        SiBomba = false;
    }
}