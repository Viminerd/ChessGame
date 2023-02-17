using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Pieces
{
    public string StartPosition;
    public string TheColour;

    //public Rook(string position, string colour)
    //    : base(position, colour) { }


    void Start()
    {
        this.position = StartPosition;
        this.colour = TheColour;
        this.PieceValue = 50; 
        PieceList.Add(this);
        if (colour == "White")
        {
            WhitePieceList.Add(this);
        }
        else if (colour == "Black")
        {
            BlackPieceList.Add(this);
        }
        MoveTo(StartPosition);
    }

    public override List<string> PossibleMoves(string enemycolour)
    {
        List<int> CurrentPos = getPosInNum();
        int letter = CurrentPos[0];
        int number = CurrentPos[1];

        List<string> PossibleMoves = new List<string>();
        PossibleMoves.Clear();

        for (int i = 1; i < 8; i++)
        {
            if (GameControl.MoveAllowed(letter + i, number))
            {
                if (GameControl.IsOccupied(letter + i, number) == false)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter + i, number);
                }
                else if (GameControl.IsOccupied(letter + i, number, enemycolour) == true)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter + i, number);
                    break;
                }
                else { break; }

            }
            else { break; }
        }
        for (int i = 1; i < 8; i++)
        {
            if (GameControl.MoveAllowed(letter - i, number))
            {
                if (GameControl.IsOccupied(letter - i, number) == false)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter - i, number);
                }
                else if (GameControl.IsOccupied(letter - i, number, enemycolour) == true)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter - i, number);
                    break;
                }
                else { break; }

            }
            else { break; }
        }
        for (int i = 1; i < 8; i++)
        {
            if (GameControl.MoveAllowed(letter, number + i))
            {
                if (GameControl.IsOccupied(letter, number + i) == false)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter, number + i);
                }
                else if (GameControl.IsOccupied(letter, number + i, enemycolour) == true)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter, number + i);
                    break;
                }
                else { break; }

            }
            else { break; }
        }
        for (int i = 1; i < 8; i++)
        {
            if (GameControl.MoveAllowed(letter, number - i))
            {
                if (GameControl.IsOccupied(letter, number - i) == false)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter, number - i);
                }
                else if (GameControl.IsOccupied(letter, number - i, enemycolour) == true)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter, number - i);
                    break;
                }
                else { break; }

            }
            else { break; }
        }
        return PossibleMoves;
    }

    public override List<string> PossibleMoves(string enemycolour, bool t)
    {

        List<int> CurrentPos = getPosInNum();
        int letter = CurrentPos[0];
        int number = CurrentPos[1];

        List<string> PossibleMoves = new List<string>();
        PossibleMoves.Clear();

        for (int i = 1; i < 8; i++)
        {
            if (GameControl.MoveAllowed(letter + i, number))
            {
                if (GameControl.IsOccupied(letter + i, number) == false)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter + i, number);
                }
                else if (GameControl.IsOccupied(letter + i, number) == true)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter + i, number);
                    break;
                }
                else { break; }

            }
            else { break; }
        }
        for (int i = 1; i < 8; i++)
        {
            if (GameControl.MoveAllowed(letter - i, number))
            {
                if (GameControl.IsOccupied(letter - i, number) == false)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter - i, number);
                }
                else if (GameControl.IsOccupied(letter - i, number) == true)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter - i, number);
                    break;
                }
                else { break; }

            }
            else { break; }
        }
        for (int i = 1; i < 8; i++)
        {
            if (GameControl.MoveAllowed(letter, number + i))
            {
                if (GameControl.IsOccupied(letter, number + i) == false)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter, number + i);
                }
                else if (GameControl.IsOccupied(letter, number + i) == true)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter, number + i);
                    break;
                }
                else { break; }

            }
            else { break; }
        }
        for (int i = 1; i < 8; i++)
        {
            if (GameControl.MoveAllowed(letter, number - i))
            {
                if (GameControl.IsOccupied(letter, number - i) == false)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter, number - i);
                }
                else if (GameControl.IsOccupied(letter, number - i) == true)
                {
                    GameControl.MoveAllowed(PossibleMoves, letter, number - i);
                    break;
                }
                else { break; }

            }
            else { break; }
        }
        return PossibleMoves;
    }
}
