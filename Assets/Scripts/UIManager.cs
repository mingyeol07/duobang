using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private ObjectPool<DamageText> damageTextPool;
    public ObjectPool<DamageText> DamageTextPool => damageTextPool;

    [SerializeField] private DamageText damageTextPrefab;
    [SerializeField] private Transform damageTextParent;

    private Dictionary<Unit, List<DamageText>> damageTextDict;

    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text diamondText;

    [SerializeField] private Image fadeImage;

    [SerializeField] private Sprite selectedButtonSprite;
    [SerializeField] private Sprite unSelectedButtonSprite;

    [Header("Ability")]
    [SerializeField] private Button abilityButton;
    [SerializeField] private GameObject abilityTab;

    [SerializeField] private AbilityPanel hpPanel;
    [SerializeField] private AbilityPanel powerPanel;
    [SerializeField] private AbilityPanel movespeedPanel;
    [SerializeField] private AbilityPanel criticalPerPanel;
    [SerializeField] private AbilityPanel criticalDamagePanel;

    [SerializeField] private Button hpUpButton;
    [SerializeField] private Button powerUpButton;
    [SerializeField] private Button moveSpeedUpButton;
    [SerializeField] private Button criticalPerUpButton;
    [SerializeField] private Button criticalDamageUpButton;

    [Header("Skill")]
    [SerializeField] private Button skillButton;
    [SerializeField] private GameObject skillTab;

    [SerializeField] private AbilityPanel normalAttackPanel;
    [SerializeField] private AbilityPanel skill1Panel;
    [SerializeField] private AbilityPanel skill2Panel;

    [SerializeField] private Button normalAttackButton;
    [SerializeField] private Button skill1Button;
    [SerializeField] private Button skill2Button;

    private void Awake()
    {
        Instance = this;

        damageTextPool = new ObjectPool<DamageText>(damageTextPrefab, 30, damageTextParent);
        damageTextDict = new Dictionary<Unit, List<DamageText>>();

        hpUpButton.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.TryUpgradeHp(hpPanel);
            SoundManager.Instance.PlaySFX(SoundName.ButtonUp);
        });

        powerUpButton.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.TryUpgradePower(powerPanel);
            SoundManager.Instance.PlaySFX(SoundName.ButtonUp);
        });

        moveSpeedUpButton.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.TryUpgradeMovespeed(movespeedPanel);
            SoundManager.Instance.PlaySFX(SoundName.ButtonUp);
        });

        criticalPerUpButton.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.TryUpgradeCriticalPercent(criticalPerPanel);
            SoundManager.Instance.PlaySFX(SoundName.ButtonUp);
        });

        criticalDamageUpButton.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.TryUpgradeCriticalDamage(criticalDamagePanel);
            SoundManager.Instance.PlaySFX(SoundName.ButtonUp);
        });

        normalAttackButton.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.TryUpgradeNormalAttack(normalAttackPanel);
            SoundManager.Instance.PlaySFX(SoundName.ButtonUp);
        });

        skill1Button.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.TryUpgradeSkill1(skill1Panel);
            SoundManager.Instance.PlaySFX(SoundName.ButtonUp);
        });

        skill2Button.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.TryUpgradeSkill2(skill2Panel);
            SoundManager.Instance.PlaySFX(SoundName.ButtonUp);
        });

        abilityButton.onClick.AddListener(() =>
        {
            skillTab.SetActive(false);
            abilityTab.SetActive(true);
            skillButton.image.sprite = unSelectedButtonSprite;
            abilityButton.image.sprite = selectedButtonSprite;
            SoundManager.Instance.PlaySFX(SoundName.ButtonUp);
        });

        skillButton.onClick.AddListener(() =>
        {
            skillTab.SetActive(true);
            abilityTab.SetActive(false);
            skillButton.image.sprite = selectedButtonSprite;
            abilityButton.image.sprite = unSelectedButtonSprite;
            SoundManager.Instance.PlaySFX(SoundName.ButtonUp);
        });
    }

    public void Fade(Action action)
    {
        fadeImage.DOFade(1, 1).OnComplete(() =>
        {
            action.Invoke();
            fadeImage.DOFade(0, 1);
        });
    }

    public void InitStatAndPanel()
    {
        UpgradeManager.Instance.InitPlayerStatAndUI(hpPanel, powerPanel, movespeedPanel, criticalPerPanel, criticalDamagePanel);
        UpgradeManager.Instance.InitPlayerSkill(normalAttackPanel, skill1Panel, skill2Panel);
    }

    public void InitCoinAndDiaText(int coin, int dia)
    {
        coinText.text = coin.ToString();
        diamondText.text = dia.ToString();
    }

    public void SpawnDamageText(Unit unit, int damage, bool isCritical)
    {
        DamageText damageText = damageTextPool.Get();
        damageText.Show(unit.transform.position, damage, isCritical);

        // 그 전에 생성되었던 데미지는 위로 올리기
        if (damageTextDict.TryGetValue(unit, out List<DamageText> prevDamages))
        {
            for(int i =0; i < prevDamages.Count; i++)
            {
                prevDamages[i].MoveUp();
            }
            prevDamages.Add(damageText);
        }
        else
        {
            prevDamages = new List<DamageText>();
            damageTextDict.Add(unit, prevDamages);
            prevDamages.Add(damageText);
        }
    }
    // 똥
    public void ClearStage()
    {
        damageTextDict.Clear();
    }
}
