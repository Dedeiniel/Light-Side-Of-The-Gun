using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] Transform[] backgrounds;
    [SerializeField] float smoothing = 10f; //suavidad del paralaje
    [SerializeField] float multiplier = 15f; //que tanto aumenta el paralaje por capa

    Transform cam; //camara principal
    Vector3 prevCamPos; //posicion de la camara en el frame anterior

    void Awake() 
    {
        cam = Camera.main.transform;
    }
 
    void Start()
    {
        prevCamPos = cam.position;
    }

 
    void Update()
    {
        for(var i = 0; i < backgrounds.Length; i++) 
        {
            var parallax = (prevCamPos.y - cam.position.y) * (i * multiplier);
            var targetY = backgrounds[i].position.y + parallax;

            var targetPosition = new Vector3(backgrounds[i].position.x, targetY, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
        }

        prevCamPos = cam.position;
    }
}