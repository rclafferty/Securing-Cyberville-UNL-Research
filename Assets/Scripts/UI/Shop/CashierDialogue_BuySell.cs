using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Scripts;
using Assets.Scripts.Testing;
using Assets.Scripts.Driving;

using Campaign;

public class CashierDialogue_BuySell : MonoBehaviour {

    ConversationTree ct;

    Button a1;
    Button a2;
    Button a3;
    Text answer1;
    Text answer2;
    Text answer3;
    Text dialogue;
    Backpack b;
    ArrayList backpack;

    GameObject back1;
    GameObject back2;

    string[] names;
    Text[] priceText;
    Text[] refundText;
    int[] prices;
    int[] refunds;
    Button[] buyAdd;
    Button[] buySubtract;
    int[] buyQuantities;
    Button[] sellAdd;
    Button[] sellSubtract;
    int[] sellQuantities;
    Text[] buyAmountText;
    Text[] sellAmountText;

    int[] storeStock;

    Text costAmount;
    int cost;
    Text refundAmount;
    int refund;

    int len;

    bool buyOrSell;

    GameObject shop_dialogue;
    GameObject shop_gui;

	// Use this for initialization
	void Start () {
        GameObject g = GameObject.Find("CampaignManager");
        CampaignManager cm = g.GetComponent<CampaignManager>();
        ct = cm.GetConversation(SpeakerType.ShopKeeper, 1);
        dialogue = GameObject.Find("Dialogue").GetComponent<Text>();
        answer1 = GameObject.Find("AnswerText1").GetComponent<Text>();
        answer2 = GameObject.Find("AnswerText2").GetComponent<Text>();
        answer3 = GameObject.Find("AnswerText3").GetComponent<Text>();
        a1 = GameObject.Find("AnswerText1").GetComponent<Button>();
        a2 = GameObject.Find("AnswerText2").GetComponent<Button>();
        a3 = GameObject.Find("AnswerText3").GetComponent<Button>();

        try
        {
            b = GameObject.Find("Player").GetComponent<PlayerControls>().GetBackpack();
            backpack = b.GetBackpack();
        }
        catch (System.NullReferenceException nre)
        {
            b = new Backpack();
            backpack = new ArrayList();
            backpack.Add(new Item(0, 3));
            backpack.Add(new Item(1, 4));
            backpack.Add(new Item(2, 5));
            backpack.Add(new Item(3, 6));
            backpack.Add(new Item(4, 500));
            b.CheatSetBackpack(backpack);
        }

        GameObject.Find("MoneyAmount").GetComponent<Text>().text = ((Item)backpack[4]).quantity.ToString();

        shop_dialogue = GameObject.Find("ShopDialogue");
        shop_gui = GameObject.Find("ShopGUI");

        costAmount = GameObject.Find("CostAmount").GetComponent<Text>();
        refundAmount = GameObject.Find("RefundAmount").GetComponent<Text>();

        ct = GameObject.Find("CampaignManager").GetComponent<CampaignManager>().GetConversation(SpeakerType.ShopKeeper, 1);
       
        /*dialogue.text = "Hello there! Welcome to my store! How may I help you today?";
        answer1.text = "Let's see what you've got.";
        answer2.text = "Nevermind";
        a1.onClick.RemoveAllListeners();
        a1.onClick.AddListener(() => { Transact(); buyOrSell = true; });
        a2.onClick.RemoveAllListeners();
        a2.onClick.AddListener(() => { SceneManager.LoadScene("city_hall_3"); });*/

        buyOrSell = false;
        back1 = GameObject.Find("Cube");
        back2 = GameObject.Find("Cube (1)");
        cost = 0;
        refund = 0;
        
        len = 4;
        names = new string[len];

        names[0] = "Overclock";
        names[1] = "VirusBeGone";
        names[2] = "WormBeGone";
        names[3] = "Invincibility";

        storeStock = new int[len];
        for (int i = 0; i < len; i++)
        {
            //storeStock[i] = ((len-1) * 478) / 131;
            storeStock[i] = (len - i) * 2;
        }

        int p = 30;
        prices = new int[len];
        for (int i = 0; i < len; i++)
        {
            prices[i] = p;
            p += 10;
        }

        priceText = new Text[len];
        for (int i = 0; i < len; i++)
        {
            priceText[i] = GameObject.Find(names[i] + "Price").GetComponent<Text>();
            priceText[i].text = "Price: " + prices[i];
        }
        
        refunds = new int[len];
        for (int i = 0; i < len; i++)
        {
            refunds[i] = prices[i] / 3;
        }

        refundText = new Text[len];
        for (int i = 0; i < len; i++)
        {
            refundText[i] = GameObject.Find(names[i] + "Refund").GetComponent<Text>();
            refundText[i].text = "Refund: " + refunds[i];
        }

        buyAdd = new Button[len];

        buyAdd[0] = GameObject.Find(names[0] + "Plus").GetComponent<Button>();
        buyAdd[0].onClick.RemoveAllListeners();
        buyAdd[0].onClick.AddListener(() => { BuyAddOne(0); });
        buyAdd[1] = GameObject.Find(names[1] + "Plus").GetComponent<Button>();
        buyAdd[1].onClick.RemoveAllListeners();
        buyAdd[1].onClick.AddListener(() => { BuyAddOne(1); });
        buyAdd[2] = GameObject.Find(names[2] + "Plus").GetComponent<Button>();
        buyAdd[2].onClick.RemoveAllListeners();
        buyAdd[2].onClick.AddListener(() => { BuyAddOne(2); });
        buyAdd[3] = GameObject.Find(names[3] + "Plus").GetComponent<Button>();
        buyAdd[3].onClick.RemoveAllListeners();
        buyAdd[3].onClick.AddListener(() => { BuyAddOne(3); });

        buySubtract = new Button[len];

        buySubtract[0] = GameObject.Find(names[0] + "Minus").GetComponent<Button>();
        buySubtract[0].onClick.RemoveAllListeners();
        buySubtract[0].onClick.AddListener(() => { BuySubtractOne(0); });
        buySubtract[1] = GameObject.Find(names[1] + "Minus").GetComponent<Button>();
        buySubtract[1].onClick.RemoveAllListeners();
        buySubtract[1].onClick.AddListener(() => { BuySubtractOne(1); });
        buySubtract[2] = GameObject.Find(names[2] + "Minus").GetComponent<Button>();
        buySubtract[2].onClick.RemoveAllListeners();
        buySubtract[2].onClick.AddListener(() => { BuySubtractOne(2); });
        buySubtract[3] = GameObject.Find(names[3] + "Minus").GetComponent<Button>();
        buySubtract[3].onClick.RemoveAllListeners();
        buySubtract[3].onClick.AddListener(() => { BuySubtractOne(3); });

        sellAdd = new Button[len];
        
        sellAdd[0] = GameObject.Find(names[0] + "SellPlus").GetComponent<Button>();
        sellAdd[0].onClick.RemoveAllListeners();
        sellAdd[0].onClick.AddListener(() => { SellAddOne(0); });
        sellAdd[1] = GameObject.Find(names[1] + "SellPlus").GetComponent<Button>();
        sellAdd[1].onClick.RemoveAllListeners();
        sellAdd[1].onClick.AddListener(() => { SellAddOne(1); });
        sellAdd[2] = GameObject.Find(names[2] + "SellPlus").GetComponent<Button>();
        sellAdd[2].onClick.RemoveAllListeners();
        sellAdd[2].onClick.AddListener(() => { SellAddOne(2); });
        sellAdd[3] = GameObject.Find(names[3] + "SellPlus").GetComponent<Button>();
        sellAdd[3].onClick.RemoveAllListeners();
        sellAdd[3].onClick.AddListener(() => { SellAddOne(3); });

        sellSubtract = new Button[len];
        
        sellSubtract[0] = GameObject.Find(names[0] + "SellMinus").GetComponent<Button>();
        sellSubtract[0].onClick.RemoveAllListeners();
        sellSubtract[0].onClick.AddListener(() => { SellSubtractOne(0); });
        sellSubtract[1] = GameObject.Find(names[1] + "SellMinus").GetComponent<Button>();
        sellSubtract[1].onClick.RemoveAllListeners();
        sellSubtract[1].onClick.AddListener(() => { SellSubtractOne(1); });
        sellSubtract[2] = GameObject.Find(names[2] + "SellMinus").GetComponent<Button>();
        sellSubtract[2].onClick.RemoveAllListeners();
        sellSubtract[2].onClick.AddListener(() => { SellSubtractOne(2); });
        sellSubtract[3] = GameObject.Find(names[3] + "SellMinus").GetComponent<Button>();
        sellSubtract[3].onClick.RemoveAllListeners();
        sellSubtract[3].onClick.AddListener(() => { SellSubtractOne(3); });

        buyQuantities = new int[len];
        for (int i = 0; i < len; i++)
            buyQuantities[i] = 0;

        sellQuantities = new int[len];
        for (int i = 0; i < len; i++)
            sellQuantities[i] = 0;

        buyAmountText = new Text[len];
        for (int i = 0; i < len; i++)
        {
            buyAmountText[i] = GameObject.Find(names[i] + "Quantity").GetComponent<Text>();
            buyAmountText[i].text = "0";
        }

        sellAmountText = new Text[len];
        for (int i = 0; i < len; i++)
        {
            sellAmountText[i] = GameObject.Find(names[i] + "SellQuantity").GetComponent<Text>();
            sellAmountText[i].text = "0";
        }

        back2.SetActive(false);
        shop_gui.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (!buyOrSell) return;

        costAmount.text = cost.ToString();
        refundAmount.text = refund.ToString();
        
        for (int item = 0; item < len; item++)
        {
            buyAmountText[item].text = buyQuantities[item].ToString() + " / " + storeStock[item];
            sellAmountText[item].text = sellQuantities[item].ToString() + " / " + ((Item)backpack[item]).quantity;
        }
    }

    void UpdateGUI ()
    {
        
    }

    void Transact()
    {
        buyOrSell = true;
        back1.SetActive(false);
        back2.SetActive(true);
        shop_dialogue.SetActive(false);
        shop_gui.SetActive(true);
    }

    public void AcceptTransaction()
    {
        int money = ((Item)backpack[4]).quantity;
        Debug.Log("Money = " + money);
        if (cost <= money + refund)
        {
            for (int i = 0; i < len; i++)
            {
                b.Use(i, sellQuantities[i]);
                storeStock[i] += sellQuantities[i];
            }

            for (int i = 0; i < len; i++)
            {
                b.Pickup(i, buyQuantities[i]);
                storeStock[i] -= buyQuantities[i];
            }

            money += refund;
            money -= cost;
            GameObject.Find("MoneyAmount").GetComponent<Text>().text = money.ToString();
            ((Item)backpack[4]).quantity = money;

            ClearTransaction();
        }
        else
        {
            //CAN'T DO TRANSACTION
        }
    }

    public void ClearTransaction()
    {
        for (int i = 0; i < len; i++)
        {
            buyQuantities[i] = 0;
            sellQuantities[i] = 0;
            buyAmountText[i].text = "0";
            sellAmountText[i].text = "0";
        }
        //costAmount.text = "0";
        cost = 0;
        refund = 0;
        //refundAmount.text = "0";
    }

    public void CancelTransaction()
    {
        buyOrSell = false;
        back1.SetActive(true);
        back2.SetActive(false);
        shop_dialogue.SetActive(true);
        shop_gui.SetActive(false);

        cost = 0;
        refund = 0;

        dialogue.text = "Is there anything else I may help you with today?";
        answer1.text = "Let's see what you've got.";
        answer2.text = "Nevermind";
        a1.onClick.RemoveAllListeners();
        a1.onClick.AddListener(() => { Transact(); buyOrSell = true; });
        a2.onClick.RemoveAllListeners();
        a2.onClick.AddListener(() => { SceneManager.LoadScene("city_hall_3"); });
    }

    public void BuyAddOne(int item)
    {
        //Debug.Log("item = " + item);
        if (buyQuantities[item] + 1 > storeStock[item]) return;

        buyQuantities[item]++;
        cost += prices[item];
        
    }

    public void BuySubtractOne(int item)
    {
        if (buyQuantities[item] - 1 < 0) return;

        //Debug.Log("item = " + item);
        buyQuantities[item]--;
        cost -= prices[item];
    }

    public void SellAddOne(int item)
    {

        if (sellQuantities[item] + 1 > ((Item)backpack[item]).quantity) return;
        //Debug.Log("item = " + item);
        sellQuantities[item]++;
        refund += refunds[item];
    }

    public void SellSubtractOne(int item)
    {
        //Debug.Log("item = " + item);
        if (sellQuantities[item] - 1 < 0) return;
        sellQuantities[item]--;
        refund -= refunds[item];
    }
}


/* while (ct.SpeakerText.Substring(1, 2) != "OG")
        {
            if (ct == null || ct.SpeakerText == null)
            {
                SceneManager.LoadScene("city_hall_3");
                return;
            }
            dialogue.text = ct.SpeakerText;
            answer1.text = ct.Responses[0];
            try
            {
                answer2.text = ct.Responses[1];
            }
            catch (System.Exception nre)
            {
                answer2.text = "";
            }
            try
            {
                answer3.text = ct.Responses[2];
            }
            catch (System.Exception nre)
            {
                answer3.text = "";
            }

            //ct = ct.NextTrees[i - 1];

            Debug.Log("SpeakerText = " + ct.SpeakerText);
            if (ct.SpeakerText.Substring(1, 2) == "SE")
            {
                Debug.Log("SpeakerText = " + ct.SpeakerText);
                //SE -- SEND EVENT
                string s = ct.SpeakerText.Substring(4);
                GameObject.Find("CampaignManager").GetComponent<CampaignManager>().CheckEvent(new SpeakEvent(SpeakerType.ShopKeeper, 1, s));
                ct = ct.NextTrees[0];
            }
        }
 */