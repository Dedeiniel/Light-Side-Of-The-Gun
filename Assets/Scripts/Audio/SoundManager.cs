using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SoundType
{
    BeamIntro,
    BeamLoop,
    BeamOutro,
    BeamOverheat,
    BomExplosion,
    BombPickup,
    ColorChange,
    EnemyExplode,
    IcePickup,
    PrismPickup
}
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    public SoundList[] soundList;
    private static SoundManager instance {get; set;}
    private AudioSource audioSource;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 0.5f)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip clipToPlay = clips[0];
        instance.audioSource.PlayOneShot(clipToPlay, volume);
    }
#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for(int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif

}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}