using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _failSound;
    [SerializeField] private AudioClip _successSound;

    private AudioSource _audioSource;

    private bool _isSoundEnabled;



    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        CheckSoundEnabled();
    }


    public void PlaySound(SoundType soundType)
    {
        if (_isSoundEnabled == false)
            return;

        switch (soundType)
        {
            case SoundType.Click: _audioSource.clip = _clickSound; break;
            case SoundType.Fail: _audioSource.clip = _failSound; break;
            case SoundType.Success: _audioSource.clip = _successSound; break;
        }

        _audioSource.Play();
    }


    public void CheckSoundEnabled()
    {
        _isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
    }
}
