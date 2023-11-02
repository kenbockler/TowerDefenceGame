using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Events
{
    // TOWER EVENTS
    public static event Action<TowerData> OnTowerSelected;
    public static void SelectTower(TowerData data) => OnTowerSelected?.Invoke(data);

    // GOLD EVENTS
    public static event Action<int> OnSetGold;
    public static void SetGold(int value) => OnSetGold?.Invoke(value);

    public static event Func<int> OnRequestGold;
    public static int RequestGold() => OnRequestGold?.Invoke() ?? 0;

    // LIVES EVENTS
    public static event Action<int> OnSetLives;
    public static void SetLives(int value) => OnSetLives?.Invoke(value);

    public static event Func<int> OnRequestLives;
    public static int RequestLives() => OnRequestLives?.Invoke() ?? 0;

    // LEVEL EVENTS
    public static event Action<ScenarioData> OnStartLevel;
    public static void StartLevel(ScenarioData data) => OnStartLevel?.Invoke(data);

    public static event Action<bool> OnEndLevel;
    public static void EndLevel(bool isWin) => OnEndLevel?.Invoke(isWin);
}
