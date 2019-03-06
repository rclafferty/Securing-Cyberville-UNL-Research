using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Assets.Scripts;
using Assets.Scripts.UserSessionLogger;



namespace Assets.Scripts.Driving
{
    public class Backpack
    {
        GameObject backing;
        GameObject backpackText;
        GameObject itemText;

        ArrayList specialItems;
        ArrayList backpack;

        GameObject[] itemTexts;

        string[] items;
        //int[] itemQuantity;

        public Backpack()
        {
            backing = GameObject.Find("Backing");
            backpackText = GameObject.Find("BackpackText");
            itemText = GameObject.Find("ItemText");

            specialItems = new ArrayList();

            int item_length = 5;

            backpack = new ArrayList();
            Item overclocks = new Item(0, 0);
            backpack.Add(overclocks);
            Item virusBeGone = new Item(1, 0);
            backpack.Add(virusBeGone);
            Item wormBeGone = new Item(2, 0);
            backpack.Add(wormBeGone);
            Item invincibility = new Item(3, 0);
            backpack.Add(invincibility);
            Item money = new Item(4, 0);
            backpack.Add(money);

            //initialize item names
            items = new string[item_length];
            items[0] = "Overclock";
            items[1] = "VirusBeGone";
            items[2] = "WormBeGone";
            items[3] = "Invincibility10";
            items[4] = "Money";

            itemTexts = new GameObject[item_length];
            for (int i = 0; i < item_length; i++)
                itemTexts[i] = GameObject.Find(items[i] + "Quantity");
        }

        public void ShowBag()
        {
            backing.SetActive(true);
            backpackText.SetActive(true);
            itemText.SetActive(true);
            UpdateBackpack();
        }

        public void HideBag()
        {
            backing.SetActive(false);
            backpackText.SetActive(false);
            itemText.SetActive(false);
        }

        public void Pickup(int i, int q)
        {
            if (i < backpack.Count)
            {
                ((Item)backpack[i]).Add(q);
            }
            else
            {
                Item item = new Item(i, q);
                backpack.Add(item);
            }
            
            GameObject.Find("UserSessionLogger").GetComponent<UserSessionLoggerBehavior>().LogToDatabase("Picked up " + ToTitleCase(items[i]), UserSessionLoggerBehavior.UserAction.PickupItem);
        }

        private string ToTitleCase(string message)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(message.ToLower());
        }

        public void Use(int i, int q)
        {
            if (i < backpack.Count)
            {
                ((Item)backpack[i]).Subtract(q);
            }
            else
            {
                //CANNOT USE ITEM
                Debug.Log("Cannot use that item");
            }
            GameObject.Find("UserSessionLogger").GetComponent<UserSessionLoggerBehavior>().LogToDatabase("Used " + ToTitleCase(items[i]), UserSessionLoggerBehavior.UserAction.UsedItem);
        }

        //cheat set backpack names and quantities
        public void CheatSetBackpack(ArrayList i)
        {
            SetBackpack(i);
        }

        void UpdateBackpack()
        {
            for (int i = 0; i < itemTexts.Length; i++)
            {
                itemTexts[i].GetComponent<Text>().text = ((Item)backpack[i]).quantity.ToString();
            }
        }

        public void AddSpecialItem (int id, int quantity)
        {   //NEED TO FIND IMPLEMENTATION FOR THIS IN GAMEPLAY
            if (id < specialItems.Count)
            {
                ((Item)specialItems[id]).Add(quantity);
            }
            else
            {
                Item item = new Item(id, quantity);
                specialItems.Add(item);
            }
        }

        public string LookupItemName(int id)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (id == i)
                    return items[i];
            }

            return "n/a";
        }

        public int LookupItemID(string name)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (name == items[i] || name == items[i] + "(Clone)")
                    return i;
            }
            return -1;
        }

        public ArrayList GetBackpack()
        {
            return backpack;
        }

        public void SetBackpack(ArrayList a)
        {
            backpack = a;
        }
    }
}
