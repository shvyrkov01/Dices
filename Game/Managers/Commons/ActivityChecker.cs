using UnityEngine;
using System.Collections;

public class ActivityChecker : MonoBehaviour
{
    [SerializeField] private float _maxIdleTime;

    private BoneGenerator _boneGenerator;



    private void Awake()
    {
        _boneGenerator = FindObjectOfType<BoneGenerator>();
    }


    private void Start()
    {
        StartCoroutine(ActivityCheck());
    }


    private void OnEnable()
    {
        _boneGenerator.OnThrowed += OnThrowedBones;
    }


    private void OnDisable()
    {
        _boneGenerator.OnThrowed -= OnThrowedBones;
    }


    private void OnThrowedBones()
    {
        StopAllCoroutines();
        StartCoroutine(ActivityCheck());
    }


    private IEnumerator ActivityCheck()
    {
        yield return new WaitForSeconds(_maxIdleTime);

        yield return new WaitUntil(() => GameplayManager.GameplayState == GameplayState.Started);

        Mediator.Instance.PopupsManager.CreatePopup<AlertPopup>().Init(AlertType.AFK);

        yield return new WaitForSeconds(10f);

        yield return new WaitUntil(() => GameplayManager.GameplayState == GameplayState.Started);
            
        CollectFine();

        DataExchangeBetweenScenes.SetData(new ContainerData(ExchangeDataType.Show_Alert_Popup, AlertType.Penalty));
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Modes");
    }


    private void CollectFine()
    {
        Mediator.Instance.ResourcesStorage.Chips.CollectFine(100);
    }
}
