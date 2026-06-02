using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public GameObject[] Musica;

    void Awake()
    {
        instance = this;
    }
    
    public void PlayMainMenu()
    {
       Musica[0].SetActive(true);
    }

    public void PlayCentralHub()
    {
        MuteAll();
        Musica[1].SetActive(true);
    }

    public void PlayMainBattle()
    {
        MuteAll();
        Musica[2].SetActive(true);
    }

    public void PlayBoss()
    {
        MuteAll();
        Musica[3].SetActive(true);
    }

    public void PlayGameOver()
    {
        MuteAll();
        Musica[4].SetActive(true);
    }

    public void PlayWin()
    {
        MuteAll();
        Musica[5].SetActive(true);
    }

    public void MuteAll()
    {
        foreach(GameObject musica in Musica)
        {
            musica.SetActive(false);
        }
    }

    IEnumerator FadeOut(AudioSource audioSource)
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= 0.5f * Time.deltaTime / 2f;
            yield return new WaitForSeconds(1);
        }
        audioSource.volume = 0f;
    }

    IEnumerator FadeIn(AudioSource audioSource)
    {
        while (audioSource.volume < 0.5f)
        {
            audioSource.volume += 0.5f * Time.deltaTime / 2f;
            yield return null;
        }
        audioSource.volume = 0.5f;
    }
}