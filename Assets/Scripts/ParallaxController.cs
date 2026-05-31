using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float Speed;
    public bool Horizontal = true;

    public Renderer bgRenderer;

    void Update()
    {
        if (Horizontal)
        {
            bgRenderer.material.mainTextureOffset += new Vector2(Speed * Time.deltaTime, 0);
        }
        else
        {
            bgRenderer.material.mainTextureOffset += new Vector2(0, Speed * Time.deltaTime);
        }
    }
}