using UnityEngine;
using System;
using System.Collections;

using Random = UnityEngine.Random;

[RequireComponent(typeof(GameHelperBase))]
[RequireComponent(typeof(BoneDetector))]
public class BoneGenerator : MonoBehaviour
{
    public event Action<int> OnDroppedNumber;
    public event Action<BonesState> OnUpdateBonesState;
    public event Action OnThrowed;

    [SerializeField] private int _targetDroppedValue;
    [Space(20)]
    [SerializeField] private int _numberBones;

    /// <summary>
    /// Хранит выпавшее число
    /// </summary>
    private int _droppedNumber;
    /// <summary>
    /// Хранит число кубиков, которые сообщили о выповшем значении
    /// </summary>
    private int _droppedBones;

    private int _bonesSendToPoolNumber;

    private bool _isGameFieldCleared = true;
    private bool _isBonesSleeping;


    [SerializeField] private float _power;

    [SerializeField] private Bone _boneTemplate;
    [SerializeField] private BoneDetector _boneDetector;
    [SerializeField] private GameHelperBase _gameHelperBase;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _bonesParent;

    [SerializeField] private BoneBasket[] _boneBaskets;

    private Coroutine _throwCoroutine;

    private PoolController<Bone> _bonesPool = new PoolController<Bone>();



    private void Start()
    {
        _boneTemplate = _boneDetector.GetSettedBone();
    }


    public void TryThrowBones()
    {
        if (_isBonesSleeping == false && _isGameFieldCleared == false) return;
        
        _isBonesSleeping = false;

        ClearGameField();

        if (!ReferenceEquals(_throwCoroutine, null))
            StopCoroutine(_throwCoroutine);

        _targetDroppedValue = _gameHelperBase.GetTargetDroppedValue();

        _throwCoroutine = StartCoroutine(ThrowBones());
    }


    public void TryClearGameField(int basketID = 1)
    {
        if (_isBonesSleeping == false && _isGameFieldCleared == false) return;
        ClearGameField(basketID);
    }


    private IEnumerator ThrowBones()
    {
        yield return new WaitUntil(() => _isGameFieldCleared == true);

        _isGameFieldCleared = false;

        for (int i = 0; i < _numberBones; i++)
        {
            Vector3 spawnPosition = new Vector3(_spawnPoint.position.x + Random.Range(-1f, 1f), _spawnPoint.position.y, _spawnPoint.position.z);

            Bone bone = GetBoneFromPool();
            bone.transform.position = spawnPosition;
            bone.transform.rotation = _spawnPoint.localRotation;

            bone.Init(_power, ApplyDroppedNumber, _targetDroppedValue);
        }

        OnUpdateBonesState?.Invoke(BonesState.Throw);
        OnThrowed?.Invoke();
    }


    private void ClearGameField(int basketID = 1)
    {
        foreach (Transform bone in _bonesParent)
        {
            if (!bone.gameObject.activeSelf) continue;

            if (bone.TryGetComponent(out BoneMover boneMover))
                boneMover.SetTargetMove(_boneBaskets[basketID].transform);
        }
    }


    /// <summary>
    /// Принять выпавшее число
    /// </summary>
    /// <param name="droppedNumber"></param>
    private void ApplyDroppedNumber(int droppedNumber)
    {
        _droppedBones++;
        _droppedNumber += droppedNumber;

        if (_droppedBones < _numberBones) return;

        _isBonesSleeping = true;

        OnDroppedNumber?.Invoke(_droppedNumber);
        OnUpdateBonesState?.Invoke(BonesState.Sleep);

        ResetData();
    }


    public void SendBoneToPool(Bone bone)
    {
        _bonesPool.InqueueToPool(bone);
        bone.gameObject.SetActive(false);

        _bonesSendToPoolNumber++;

        if (_bonesSendToPoolNumber == _numberBones)
        {
            _isGameFieldCleared = true;
            _bonesSendToPoolNumber = 0;
        }
    }


    private Bone GetBoneFromPool()
    {
        Bone bone = _bonesPool.DequeueFromPool();

        if (!ReferenceEquals(bone, null))
        {
            bone.gameObject.SetActive(true);
            return bone;
        }

        return Instantiate(_boneTemplate, _bonesParent);
    }


    private void ResetData()
    {
        _droppedBones = 0;
        _droppedNumber = 0;
    }
}