using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Pieces
{
    public string StartPosition;
    public string TheColour;

    //public Knight(string position, string colour)
    //    : base(position, colour) { }


    void Start()
    {
        this.position = StartPosition;
        this.colour = TheColour;
        this.PieceValue = 30; 
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

        if (GameControl.IsOccupied(letter + 2, number + 1) == false || GameControl.IsOccupied(letter + 2, number + 1, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 2, number + 1);
        }
        if (GameControl.IsOccupied(letter + 2, number - 1) == false || GameControl.IsOccupied(letter + 2, number - 1, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 2, number - 1);
        }
        if (GameControl.IsOccupied(letter + 1, number + 2) == false || GameControl.IsOccupied(letter + 1, number + 2, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number + 2);
        }
        if (GameControl.IsOccupied(letter + 1, number - 2) == false || GameControl.IsOccupied(letter + 1, number - 2, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number - 2);
        }


        if (GameControl.IsOccupied(letter - 2, number + 1) == false || GameControl.IsOccupied(letter - 2, number + 1, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 2, number + 1);
        }
        if (GameControl.IsOccupied(letter - 2, number - 1) == false || GameControl.IsOccupied(letter - 2, number - 1, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 2, number - 1);
        }
        if (GameControl.IsOccupied(letter - 1, number + 2) == false || GameControl.IsOccupied(letter - 1, number + 2, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number + 2);
        }
        if (GameControl.IsOccupied(letter - 1, number - 2) == false || GameControl.IsOccupied(letter - 1, number - 2, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number - 2);
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

        if (GameControl.IsOccupied(letter + 2, number + 1) == false || GameControl.IsOccupied(letter + 2, number + 1) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 2, number + 1);
        }
        if (GameControl.IsOccupied(letter + 2, number - 1) == false || GameControl.IsOccupied(letter + 2, number - 1) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 2, number - 1);
        }
        if (GameControl.IsOccupied(letter + 1, number + 2) == false || GameControl.IsOccupied(letter + 1, number + 2) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number + 2);
        }
        if (GameControl.IsOccupied(letter + 1, number - 2) == false || GameControl.IsOccupied(letter + 1, number - 2) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number - 2);
        }


        if (GameControl.IsOccupied(letter - 2, number + 1) == false || GameControl.IsOccupied(letter - 2, number + 1) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 2, number + 1);
        }
        if (GameControl.IsOccupied(letter - 2, number - 1) == false || GameControl.IsOccupied(letter - 2, number - 1) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 2, number - 1);
        }
        if (GameControl.IsOccupied(letter - 1, number + 2) == false || GameControl.IsOccupied(letter - 1, number + 2) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number + 2);
        }
        if (GameControl.IsOccupied(letter - 1, number - 2) == false || GameControl.IsOccupied(letter - 1, number - 2) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number - 2);
        }
        return PossibleMoves;
    }
}
