/*
Assignment: 3, KDTree implementation
Authors: Noah Van Drine, Autumn Meddings
Language: Microsoft Visual Studios- C#
Group Members: Noah Van Drine, Autumn Meddings 0697772, Usama Mahmood
Date: April 10th, 2023
*/
using System; git check git check

public class Point
{
    private float[] coord; // coordinate storage
    public Point(int dim) { coord = new float[dim]; }
    public int GetDim() { return coord.Length; }
    public float Get(int i) { return coord[i]; }
    public void Set(int i, float x) { coord[i] = x; }
    public bool Equals(Point p) { 
        if (p.GetDim() != coord.Length)
            return false;
        for (int i = 0; i < coord.Length; i++)
            if (coord[i] != p.Get(i))
                return false;
        return true;
    }
    public float distanceTo(Point p) {
        if (p.GetDim() != coord.Length)
            throw new ArgumentException("Dimensions don't match");
        float sum = 0;
        for (int i = 0; i < coord.Length; i++) {
            float d = coord[i] - p.Get(i);
            sum += d * d;
        }
        return (float)Math.Sqrt(sum);
    }
    public override String ToString() { 
        return "(" + String.Join(", ", coord) + ")";
    }
}

public class KDTree
{
    public class KDNode
    {
        public Point point;
        public KDNode left;
        public KDNode right;
        public int cutDim;

        // node constructor
        public KDNode(Point point, int cutDim)
        {
            this.point = point;
            this.cutDim = cutDim;
            left = right = null;
        }

        public bool InLeftSubtree(Point x)
        {
            return x.Get(cutDim) < point.Get(cutDim);
        }
    }

    private KDNode root;

    public KDTree()
    {
        root = null;
    }

    public bool Insert(Point p)
    {
        try
        {
            root = insert(p, root, 0);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool Delete(Point p)
    {
        try
        {
            root = delete(p, root, 0);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool Contains(Point p)
    {
        return find(p, root, 0) != null;
    }

    public KDNode insert(Point x, KDNode p, int cutDim)
    {
        if (p == null)
            p = new KDNode(x, cutDim);
        else if (p.point.Equals(x))
            throw new Exception("Duplicate point");
        else if (p.InLeftSubtree(x))
            p.left = insert(x, p.left, (p.cutDim + 1) % x.GetDim());
        else
            p.right = insert(x, p.right, (p.cutDim + 1) % x.GetDim());
        return p;
    }

    private KDNode delete(Point x, KDNode p, int cutDim)
    {
        if (p == null)
            throw new Exception("Point not found");
        else if (p.point.Equals(x))
        {
            if (p.right != null)
            {
                KDNode minRight = findMin(p.right, (cutDim + 1) %
                x.GetDim());

p.point = minRight.point;
p.right = delete(minRight.point, p.right, (cutDim + 1) %

x.GetDim());
}
else if (p.left != null)
{
KDNode minLeft = findMin(p.left, (cutDim + 1) %

x.GetDim());

p.point = minLeft.point;
p.right = delete(minLeft.point, p.left, (cutDim + 1) %

x.GetDim());

p.left = null;
}
else
{
p = null;
}
}
else if (p.InLeftSubtree(x))
{
p.left = delete(x, p.left, (cutDim + 1) % x.GetDim());
}
else
{
p.right = delete(x, p.right, (cutDim + 1) % x.GetDim());
}
return p;
}
private KDNode find(Point x, KDNode p, int cutDim)
{
if (p == null)
{
return null;
}
else if (p.point.Equals(x))
{
return p;
}
else if (p.InLeftSubtree(x))
{
return find(x, p.left, (cutDim + 1) % x.GetDim());
}
else
{
return find(x, p.right, (cutDim + 1) % x.GetDim());
}
}
private KDNode findMin(KDNode p, int cutDim)
{
    if (p == null)
    {
        return null;
    }
    else if (p.left == null)
    {
        return p;
    }
    else
    {
        return findMin(p.left, (cutDim + 1) % p.point.GetDim());
    }
}

public void Print()
{
    Print(root);
}

private void Print(KDNode node)
{
    if (node != null)
    {
        Print(node.left);
        Console.WriteLine(node.point.ToString());
        Print(node.right);
    }
}


}

class MainClass
{
static void Main(string[] args)
{
    KDTree tree = new KDTree();
     //Test 1 Insert
    // create some points and add them to the tree
    Point p1 = new Point(2);
    p1.Set(0, 1);
    p1.Set(1, 2);
    tree.Insert(p1);

    Point p2 = new Point(2);
    p2.Set(0, 3);
    p2.Set(1, 4);
    tree.Insert(p2);

    Point p3 = new Point(2);
    p3.Set(0, 5);
    p3.Set(1, 6);
    tree.Insert(p3);

    tree.Print();

     //Test 2 Contains
    // check if the tree contains a point
    Point p4 = new Point(2);
    p4.Set(0, 1);
    p4.Set(1, 2);
    Console.WriteLine(tree.Contains(p4)); // should output "True"

    //Test 2.2 Contains
    // check if the tree contains a point
    Point p5 = new Point(2);
    p5.Set(0, 1);
    p5.Set(1, 8);
    Console.WriteLine(tree.Contains(p5)); // should output "False"

    //Test 2.3 Contains
    // check if the tree contains a point
    Point p6 = new Point(2);
    p6.Set(0, 5);
    p6.Set(1, 6);
    Console.WriteLine(tree.Contains(p6)); // should output "True"

        //Test 3 Delete a point
    Point p7 = new Point(2);
    p7.Set(0, 5);
    p7.Set(1, 6);
    Console.WriteLine(tree.Delete(p7)); // should output "True" and delete (5, 6)
        tree.Print();

        //Test 3.2 Delete a point
    Point p8 = new Point(2);
    p8.Set(0, 3);
    p8.Set(1, 8);
    Console.WriteLine(tree.Delete(p8)); // should output "False" Point not found
        tree.Print();

        //Test 3.3 Delete a point
    Point p9 = new Point(2);
    p9.Set(0, 1);
    p9.Set(1, 2);
    Console.WriteLine(tree.Delete(p9)); // should output "True" and delete (1, 2)
        tree.Print();
}
}
