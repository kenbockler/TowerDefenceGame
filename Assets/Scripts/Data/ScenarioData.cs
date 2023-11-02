using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioData", menuName = "TowerDefence/Scenario")]
public class ScenarioData : ScriptableObject
{
    public string PresentedName;
    public string SceneName;
    public int StartingLives = 10;
    public int StartingMoney = 20;


    public WaveData[] Waves;
    public TowerData[] Towers;
}
