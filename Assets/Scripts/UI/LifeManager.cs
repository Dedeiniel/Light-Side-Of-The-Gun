using UnityEngine;
using TMPro;
using System;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;

    public TextMeshProUGUI VidaTexto;

    public int Vidas = 3;

    void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        VidaTexto.text = "HP " + Vidas.ToString();
        if (Vidas == 0)
        {
            FakeLevelManager.instance.ReturnToHub();
            Vidas = 3;
        }
    }
 
}