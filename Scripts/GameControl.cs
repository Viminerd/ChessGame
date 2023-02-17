using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameControl
{
    public static string Player = "White";
    public static string Bot = "Black";
    public static string WhosTurn = "White";
    public static string Game;
    public static bool GameRunning = false; 
    public static bool CheckMate = false; 
    public static GameObject DeadPieces = GameObject.Find("DeadPieces");
    public static int WDeath = 0;
    public static int BDeath = 15;

    public static bool enPassant = false; 
    public static string enPassantMove;
    public static Pieces enPassantPawn;
    public static string enPassantKillMove;

    public static King WhiteKing;
    public static King BlackKing;
    public void Start()
    {
        CheckMate = false;
    }
    public static void SwitchTurn()
    {
        if (WhosTurn == "White" || WhosTurn == "W")
        {
            WhosTurn = "Black";
        }
        else if (WhosTurn == "Black" || WhosTurn == "B")
        {
            WhosTurn = "White"; 
        }

        if (CheckMate == false)
        {
            CameraRotate.rotateCam();

        }

    }

    public static bool IsOccupied(int let, int num, string colourtocheck)
    {
        if (MoveAllowed(let, num)){
            GameObject check = GameObject.Find(Movement.IntToString(let, num));
            return check.GetComponent<Check>().isOccupied(colourtocheck);
        }
        else { return false; }
    }

    public static bool IsOccupied(int let, int num)
    {
        if (MoveAllowed(let, num)){
            GameObject check = GameObject.Find(Movement.IntToString(let, num));
            return check.GetComponent<Check>().isOccupied();
        }
        else { return false; }
    }
    public static void MoveAllowed(List<string> L, int let, int num)
    {              
        if (let < 9 && let > 0 && num < 9 && num > 0)
        {
            L.Add(Movement.IntToString(let, num));
        }
    }
    public static bool MoveAllowed(int let, int num)
    {
        return (let < 9 && let > 0 && num < 9 && num > 0);

    }
}