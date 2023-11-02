using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllers : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI LivesText;
    public GameObject EndGamePanel;
    public TextMeshProUGUI EndGameText;
    public Button BackToMenuButton;

    private ScenarioData deafaultScenario;

    private ScenarioData currentScenario;

    public int gold;
    public int Lives;
    private bool levelRunning;

    private void Awake()
    {
        SubscribeToEvents();

        BackToMenuButton.onClick.AddListener(OnBackToMenuPressed);

        UpdateUI();
        EndGamePanel.SetActive(false);
        levelRunning = false;
    }

    private void Start()
    {
        // Kui soovid mängu alustada kohe peale skripti aktiveerimist, siis:
        if (!levelRunning)
        {
            // Alusta mängu vaikimisi stsenaariumiga või mis tahes stsenaariumiga, mida pead vajalikuks.
            OnStartLevel(deafaultScenario);
            // OnStartLevel(currentScenario);
        }
    }

    private void Update()
    {
        //if (Lives <= 0 && levelRunning)
        //{
        //    EndGame();
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        Events.OnSetGold += OnSetGold;
        Events.OnRequestGold += OnRequestGold;
        Events.OnSetLives += OnSetLives;
        Events.OnRequestLives += OnRequestLives;
        Events.OnStartLevel += OnStartLevel;
        Events.OnEndLevel += OnEndLevel;
    }

    private void UnsubscribeFromEvents()
    {
        Events.OnSetGold -= OnSetGold;
        Events.OnRequestGold -= OnRequestGold;
        Events.OnSetLives -= OnSetLives;
        Events.OnRequestLives -= OnRequestLives;
        Events.OnStartLevel -= OnStartLevel;
        Events.OnEndLevel -= OnEndLevel;
    }

    private void OnSetGold(int amount)
    {
        gold = amount;
        UpdateMoneyUI();
    }

    private int OnRequestGold() => gold;
    private int OnRequestLives() => Lives;

    private void OnSetLives(int value)
    {
        if (Lives > 0 && value <= 0)
        {
            Events.EndLevel(false);
        }

        Lives = Mathf.Max(0, value);

        UpdateLivesUI();
    }

    private void OnStartLevel(ScenarioData data)
    {
        levelRunning = true;

        currentScenario = data;
        gold = currentScenario.StartingMoney;
        Lives = currentScenario.StartingLives;
      
        UpdateUI();
    }

    private void OnEndLevel(bool isWin)
    {
        if (!levelRunning) return;
        levelRunning = false;

        EndGamePanel.SetActive(true);
        EndGameText.text = isWin ? "Victory!" : "Game Lost!";
        // Lisatavad tegevused leveli lõppedes
    }

    public void OnBackToMenuPressed()
    {
        // Tagasi menüüsse minemise loogika
        EndGamePanel.SetActive(false);
        MenuPresenter.Instance?.gameObject.SetActive(true);
        SceneManager.LoadScene("MenuScene");
        
    }

    private void UpdateMoneyUI()
    {
        GoldText.text = $"Raha: {gold}";
    }

    private void UpdateLivesUI()
    {
        LivesText.text = $"Elud: {Lives}";
    }

    private void UpdateUI()
    {
        UpdateMoneyUI();
        UpdateLivesUI();
    }


    private void ToggleMenu()
    {
        // Kui EndGamePanel on aktiivne, see tähendab, et mäng on läbi ja me ei taha, et ESC klahv mõjutaks.
        if (EndGamePanel.activeSelf)
        {
            return;
        }

        // Kontrolli, kas MenuPresenter on aktiivne ja pane see vastavalt kas nähtavale või peitu.
        if (MenuPresenter.Instance != null)
        {
            bool isMenuActive = MenuPresenter.Instance.gameObject.activeSelf;
            MenuPresenter.Instance.gameObject.SetActive(!isMenuActive);
        }
        else
        {
            // Kui MenuPresenter ei ole saadaval, lae menüü stseen.
            SceneManager.LoadScene("MenuScene");
        }
    }
    }
