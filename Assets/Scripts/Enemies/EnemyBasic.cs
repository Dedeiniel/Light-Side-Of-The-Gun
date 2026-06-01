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
    [Space(5)]
    public float MaxEnemyHP;
    public float EnemyHP;
    [Space(5)]
    public float EnemySpeed = 3f;
    public float MaxAmplitude = 1.5f;
    public float Frequency = 4f;
    float amplitude;

    void Start()
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
    }

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
    }

    void Update()
    {
        if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.UP)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);

        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.DOWN)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.RIGHT)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.LEFT)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            float TrackX = transform.position.x;
             TrackX += EnemySpeed * Time.deltaTime;
            float OscilationMove = transform.position.y + Mathf.Sin(Time.time * Frequency) * amplitude;

            transform.position = new Vector3(TrackX, OscilationMove, transform.position.z);
        }

    }

    void OnTriggerEnter() 
    {

    }
}