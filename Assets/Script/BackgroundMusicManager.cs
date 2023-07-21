using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;

    private void Awake()
    {
        _audioSource.GetComponent<AudioSource>().enabled = true;
    }

    private void PlaySound()
    {
        if (PauseMenu.GameIsPaused)
        {
            _audioSource.pitch -= 1f;
        }
    }
}
