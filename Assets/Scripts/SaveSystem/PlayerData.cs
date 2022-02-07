using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentFund;
    public bool[] fieldUnlocked;

    public int[,] fieldsSeeds;
    public float[,] fieldsTimers;

    public PlayerData(StateMachine machine)
    {
        this.currentFund = machine.CurrentFunds;
        fieldUnlocked = new bool[machine.Fields.Length];
        fieldsSeeds = new int[machine.Fields.Length, machine.Fields[0].HaverstableZones.Length];
        fieldsTimers = new float[machine.Fields.Length, machine.Fields[0].HaverstableZones.Length];

        for (int i=0; i< machine.Fields.Length; i++)
        {
            if (machine.Fields[i].IsLocked)
            {
                fieldUnlocked[i] = false;
            }
            else
            {
                fieldUnlocked[i] = true;
            }
            for (int j = 0; j < machine.Fields[i].HaverstableZones.Length; j++)
            {
                //we go through each harvestable zone and save the seed planted (0 if theres none, their placement in the market otherwise)
                if (machine.Fields[i].HaverstableZones[j].CurrentSeed == null)
                {
                    fieldsSeeds[i, j] = 0;
                    fieldsTimers[i, j] = 0;
                }
                else
                {
                    fieldsTimers[i, j] = machine.Fields[i].HaverstableZones[j].SeedTimer;
                    switch (machine.Fields[i].HaverstableZones[j].CurrentSeed.Type)
                    {
                        case SeedsType.Wheat:
                            fieldsSeeds[i, j] = 1;
                            break;
                        case SeedsType.Corn:
                            fieldsSeeds[i, j] = 2;
                            break;
                        case SeedsType.Tomato:
                            fieldsSeeds[i, j] = 3;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
