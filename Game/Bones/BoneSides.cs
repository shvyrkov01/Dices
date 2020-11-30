using UnityEngine;

[System.Serializable]
public class BoneSides
{
    private Transform _transform;




    public BoneSides(Transform transform)
    {
        _transform = transform;
    }


    public Vector3 GetCrossTargetVector(int targetValue)
    {
        switch(targetValue)
        {
            case 1: return Vector3.Cross(Vector3.down, _transform.up);
            case 2: return Vector3.Cross(Vector3.down, _transform.right);
            case 3: return Vector3.Cross(Vector3.down, _transform.forward);
            case 4: return Vector3.Cross(Vector3.down, -_transform.forward);
            case 5: return Vector3.Cross(Vector3.down, -_transform.right);
            case 6: return Vector3.Cross(Vector3.down, -_transform.up);
        }

        return Vector3.zero;
    }
}