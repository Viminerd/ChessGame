using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool goToStart = false;
    private bool rotateToBoard = false; 
    private Vector3 target, targetDirection, newDirection;
    private bool run = false;
    private bool RotateRun = false;
    private bool LookingAt = false;

    private GameObject TheCamera;
    private static GameObject MoveTo;
    private static string MovingFrom;
    private static GameObject Target, startPos;
    public static GameObject ChoosePiece, CheckmateMenu, WhiteWins, BlackWins;

    public Button pvpButton; 
    public Button pvbButton;
    public Button whitebutton;
    public Button blackbutton;
    

    private static float speed, cameraspeed, step;


    public void Start()
    {
        whitebutton.gameObject.SetActive(false);
        blackbutton.gameObject.SetActive(false);

        float scale = 1.5f;
        speed = 2.5f;
        cameraspeed = 0.9f;
        speed = speed * scale;
        cameraspeed = cameraspeed * scale;


        step = speed * Time.deltaTime;
        TheCamera = GameObject.Find("MainCamera");
        Target = GameObject.Find("CameraTarget");
        target = GameObject.Find("cameraTarget").transform.position;
        targetDirection = target - TheCamera.transform.position;
        ChoosePiece = GameObject.Find("ChoosePiece");
        CheckmateMenu = GameObject.Find("CheckmateMenu");
        WhiteWins = GameObject.Find("WhiteWins");
        BlackWins = GameObject.Find("BlackWins");
        WhiteWins.SetActive(false); 
        BlackWins.SetActive(false);
        CheckmateMenu.SetActive(false); 
        ChoosePiece.GetComponent<Canvas>().enabled = false;
        startPos = GameObject.Find("WhitePos");
    }

    public void PlayGame()
    {
        goToStart = true;
        rotateToBoard = true;
        run = true; 
        GameControl.SwitchTurn();
        GameControl.Player = "White";
        GameControl.Bot = "Black";
        GameControl.Game = "PvP";
    }    
    public void PlayBotGame()
    {
        pvbButton.gameObject.SetActive(false);
        pvpButton.gameObject.SetActive(false);

        whitebutton.gameObject.SetActive(true);
        blackbutton.gameObject.SetActive(true);
        GameControl.Game = "PvB";
    }
    public void startWhite()
    {
        GameObject.Find("BotHolder").GetComponent<Bot>().StartTheBot();
        goToStart = true;
        rotateToBoard = true;
        run = true;
        GameControl.SwitchTurn();
        GameControl.Player = "White";
        GameControl.Bot = "Black";
        startPos = GameObject.Find("WhitePos");
        
        GameControl.Game = "PvB";
        whitebutton.gameObject.SetActive(false);
        blackbutton.gameObject.SetActive(false);

    }    
    public void startBlack()
    {
        GameObject.Find("BotHolder").GetComponent<Bot>().StartTheBot();
        goToStart = true;
        rotateToBoard = true;
        run = true;
        GameControl.SwitchTurn();
        GameControl.Player = "Black";
        GameControl.Bot = "White";
        startPos = GameObject.Find("BlackPos");

        GameControl.Game = "PvB";
        whitebutton.gameObject.SetActive(false);
        blackbutton.gameObject.SetActive(false);

    }

    public static void setQueen()
    {
        GameObject pieceToReplace = GameObject.Find(Movement.PieceToReplace.GetComponent<Pieces>().name);
        GameObject newp; 
        if (Movement.PieceToReplace.getColour() == "White")
        {
            newp = Instantiate(GameObject.Find("WQueen"));
            Pieces.WhitePieceList.Add(newp.GetComponent<Pieces>());
            Pieces.WhitePieceList.Remove(pieceToReplace.GetComponent<Pieces>());
        }
        else
        {
            newp = Instantiate(GameObject.Find("BQueen"));
            Pieces.BlackPieceList.Add(newp.GetComponent<Pieces>());
            Pieces.BlackPieceList.Remove(pieceToReplace.GetComponent<Pieces>());
        }
        newp.GetComponent<Queen>().StartPosition = pieceToReplace.GetComponent<Pieces>().getPosition();
        changePiece(newp, pieceToReplace);
    }    
    public static void setKnight()
    {
        GameObject pieceToReplace = GameObject.Find(Movement.PieceToReplace.GetComponent<Pieces>().name);
        GameObject newp; 
        if (Movement.PieceToReplace.getColour() == "White")
        {
            newp = Instantiate(GameObject.Find("WKnight1"));
            Pieces.WhitePieceList.Add(newp.GetComponent<Pieces>());
            Pieces.WhitePieceList.Remove(pieceToReplace.GetComponent<Pieces>());
        }
        else
        {
            newp = Instantiate(GameObject.Find("BKnight1"));
            Pieces.BlackPieceList.Add(newp.GetComponent<Pieces>());
            Pieces.BlackPieceList.Remove(pieceToReplace.GetComponent<Pieces>());
        }
        newp.GetComponent<Knight>().StartPosition = pieceToReplace.GetComponent<Pieces>().getPosition();
        changePiece(newp, pieceToReplace);
    }    
    public static void setRook()
    {
        GameObject pieceToReplace = GameObject.Find(Movement.PieceToReplace.GetComponent<Pieces>().name);
        GameObject newp; 
        if (Movement.PieceToReplace.getColour() == "White")
        {
            newp = Instantiate(GameObject.Find("WRook1"));
            Pieces.WhitePieceList.Add(newp.GetComponent<Pieces>());
            Pieces.WhitePieceList.Remove(pieceToReplace.GetComponent<Pieces>());
        }
        else
        {
            newp = Instantiate(GameObject.Find("BRook1"));
            Pieces.BlackPieceList.Add(newp.GetComponent<Pieces>());
            Pieces.BlackPieceList.Remove(pieceToReplace.GetComponent<Pieces>());
        }
        newp.GetComponent<Rook>().StartPosition = pieceToReplace.GetComponent<Pieces>().getPosition();
        changePiece(newp, pieceToReplace);
    }    
    public static void setBishop()
    {
        GameObject pieceToReplace = GameObject.Find(Movement.PieceToReplace.GetComponent<Pieces>().name);
        GameObject newp; 
        if (Movement.PieceToReplace.getColour() == "White")
        {
            newp = Instantiate(GameObject.Find("WBishop1"));
            Pieces.WhitePieceList.Add(newp.GetComponent<Pieces>());
            Pieces.WhitePieceList.Remove(pieceToReplace.GetComponent<Pieces>());
        }
        else
        {
            newp = Instantiate(GameObject.Find("BBishop1"));
            Pieces.BlackPieceList.Add(newp.GetComponent<Pieces>());
            Pieces.BlackPieceList.Remove(pieceToReplace.GetComponent<Pieces>());
        }
        newp.GetComponent<Bishop>().StartPosition = pieceToReplace.GetComponent<Pieces>().getPosition();
        changePiece(newp, pieceToReplace);
    }

    private static void changePiece(GameObject newp, GameObject pieceToReplace)
    {
        newp.transform.localScale = new Vector3(newp.transform.localScale[0] / 2, newp.transform.localScale[1] / 2, newp.transform.localScale[2] / 2);
        newp.transform.Rotate(0,0,90f);
        newp.GetComponent<Pieces>().setColour(pieceToReplace.GetComponent<Pieces>().getColour());
        newp.GetComponent<Pieces>().setValue(90);
        GameControl.SwitchTurn(); //Start() does a MoveTo which switchturns.. 
        newp.GetComponent<Pieces>().setPosition(pieceToReplace.GetComponent<Pieces>().getColour());

        Pieces.PieceList.Add(newp.GetComponent<Pieces>());
        Pieces.PieceList.Remove(pieceToReplace.GetComponent<Pieces>());
        Destroy(GameObject.Find(Movement.PieceToReplace.GetComponent<Pieces>().name));


        ChoosePiece.GetComponent<Canvas>().enabled = false;
        Pieces.endOfTurn();
    }
    public void Quit()
    {
        
        Application.Quit();

    }

    public static void activateChoice()
    {
        ChoosePiece.GetComponent<Canvas>().enabled = true ;
    }
    public static void activateChoice(string s)
    {
        setQueen();
    }
    public void Update()
    {
        if (TheCamera.transform.position != startPos.transform.position && run)
        {
            TheCamera.transform.position = Vector3.MoveTowards(TheCamera.transform.position, startPos.transform.position, step);
        }
        if (TheCamera.transform.forward != target - TheCamera.transform.position && run || RotateRun)
        {
            if (!LookingAt) {
                targetDirection = target - TheCamera.transform.position;
                newDirection = Vector3.RotateTowards(TheCamera.transform.forward, targetDirection, cameraspeed * Time.deltaTime, 10f);
                TheCamera.transform.rotation = Quaternion.LookRotation(newDirection);
            }
            if (TheCamera.transform.forward == target)       
            {
                TheCamera.transform.LookAt(target);
                LookingAt = true; 
            }
        }
         
        if (TheCamera.transform.position == startPos.transform.position)
        {
            GameControl.GameRunning = true; 
            CameraRotate.startUp();
            run = false;
            RotateRun = true; 

        }
    } 

}
