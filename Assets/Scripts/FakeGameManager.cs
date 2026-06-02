using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeGameManager : MonoBehaviour
{
    public static FakeGameManager instance;

    public enum GameStates 
    {
        Title,
        Intro,
        Intro2,
        Playing,
        Win,
    }
    public GameStates state;

    public GameObject PantallaTitulo;
    public GameObject EsquemaControles;
    public GameObject EsquemaInterfaz;
    public GameObject Player;
    public GameObject PantallaVictoria;
    [Space(5)]
    public float ExitTime;
    float exitCounter;
    [Space(5)]
    public int JefesDerrotados;

    void Awake() 
    {
        instance = this;
    }

    void Update() 
    {
        switch (state) 
        {
            case GameStates.Title:
                PantallaTitulo.SetActive(true);
                MusicManager.instance.PlayMainMenu();
                if (Input.GetMouseButtonDown(0))
                {
                    state = GameStates.Intro;
                }
                break;
            case GameStates.Intro:
                PantallaTitulo.SetActive(false);
                EsquemaControles.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    state = GameStates.Intro2;
                }
                break;
            case GameStates.Intro2:
                EsquemaControles.SetActive(false);
                EsquemaInterfaz.SetActive(true);
                if (Input.GetMouseButtonDown(0)) 
                {
                    state = GameStates.Playing;
                    MusicManager.instance.PlayCentralHub();
                    Player.SetActive(true);
                }
                break;
            case GameStates.Playing:
                EsquemaInterfaz.SetActive(false);
                if (Input.GetKey(KeyCode.Escape)) 
                {
                    exitCounter += Time.deltaTime;
                    if (exitCounter >= ExitTime) 
                    {
                        Application.Quit();
                    }
                }
                if (JefesDerrotados == 4) 
                {
                    FakeLevelManager.instance.ReturnToHub();
                    MusicManager.instance.PlayWin();
                    state = GameStates.Win;
                }
                break;
            case GameStates.Win:
                Player.SetActive(false);
                PantallaVictoria.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    Application.Quit();
                }
                else if (Input.GetKeyDown(KeyCode.Return)) 
                {
                    ResetGame();
                }
                break;
        }
    }

    public void ResetGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}