using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIM_UIManager : Singleton<UIM_UIManager>
{
    [Header("Player UI")]
    [SerializeField, Header("Player left bar life")] Image pLeftBar;
    [SerializeField, Header("Player right bar power")] Image pRightBar;
    [SerializeField, Header("Player damage bar")] Image damageBar;
    [SerializeField, Header("Player button quit")] Button quit;
    [SerializeField, Header("Death Menu")] GameObject deathMenu;

    public bool IsValidUIPlayer => pLeftBar && pRightBar;
    public void Focus()
    {
        quit.Select();
        quit.OnSelect(null);
    }

    protected override void Awake() => base.Awake();

    void Start() => PM_PlayerManager.Instance.PlayerOne.OnUpdatePlayerStats += UpdatePlayerUI;

    public void UpdatePlayerUI(PM_PlayerSettings _playerStats)
    {
        if (!IsValidUIPlayer) return;
        pLeftBar.fillAmount = _playerStats.Life / 100f;
        pRightBar.fillAmount = _playerStats.Power / 100f;

        Color alpha = damageBar.color;//opacity
        alpha.a = (1 - pLeftBar.fillAmount);
        damageBar.color = alpha;
        if (_playerStats.IsDead())
            deathMenu.SetActive(true);
    }

    public void MenuQuit() => Application.Quit();
}
