using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Pieces
{
    public string StartPosition;
    public string TheColour;
    public bool Checked; 

    //public King(string position, string colour)
    //    : base(position, colour) { }


    void Start()
    {
        Checked = false; 
        this.position = StartPosition;
        this.colour = TheColour;
        this.PieceValue = 1000; 
        PieceList.Add(this);
        MoveTo(StartPosition);
        if (TheColour == "White")
        {
            GameControl.WhiteKing = this;
            WhitePieceList.Add(this); 
        }
        else
        {
            GameControl.BlackKing = this;
            BlackPieceList.Add(this); 
        }
    }
    public bool isChecked()
    {
        return Checked; 
    }

    public override List<string> PossibleMoves(string enemycolour)
    {
        List<int> CurrentPos = getPosInNum();
        int letter = CurrentPos[0];
        int number = CurrentPos[1];

        List<string> PossibleMoves = new List<string>();
        PossibleMoves.Clear();

        if (GameControl.IsOccupied(letter + 1, number + 1) == false || GameControl.IsOccupied(letter + 1, number + 1, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number + 1);
        }
        if (GameControl.IsOccupied(letter + 1, number - 1) == false || GameControl.IsOccupied(letter + 1, number - 1, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number - 1);

        }
        if (GameControl.IsOccupied(letter + 1, number) == false || GameControl.IsOccupied(letter + 1, number, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number);
        }


        if (GameControl.IsOccupied(letter - 1, number + 1) == false || GameControl.IsOccupied(letter - 1, number + 1, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number + 1);
        }
        if (GameControl.IsOccupied(letter - 1, number - 1) == false || GameControl.IsOccupied(letter - 1, number - 1, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number - 1);
        }
        if (GameControl.IsOccupied(letter - 1, number) == false || GameControl.IsOccupied(letter - 1, number, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number);
        }


        if (GameControl.IsOccupied(letter, number + 1) == false || GameControl.IsOccupied(letter, number + 1, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter, number + 1);
        }
        if (GameControl.IsOccupied(letter, number - 1) == false || GameControl.IsOccupied(letter, number - 1, enemycolour) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter, number - 1);
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

        if (GameControl.IsOccupied(letter + 1, number + 1) == false || GameControl.IsOccupied(letter + 1, number + 1) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number + 1);
        }
        if (GameControl.IsOccupied(letter + 1, number - 1) == false || GameControl.IsOccupied(letter + 1, number - 1) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number - 1);
        }
        if (GameControl.IsOccupied(letter + 1, number) == false || GameControl.IsOccupied(letter + 1, number) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number);
        }


        if (GameControl.IsOccupied(letter - 1, number + 1) == false || GameControl.IsOccupied(letter - 1, number + 1) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number + 1);
        }
        if (GameControl.IsOccupied(letter - 1, number - 1) == false || GameControl.IsOccupied(letter - 1, number - 1) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number - 1);
        }
        if (GameControl.IsOccupied(letter - 1, number) == false || GameControl.IsOccupied(letter - 1, number) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number);
        }


        if (GameControl.IsOccupied(letter, number + 1) == false || GameControl.IsOccupied(letter, number + 1) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter, number + 1);
        }
        if (GameControl.IsOccupied(letter, number - 1) == false || GameControl.IsOccupied(letter - 1, number - 1) == true)
        {
            GameControl.MoveAllowed(PossibleMoves, letter, number - 1);
        }

        return PossibleMoves;
    }
}
