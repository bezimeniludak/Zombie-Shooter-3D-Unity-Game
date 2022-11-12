using UnityEngine;

public class DetectHit : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager._currentHealth -= 10;
        _audioSource.PlayOneShot(_audioSource.clip);
    }
}
