using UnityEngine;

public class EnemyMedium : MonoBehaviour
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
    [Space(5)]
    public int EnemyWeight = 3;
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
    [Space(5)]
    public GameObject subEnemy1;
    public GameObject subEnemy2;

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

        EnemyHP = MaxEnemyHP;
        amplitude = UnityEngine.Random.Range(-MaxAmplitude, MaxAmplitude);
        EnemyManager.instance.CurrentEnemiesInScene += EnemyWeight;
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

        if (EnemyHP <= 0f)
        {
            this.gameObject.SetActive(false);
        }


    }

    void OnDisable()
    {
        if (EnemyHP <= 0f)
        {
            EnemyManager.instance.CurrentEnemiesInScene--;
            ScoreManager.instance.Puntaje += PuntajeEnemigo;
            subEnemy1.transform.position = transform.position;
            subEnemy2.transform.position = transform.position;
            subEnemy1.SetActive(true);
            subEnemy2.SetActive(true);
        }
        else 
        {
            EnemyManager.instance.CurrentEnemiesInScene-= EnemyWeight;
        }
    }
}