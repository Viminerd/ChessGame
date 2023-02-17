using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ChoiceBox :MonoBehaviour
{
    public static List<GameObject> Orbs = new List<GameObject>();
    public string position;
    public void OnMouseDown()
    {
        Movement.PieceToMove.GetComponent<Pieces>().MoveTo(position);
        foreach (GameObject g in Orbs)
        {
            g.transform.position = GameObject.Find("ChoiceBoxes").transform.position;
            g.GetComponent<ChoiceBox>().position = ""; 
        }

       if (GameControl.WhosTurn == GameControl.Player)
        {
            Player.StartUp(); 
        }
        else if (GameControl.WhosTurn == GameControl.Bot)
        {
            Bot.StartUp();
        }


    }

    public void setPosition(string pos)
    {
        position = pos; 
    }
    //public string OrbPosition;
    //private void OnMouseDown()
    //{
    //    LatestOrbPos = OrbPosition;
    //    GameControl.SetOccupancy(PositionHolder, false, 0);
    //    MoveTo(ObjectSelected, OrbPosition, true);
    //    GameControl.SetOccupancy(OrbPosition, true, value);

    //    int ind = PlayerObjectList.FindIndex(x => x.Equals(GameObject.Find(ObjectSelected)));
    //    PlayerPositionList[ind] = OrbPosition;

    //    switch (Type)
    //    {
    //        case "Pawn":
    //            GameObject.Find(ObjectSelected).GetComponent<Pawn>().Position = OrbPosition;
    //            GameObject.Find(ObjectSelected).GetComponent<Pawn>().FirstMove = false;
    //            if (GetPosInNum(PositionHolder)[1] == GetPosInNum(OrbPosition)[1]
    //                - 2 * GameObject.Find(ObjectSelected).GetComponent<Pawn>().direction)
    //            {
    //                TrickMove.Clear(); 
    //                TrickMove.Add(GetPosInNum(OrbPosition)[0]);
    //                TrickMove.Add(GetPosInNum(OrbPosition)[1] - GameObject.Find(ObjectSelected).GetComponent<Pawn>().direction);
    //                PawnTrickMove = GameObject.Find(ObjectSelected); 
    //            }
    //            else
    //            {
    //                PawnTrickMove = null; 
    //            }
    //            break;
    //        case "Rook":
    //            GameObject.Find(ObjectSelected).GetComponent<Rook>().Position = OrbPosition;
    //            PawnTrickMove = null;
    //            break;            
    //        case "Knight":
    //            GameObject.Find(ObjectSelected).GetComponent<Knight>().Position = OrbPosition;
    //            PawnTrickMove = null;
    //            break;            
    //        case "Queen":
    //            GameObject.Find(ObjectSelected).GetComponent<Queen>().Position = OrbPosition;
    //            PawnTrickMove = null;
    //            break;            
    //        case "Bishop":
    //            GameObject.Find(ObjectSelected).GetComponent<Bishop>().Position = OrbPosition;
    //            PawnTrickMove = null;
    //            break;            
    //        case "King":
    //            GameObject.Find(ObjectSelected).GetComponent<King>().Position = OrbPosition;
    //            PawnTrickMove = null;
    //            break;
    //    }
    //    foreach (GameObject Obj in Movement.Orbs)
    //    {
    //        Obj.transform.position = ChoiceOrb.transform.position;
    //    }

    //    GameControl.PlayerTurn = false;

    //}



}



