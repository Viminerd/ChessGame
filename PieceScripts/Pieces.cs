using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    //public string StartPosition;
    //public string TheColour;
    public static List<Pieces> PieceList = new List<Pieces>();
    public static List<Pieces> WhitePieceList = new List<Pieces>();
    public static List<Pieces> BlackPieceList = new List<Pieces>();
    public static List<Pieces> PiecesWhoAreCheckingWhiteKing = new List<Pieces>();
    public static List<Pieces> PiecesWhoAreCheckingBlackKing = new List<Pieces>();
    public static List<string> WhiteMovesList = new List<string>();
    public static List<string> BlackMovesList = new List<string>();

    public static string newpiece = null; 
    public static Pieces PieceToMove; 
    protected string position;
    protected string colour;
    protected int PieceValue;
     
    //public Pieces(string position, string colour)
    //{
    //    this.position = position;
    //    this.colour = colour;
    //}

    public List<int> getPosInNum()
    {
        List<int> pos = new List<int>
            {
                Movement.GetPosInNum(position)[0],
                Movement.GetPosInNum(position)[1]
            };

        return pos;
    }

    private void OnMouseDown()
    {
        if (GameControl.GameRunning == true)
        {
            if (GameControl.WhosTurn == colour && GameControl.Game != "PvB" || GameControl.WhosTurn == colour && GameControl.WhosTurn == GameControl.Player)
            {
                Movement.PieceToMove = this;
                if (GameControl.WhosTurn == GameControl.Player)
                {
                    Player.Move(PossibleMoves(GameControl.Bot));
                }
                else if (GameControl.WhosTurn == GameControl.Bot)
                {
                    Bot.Move(PossibleMoves(GameControl.Player));
                }
            }
        }
    }
    public int GetValue()
    {
        return PieceValue; 
    }

    public string GetPosition()
    {
        return position; 
    }

    public void SetPosition(string newPos)
    {
        position = newPos;
    }
    public string GetColour()
    {
        return colour; 
    }
    public string GetEnemyColour()
    {
        if (colour == "White")
        {
            return "Black"; 
        }
        else
        {
            return "White";
        }
    }

    public void MoveTo(string newPos)
    {   //  Kill pieces on moving position
        PieceToMove = this;
        foreach (Pieces p in PieceList)
        {
            if (p.GetComponent<Collider>().enabled == true)
            {
                if (p.GetPosition() == newPos && this != p || newPos == GameControl.enPassantKillMove && p == GameControl.enPassantPawn)
                {
                    if (p.GetColour() == "White")
                    {
                        //Debug.Log("Killed" + p.GetColour() + p);
                        p.transform.position = GameObject.Find(GameControl.DeadPieces.transform.GetChild(GameControl.WDeath).name).transform.position;
                        GameObject.Find(p.GetPosition()).GetComponent<Check>().setOccupancy(0, p.GetColour(), false);
                        p.GetComponent<Collider>().enabled = false;
                        GameControl.WDeath++;
                    }
                    else if (p.GetColour() == "Black")
                    {
                        //Debug.Log("Killed " + p.GetColour() + " on " + p.GetPosition());
                        p.transform.position = GameObject.Find(GameControl.DeadPieces.transform.GetChild(GameControl.BDeath).name).transform.position;
                        GameObject.Find(p.GetPosition()).GetComponent<Check>().setOccupancy(0, p.GetColour(), false);
                        p.GetComponent<Collider>().enabled = false;

                        GameControl.BDeath++;

                    }
                }
            }
            
        }
        //
        
        // Set Occupancy
        GameObject.Find(newPos).GetComponent<Check>().setOccupancy(PieceValue, colour, true);

        if (newPos != position)
        {
            GameObject.Find(position).GetComponent<Check>().setOccupancy(0, colour, false);
            
        }
        //

        // Set en passant pawn
        if (this is Pawn && newPos[1] == position[1] + gameObject.GetComponent<Pawn>().direction * 2)
        {
            GameControl.enPassant = true;
            GameControl.enPassantMove = newPos;
            GameControl.enPassantPawn = this;
            string let = GameControl.enPassantMove[0] + "";
            string num = GameControl.enPassantMove[1] - '0' - this.GetComponent<Pawn>().getDirection() + "";
            GameControl.enPassantKillMove = let + num;
            GameControl.enPassantPawn = this;
        }
        else //reset en passant
        {
            GameControl.enPassant = false;
            GameControl.enPassantMove = "";
            GameControl.enPassantPawn = null;
            GameControl.enPassantKillMove = "";
        }
        // 

        // The move
        while (gameObject.transform.position != GameObject.Find("MovementHolder").GetComponent<Movement>().getVector(newPos))
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
            GameObject.Find("MovementHolder").GetComponent<Movement>().getVector(newPos),
            1 * Time.deltaTime);
            position = newPos; 
        }
        //

        if (this is Pawn)
        {
            this.GetComponent<Pawn>().FirstMove = true;
            if (this.colour == "White" && newPos[1] == '8')
            {
                if (GameControl.Game != "PvB" || GameControl.WhosTurn == GameControl.Player)
                {
                    Movement.PieceToReplace = this;
                    GameControl.WhosTurn = "W";
                    MainMenu.activateChoice();
                }
                else
                {
                    Movement.PieceToReplace = this;
                    GameControl.WhosTurn = "W";
                    MainMenu.activateChoice("Queen"); 
                }
            }
            else if (this.colour == "Black" && newPos[1] == '1')
            {
                if (GameControl.Game != "PvB" || GameControl.WhosTurn == GameControl.Player)
                {
                    Movement.PieceToReplace = this;
                    GameControl.WhosTurn = "B";
                    MainMenu.activateChoice();
                }
                else
                {
                    Movement.PieceToReplace = this;
                    GameControl.WhosTurn = "W";
                    MainMenu.activateChoice("Queen");
                }

            }
            else
            {
                endOfTurn(); 
            }
        }
        else
        {
            endOfTurn(); 
        }


    }
    public static void endOfTurn()
    {
        WhiteMovesList.Clear();
        BlackMovesList.Clear();
        PiecesWhoAreCheckingWhiteKing.Clear();
        PiecesWhoAreCheckingBlackKing.Clear();
        GameControl.SwitchTurn();

    }

    public virtual List<string> PossibleMoves(string enemycolour)
    {
        List<string> s = new List<string>();
        string ss = "base";
        Debug.Log("ERROR"); 
        s.Add(ss);
        return s;
    }
    public virtual List<string> PossibleMoves(string enemycolour, bool b) //Only attack moves
    {
        List<string> s = new List<string>();
        string ss = "base";
        s.Add(ss);
        return s;
    } 
    public void setPosition(string pos)
    {
        position = pos; 
    }
    public string getPosition()
    {
        return position;
    }
    public void setColour(string c)
    {
        colour = c; 
    }
    public string getColour()
    {
        return colour; 
    }
    public void setValue(int v)
    {
        PieceValue = v; 
    }
    public int getValue()
    {
        return PieceValue;
    }
}
