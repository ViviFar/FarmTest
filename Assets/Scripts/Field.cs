using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Field : MonoBehaviour
{
    [SerializeField]
    private FieldButton[] harvestableZones;
    public FieldButton[] HaverstableZones { get { return harvestableZones; } }
    [SerializeField]
    private bool isLocked;
    public bool IsLocked { get { return isLocked; } }
    [SerializeField]
    private int price;
    private bool hasSeed = false;
    [SerializeField]
    private GameObject harvestableZonesContainer;
    [SerializeField]
    private GameObject unlockFieldButton;
    [SerializeField]
    private Text priceShowingZone; 

    // Start is called before the first frame update
    void Awake()
    {
        priceShowingZone.text = "$" + price.ToString();
        if (isLocked)
        {
            unlockFieldButton.gameObject.SetActive(true);
            unlockFieldButton.GetComponent<Button>().onClick.AddListener(Unlock);
        }
        else
        {
            UnlockWithoutPriceChecking();
        }
    }

    public void Unlock()
    {
        if (StateMachine.Instance.Pay(price))
        {
            UnlockWithoutPriceChecking();
        }
    }

    public void UnlockWithoutPriceChecking()
    {
        isLocked = false;
        unlockFieldButton.gameObject.SetActive(false);
    }
}
