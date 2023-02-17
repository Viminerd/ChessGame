using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Pieces PieceToMove, PieceToReplace;

    
    public void Start()
    {
        if (GameControl.Player == "White")
        {
            
            Player.playerKing = GameObject.Find("WKing").GetComponent<Pieces>();
            Bot.botKing = GameObject.Find("BKing").GetComponent<Pieces>(); ; 
            Player.StartUp();
        }
        else if (GameControl.Bot == "White")
        {
            Player.playerKing = GameObject.Find("BKing").GetComponent<Pieces>();
            Bot.botKing = GameObject.Find("WKing").GetComponent<Pieces>();
            Bot.StartUp(); 
        }
        GameControl.WhosTurn = "B"; 


    }

    public Vector3 getVector(string NewPos)
    {
        if (GameObject.Find(NewPos) != null)
        {
            return GameObject.Find(NewPos).transform.position;
        }
        else
        {
            return new Vector3(0.0f, 0.0f, 0.0f); 
        }          
    }
    private void OnMouseDown()
    {
        Debug.Log(Player.playerKing.GetPosition());

              

    }

    public static List<int> GetPosInNum(string str)
    {
        int let = str[0];
        int number = str[1] - '0';
        int letter;
        switch (let)
        {
            case 'A':
                letter = 1;
                break;
            case 'B':
                letter = 2;
                break;
            case 'C':
                letter = 3;
                break;
            case 'D':
                letter = 4;
                break;
            case 'E':
                letter = 5;
                break;
            case 'F':
                letter = 6;
                break;
            case 'G':
                letter = 7;
                break;
            case 'H':
                letter = 8;
                break;
            default:
                letter = 0;
                break;
        }

        List<int> LetNum = new List<int>
        {
            letter,
            number
        };
        return LetNum;
    }
    public static string IntToString(int i, int ii)
    {
        string check;
        switch (i)
        {
            case 1:
                check = "A" + ii;
                break;
            case 2:
                check = "B" + ii;
                break;
            case 3:
                check = "C" + ii;
                break;
            case 4:
                check = "D" + ii;
                break;
            case 5:
                check = "E" + ii;
                break;
            case 6:
                check = "F" + ii;
                break;
            case 7:
                check = "G" + ii;
                break;
            case 8:
                check = "H" + ii;
                break;
            default:
                check = "";
                break;

        }

        return check;
    }

    public static Vector3 GetPosition(string newPos)
    {
        return GameObject.Find(newPos).transform.position; 
      
    }



}
