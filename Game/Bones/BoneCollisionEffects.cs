using MoreMountains.NiceVibrations;
using UnityEngine;

public class BoneCollisionEffects : MonoBehaviour
{
    private bool _isVibrationEnabled;
    private bool _isSoundEnabled;

    private Bone _bone;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;



    private void Awake()
    {
        _bone = GetComponentInParent<Bone>();
        _rigidbody = GetComponentInParent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _isVibrationEnabled = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;
        _isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
    }


    private void OnEnable()
    {
        _bone.OnTouchedGround += BoneTouchedGroundHandler;
    }


    private void OnDisable()
    {
        _bone.OnTouchedGround -= BoneTouchedGroundHandler;
    }


    public void BoneTouchedGroundHandler()
    {

        if (_isVibrationEnabled && GameplayManager.CurrentEntity == EntityType.Player)
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);
        }

        if (_isSoundEnabled)
        {
            _audioSource.volume = Mathf.Clamp((_rigidbody.velocity.magnitude / 10f) + 0.4f, 0f, 1f);
            _audioSource.Play();
        }
    }
}
