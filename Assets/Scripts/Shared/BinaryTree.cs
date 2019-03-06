using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class BinaryTree
    {
        public bool complete;
        public string dialogue;
        public int step;
        public BinaryTree left;
        public BinaryTree right;

        public string answer1Text;
        public string answer2Text;

        public BinaryTree()
        {
            left = null;
            right = null;
            complete = false;
            dialogue = "";
        }
    }
}
