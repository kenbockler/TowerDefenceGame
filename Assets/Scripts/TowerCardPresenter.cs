using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerCardPresenter : MonoBehaviour
{
    public TowerData TowerData;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI ShortCutText;
    public Image IconImage;

    private Button button;

    private void Awake()
    {
        Events.OnSetGold += OnSetMoney;
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(Pressed);
        }

        if (TowerData != null)
        {
            init(TowerData);
        }
    }

    private void OnDestroy()
    {
        Events.OnSetGold -= OnSetMoney;
    }

    private void OnSetMoney(int amount)
    {
        button.interactable = amount >= TowerData.Cost;
    }

    public void init(TowerData towerData)
    {
        this.TowerData = towerData;
        CostText.text = towerData.Cost.ToString();
        ShortCutText.text = towerData.ShortCut;
        IconImage.sprite = towerData.Icon;
    }

    public void Pressed()
    {
        Events.SelectTower(TowerData);
    }
}
