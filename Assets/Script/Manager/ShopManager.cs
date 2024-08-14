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
            // 인스턴스가 존재하지 않는다면 새로 생성
            if (instance == null)
            {
                instance = FindObjectOfType<ShopManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<ShopManager>();

                    // 수동으로 초기화 메서드 호출
                    instance.Start();
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


    private bool bmIsUpgradUIOn;
    private int mAttackLevel;
    private int mDefenseLevel;
    private int mHealthLevel;


    private void Start() 
    {

        bmIsUpgradUIOn = false;
    }


    public void UpgradeUIButton()
    {
        if (!bmIsUpgradUIOn)
        {
            UpgradeUIOn();
        }
        else 
        {
            UpgradeUIOff();
        }
    }
    public void UpgradeUIOn() 
    {
        bmIsUpgradUIOn = true;
        Vector2 newPosition = bottomMidUI.GetComponent<RectTransform>().anchoredPosition;
        newPosition.y = -1000;
        bottomMidUI.GetComponent<RectTransform>().anchoredPosition = newPosition;
    }

    public void UpgradeUIOff()
    {
        bmIsUpgradUIOn = false;
        Vector2 newPosition = bottomMidUI.GetComponent<RectTransform>().anchoredPosition;
        newPosition.y = -1500;
        bottomMidUI.GetComponent<RectTransform>().anchoredPosition = newPosition;
    }

    public void UpgradeAttackPoint()
    {
        int money = 1000 * mAttackLevel;
        if (!GameManager.Instance.SpendMoney(money)) 
        {
            return;
        }
        else
        {
            mAttackLevel += 1;
            money = 1000 * mAttackLevel;
            attackText.text = "Money : " + money;
            attackLevelText.text = "Lv. " + mAttackLevel;
        }
    }

    public void UpgradeDefensePoint()
    {
        int money = 1000 * mDefenseLevel;
        if (!GameManager.Instance.SpendMoney(money))
        {
            return;
        }
        else
        {
            mDefenseLevel += 1;
            money = 1000 * mDefenseLevel;
            defenseText.text = "Money : " + money;
            defenseLevelText.text = "Lv. " + mDefenseLevel;
        }
    }

    public void UpgradeHealthPoint()
    {
        int money = 1000 * mHealthLevel;
        if (!GameManager.Instance.SpendMoney(money))
        {
            return;
        }
        else
        {
            mHealthLevel += 1;
            money = 1000 * mHealthLevel;
            healthText.text = "Money : " + money;
            healthLevelText.text = "Lv. " + mHealthLevel;
        }
    }
}
