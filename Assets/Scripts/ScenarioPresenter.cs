using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScenarioPresenter : MonoBehaviour
{
    public ScenarioData ScenarioData;

    public TextMeshProUGUI NameText;
    private Button button;

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        NameText.text = ScenarioData.PresentedName;
    }

    public void SetData(ScenarioData data)
    {
        ScenarioData = data;
        NameText.text = data.PresentedName;
    }

    public void OnClick()
    {
        MenuPresenter.Instance.ScenarioSelected(ScenarioData);
    }
}
