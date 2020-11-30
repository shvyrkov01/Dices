using System.Collections;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

public class Bone : MonoBehaviour, IBone
{
    public event Action<int> OnDroppedValue;
    /// <summary>
    /// Вызывается при касании земли
    /// </summary>
    public event Action OnTouchedGround;

    public int Value { get; private set; }
    private int _targetValue;

    private bool _isTouchedGround;

    [SerializeField] private float _torqueForce;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody _rigidbody;

    private Action<int> _droppedValueHandler;

    [SerializeField] private Vector3Int _directionValues;
    private Vector3Int _opposingDirectionValues;

    [SerializeField] private BoneSides _boneSides;



    private void Start()
    {
        InitComponents();
    }


    private void OnDisable()
    {
        ResetData();
    }


    private void InitComponents()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
        _boneSides = new BoneSides(GetComponent<Transform>());

        _opposingDirectionValues = 7 * Vector3Int.one - _directionValues;
    }


    public void Init(float power, Action<int> droppedValueHandler, int targetDroppedValue = 0)
    {
        _droppedValueHandler = droppedValueHandler;

        if (ReferenceEquals(_rigidbody, null))
            InitComponents();

        Vector3 directionPower = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        _rigidbody.AddForce(transform.forward * power * 100f, ForceMode.Impulse);
        _rigidbody.AddTorque(directionPower * power * 50f);


        if (targetDroppedValue > 0)
        {
            _targetValue = targetDroppedValue;

            Vector3 targetRotation = _boneSides.GetCrossTargetVector(_targetValue);
            transform.rotation = Quaternion.LookRotation(Vector3.right, targetRotation);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + Random.Range(0, 270f));

            StartCoroutine(ReachingGoal());
        }


        StartCoroutine(CheckSide());
    }


    private int GetCurrentValue()
    {
        if (Vector3.Cross(Vector3.up, transform.right).magnitude < 0.5f)
        {
            if (Vector3.Dot(Vector3.up, transform.right) > 0)
                return _directionValues.x;
            else
                return _opposingDirectionValues.x;
        }
        else if (Vector3.Cross(Vector3.up, transform.up).magnitude < 0.5f)
        {
            if (Vector3.Dot(Vector3.up, transform.up) > 0)
                return _directionValues.y;
            else
                return _opposingDirectionValues.y;
        }
        else if (Vector3.Cross(Vector3.up, transform.forward).magnitude < 0.5f)
        {
            if (Vector3.Dot(Vector3.up, transform.forward) > 0)
                return _directionValues.z;
            else
                return _opposingDirectionValues.z;
        }

        return 0;
    }


    private IEnumerator CheckSide()
    {
        yield return new WaitUntil(() => _rigidbody.IsSleeping());

        int droppedValue = GetCurrentValue();

        if(!ReferenceEquals(_droppedValueHandler, null))
            _droppedValueHandler?.Invoke(droppedValue);

        OnDroppedValue?.Invoke(droppedValue);
    }


    private IEnumerator ReachingGoal()
    {
        int currentValue = 0;
        float reachingTime = 0;

        if (ReferenceEquals(_rigidbody, null))
            InitComponents();

        yield return new WaitUntil(() => _isTouchedGround);

        Vector3 torqueTarget = Vector3.zero;

        while (currentValue != _targetValue && _rigidbody.IsSleeping() == false)
        {
            yield return new WaitForFixedUpdate();

            reachingTime += 0.02f;

            currentValue = GetCurrentValue();

            if (currentValue == _targetValue) break;

            Vector3 targetRotation = _boneSides.GetCrossTargetVector(_targetValue);

            _rigidbody.AddTorque(targetRotation * _torqueForce);

            if (reachingTime > 10)
                break;
        }

        _rigidbody.angularDrag = 10;

        yield return new WaitForSeconds(0.2f);

        _rigidbody.angularDrag = 0.05f;



        _rigidbody.angularVelocity = Vector3.zero;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<MeshCollider>() != null)
            OnTouchedGround?.Invoke();

        _isTouchedGround = true;
    }


    private void ResetData()
    {
        _targetValue = 0;
        _isTouchedGround = false;
        StopAllCoroutines();
    }
}
