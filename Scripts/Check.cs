using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Check : MonoBehaviour
{
    public bool BlackOccupied;
    public bool WhiteOccupied;
    public int Value; 

    //public Check()
    //{
    //    WhiteOccupied = false; 
    //    BlackOccupied = false;
    //    Value = 0; 
    //}

    public void setOccupancy(int value, string colour, bool b)
    {        
        if (colour.ToLower() == "white")
        {
            WhiteOccupied = b; 
        }
        else if (colour.ToLower() == "black")
        {
            BlackOccupied = b; 
        }
        Value = value; 
    }

    public bool isOccupied(string colour)
    {
        if (colour.ToLower() == "white")
        {
            return WhiteOccupied;
        }
        else if (colour.ToLower() == "black")
        {
            return BlackOccupied; 
        }
        else
        {
            return true;
        }
    }
    public bool isOccupied()
    {
        if (BlackOccupied || WhiteOccupied)
        {
            return true; 
        }
        else { return false; }
    }
}

