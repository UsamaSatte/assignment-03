
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

 public class Point 
{ 
    private float[] coord; // coordinate storage 

    // Constructor to create a zero point with a given dimension
    public Point(int dim) 
    {
        coord = new float[dim];
        for (int i = 0; i < dim; i++) {
            coord[i] = 0;
        }
    } 

    // Getter method to retrieve the dimension of the point
    public int GetDim() 
    { 
        return coord.Length; 
    } 

    // Getter method to retrieve the value of a coordinate
    public float Get(int i) 
    { 
        return coord[i]; 
    } 

    // Setter method to set the value of a coordinate
    public void Set(int i, float x) 
    { 
        coord[i] = x;
    } 

    // Method to check if two points are equal
    public bool Equals(Point other) 
    { 
        if (other == null || other.GetDim() != GetDim()) {
            return false;
        }
        for (int i = 0; i < GetDim(); i++) {
            if (coord[i] != other.Get(i)) {
                return false;
            }
        }
        return true;
    } 

    // Method to compute the Euclidean distance between two points
    public float DistanceTo(Point other) 
    { 
        if (other == null || other.GetDim() != GetDim()) {
            return float.NaN;
        }
        float sum = 0;
        for (int i = 0; i < GetDim(); i++) {
            float diff = coord[i] - other.Get(i);
            sum += diff * diff;
        }
        return (float)Math.Sqrt(sum);
    } 

    // Method to convert the point to a string representation
    public override string ToString() 
    { 
        StringBuilder sb = new StringBuilder();
        sb.Append("(");
        for (int i = 0; i < GetDim(); i++) {
            sb.Append(coord[i]);
            if (i < GetDim() - 1) {
                sb.Append(", ");
            }
        }
        sb.Append(")");
        return sb.ToString();
    }
}

class Program {
    static void Main(string[] args) {
        // Create points with different dimensions
        Point p1 = new Point(2);
        Point p2 = new Point(3);
        Point p3 = new Point(2);
        Point p4 = new Point(2);

        //suffix "f" in the input values is for telling the compiler to treat the values as float

        // Set the coordinates of the first point
        p1.Set(0, 1.0f);
        p1.Set(1, 2.0f);

        // Set the coordinates of the second point
        p2.Set(0, 3.0f);
        p2.Set(1, 4.0f);
        p2.Set(2, 5.0f);

        // Set the coordinates of the third point
        p3.Set(0, 6.0f);
        p3.Set(1, 7.0f);

        // Set the coordinates of the fourth point
        p4.Set(0, 6.0f);
        p4.Set(1, 7.0f);

        //Test Print the coordinates of the points
        Console.WriteLine("p1 = " + p1.ToString()); //p1 = (1, 2)
        Console.WriteLine("p2 = " + p2.ToString()); //p2 = (3, 4, 5)
        Console.WriteLine("p3 = " + p3.ToString()); //p3 = (6, 7)
        Console.WriteLine("p4 = " + p4.ToString()); //p4 = (6, 7)

        // Test 1 Compute the distance between the p1 and p2 points
        float dist = p1.DistanceTo(p2);
        Console.WriteLine("distance between p1 and p2 = " + dist);//output: NaN (Not a Number) Because for the different dimensions of the input.

        // Test 2 Compute the distance between the p3 and p4 points
        float dist_p = p3.DistanceTo(p4);
        Console.WriteLine("distance between p3 and p4 = " + dist_p);//output = 0

        // Test 3 Check if the p1 and p2 points are equal
        bool eq = p1.Equals(p2);
        Console.WriteLine("p1 and p2 are equal: " + eq);//False

        // Test 4 Check if the p3 and p4 points are equal
        bool eq_p = p3.Equals(p4);
        Console.WriteLine("p3 and p4 are equal: " + eq_p);//True

        // Test 5
        Console.WriteLine($"p1 dim: {p1.GetDim()}"); // output: p1 dim: 2
        Console.WriteLine($"p2 dim: {p2.GetDim()}"); // output: p2 dim: 3

        // Test 6
        Console.WriteLine($"p1 x: {p1.Get(0)}"); // output: p1 x: 1
        Console.WriteLine($"p1 y: {p1.Get(1)}"); // output: p1 y: 2
        
        // Test 7
        Console.WriteLine($"p2 x: {p2.Get(0)}"); // output: p2 x: 3
        Console.WriteLine($"p2 y: {p2.Get(1)}"); // output: p2 y: 4
    }
}
