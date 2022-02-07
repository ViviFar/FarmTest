using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldButton : MonoBehaviour
{
    private Seeds currentSeed;
    public Seeds CurrentSeed
    {
        get {
            if (currentSeed == null)
            {
                return null;
            }
            return currentSeed; }
    }

    private Button button;
    private Image buttonImage;
    private Color baseColor;
    private float seedTimer = 0;
    public float SeedTimer { get { return seedTimer; } }

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onFieldButtonClick);
        buttonImage = button.GetComponent<Image>();
        baseColor = buttonImage.color;
    }

    private void Update()
    {
        seedTimer += Time.deltaTime;
        if (currentSeed != null)
        {
            if (seedTimer >= currentSeed.TimeToGrow)
            {
                button.interactable = true;
            }
            if(seedTimer>= currentSeed.TimeToHarvest)
            {
                button.image.color = Color.black;
            }
            if (seedTimer >= currentSeed.TimeToRotAndDecay)
            {
                ResetButton();
            }
        }
    }

    public void onFieldButtonClick()
    {
        if(StateMachine.Instance.SeedInBuying != null && currentSeed == null)
        {
            if (StateMachine.Instance.Pay(StateMachine.Instance.SeedInBuying.BuyingPrice))
            {
                PlantSeed(StateMachine.Instance.SeedInBuying, 0f);
            }
        }
        else if(currentSeed != null)
        {
            Harvest();
        }
    }

    public void PlantSeed(Seeds seed, float timer)
    {
            GameObject go = Instantiate(seed.gameObject);
            currentSeed = go.GetComponent<Seeds>();
            buttonImage.color = seed.Clr;
            seedTimer = timer;
            button.interactable = false;
    }

    public void Harvest()
    {
        if (currentSeed != null)
        {
            if (seedTimer <= currentSeed.TimeToHarvest)
            {
                StateMachine.Instance.Pay(-1 * currentSeed.SellingPrice);
            }
            else
            {
                StateMachine.Instance.Pay((int)(-0.25 * currentSeed.SellingPrice));
            }
            ResetButton();
        }
    }

    public void ResetButton()
    {
        Destroy(currentSeed.gameObject);
        currentSeed = null;
        buttonImage.color = baseColor;
    }

    public void LoadTimer(float timerLoaded)
    {
        seedTimer = timerLoaded;
    }
}
