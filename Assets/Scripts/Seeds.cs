using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeedsType
{
    Wheat,
    Corn,
    Tomato
}

public class Seeds : MonoBehaviour
{
    [SerializeField]
    private SeedsType type;
    public SeedsType Type { get { return type; } }
    [SerializeField]
    private Color clr;
    public Color Clr { get { return clr; } }
    [SerializeField]
    // time duration for the period needed for the seed to become harvestable
    private float timeToGrow;
    public float TimeToGrow { get { return timeToGrow; } }
    [SerializeField]
    // time duration for the period when the player have to harvest the seed to get money
    private float timeToHarvest;
    public float TimeToHarvest { get { return timeToHarvest; } }
    [SerializeField]
    // time duration for the period when harvesting won't give money
    private float timeToRotAndDecay;
    public float TimeToRotAndDecay { get { return timeToRotAndDecay; } }
    [SerializeField]
    private int buyingPrice;
    public int BuyingPrice { get { return buyingPrice; } }
    [SerializeField]
    private int sellingPrice;
    public int SellingPrice { get { return sellingPrice; } }
    
}
