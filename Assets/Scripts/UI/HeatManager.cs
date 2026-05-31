using UnityEngine;
using UnityEngine.UI;

public class HeatManager : MonoBehaviour
{
    public Slider BarraCalor;

    void Update() 
    {
        if (FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
        {
            BarraCalor.value = ShootLaser.instance.heatTimer / ShootLaser.instance.TiempoDeDisparo;
        }
    }
}
