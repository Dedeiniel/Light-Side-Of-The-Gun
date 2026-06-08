using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(FadeIn(audioSource));

    }

    IEnumerator FadeOut(AudioSource audioSource)
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= 0.5f * Time.deltaTime / 2f;
            yield return new WaitForSeconds(2);
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