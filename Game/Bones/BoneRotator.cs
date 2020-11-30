using UnityEngine;

public class BoneRotator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _vectorRotation;

    private Transform _transform;



    private void Start()
    {
        _transform = GetComponent<Transform>();
    }


    private void Update()
    {
        _transform.Rotate(_vectorRotation * _speed * Time.deltaTime);    
    }
}
