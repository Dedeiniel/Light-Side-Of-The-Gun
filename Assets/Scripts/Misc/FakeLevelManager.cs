using System.Collections;
using UnityEngine;

public class FakeLevelManager : MonoBehaviour
{
    public static FakeLevelManager instance;

    public enum LevelState 
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        HUB
    }
    public LevelState currentLevel = LevelState.HUB;

    public CanvasGroup TransicionDeNivel;
    public float TiempoTransicion = 1f;
    [Space(5)]
    [Tooltip("0 = UP, 1 = DOWN, 2 = RIGHT, 3 = LEFT")]
    public Transform[] Levels;// 0 = UP, 1 = DOWN, 2 = RIGHT, 3 = LEFT
    [Tooltip("0 = UP, 1 = DOWN, 2 = RIGHT, 3 = LEFT")]
    public Transform[] PlayerSpawnPoints;// 0 = UP, 1 = DOWN, 2 = RIGHT, 3 = LEFT
    [Space(5)]
    public Transform Player;
    public Transform MainCamera;

    private Vector3 originalCamPosition;

    void Awake() 
    {
        instance = this;
    }

    void Update() 
    {
        switch (currentLevel) 
        {
            case LevelState.UP:
                break;
            case LevelState.DOWN:
                break;
            case LevelState.RIGHT:
                break;
            case LevelState.LEFT:
                break;
            case LevelState.HUB:
                break;
        }
    }


    public void ReturnToHub() 
    {
        FadeToBlack(TiempoTransicion);
        Player.position = new Vector3(0f, 0f, 0f);
        PlayerController.instance.activateRotation = true;
        MainCamera.position = new Vector3(0f ,0f ,-10f);
        currentLevel = LevelState.HUB;
        MusicManager.instance.PlayCentralHub();
        FadeFromBlack(TiempoTransicion);
    }

    public void GoToLevel(float rotacion, int levelIndex) 
    {
        FadeToBlack(TiempoTransicion);
        PlayerController.instance.activateRotation = false;
        Player.rotation = Quaternion.Euler(0f, 0f, rotacion);
        Player.position = PlayerSpawnPoints[levelIndex].position;
        MainCamera.position = Levels[levelIndex].position;
        FadeFromBlack(TiempoTransicion);
        MusicManager.instance.PlayMainBattle();
    }

    public void FadeToBlack(float duration) 
    {
        StartCoroutine(AnimateFade(0f, 1f, duration));
    }

    public void FadeFromBlack(float duration) 
    {
        StartCoroutine(AnimateFade(1f, 0f, duration));
    }

    private IEnumerator AnimateFade(float startAlpha, float targetAlpha, float duration) 
    {
        float timer = 0f;
        TransicionDeNivel.alpha = startAlpha;

        while (timer < duration) 
        {
            timer += Time.deltaTime;
            TransicionDeNivel.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer/duration);
            yield return null;
        }

        TransicionDeNivel.alpha = targetAlpha;
    }   

}