using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    Vector3 pos, dir;

    public GameObject laserObj;
    LineRenderer laser;
    List<Vector3> laserIndices = new List<Vector3>();

    Dictionary<string, float> refractiveMaterials = new Dictionary<string, float>()
    {
        {"Air", 1.0f },
        {"Glass", 2f }
    };

    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.laserObj.tag = "Laser";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        if (ShootLaser.instance.currentColor == ShootLaser.ColorState.Rojo)
        {
            this.laser.startColor = Color.red;
            this.laser.endColor = Color.red;
        }
        else if (ShootLaser.instance.currentColor == ShootLaser.ColorState.Verde) 
        {
            this.laser.startColor = Color.green;
            this.laser.endColor = Color.green;
        }
        else 
        {
            this.laser.startColor = Color.blue;
            this.laser.endColor = Color.blue;
        }

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir); 
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit, 30,1))
        {
            laserIndices.Add(hit.point);
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser) 
    {
        if (hitInfo.collider.gameObject.tag == "Mirror") 
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        else if (hitInfo.collider.gameObject.tag == "Refractor") 
        {
            Vector3 pos = hitInfo.point;
            laserIndices.Add(pos);

            Vector3 newPos1 = new Vector3(Mathf.Abs(direction.x)/ (direction.x + 0.0001f) * 0.001f + pos.x, Mathf.Abs(direction.y) / (direction.y + 0.0001f) * 0.001f + pos.y);

            float n1 = refractiveMaterials["Air"];
            float n2 = refractiveMaterials["Glass"];

            Vector3 norm = hitInfo.normal;
            Vector3 incident = direction;

            Vector3 refractedVector = Refract(n1, n2, norm, incident);

            Ray ray1 = new Ray(newPos1, refractedVector);
            Vector3 newRayStartPos = ray1.GetPoint(1.5f);

            Ray ray2 = new Ray(newRayStartPos, -refractedVector);
            RaycastHit hit2;

            if (Physics.Raycast(ray2, out hit2, 3f, 1)) 
            {
                laserIndices.Add(hit2.point);
            }

            UpdateLaser();

            Vector3 refractedVector2 = Refract(n2, n1, -hit2.normal, refractedVector);
            CastRay(hit2.point, refractedVector2, laser);
        }
        else if (hitInfo.collider.gameObject.tag == "Enemy") 
        {
            if (hitInfo.collider.gameObject.GetComponent<EnemyBasic>().ThisSpriteColor.color != null && hitInfo.collider.gameObject.GetComponent<EnemyBasic>().ThisSpriteColor.color == this.laser.startColor) 
            {
                hitInfo.collider.gameObject.GetComponent<EnemyBasic>().BeingHit = true;
            }
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else if (hitInfo.collider.gameObject.tag == "Enemy Medium")
        {
            if (hitInfo.collider.gameObject.GetComponent<EnemyMedium>().ThisSpriteColor.color != null && hitInfo.collider.gameObject.GetComponent<EnemyMedium>().ThisSpriteColor.color == this.laser.startColor)
            {
                hitInfo.collider.gameObject.GetComponent<EnemyMedium>().BeingHit = true;
            }
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else if (hitInfo.collider.gameObject.tag == "Boss")
        {
            if (hitInfo.collider.gameObject.GetComponent<Boss>().ThisSpriteColor.color != null && hitInfo.collider.gameObject.GetComponent<Boss>().ThisSpriteColor.color == this.laser.startColor)
            {
                hitInfo.collider.gameObject.GetComponent<Boss>().BeingHit = true;
            }
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    Vector3 Refract(float n1, float n2, Vector3 norm, Vector3 incident) 
    {
        incident.Normalize();

        Vector3 refractedVector = (n1/n2 * Vector3.Cross(norm, Vector3.Cross(-norm, incident)) - norm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(norm, incident) * (n1/n2 * n1/n2), Vector3.Cross(norm, incident)))).normalized;

        return refractedVector;
    }
}