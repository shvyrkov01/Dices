using UnityEngine;
using System.Collections;
using System;

public class BoneMover : MonoBehaviour
{
    [SerializeField] private float _movingSpeed;

    private Transform _transform;



    private void Start()
    {
        _transform = GetComponent<Transform>();    
    }


    public void SetTargetMove(Transform target)
    {
        StartCoroutine(MoveToTarget(target));
    }


    private IEnumerator MoveToTarget(Transform target)
    {
        while(_transform.position != target.position)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, _movingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
