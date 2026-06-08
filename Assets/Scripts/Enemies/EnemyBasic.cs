using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    public enum EnemyColor 
    {
        Rojo,
        Verde,
        Azul,
        Count
    }
    public EnemyColor ThisEnemyColor;

    public SpriteRenderer ThisSpriteColor;
    private Color damageColor;
    private Color originalColor;
    [Space(5)]
    public int EnemyWeight = 1;
    [Space(5)]
    public float MaxEnemyHP;
    public float EnemyHP;
    [Space(5)]
    public int PuntajeEnemigo = 50;
    [Space(5)]
    public float EnemySpeed = 3f;
    public float MaxAmplitude = 1.5f;
    public float Frequency = 4f;
    float amplitude;
    public bool SpawnedByMedium;

    public GameObject PWUP;

    public bool BeingHit;

    private float damageCounter;
    [Space(5)]
    public GameObject ExplosionPrefab;

    void OnEnable() 
    {
        ThisEnemyColor = (EnemyColor)UnityEngine.Random.Range(0, (int)EnemyColor.Count);
        if (ThisEnemyColor == EnemyColor.Rojo)
        {
            ThisSpriteColor.color = Color.red;
        }
        else if (ThisEnemyColor == EnemyColor.Verde)
        {
            ThisSpriteColor.color = Color.green;
        }
        else if (ThisEnemyColor == EnemyColor.Azul)
        {
            ThisSpriteColor.color = Color.blue;
        }

        originalColor = ThisSpriteColor.color;
        damageColor = new Color(ThisSpriteColor.color.r * 2f, ThisSpriteColor.color.g * 2f, ThisSpriteColor.color.b * 2f, ThisSpriteColor.color.a);
        EnemyHP = MaxEnemyHP;
        amplitude = UnityEngine.Random.Range(-MaxAmplitude, MaxAmplitude);
        if (!SpawnedByMedium) 
        {
            EnemyManager.instance.CurrentEnemiesInScene += EnemyWeight;
        }
    }

    void Update()
    {
        if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.UP)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            float TrackY = transform.position.y;
            TrackY -= EnemySpeed * Time.deltaTime;
            float OscilationMove = transform.position.x + Mathf.Sin(Time.time * Frequency) * amplitude;

            transform.position = new Vector3(OscilationMove, TrackY, transform.position.z);

        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.DOWN)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            float TrackY = transform.position.y;
            TrackY += EnemySpeed * Time.deltaTime;
            float OscilationMove = transform.position.x + Mathf.Sin(Time.time * Frequency) * amplitude;

            transform.position = new Vector3(OscilationMove, TrackY, transform.position.z);
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.RIGHT)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            float TrackX = transform.position.x;
            TrackX -= EnemySpeed * Time.deltaTime;
            float OscilationMove = transform.position.y + Mathf.Sin(Time.time * Frequency) * amplitude;

            transform.position = new Vector3(TrackX, OscilationMove, transform.position.z);
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.LEFT)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            float TrackX = transform.position.x;
             TrackX += EnemySpeed * Time.deltaTime;
            float OscilationMove = transform.position.y + Mathf.Sin(Time.time * Frequency) * amplitude;

            transform.position = new Vector3(TrackX, OscilationMove, transform.position.z);
        }

        if(EnemyHP <= 0f) 
        {
            this.gameObject.SetActive(false);
        }

        if (BeingHit) 
        {
            ThisSpriteColor.color = damageColor;
            if(damageCounter >= 0.5f) 
            {
                EnemyHP -= ShootLaser.instance.DañoLaser;
                damageCounter = 0;
                BeingHit = false;
            }
            else 
            {
                damageCounter += Time.deltaTime;
            }
        }
        else 
        {
            ThisSpriteColor.color = originalColor;
            damageCounter = 0;
        }

    }

    void OnDisable() 
    {
        if (EnemyHP <= 0f) 
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            ScoreManager.instance.Puntaje += PuntajeEnemigo;
            int dice = UnityEngine.Random.Range(0,6);
            if (dice == 0)
            {
                Instantiate(PWUP, transform.position, Quaternion.identity);
            }
        }
        EnemyManager.instance.CurrentEnemiesInScene -= EnemyWeight;
        BeingHit = false;
        damageCounter = 0;
    }

}