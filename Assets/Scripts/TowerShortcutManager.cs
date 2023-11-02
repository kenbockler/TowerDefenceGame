using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShortcutManager : MonoBehaviour
{
    public List<TowerData> AllTowerData;

    void Update()
    {
        int currentMoney = Events.RequestGold();

        foreach (TowerData towerData in AllTowerData)
        {
            if (Input.GetKeyDown(towerData.ShortCut.ToLower()))
            {
                
                if (currentMoney >= towerData.Cost)
                {
                    Events.SelectTower(towerData);
                    break; 
                }
                else
                {
                    Debug.Log("Pole piisavalt raha torni ehitamiseks!");
                }
            }
        }
    }
}
