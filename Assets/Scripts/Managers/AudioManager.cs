using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;

    private bool _isSoundActive = true;
    private bool _isMusicActive = true;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if(_isSoundActive)
            _sfxAudioSource.PlayOneShot(clip);
    }

    public void ToggleSound()
    {
        _isSoundActive = !_isSoundActive;
        _isMusicActive = !_isMusicActive;
    }
}
