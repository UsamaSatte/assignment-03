////////////////////////////////////////////////////////////////////////////////////////////////////////
//Code Written In: C#                                                                                 //
//Written By: Usama Mahmood                                                                           //
//Methods: Insert, Delete, Contains, FindMin, Print.                                                  //
//Description: This is a C# implementation of quadtree data structures, where each internal node has  //
//four children, as four quadrants.                                                                   //
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

public class PointQuadtree
{
    private class Node //Node has a Point field and four child nodes (NW, NE, SW, and SE)
    {
        public Point point;
        public Node NW;
        public Node NE;
        public Node SW;
        public Node SE;

        public Node(Point p)
        {
            point = p;
            NW = null;
            NE = null;
            SW = null;
            SE = null;
        }
    }

    private Node root;

    public PointQuadtree()
    {
        root = null;
    }

    public bool Insert(Point p) //Insert method inserts a new point into the tree.
    {
        if (root == null)
        {
            root = new Node(p);
            return true;
        }
        else
        {
          float xmin = float.MinValue;
          float ymin = float.MinValue;
          float xmax = float.MaxValue;
          float ymax = float.MaxValue;
        return Insert(root, p, xmin, ymin, xmax, ymax);
       // return Insert(root, p, float.MinValue, float.MinValue, float.MaxValue, float.MaxValue);
        }
    }

    private bool Insert(Node node, Point p, float xmin, float ymin, float xmax, float ymax)
    {
        if (node.point.Equals(p))
        {
            return false; // Duplicate point not inserted
        }

        if (node.point.X >= p.X)
        {
            if (node.point.Y >= p.Y)
            {
                if (node.NW == null)
                {
                    node.NW = new Node(p);
                    return true;
                }
                else
                {
                    return Insert(node.NW, p, xmin, ymin, node.point.X, node.point.Y);
                }
            }
            else
            {
                if (node.SW == null)
                {
                    node.SW = new Node(p);
                    return true;
                }
                else
                {
                    return Insert(node.SW, p, xmin, node.point.Y, node.point.X, ymax);
                }
            }
        }
        else
        {
            if (node.point.Y >= p.Y)
            {
                if (node.NE == null)
                {
                    node.NE = new Node(p);
                    return true;
                }
                else
                {
                    return Insert(node.NE, p, node.point.X, ymin, xmax, node.point.Y);
                }
            }
            else
            {
                if (node.SE == null)
                {
                    node.SE = new Node(p);
                    return true;
                }
                else
                {
                    return Insert(node.SE, p, node.point.X, node.point.Y, xmax, ymax);
                }
            }
        }
    }

    public bool Delete(Point p) //Delete method removes a point from the tree.
    {
        if (root == null)
        {
            return false; // Empty tree
        }
        else
        {
            return Delete(root, p, float.MinValue, float.MinValue, float.MaxValue, float.MaxValue, null);
        }
    }

    private bool Delete(Node node, Point p, float xmin, float ymin, float xmax, float ymax, Node parent)
    {
        if (node == null)
        {
            return false; // Point not found
        }

        if (node.point.Equals(p))
        {
            if (node.SE != null)
            {
                Node min = FindMin(node.SE);
                node.point = min.point;
                Delete(node.SE, min.point, node.point.X, node.point.Y, xmax, ymax, node);
            }
            else if (node.NE != null)
            {
                Node min = FindMin(node.NE);
                node.point = min.point;
                Delete(node.NE, min.point, node.point.X, ymin, xmax, node.point.Y, node);
            }
            else if (node.SW != null)
             {
             Node min = FindMin(node.SW);
             node.point = min.point;
             Delete(node.SW, min.point, xmin, node.point.Y, node.point.X, ymax, node);
             }
             else if (node.NW != null)
             {
              Node min = FindMin(node.NW);
              node.point = min.point;
              Delete(node.NW, min.point, xmin, ymin, node.point.X, node.point.Y, node);
             }
             else
             {
                if (parent == null)
                {
                 root = null;
                }
                else if (parent.SE == node)
                {
                 parent.SE = null;
                }
                else if (parent.NE == node)
                {
                 parent.NE = null;
                }
                else if (parent.SW == node)
                {
                 parent.SW = null;
                }
                else if (parent.NW == node)
                {
                 parent.NW = null;
                }

                 return true;
             }
        }

        if (node.point.X >= p.X)
        {
         if (node.point.Y >= p.Y)
         {
            return Delete(node.NW, p, xmin, ymin, node.point.X, node.point.Y, node);
         }
         else
         {
            return Delete(node.SW, p, xmin, node.point.Y, node.point.X, ymax, node);
         }
        }
        else
      {
        if (node.point.Y >= p.Y)
        {
            return Delete(node.NE, p, node.point.X, ymin, xmax, node.point.Y, node);
        }
        else
        {
            return Delete(node.SE, p, node.point.X, node.point.Y, xmax, ymax, node);
        }
      }
    return false; // default return statement
    }

    private Node FindMin(Node node) //FindMin method finds the minimum node in a subtree, which is used by the Delete method.
   {
     while (node.NW != null || node.NE != null || node.SW != null || node.SE != null)
     {
        if (node.NW != null)
        {
            node = node.NW;
        }
        else if (node.NE != null)
        {
            node = node.NE;
        }
        else if (node.SW != null)
        {
            node = node.SW;
        }
        else if (node.SE != null)
        {
            node = node.SE;
        }
      }
      return node;
    }

    public bool Contains(Point p) //Contains method checks whether a point is in the tree.
{
    if (root == null)
    {
        return false;
    }
    else
    {
        return Contains(root, p);
    }
}
//Contains method also searches the tree recursively to find the node with the point being searched for.
private bool Contains(Node node, Point p)
{
    if (node == null)
    {
        return false;
    }

    if (node.point.Equals(p))
    {
        return true;
    }

    if (node.point.X >= p.X)
    {
        if (node.point.Y >= p.Y)
        {
            return Contains(node.NW, p);
        }
        else
        {
            return Contains(node.SW, p);
        }
    }
    else
    {
        if (node.point.Y >= p.Y)
        {
            return Contains(node.NE, p);
        }
        else
        {
            return Contains(node.SE, p);
        }
    }
}



  public void Print()
 {
    if (root == null)
    {
        Console.WriteLine("Empty tree");
    }
    else
    {
        Print(root);
    }
 }

 private void Print(Node node)
 {
    if (node == null)
    {
        return;
    }
    
    Console.WriteLine("(" + node.point.X + ", " + node.point.Y + ")");
    
    Print(node.NW);
    Print(node.NE);
    Print(node.SW);
    Print(node.SE);
  }



}

class MainClass {
  static void Main(string[] args) {
    // Create a new PointQuadtree
    PointQuadtree quadtree = new PointQuadtree();

    // Insert some points
    quadtree.Insert(new Point(1, 2));
    quadtree.Insert(new Point(3, 4));
    quadtree.Insert(new Point(5, 6));
    quadtree.Insert(new Point(7, 8));
    quadtree.Insert(new Point(9, 10));

    // Test 1 the Insert method
    Console.WriteLine("PointQuadtree after insertions:");
    quadtree.Print();

    // Test 2 Contains if a point exists in the tree
    Point p_1 = new Point(7, 4);
    bool contains = quadtree.Contains(p_1);
    Console.WriteLine("The tree {0} the point ({1}, {2}).", contains ? "contains" : "does not contain", p_1.X, p_1.Y);

    // Test 2.1 Contains if a point exists in the tree
    Point p_2 = new Point(6, 5);
    bool contains_2 = quadtree.Contains(p_2);
    Console.WriteLine("The tree {0} the point ({1}, {2}).", contains_2 ? "contains" : "does not contain", p_2.X, p_2.Y);

    // Test 2.2 Contains if a point exists in the tree
    Point p_3 = new Point(9, 10);
    bool contains_3 = quadtree.Contains(p_3);
    Console.WriteLine("The tree {0} the point ({1}, {2}).", contains_3 ? "contains" : "does not contain", p_3.X, p_3.Y);

    // Test 3 the Delete method
    quadtree.Delete(new Point(3, 4));
    Console.WriteLine("PointQuadtree after 1st deletion:");
    quadtree.Print();

    // Test 3.1 the Delete method
    quadtree.Delete(new Point(11, 14));
    Console.WriteLine("PointQuadtree after 2nd deletion:");
    quadtree.Print();

    // Test 3.2 the Delete method
    quadtree.Delete(new Point(1, 2));
    Console.WriteLine("PointQuadtree after 3rd deletion:");
    quadtree.Print();

}
}