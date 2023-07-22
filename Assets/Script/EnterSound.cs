using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSound : MonoBehaviour
{
    [SerializeField] float _lookRadius;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] string targetTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            PlaySound();          
            Debug.Log("Enemy detected!");          
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
    }
    public void PlaySound()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }
}
