using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [System.Serializable]
    class ShopItem
    {
        public GameObject go;
        public Sprite image;
        public string name;
        public int price;
        public bool IsPurchased = false;
    }

    [SerializeField] List<ShopItem> ShopItemList;

    MenuManager scriptMenuManager;

    private void Awake()
    {
        //PlayerPrefs.SetInt("isHave1", 0);
        //PlayerPrefs.SetInt("isHave2", 0);
        //PlayerPrefs.SetInt("isHave3", 0);
        //PlayerPrefs.SetInt("isHave4", 0);
        //PlayerPrefs.SetInt("money", 0);
        scriptMenuManager = GameObject.Find("GameManager").GetComponent<MenuManager>();
        for (int i = 0; i < ShopItemList.Count; i++)
        {
            ShopGenerate(i);
        }

    }

    public void ShopGenerate(int i)
    {
        if (PlayerPrefs.GetInt("isHave" + i).Equals(1)) ShopItemList[i].IsPurchased = true;
        ShopItemList[i].go.GetComponentInChildren<Image>().sprite = ShopItemList[i].image;
        ShopItemList[i].go.GetComponentInChildren<TextMeshProUGUI>().text = ShopItemList[i].name;
        ShopItemList[i].go.GetComponentInChildren<Toggle>().isOn = false;
        if (!ShopItemList[i].IsPurchased)
        {
            ShopItemList[i].go.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = ShopItemList[i].price + "";
            ShopItemList[i].go.GetComponentInChildren<Toggle>().interactable = false;
            if (PlayerPrefs.GetInt("money") >= ShopItemList[i].price)
            {
                ShopItemList[i].go.GetComponentInChildren<Button>().interactable = true;
            }
        }
        else
        {
            ShopItemList[i].price = 0;
            ShopItemList[i].go.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Purchased";
            ShopItemList[i].go.GetComponentInChildren<Button>().interactable = false;
            ShopItemList[i].go.GetComponentInChildren<Toggle>().interactable = true;
            if (PlayerPrefs.GetInt("Equipped", 0).Equals(i))
            {
                ShopItemList[i].go.GetComponentInChildren<Toggle>().isOn = true;
            }
        }
    }

    public void Toggle(int i)
    {
        PlayerPrefs.SetInt("Equipped", i);
    }

    public void Purchased(int i)
    {
        PlayerPrefs.SetInt("isHave" + i, 1);
        int money = PlayerPrefs.GetInt("money");
        money -= ShopItemList[i].price;
        PlayerPrefs.SetInt("money", money);
        ShopItemList[i].IsPurchased = true;
        scriptMenuManager.UpdateMoney();
        Awake();
    }
}
