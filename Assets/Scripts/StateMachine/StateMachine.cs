using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : GenericSingleton<StateMachine>
{
    [SerializeField]
    private Field[] fields;
    public Field[] Fields { get { return fields; } }
    private int currentFunds = 20; //player starts with €20 to buy some seeds
    public int CurrentFunds { get { return currentFunds; } }
    [SerializeField]
    private Text currentFundsTextZone;
    [SerializeField]
    private Text warningSeedSelected;

    [SerializeField]
    private Seeds wheat;
    [SerializeField]
    private Seeds corn;
    [SerializeField]
    private Seeds tomato;

    private Seeds seedInBuying;
    public Seeds SeedInBuying
    {
        get { return seedInBuying; }
        set {
            //click on the marketbutton a second time to cancel buying this seed or on another market button to buy another seed
            if (seedInBuying == value)
            {
                warningSeedSelected.gameObject.SetActive(false);
                seedInBuying = null;
            }
            else
            {
                warningSeedSelected.gameObject.SetActive(true);
                seedInBuying = value;
                warningSeedSelected.text = "You are currently buying " + seedInBuying.Type.ToString()
                    + ". Please tap an empty growing area to confirm buying or " + seedInBuying.Type.ToString()
                    + " to cancel action or another seed in the market place to change the seed you are buying.";
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadGame();
        UpdateFunds();
        warningSeedSelected.gameObject.SetActive(false);
    }

    public bool Pay(int amount)
    {
        if (currentFunds >= amount)
        {
            currentFunds -= amount;
            UpdateFunds();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateFunds()
    {
        currentFundsTextZone.text = "$" + currentFunds.ToString();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        SaveSystem.SaveStateMachine(this);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadDatas();
        if (data != null)
        {
            currentFunds = data.currentFund;
            for (int i = 0; i < data.fieldUnlocked.Length; i++)
            {
                if (data.fieldUnlocked[i])
                {
                    fields[i].UnlockWithoutPriceChecking();
                    for (int j = 0; j < fields[i].HaverstableZones.Length; j++)
                    {
                        switch (data.fieldsSeeds[i, j])
                        {
                            case 1:
                                fields[i].HaverstableZones[j].PlantSeed(wheat, data.fieldsTimers[i, j]);
                                break;
                            case 2:
                                fields[i].HaverstableZones[j].PlantSeed(corn, data.fieldsTimers[i, j]);
                                break;
                            case 3:
                                fields[i].HaverstableZones[j].PlantSeed(tomato, data.fieldsTimers[i, j]);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
