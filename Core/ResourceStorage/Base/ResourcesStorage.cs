using UnityEngine;

public class ResourcesStorage : MonoBehaviour
{
    public Chips Chips => _chips;
    public Energy Energy => _energy;
    public SurpriseBox SurpriseBox => _surpriseBox;

    [SerializeField] private Chips _chips = new Chips();
    [SerializeField] private Energy _energy = new Energy();
    [SerializeField] private SurpriseBox _surpriseBox = new SurpriseBox();



    private void Start()
    {
        Init();
    }


    private void Init()
    {
        _chips.Init();
        _energy.Init();
        _surpriseBox.Init();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            _energy.AddResources(1);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            _energy.TrySpendResources(1);
    }
}
