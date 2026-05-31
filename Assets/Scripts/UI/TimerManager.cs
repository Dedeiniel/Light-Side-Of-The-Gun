using UnityEngine;
using TMPro;
using System;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI TimerTexto;
    float tiempoJuego;

    void Update()
    {
        if (FakeGameManager.instance.state == FakeGameManager.GameStates.Playing) 
        {
            tiempoJuego += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(tiempoJuego);

            TimerTexto.text = time.ToString(@"mm\:ss\:ff");
        }
    }
}