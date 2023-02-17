using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private static List<Pieces> PiecesWhoAreCheckingKing = new List<Pieces>();
    private static List<PieceStruct> PiecesWhoAreThreatheningPieces = new List<PieceStruct>();
    private static List<string> PlayerMoves = new List<string>();
    public static Pieces botKing;
    private static bool GotPossibleMove;

    // Move the bot

    private static double isThreathened;
    private static double killsAFriend;
    private static double killsAThreat;
    private static double isSuicide;
    private static double dontMoveBack;
    private static double checkMateMove;
    private static double checksPlayer;

    public struct PieceStruct
    {
        public Pieces ThePiece;
        public string TheMove;             
        public int TheMoveValue;
        public int ThePieceValue;
        public string PreviousPosition; 

        public PieceStruct(Pieces piece, string move, int valueofmove, int valueofpiece, string previouspos)
        {
            this.ThePiece = piece;
            this.ThePieceValue = valueofpiece;
            this.TheMove = move;
            this.TheMoveValue = valueofmove;
            PreviousPosition = previouspos;
        }
    }

    private static List<PieceStruct> MoveStructs = new List<PieceStruct>();
    public void StartTheBot()
    {
        StartCoroutine(MoveTheBot());
    }
    private IEnumerator MoveTheBot()
    {
        while (true)
        { 
            while (GameControl.WhosTurn == GameControl.Player || GameControl.GameRunning == false)
            {
                yield return null;
            }
            
            yield return new WaitForSeconds(1.5f);
            PiecesWhoAreThreatheningPieces.Clear(); 
            MoveStructs.Clear();
            PlayerMoves = GetPlayerMoves("No exception");
            GotPossibleMove = false;
            List<Pieces> listofpieces;
            if (GameControl.Bot == "White")
            {
                listofpieces = Pieces.WhitePieceList;
            }
            else { listofpieces = Pieces.BlackPieceList; }


            foreach (Pieces p in listofpieces)
            {
                if (p.GetComponent<Collider>().enabled == true)
                {
                    Movement.PieceToMove = p;
                    BotPossibleMoves(p.PossibleMoves(GameControl.Player), p);
                }

            }
            foreach (GameObject g in ChoiceBox.Orbs)
            {
                g.transform.position = GameObject.Find("ChoiceBoxes").transform.position;
                g.GetComponent<ChoiceBox>().position = "";
            }
            if (GotPossibleMove == false)
            {
                Debug.Log("Bot is Checkmated");
                GameControl.CheckMate = true; 
                MainMenu.CheckmateMenu.SetActive(true);
                if (GameControl.Player == "White")
                {
                    MainMenu.WhiteWins.SetActive(true);
                }
                else
                {
                    MainMenu.BlackWins.SetActive(true);
                }
                break;
            }
            double bestValue = -100000;
            List<PieceStruct> bestmoves = new List<PieceStruct>();
            foreach(PieceStruct ps in MoveStructs)
            {
                double val = ps.TheMoveValue;

                isThreathened = 0;
                killsAFriend = 0;
                killsAThreat = 0;
                isSuicide = 0;
                dontMoveBack = 0;
                checkMateMove = 0; 
                checksPlayer = 0; 
                

                if (PlayerMoves.Contains(ps.TheMove)) //Dont suicide
                {
                    isSuicide = ps.ThePieceValue;
                }

                if (PlayerMoves.Contains(ps.ThePiece.GetPosition()) && ps.ThePiece is not King) //Move if piece is threthened
                {
                    isThreathened = ps.ThePieceValue / 2;
                }
                TryABotMove(ps, PlayerMoves); //sets killsAFriend and checkMateMove
     
                foreach(PieceStruct playerstruct in PiecesWhoAreThreatheningPieces)
                {
                    if (ps.TheMove == playerstruct.ThePiece.GetPosition())
                    {
                        double v = playerstruct.ThePieceValue;
                        if (v > killsAThreat)
                        {
                            killsAThreat = v; 
                        }
                    } 
                }
                val = val + isThreathened - isSuicide + killsAThreat - killsAFriend + checkMateMove;

                //Check possible moves after this move
                string oldPos = ps.ThePiece.GetPosition();
                ps.ThePiece.SetPosition(ps.TheMove);
                List<string> templist = ps.ThePiece.PossibleMoves(GameControl.Player, true);
                foreach(string s in templist)
                {
                    if (s == Player.playerKing.GetPosition() && isSuicide == 0)
                    {
                        checksPlayer = 50; 
                    }
                }
                ps.ThePiece.SetPosition(oldPos); 


                val += checksPlayer;








                //Debug.Log(ps.ThePiece+ps.TheMove + "CHECKSPLAYER "+checksPlayer + " isThreathened: " + isThreathened + ". isSuicide: " + isSuicide + ". killsAThreat: " + killsAThreat + ". killsAFriend: " + killsAFriend);

                //end of bot move value evaluation

                if (val > bestValue)
                {
                    bestmoves.Clear();
                    bestmoves.Add(ps);
                    bestValue = val;
                    //Debug.Log(ps.TheMove + " isThreathened: " + isThreathened + ". isSuicide: " + isSuicide + ". killsAThreat: " + killsAThreat + ". killsAFriend: " + killsAFriend);
                }
                else if(val == bestValue)
                {
                    bestmoves.Add(ps); 
                }                             
            }

          
            if (bestmoves.Count == 1)
            {
                bestmoves[0].ThePiece.MoveTo(bestmoves[0].TheMove);
                
            }
            else if (bestmoves.Count >1)
            {
                int randomint = UnityEngine.Random.Range(0, bestmoves.Count-1);
                bestmoves[randomint].ThePiece.MoveTo(bestmoves[randomint].TheMove);
            }
            Player.StartUp();
        }
    }

    private static void TryABotMove(PieceStruct ps, List<string> oldplayermoves)
    {
        bool stillCheck = true;
        double value = 0; 
        bool kingstatusholder = botKing.GetComponent<King>().Checked;
        int valueholder = GameObject.Find(ps.TheMove).GetComponent<Check>().Value;

        GameObject.Find(ps.ThePiece.GetPosition()).GetComponent<Check>().setOccupancy(ps.ThePiece.GetValue(), GameControl.Bot, false);
        GameObject.Find(ps.TheMove).GetComponent<Check>().setOccupancy(ps.ThePiece.GetValue(), GameControl.Bot, true);

        List<string> templist = GetPlayerMoves(ps.TheMove); //Argument move to not get the piece on pos "move" moves.

        if (ps.TheMove == Player.playerKing.GetPosition() && templist.Count == 0)
        {
            checkMateMove = 10000;
        }
        
        foreach (string playermove in templist)
        {
            if (oldplayermoves.Contains(playermove) == false)
            {
                if (GameObject.Find(playermove).GetComponent<Check>().Value > value)
                {
                    killsAFriend = GameObject.Find(playermove).GetComponent<Check>().Value; 
                }
            }
        }

        GameObject.Find(ps.ThePiece.GetPosition()).GetComponent<Check>().setOccupancy(ps.ThePiece.GetValue(), GameControl.Bot, true);
        GameObject.Find(ps.TheMove).GetComponent<Check>().setOccupancy(valueholder, GameControl.Bot, false);
        botKing.GetComponent<King>().Checked = kingstatusholder;

    }
    public static void BotPossibleMoves(List<string> l, Pieces p)
    {
        PiecesWhoAreCheckingKing.Clear();
        foreach (string s in l)
        {
            if (botKing.GetComponent<King>().Checked == true && PlayerMoves.Contains(s) == false && Movement.PieceToMove is King && Try(s) == false)
            {
                MoveStructs.Add(new PieceStruct(p, s, GameObject.Find(s).GetComponent<Check>().Value, p.getValue(), p.GetPosition()));
                GotPossibleMove = true;
            }
            else if (botKing.GetComponent<King>().Checked == true && Try(s) == false)
            {
                MoveStructs.Add(new PieceStruct(p, s, GameObject.Find(s).GetComponent<Check>().Value, p.getValue(), p.GetPosition()));
                GotPossibleMove = true;
            }
            else if (botKing.GetComponent<King>().Checked == false && Try(s) == false)
            {
                if (Movement.PieceToMove is King && PlayerMoves.Contains(s) == false || Movement.PieceToMove is not King)
                {
                    MoveStructs.Add(new PieceStruct(p, s, GameObject.Find(s).GetComponent<Check>().Value, p.getValue(), p.GetPosition()));
                    GotPossibleMove = true;
                }
            }
        }
    }



    //Player versus Player part.
    public static void StartUp()
    {
        if (GameControl.Game != "PvB")
        {
            PlayerMoves = GetPlayerMoves("No exception");
            GotPossibleMove = false;
            List<Pieces> listofpieces;
            if (GameControl.Bot == "White")
            {
                listofpieces = Pieces.WhitePieceList;
            }
            else { listofpieces = Pieces.BlackPieceList; }


            foreach (Pieces p in listofpieces)
            {
                if (p.GetComponent<Collider>().enabled == true)
                {
                    Movement.PieceToMove = p;
                    Move(p.PossibleMoves(GameControl.Player));
                }

            }
            foreach (GameObject g in ChoiceBox.Orbs)
            {
                g.transform.position = GameObject.Find("ChoiceBoxes").transform.position;
                g.GetComponent<ChoiceBox>().position = "";
            }
            if (GotPossibleMove == false)
            {
                Debug.Log("Bot is Checkmated");
                GameControl.CheckMate = true;
                MainMenu.CheckmateMenu.SetActive(true); 
                if (GameControl.Player == "White")
                {
                    MainMenu.WhiteWins.SetActive(true);
                }
                else
                {
                    MainMenu.BlackWins.SetActive(true);
                }
                 

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
            if (botKing.GetComponent<King>().Checked == true && PlayerMoves.Contains(s) == false && Movement.PieceToMove is King && Try(s) == false)
            {
                GameObject ChoiceBoxes = GameObject.Find("ChoiceBoxes");
                GameObject duplicate = GameObject.Find(ChoiceBoxes.transform.GetChild(i).name);
                duplicate.GetComponent<ChoiceBox>().setPosition(s);
                duplicate.transform.position = Movement.GetPosition(s);
                ChoiceBox.Orbs.Add(duplicate);
                i++;
                GotPossibleMove = true; 
            }
            else if (botKing.GetComponent<King>().Checked == true && Try(s) == false)
            {
                GameObject ChoiceBoxes = GameObject.Find("ChoiceBoxes");
                GameObject duplicate = GameObject.Find(ChoiceBoxes.transform.GetChild(i).name);
                duplicate.GetComponent<ChoiceBox>().setPosition(s);
                duplicate.transform.position = Movement.GetPosition(s);
                ChoiceBox.Orbs.Add(duplicate);
                i++;
                GotPossibleMove = true; 
            }
            else if (botKing.GetComponent<King>().Checked == false && Try(s) == false)
            {
                if (Movement.PieceToMove is King && PlayerMoves.Contains(s) == false || Movement.PieceToMove is not King)
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
        bool kingstatusholder = botKing.GetComponent<King>().Checked;
        int valueholder = GameObject.Find(s).GetComponent<Check>().Value; 
        
        GameObject.Find(Movement.PieceToMove.GetPosition()).GetComponent<Check>().setOccupancy(Movement.PieceToMove.GetValue(), GameControl.Bot, false);
        GameObject.Find(s).GetComponent<Check>().setOccupancy(Movement.PieceToMove.GetValue(), GameControl.Bot, true);

        List<string> templist = GetPlayerMoves(s); //pass move to not get the piece on pos move moves.
        if (botKing.GetComponent<King>().Checked == false && Movement.PieceToMove is not King
            || PiecesWhoAreCheckingKing.Count == 1 && s == PiecesWhoAreCheckingKing[0].GetPosition() && Movement.PieceToMove is not King
            || PiecesWhoAreCheckingKing.Count == 1 && s == PiecesWhoAreCheckingKing[0].GetPosition() && Movement.PieceToMove is King && templist.Contains(s) == false
            || Movement.PieceToMove is King && templist.Contains(s) == false)
        {
            stillCheck = false;
        }
        GameObject.Find(Movement.PieceToMove.GetPosition()).GetComponent<Check>().setOccupancy(Movement.PieceToMove.GetValue(), GameControl.Bot, true);
        GameObject.Find(s).GetComponent<Check>().setOccupancy(valueholder, GameControl.Bot, false);
        botKing.GetComponent<King>().Checked = kingstatusholder; 

        return stillCheck;

    }

    private static List<string> GetPlayerMoves(string exception)
    {
        List<Pieces> listtosearch;
        List<string> ListOfMoves = new List<string>();
        PiecesWhoAreCheckingKing.Clear(); 
        if (GameControl.Player == "White")
        {
            listtosearch = Pieces.WhitePieceList;
            botKing = GameControl.BlackKing;
        }
        else 
        {
            botKing = GameControl.WhiteKing;
            listtosearch = Pieces.BlackPieceList;
        }

        bool alreadychecked = false; 
        foreach (Pieces p in listtosearch)
        {

            if (p.GetComponent<Collider>().enabled == true && p.getPosition() != exception)
            {
                List<string> templist = p.PossibleMoves(GameControl.Bot, true);
                foreach (string s in templist)
                {
                    ListOfMoves.Add(s);
                    if (s == botKing.GetPosition())
                    {
                        botKing.GetComponent<King>().Checked = true;
                        PiecesWhoAreCheckingKing.Add(p);
                        alreadychecked = true;
                    }
                    if (alreadychecked == false)
                    {
                        botKing.GetComponent<King>().Checked = false;
                    }
                    //if (GameObject.Find(s).GetComponent<Check>().Value > 9) {
                    //    Debug.Log(p + ": " + s);
                    //    Debug.Log(GameObject.Find(s).GetComponent<Check>().Value +"OK COOL");
                    //    PiecesWhoAreThreatheningPieces.Add(new PieceStruct(p, s, GameObject.Find(s).GetComponent<Check>().Value, GameObject.Find(p.GetPosition()).GetComponent<Check>().Value, "null"));
                    //}

                }
            }
        }
        return ListOfMoves;
    }
}
