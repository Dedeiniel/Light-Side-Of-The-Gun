using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public static ShootLaser instance;
    
    public enum LaserState
    {
        Cargar,
        Disparar,
        Enfriar
    }
    LaserState currentState = LaserState.Cargar;

    public enum ColorState 
    {
        Rojo,
        Verde,
        Azul
    }
    public ColorState currentColor = ColorState.Rojo;


    public Material material;
    [Space(5)]
    public GameObject LaserLoop;
    LaserBeam beam;
    LaserBeam beam1;
    LaserBeam beam2;

    [Space(5)]
    public float TiempoDeCarga = 2f;
    float chargeTimer;
    [Space(5)]
    public float TiempoDeDisparo = 10f;
    public float heatTimer;
    [Space(5)]
    public float TiempoDeEnfriamiento = 2f;
    [Space(5)]
    public float DañoLaser;
    public float damageCounter;

    private bool shootingFlag;
    private bool chargeFlag;
    private bool endLaserFlag;

    void Awake() 
    {
        instance = this;
    }

    void Update()
    {
        switch (currentColor) 
        {
            case ColorState.Rojo:
                if (Input.GetMouseButtonDown(1)) 
                {
                    SoundManager.PlaySound(SoundType.ColorChange);                    
                    currentColor = ColorState.Verde;
                }
                break;
            case ColorState.Verde:
                if (Input.GetMouseButtonDown(1))
                {
                    SoundManager.PlaySound(SoundType.ColorChange);                    
                    currentColor = ColorState.Azul;
                }
                break;
            case ColorState.Azul:
                if (Input.GetMouseButtonDown(1))
                {
                    SoundManager.PlaySound(SoundType.ColorChange);                    
                    currentColor = ColorState.Rojo;
                }
                break;
        }

        switch (currentState)
        {
            case LaserState.Cargar:
                if (Input.GetMouseButton(0))
                {
                    if(!chargeFlag)
                    {
                        SoundManager.PlaySound(SoundType.BeamIntro);
                        chargeFlag = true;
                    }  
                    chargeTimer += Time.deltaTime;
                    if (chargeTimer >= TiempoDeCarga)
                    {                                                
                        currentState = LaserState.Disparar;
                        chargeTimer = 0f;
                        chargeFlag = false;
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {                    
                    chargeTimer = 0f;
                }
                break;
            case LaserState.Disparar:
                if (Input.GetMouseButton(0))
                {
                    if(!shootingFlag)
                    {
                        LaserLoop.SetActive(true);
                        shootingFlag = true;
                    }
                    endLaserFlag = false;
                    heatTimer += Time.deltaTime;
                    if (heatTimer >= TiempoDeDisparo)
                    {
                        SoundManager.PlaySound(SoundType.BeamOverheat);                        
                        currentState = LaserState.Enfriar;
                        heatTimer = TiempoDeDisparo;
                        LaserLoop.SetActive(false);
                        shootingFlag = false;                        
                    }
                    if (beam != null)
                    {
                        Destroy(beam.laserObj);
                    }
                    if (beam1 !=null && beam2 != null) 
                    {
                        Destroy(beam1.laserObj);
                        Destroy(beam2.laserObj);
                    }
                    beam = new LaserBeam(gameObject.transform.position, gameObject.transform.up, material);
                    if (PowerUps.instance.SiLaser) 
                    {
                        Vector2 newDirection1 = (gameObject.transform.up + gameObject.transform.right).normalized;
                        Vector2 newDirection2 = (gameObject.transform.up + -gameObject.transform.right).normalized;
                        beam1 = new LaserBeam(gameObject.transform.position,newDirection1, material);
                        beam2 = new LaserBeam(gameObject.transform.position,newDirection2, material);
                    }
                }
                else 
                {
                    if(!endLaserFlag)
                    {
                        LaserLoop.SetActive(false);
                        shootingFlag = false;                        
                        SoundManager.PlaySound(SoundType.BeamOutro);
                        endLaserFlag = true;                        
                    }
                    if (beam != null)
                    {
                        Destroy(beam.laserObj);
                    }
                    if (beam1 != null && beam2 != null)
                    {
                        Destroy(beam1.laserObj);
                        Destroy(beam2.laserObj);
                    }
                    heatTimer -= Time.deltaTime;
                    if(heatTimer <= 0f) 
                    {
                        endLaserFlag = false;
                        currentState = LaserState.Cargar;
                        heatTimer = 0f;
                    }
                }
                break;
            case LaserState.Enfriar:
                if (beam != null)
                {
                    Destroy(beam.laserObj);
                }
                if (beam1 != null && beam2 != null)
                {
                    Destroy(beam1.laserObj);
                    Destroy(beam2.laserObj);
                }
                heatTimer -= (TiempoDeDisparo / TiempoDeEnfriamiento) * Time.deltaTime;
                if(heatTimer <= 0f) 
                {                    
                    heatTimer = 0f;
                    currentState = LaserState.Cargar;
                }
                break;
        }
    }
}