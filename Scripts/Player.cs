using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private static List<Pieces> PiecesWhoAreCheckingKing = new List<Pieces>();
    private static List<string> BotMoves = new List<string>();
    public static Pieces playerKing;
    private static bool GotPossibleMove;
    public static void StartUp()
    {
        BotMoves = GetBotMoves("No exception");
        GotPossibleMove = false;
        List<Pieces> listofpieces; 
        if(GameControl.Player == "White")
        {
            listofpieces = Pieces.WhitePieceList;
        }
        else { listofpieces = Pieces.BlackPieceList; }


        foreach (Pieces p in listofpieces)
        {
            if (p.GetComponent<Collider>().enabled == true)
            {
                Movement.PieceToMove = p;
                Move(p.PossibleMoves(GameControl.Bot));
            }
        }
        foreach (GameObject g in ChoiceBox.Orbs)
        {
            g.transform.position = GameObject.Find("ChoiceBoxes").transform.position;
            g.GetComponent<ChoiceBox>().position = "";
        }
        if (GotPossibleMove == false)
        {
            Debug.Log("Player is Checkmated");
            GameControl.CheckMate = true;
            MainMenu.CheckmateMenu.SetActive(true);
            if (GameControl.Bot == "White")
            {
                MainMenu.WhiteWins.SetActive(true);
            }
            else
            {
                MainMenu.BlackWins.SetActive(true);
            }
        }

    }

    public static void Move(List<string> l)
    {
        PiecesWhoAreCheckingKing.Clear();

        foreach (GameObject g in ChoiceBox.Orbs)
        {
            g.transform.position = GameObject.Find("ChoiceBoxes").transform.position;
            g.GetComponent<ChoiceBox>().position = "";
        }
        int i = 0;
        foreach (string s in l)
        {
            if (playerKing.GetComponent<King>().Checked == true && BotMoves.Contains(s) == false && Movement.PieceToMove is King && Try(s) == false)
            {
                GameObject ChoiceBoxes = GameObject.Find("ChoiceBoxes");
                GameObject duplicate = GameObject.Find(ChoiceBoxes.transform.GetChild(i).name);
                duplicate.GetComponent<ChoiceBox>().setPosition(s);
                duplicate.transform.position = Movement.GetPosition(s);
                ChoiceBox.Orbs.Add(duplicate);
                i++;

                GotPossibleMove = true; 
            }
            else if (playerKing.GetComponent<King>().Checked == true && Try(s) == false)
            {
                GameObject ChoiceBoxes = GameObject.Find("ChoiceBoxes");
                GameObject duplicate = GameObject.Find(ChoiceBoxes.transform.GetChild(i).name);
                duplicate.GetComponent<ChoiceBox>().setPosition(s);
                duplicate.transform.position = Movement.GetPosition(s);
                ChoiceBox.Orbs.Add(duplicate);
                i++;
                GotPossibleMove = true; 
            }
            else if (playerKing.GetComponent<King>().Checked == false && Try(s) == false)
            {
                if (Movement.PieceToMove is King && BotMoves.Contains(s) == false || Movement.PieceToMove is not King)
                {
                    GameObject ChoiceBoxes = GameObject.Find("ChoiceBoxes");
                    GameObject duplicate = GameObject.Find(ChoiceBoxes.transform.GetChild(i).name);
                    duplicate.GetComponent<ChoiceBox>().setPosition(s);
                    duplicate.transform.position = Movement.GetPosition(s);
                    ChoiceBox.Orbs.Add(duplicate);
                    i++;
                    GotPossibleMove = true; 
                }
            }
        }

    }
    private static bool Try(string s)
    {
        bool stillCheck = true;
        bool kingstatusholder = playerKing.GetComponent<King>().Checked;
        int valueholder = GameObject.Find(s).GetComponent<Check>().Value;

        GameObject.Find(Movement.PieceToMove.GetPosition()).GetComponent<Check>().setOccupancy(Movement.PieceToMove.GetValue(), GameControl.Player, false);
        GameObject.Find(s).GetComponent<Check>().setOccupancy(Movement.PieceToMove.GetValue(), GameControl.Player, true);

        List<string> templist = GetBotMoves(s);
        if (playerKing.GetComponent<King>().Checked == false && Movement.PieceToMove is not King
            || PiecesWhoAreCheckingKing.Count == 1 && s == PiecesWhoAreCheckingKing[0].GetPosition() && Movement.PieceToMove is not King
            || PiecesWhoAreCheckingKing.Count == 1 && s == PiecesWhoAreCheckingKing[0].GetPosition() && Movement.PieceToMove is King && templist.Contains(s) == false
            || Movement.PieceToMove is King && templist.Contains(s) == false)
        {
            stillCheck = false;
        }
        GameObject.Find(Movement.PieceToMove.GetPosition()).GetComponent<Check>().setOccupancy(Movement.PieceToMove.GetValue(), GameControl.Player, true);
        GameObject.Find(s).GetComponent<Check>().setOccupancy(valueholder, GameControl.Player, false);
        playerKing.GetComponent<King>().Checked = kingstatusholder;

        return stillCheck;

    }

    private static List<string> GetBotMoves(string exception)
    {
        List<Pieces> listtosearch;
        List<string> ListOfMoves = new List<string>();
        PiecesWhoAreCheckingKing.Clear();
        if (GameControl.Bot == "White")
        {
            listtosearch = Pieces.WhitePieceList;
            playerKing = GameControl.BlackKing;
        }
        else
        {
            playerKing = GameControl.WhiteKing;
            listtosearch = Pieces.BlackPieceList;
        }

        bool alreadychecked = false;
        foreach (Pieces p in listtosearch)
        {

            if (p.GetComponent<Collider>().enabled == true && p.getPosition() != exception)
            {
                List<string> templist = p.PossibleMoves(GameControl.Player, true);
                foreach (string s in templist)
                {
                    ListOfMoves.Add(s);
                    if (s == playerKing.GetPosition())
                    {
                        playerKing.GetComponent<King>().Checked = true;
                        PiecesWhoAreCheckingKing.Add(p);
                        alreadychecked = true;
                    }
                    if (alreadychecked == false)
                    {
                        playerKing.GetComponent<King>().Checked = false;
                    }
                }
            }
        }
        return ListOfMoves;
    }
}
