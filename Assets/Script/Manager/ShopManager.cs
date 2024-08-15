using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private static ShopManager instance;
    public static ShopManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ShopManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<ShopManager>();
                }
            }
            return instance;
        }
    }

    public GameObject bottomMidUI;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI attackLevelText;
    public TextMeshProUGUI attackPointText;

    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI defenseLevelText;
    public TextMeshProUGUI defensePointText;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI healthLevelText;
    public TextMeshProUGUI healthPointText;


    private bool bmIsUpgradeUIOn;
    private int mAttackLevel;
    private int mDefenseLevel;
    private int mHealthLevel;
    private const int BaseUpgradeCost = 500;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Initialize();
    }

    private void Initialize()
    {
        bmIsUpgradeUIOn = false;
        mAttackLevel = 1;
        mDefenseLevel = 1;
        mHealthLevel = 1;
    }

    public void UpgradeUIButton()
    {
        if (!bmIsUpgradeUIOn)
        {
            ToggleUpgradeUI(true);
        }
        else 
        {
            ToggleUpgradeUI(false);
        }
    }
    private void ToggleUpgradeUI(bool isOn)
    {
        bmIsUpgradeUIOn = isOn;
        float newPositionY = isOn ? -1000 : -1500;
        SetUIPosition(bottomMidUI, newPositionY);
    }

    private void SetUIPosition(GameObject uiObject, float yPosition)
    {
        Vector2 newPosition = uiObject.GetComponent<RectTransform>().anchoredPosition;
        newPosition.y = yPosition;
        uiObject.GetComponent<RectTransform>().anchoredPosition = newPosition;
    }

    public void UpgradeAttackPoint()
    {
        UpgradePoint(ref mAttackLevel, attackText, attackLevelText, attackPointText, 0);
    }

    public void UpgradeDefensePoint()
    {
        UpgradePoint(ref mDefenseLevel, defenseText, defenseLevelText, defensePointText, 1);
    }

    public void UpgradeHealthPoint()
    {
        UpgradePoint(ref mHealthLevel, healthText, healthLevelText, healthPointText, 2);
    }

    private void UpgradePoint(ref int level, TextMeshProUGUI text, TextMeshProUGUI levelText, TextMeshProUGUI pointText, int type)
    {
        int money = BaseUpgradeCost * level;
        if (!GameManager.Instance.SpendMoney(money))
        {
            return;
        }

        level += 1;
        money = BaseUpgradeCost * level;
        text.text = "Money : " + money;
        levelText.text = "Lv. " + level;
        int power = GameManager.Instance.player.PlayerLevelUp(type);
        pointText.text = "+ " + power;
    }
}
