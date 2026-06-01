using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [Tooltip("0 = UP, 1 = DOWN, 2 = RIGHT, 3 = LEFT")]
    public int LevelIndex;// 0 = UP, 1 = DOWN, 2 = RIGHT, 3 = LEFT
    [Space(5)]
    [Tooltip("0f = UP, 180f = DOWN, -90 = RIGHT, 90 = LEFT")]
    public float Rotation;// 0f = UP, 180f = DOWN, -90 = RIGHT, 90 = LEFT
    [Space(5)]
    public bool BossDefeated;
    public GameObject Flecha;
    [Space(5)]
    public FakeLevelManager.LevelState ThisLevel;

    private Collider thisCollider;

    void Awake() 
    {
        thisCollider = GetComponent<Collider>();
    }

    void Update() 
    {
        if (BossDefeated) 
        {
            thisCollider.isTrigger = false;
            Flecha.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            FakeLevelManager.instance.GoToLevel(Rotation, LevelIndex);
            FakeLevelManager.instance.currentLevel = ThisLevel;
        }
    } 

}