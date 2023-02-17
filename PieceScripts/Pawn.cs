using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn: Pieces
{
    public string StartPosition;
    public string TheColour;
    public bool FirstMove;
    public int direction; 

    void Start()
    {
        this.position = StartPosition;
        this.colour = TheColour;
        this.PieceValue = 10; 
        PieceList.Add(this);
        if (TheColour == "White")
        {
            direction = 1;
            WhitePieceList.Add(this); 
        }
        else 
        { 
            direction = -1;
            BlackPieceList.Add(this); 
        }

        MoveTo(StartPosition);
        FirstMove = false; 
    }
    public int getDirection()
    {
        return direction; 
    }

    
    public override List<string> PossibleMoves(string enemycolour)
    {
        List<int> CurrentPos = getPosInNum();
        int letter = CurrentPos[0];
        int number = CurrentPos[1];

        List<string> PossibleMoves = new List<string>();
        PossibleMoves.Clear();
        if (GameControl.IsOccupied(letter, number + direction * 2) == false && FirstMove == false && GameControl.IsOccupied(letter, number + direction) == false)
        {
            GameControl.MoveAllowed(PossibleMoves, letter, number + direction * 2);
        }

        if (GameControl.IsOccupied(letter, number + direction) == false)
        {
            GameControl.MoveAllowed(PossibleMoves, letter, number + direction);
        }

        string attackmove1 = Movement.IntToString(letter + 1, number + direction);
        string attackmove2 = Movement.IntToString(letter - 1, number + direction);
        
        if ((GameControl.IsOccupied(letter + 1, number + direction, enemycolour) == true)
            || GameControl.enPassant && GameControl.MoveAllowed(letter + 1, number + direction) &&
            (attackmove1 == GameControl.enPassantKillMove))
        {
            GameControl.MoveAllowed(PossibleMoves, letter + 1, number + direction);
        }
        if ((GameControl.IsOccupied(letter - 1, number + direction, enemycolour) == true)
            || GameControl.enPassant && GameControl.MoveAllowed(letter - 1, number + direction) && 
            (attackmove2 == GameControl.enPassantKillMove))
        {
            GameControl.MoveAllowed(PossibleMoves, letter - 1, number + direction);
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

        GameControl.MoveAllowed(PossibleMoves, letter + 1, number + direction);
        GameControl.MoveAllowed(PossibleMoves, letter - 1, number + direction);

        return PossibleMoves;
    }
}
