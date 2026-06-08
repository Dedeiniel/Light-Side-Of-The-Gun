using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss instance;

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
    
    public enum BossAttack 
    {
        Attack,
        ChangeColor,
        Protect
    }
    public BossAttack currentAttack;

    private Rigidbody rb;

    public LevelTrigger[] HubTriggers;// 0 = UP; 1 = DOWN; 2 = RIGHT; 3 = LEFT
    [Space(5)]
    public SpriteRenderer ThisSpriteColor;
    private Color damageColor;
    private Color originalColor;
    [Space(5)]
    public float MaxEnemyHP;
    public float EnemyHP;
    [Space(5)]
    public int PuntajeBoss = 1000;
    [Space(5)]
    public float BossSpeed = 3f;
    public float BossStartSpeed = 3f;
    public float MoveLimit = 3f;
    [Space(5)]
    public float AttackRate = 10f;
    float attackTimer;
    [Space(5)]
    public float ColorChangeTime = 2.5f;
    float colorChangeTimer;
    [Space(5)]

    float StartingX;
    float StartingY;

    public float CurrentSpiralAngle;
    public float rotationSpeed = 45f;
    public float fireRate = 0.05f;
    public Transform[] bulletFirePoints;
    [Space(5)]
    public GameObject BulletPrefab;
    float bulletTimer;
    [Space(5)]
    public GameObject MirrorWall;
    public GameObject RefractorWall;
    [Space(5)]
    public int BulletRingNumber = 10;

    public bool BeingHit;

    private float damageCounter;

    private bool bossDefeatSequence;
    private bool bossDefeatSequenceDone;
    [Space(5)]
    public GameObject ExplosionS;
    public GameObject ExplosionM;
    public GameObject ExplosionL;

    void Awake() 
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        StartingX = transform.position.x;
        StartingY = transform.position.y;
    }

    void OnEnable()
    {
        ChangeColor();

        originalColor = ThisSpriteColor.color;
        damageColor = new Color(ThisSpriteColor.color.r * 2f, ThisSpriteColor.color.g * 2f, ThisSpriteColor.color.b * 2f, ThisSpriteColor.color.a);
        EnemyHP = MaxEnemyHP;
        currentBossState = BossState.Phase1;
        BossSpeed = BossStartSpeed;
        CurrentSpiralAngle = EnemyManager.instance.CurrentSpiralAngle;
        StartingX = transform.position.x;
        StartingY = transform.position.y;
        MusicManager.instance.PlayBoss();
        damageCounter = 0;
        BeingHit = false;
        bossDefeatSequence = false;
        bossDefeatSequenceDone = false;
    }

    void Update()
    {
        if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.UP)
        {
            if (FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                MoveLimit = 10;
                float newX = StartingX + Mathf.PingPong(Time.time * BossSpeed, MoveLimit) - (MoveLimit / 2);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            if(EnemyHP <= 0) 
            {
                if (!bossDefeatSequence)
                {
                    BossDefeat();
                    bossDefeatSequence = true;
                    HubTriggers[0].BossDefeated = true;
                }
                if (bossDefeatSequenceDone)
                {
                    FakeGameManager.instance.state = FakeGameManager.GameStates.Playing;
                    FakeLevelManager.instance.ReturnToHub();
                    this.gameObject.SetActive(false);
                }
            }
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.DOWN)
        {
            if (FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                MoveLimit = 10;
                float newX = StartingX + Mathf.PingPong(Time.time * BossSpeed, MoveLimit) - (MoveLimit / 2);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            if (EnemyHP <= 0)
            {
                if (!bossDefeatSequence)
                {
                    BossDefeat();
                    bossDefeatSequence = true;
                    HubTriggers[1].BossDefeated = true;
                }
                if (bossDefeatSequenceDone)
                {
                    FakeGameManager.instance.state = FakeGameManager.GameStates.Playing;
                    FakeLevelManager.instance.ReturnToHub();
                    this.gameObject.SetActive(false);
                }
            }
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.RIGHT)
        {
            if (FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
                MoveLimit = 5;
                float newY = StartingY + Mathf.PingPong(Time.time * BossSpeed, MoveLimit) - (MoveLimit / 2);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            if (EnemyHP <= 0)
            {
                if (!bossDefeatSequence)
                {
                    BossDefeat();
                    bossDefeatSequence = true;
                    HubTriggers[2].BossDefeated = true;
                }
                if (bossDefeatSequenceDone)
                {
                    FakeGameManager.instance.state = FakeGameManager.GameStates.Playing;
                    FakeLevelManager.instance.ReturnToHub();
                    this.gameObject.SetActive(false);
                }
            }
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.LEFT)
        {
            if(FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                MoveLimit = 5;
                float newY = StartingY + Mathf.PingPong(Time.time * BossSpeed, MoveLimit) - (MoveLimit / 2);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            if (EnemyHP <= 0)
            {
                if (!bossDefeatSequence) 
                {
                    BossDefeat();
                    bossDefeatSequence = true;
                    HubTriggers[3].BossDefeated = true;
                }
                if(bossDefeatSequenceDone) 
                {
                    FakeGameManager.instance.state = FakeGameManager.GameStates.Playing;
                    FakeLevelManager.instance.ReturnToHub();
                    this.gameObject.SetActive(false);
                }
            }
        }
        if(FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
        {
            switch (currentBossState)
            {
                case BossState.Phase1:
                    if (EnemyHP <= MaxEnemyHP * 0.75f)
                    {
                        BossSpeed *= 2;
                        currentBossState = BossState.Phase2;
                    }
                    break;
                case BossState.Phase2:
                    if (EnemyHP <= MaxEnemyHP * 0.25f)
                    {
                        BossSpeed *= 1.5f;
                        currentBossState = BossState.Phase3;
                    }
                    break;
                case BossState.Phase3:
                    break;
            }
            attackTimer += Time.deltaTime;
            switch (currentAttack)
            {
                case BossAttack.Attack:
                    if (attackTimer < AttackRate)
                    {
                        bulletTimer += Time.deltaTime;
                        CurrentSpiralAngle += rotationSpeed * Time.deltaTime;
                        if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.UP)
                        {
                            if (CurrentSpiralAngle > 360f || CurrentSpiralAngle < 180f)
                            {
                                rotationSpeed *= -1;
                            }
                        }
                        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.DOWN)
                        {
                            if (CurrentSpiralAngle > 180f || CurrentSpiralAngle < 0f)
                            {
                                rotationSpeed *= -1;
                            }
                        }
                        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.RIGHT)
                        {
                            if (CurrentSpiralAngle > 270f || CurrentSpiralAngle < 90f)
                            {
                                rotationSpeed *= -1;
                            }
                        }
                        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.LEFT)
                        {
                            if (CurrentSpiralAngle > 90f || CurrentSpiralAngle < -90f)
                            {
                                rotationSpeed *= -1;
                            }
                        }
                        if (bulletTimer >= fireRate)
                        {
                            FireBullet(CurrentSpiralAngle);
                            if (currentBossState == BossState.Phase2 || currentBossState == BossState.Phase3)
                            {
                                BulletRing(BulletRingNumber);
                            }
                            bulletTimer = 0;
                        }
                    }
                    else
                    {
                        attackTimer = 0f;
                        currentAttack = BossAttack.ChangeColor;
                    }
                    break;
                case BossAttack.ChangeColor:
                    if (attackTimer < AttackRate)
                    {
                        colorChangeTimer += Time.deltaTime;
                        if (colorChangeTimer >= ColorChangeTime)
                        {
                            ChangeColor();
                            originalColor = ThisSpriteColor.color;
                            damageColor = new Color(ThisSpriteColor.color.r * 2f, ThisSpriteColor.color.g * 2f, ThisSpriteColor.color.b * 2f, ThisSpriteColor.color.a);
                            colorChangeTimer = 0f;
                        }
                    }
                    else
                    {
                        attackTimer = 0f;
                        currentAttack = BossAttack.Attack;
                    }
                    break;
            }

            if (BeingHit)
            {
                ThisSpriteColor.color = damageColor;
                if (damageCounter >= 0.5f)
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

    }


    public void ChangeColor() 
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
    }

    public void FireBullet(float angle) 
    {
        Instantiate(BulletPrefab, bulletFirePoints[0].position, Quaternion.Euler(0, 0, angle));
        Instantiate(BulletPrefab, bulletFirePoints[1].position, Quaternion.Euler(0, 0, angle));
    }

    public void BulletRing(int bulletCount)  
    {
        float angleStep = 360f / bulletCount;
        float currentangl = 0f;

        for(int i = 0; i < bulletCount; i++) 
        {
            float radians = currentangl * Mathf.Deg2Rad;
            Vector3 moveDirection = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);

            GameObject instanceBullet = Instantiate(BulletPrefab, bulletFirePoints[0].position, Quaternion.identity);
            GameObject instanceBullet1 = Instantiate(BulletPrefab, bulletFirePoints[1].position, Quaternion.identity);
            instanceBullet.transform.right = moveDirection;
            instanceBullet1.transform.right = moveDirection;

            currentangl += angleStep;
        }
    }

    public void BossDefeat() 
    {
        StartCoroutine(ExplodingBoss());
    }

    public IEnumerator ExplodingBoss() 
    {
        FakeGameManager.instance.state = FakeGameManager.GameStates.Stop;

        Instantiate(ExplosionS, bulletFirePoints[0].position, Quaternion.identity);
        
        yield return new WaitForSeconds(1f);

        Instantiate(ExplosionM, bulletFirePoints[1].position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Instantiate(ExplosionL, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        ScoreManager.instance.Puntaje += PuntajeBoss;
        FakeGameManager.instance.JefesDerrotados++;
        bossDefeatSequenceDone = true;

    }
}