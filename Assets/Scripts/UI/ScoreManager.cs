using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI PuntajeTexto;
    public int Puntaje;
    int module;
    int thousandCounter = 1;

    void Awake() 
    {
        instance = this;
    }

    void Update()
    {
        module = Puntaje;
        if(module/thousandCounter >= 1000) 
        {
            thousandCounter++;
            LifeManager.instance.Vidas++;
        }
        PuntajeTexto.text = "Puntaje " + Puntaje.ToString();
    }
}