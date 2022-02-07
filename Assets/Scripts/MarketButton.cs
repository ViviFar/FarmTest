using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketButton : MonoBehaviour
{
    [SerializeField]
    private Seeds seedToSell;
    [SerializeField]
    private Text seedName;
    [SerializeField]
    private Text seedPrice;

    private Button button;
    // Start is called before the first frame update
    void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(onMarketButtonClick);
        button.image.color = seedToSell.Clr;
        seedName.text = seedToSell.Type.ToString();
        seedPrice.text = "$" + seedToSell.BuyingPrice.ToString();
    }

    public void onMarketButtonClick()
    {
        StateMachine.Instance.SeedInBuying = seedToSell;
    }
}
