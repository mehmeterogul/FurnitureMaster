using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _mainMenuAudioSource;

    private bool _isSoundActive = true;
    private bool _isMusicActive = true;

    private float transitionDuration = 1f;

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

    private void Start()
    {
        _mainMenuAudioSource.Play();
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

    public void StartTransition()
    {
        StartCoroutine(Crossfade());
    }

    private IEnumerator Crossfade()
    {
        float elapsedTime = 0;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            _mainMenuAudioSource.volume = 1 - t;
            _musicAudioSource.volume = t;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _mainMenuAudioSource.Stop();
        _mainMenuAudioSource.volume = 1f;
        _musicAudioSource.volume = 1f;
        _musicAudioSource.Play();
    }
}
