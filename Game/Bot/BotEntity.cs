using UnityEngine;
using System.Collections;

public class BotEntity : MonoBehaviour
{
    [SerializeField] private float _throwDelay;

    private BoneGenerator _boneGenerator;

    private Coroutine _throwDelayCoroutine;



    private void Awake()
    {
        _boneGenerator = FindObjectOfType<BoneGenerator>();
    }


    /// <summary>
    /// Совершить бросок
    /// </summary>
    public void Throw()
    {
        if (_throwDelayCoroutine != null)
            StopCoroutine(_throwDelayCoroutine);

        _throwDelayCoroutine = StartCoroutine(ThrowDelay());
    }


    private IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(_throwDelay);

        _boneGenerator.TryThrowBones();

        _throwDelayCoroutine = null;
    }
}
