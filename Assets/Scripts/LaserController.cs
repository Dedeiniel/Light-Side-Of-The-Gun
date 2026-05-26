using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float defDistanceRay = 100f;
    public Transform laserPoint;
    public LineRenderer lineRenderer;
    Transform m_transform;

    [Space(5)]
    public bool activateLaser;

    void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    void Update() 
    {
        ShootLaser();

        if (activateLaser)
        {
            lineRenderer.enabled = true;                
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
    
    void ShootLaser() 
    {
        if (Physics2D.Raycast(m_transform.position, m_transform.up)) 
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserPoint.position, transform.up);
            Draw2DRay(laserPoint.position, _hit.point);
        }
        else 
        {
            Draw2DRay(laserPoint.position, laserPoint.transform.up * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos) 
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    
}