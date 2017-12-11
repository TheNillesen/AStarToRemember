using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aStarLooksForKeys
{
    class Node
    {
        public MyType myType;

        public string symbole;

        public Node(MyType myType, string symbole)
        {
            this.myType = myType;
            this.symbole = symbole;
        }


    }
}
