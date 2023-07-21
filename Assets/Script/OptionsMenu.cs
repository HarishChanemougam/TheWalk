using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("Volume", volume);
    }
}
