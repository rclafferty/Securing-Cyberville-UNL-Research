using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts
{
    class Item
    {
        public int id;
        public int quantity;

        public Item(int i)
        {
            id = i;
            quantity = 1;
        }

        public Item(int i, int q)
        {
            id = i;
            quantity = q;
        }

        public void Add(int i)
        {
            quantity += i;
            //Debug.Log(quantity);
        }

        public void Subtract(int i)
        {
            quantity -= i;
        }
    }
}
