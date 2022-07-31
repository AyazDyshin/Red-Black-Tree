using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Color;



enum Color {R,B}

namespace redblacktreet
{
   
    class Node
    {

        public int value;
        public Color color;
        public Node left;
        public Node right;
        public Node parent;
        public static Node Null = new Node(B);
        public Node(int value, Color color, Node left, Node right, Node parent)
        {
            this.value = value;
            this.color = color;
            this.left = left;
            this.right = right;
            this.parent = parent;

        }
        public Node(int value, Node left, Node right, Node parent)
        {
            this.value = value;
            this.left = left;
            this.right = right;
            this.parent = parent;

        }
        public Node(Color color)
        {

            this.color = color;

        }


    }
    class Tree
    {
        public Node root;
        public Tree()
        {
            root = Node.Null;
        }
        public  bool contains(int val, Node N)
        {
            if (N == Node.Null)
            {

                return false;

            }


            else if (N.value == val)
            {
                return true;
            }
            else if (N.value < val)
            {
                return contains(val, N.right);
            }
            else
            {
                return contains(val, N.left);
            }

        }
        private  void LeftRotate( Node x)
        {
            var y = x.right;
            x.right = y.left;
            if (y.left != Node.Null)
            {
                y.left.parent = x;
            }
            y.parent = x.parent;
            if (x.parent == Node.Null)
            {
                root = y;
            }
            else if (x == x.parent.left)
            {


                x.parent.left = y;
            }
            else
            {
                x.parent.right = y;
            }

            y.left = x;
            x.parent = y;
        }
        private  void RightRotate( Node x)
        {
            var y = x.left;
            x.left = y.right;
            if (y.right != Node.Null)
            {
                y.right.parent = x;
            }
            y.parent = x.parent;
            if (x.parent == Node.Null)
            {
                root = y;
            }
            else if (x == x.parent.right)
            {


                x.parent.right = y;
            }
            else
            {
                x.parent.left = y;
            }

            y.right = x;
            x.parent = y;
        }
        private  void insertFixUp (Node z)
        {

            while (z.parent.color == R)
            {
                if (z.parent == z.parent.parent.left)
                {
                    var y = z.parent.parent.right;
                    if (y.color == R)
                    {
                        z.parent.color = B;
                        y.color = B;
                        z.parent.parent.color = R;
                        z = z.parent.parent;
                    }
                    else
                    {
                        if (z == z.parent.right)
                        {
                            z = z.parent;
                            this.LeftRotate( z);


                        }
                        z.parent.color = B;
                        z.parent.parent.color = R;
                        this.RightRotate( z.parent.parent);
                    }
                }
                else
                {
                    var y = z.parent.parent.left;
                    if (y.color == R)
                    {
                        z.parent.color = B;
                        y.color = B;
                        z.parent.parent.color = R;
                        z = z.parent.parent;
                    }
                    else
                    {
                        if (z == z.parent.left)
                        {
                            z = z.parent;
                            this.RightRotate(z);


                        }
                        z.parent.color = B;
                        z.parent.parent.color = R;
                        this.LeftRotate(z.parent.parent);
                    }
                }
            }
            root.color = B;
        }
        public bool insert( int f)
        {
            if (contains(f, root))
            {
                return false;
            }
            else
            {
                Node z = new Node(f, Node.Null, Node.Null, Node.Null);

                var y = Node.Null;
                var x = root;
                while (x != Node.Null)
                {
                    y = x;
                    if (z.value < x.value)
                    {
                        x = x.left;

                    }
                    else
                    {
                        x = x.right;
                    }
                }
                z.parent = y;
                if (y == Node.Null)
                {
                    root = z;

                }
                else
                {
                    if (z.value < y.value)
                    {
                        y.left = z;
                    }
                    else
                    {
                        y.right = z;
                    }
                }

                z.left = Node.Null;
                z.right = Node.Null;
                z.color = R;
                this.insertFixUp(z);
                return true;
            }
        }
        private static Node containsGiveMeNode(int val, Node N)
        {
            if (N == null)
            {

                return null;

            }


            else if (N.value == val)
            {
                return N;
            }
            else if (N.value < val)
            {
                return containsGiveMeNode(val, N.right);
            }
            else
            {
                return containsGiveMeNode(val, N.left);
            }

        }
        private static Node GiveMedeleteReplacement(Node t)
        {

            Node temp = t.right;
            while (temp.left != Node.Null)
            {

                temp = temp.left;
            }
            return temp;
        }
        public  void deleteFixUp( Node x)
        {
            while ((x != root) && (x.color == B))
            {
                if (x == x.parent.left)
                {
                    var w = x.parent.right;
                    if (w.color == R)
                    {
                        w.color = B;
                        x.parent.color = R;
                        this.LeftRotate( x.parent);
                        w = x.parent.right;
                    }
                    if ((w.left.color == B) && (w.right.color == B))
                    {
                        w.color = R;
                        x = x.parent;

                    }
                    else
                    {
                        if (w.right.color == B)
                        {
                            w.left.color = B;
                            w.color = R;
                            this.RightRotate( w);
                            w = x.parent.right;
                        }
                        w.color = x.parent.color;
                        x.parent.color = B;
                        w.right.color = B;
                        this.LeftRotate( x.parent);
                        x = root;

                    }
                }
                else
                {
                    var w = x.parent.left;
                    if (w.color == R)
                    {
                        w.color = B;
                        x.parent.color = R;
                        this.RightRotate(x.parent);
                        w = x.parent.left;
                    }
                    if ((w.right.color == B) && (w.left.color == B))
                    {
                        w.color = R;
                        x = x.parent;

                    }
                    else
                    {
                        if (w.left.color == B)
                        {
                            w.right.color = B;
                            w.color = R;
                            this.LeftRotate( w);
                            w = x.parent.left;
                        }
                        w.color = x.parent.color;
                        x.parent.color = B;
                        w.left.color = B;
                        this.RightRotate(x.parent);
                        x = root;

                    }
                }
            }
            x.color = B;
        }
        public  bool delete( int f)
        {
            Node x;
            Node y;
            if (contains(f, root) == false)
            {
                return false;
            }
            else
            {
                var z = containsGiveMeNode(f, root);

                if (z.left == Node.Null || z.right == Node.Null)
                {
                    y = z;
                }
                else
                {
                    y = GiveMedeleteReplacement(z);

                }
                if (y.left != Node.Null)
                {
                    x = y.left;
                }
                else
                {
                    x = y.right;
                }

                x.parent = y.parent;

                if (y.parent == Node.Null)
                {
                    root = x;
                }
                else
                {
                    if (y == y.parent.left)
                    {
                        y.parent.left = x;
                    }
                    else
                    {
                        y.parent.right = x;
                    }
                }
                if (y != z)
                {
                    z.value = y.value;
                }
                if (y.color == B)
                {
                    this.deleteFixUp(x);
                }
            }
            return true;
        }
        private int BlackHeight(Node n, int counter)
        {
            if (n == null)
            {
                return 1;
            }
            if (n.color == B)
            {
                counter++;

            }
            if ((n.left == null) && (n.right == null))
            {
                return counter;
            }
            else if ((n.left != null) && (n.right == null))
            {
                if (counter == BlackHeight(n.left, counter))
                {
                    return counter;
                }
                else
                {

                    return -1;


                }
            }
            else if ((n.left == null) && (n.right != null))
            {
                if (counter == BlackHeight(n.right, counter))
                {
                    return counter;
                }
                else
                {

                    return -1;

                }
            }
            else if ((n.left != null) && (n.right != null))
            {
                if (BlackHeight(n.left, counter) != BlackHeight(n.right, counter))
                {

                    return -1;
                }
                else
                {
                    return BlackHeight(n.left, counter);

                }
            }

            else
            {

                return -1;
            }

        }
        private  bool RedCondition(Node n)
        {
            if (n == Node.Null)
            {
                return true;
            }
            if (n.color == R)
            {
                if ((n.left.color == R) || (n.right.color == R))
                {
                    return false;
                }

            }
            if (n.left != Node.Null)
            {
                return RedCondition(n.left);
            }
            if (n.right != Node.Null)
            {
                return RedCondition(n.right);
            }
            return true;
        }
        private  bool rootBlack(Tree T)
        {
            if (T.root.color == B)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public  bool Check()
        {
            if ((BlackHeight(root, 0) != -1) && (RedCondition(root) != false) && (rootBlack(this) != false))
            {
                return true;
            }
            else return false;
        }
        public static void Print(Node p, int padding)
        {
            if (p != Node.Null)
            {
                if (p.right != Node.Null)
                {
                    Print(p.right, padding + 4);
                }
                if (padding > 0)
                {
                    Console.Write(" ".PadLeft(padding));
                }
                if (p.right != Node.Null)
                {
                    Console.Write("/\n");
                    Console.Write(" ".PadLeft(padding));
                }
                Console.Write(p.value.ToString() + "" + p.color.ToString() + "\n ");
                if (p.left != Node.Null)
                {
                    Console.Write(" ".PadLeft(padding) + "\\\n");
                    Print(p.left, padding + 4);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            Tree T = new Tree();
            Random random = new Random();
            for (int i = 1; i <= 10000; i++)
            {

                int num = random.Next(10000);
                T.insert(num);
            }

            if (T.Check())
            {
                Console.WriteLine("after inserting random number, result tree is RedBlack Tree");
            }


            while (T.root != Node.Null)
            {
                T.delete( T.root.value);

            }
            Console.WriteLine("deletion done");
            if (T.root == Node.Null)
            {
                Console.WriteLine("Tree is empty");
            }
        }
    }
}