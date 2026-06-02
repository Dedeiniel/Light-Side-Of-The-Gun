using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum EnemyColor
    {
        Rojo,
        Verde,
        Azul,
        Count
    }
    public EnemyColor ThisEnemyColor;

    public enum BossState 
    {
        Phase1,
        Phase2,
        Phase3
    }
    public BossState currentBossState;
    
    private Rigidbody rb;

    public SpriteRenderer ThisSpriteColor;
    [Space(5)]
    public float MaxEnemyHP;
    public float EnemyHP;
    [Space(5)]
    public int PuntajeBoss = 1000;
    [Space(5)]
    public float BossSpeed = 3f;
    public float MoveLimit = 3f;

    float StartingX;
    float StartingY;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        StartingX = transform.position.x;
        StartingY = transform.position.y;
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


    }

    void Update()
    {
        if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.UP)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            MoveLimit = 10;
            float newX = StartingX + Mathf.PingPong(Time.time * BossSpeed, MoveLimit) - (MoveLimit / 2);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.DOWN)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            MoveLimit = 10;
            float newX = StartingX + Mathf.PingPong(Time.time * BossSpeed, MoveLimit) - (MoveLimit / 2);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.RIGHT)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            MoveLimit = 5;
            float newY = StartingY + Mathf.PingPong(Time.time * BossSpeed, MoveLimit) - (MoveLimit / 2);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.LEFT)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            MoveLimit = 5;
            float newY = StartingY + Mathf.PingPong(Time.time * BossSpeed, MoveLimit) - (MoveLimit / 2);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        switch (currentBossState) 
        {
            case BossState.Phase1:
                if(EnemyHP<= MaxEnemyHP * 0.75f) 
                {
                    BossSpeed *= 2;
                    currentBossState = BossState.Phase2;
                }
                break;
            case BossState.Phase2:
                if(EnemyHP <= MaxEnemyHP * 0.25f) 
                {
                    BossSpeed *= 1.5f;
                    currentBossState = BossState.Phase3;
                }
                break;
            case BossState .Phase3:
                break;
        }
    }
}