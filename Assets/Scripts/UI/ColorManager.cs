using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public Image ImagenColor;

    void Update()
    {
        if (FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
        {
            if (ShootLaser.instance.currentColor == ShootLaser.ColorState.Rojo)
            {
                ImagenColor.color = Color.red;
            }
            else if (ShootLaser.instance.currentColor == ShootLaser.ColorState.Verde)
            {
                ImagenColor.color = Color.green;
            }
            else
            {
                ImagenColor.color = Color.blue;
            }
        }
    }
}