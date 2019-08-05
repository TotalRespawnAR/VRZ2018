using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersistantDataChecker : MonoBehaviour
{

    public string txt;
    PersistantPlayerEntry _playerData;
    PersistantScoreGrabber _scoreGrabber;
    Data_PlayerPoints CurPlayerPoints;

    Rect ButtonNewGame;
    Rect ButtonAgain;
    float Xspace = 5f;
    float Yspace = 5f;
    float TextBoxDimentionsWidth = 100f; //=12 chars round to 10 
    float TextBoxDimentionsHeight = 20f;
    float ToggleDimentions = 20f;
    float PosX = 10;
    float PosY = 10;
    float curXpos = 0;
    float curYpos = 0;

    float Line1 = 10f;
    float Line2 = 30f;
    float Line3 = 50f;
    float Line4 = 70f;
    float Line5 = 90f;
    float Line6 = 110f;
    float Line7 = 130f;
    float Line8 = 150f;
    float Line9 = 170f;
    float Line10 = 190f;

    float Xplace1 = 10f;
    float Xplace2 = 110f;
    float Xplace3 = 210f;
    float Xplace4 = 310f;
    float Xplace5 = 410f;
    // this only checkos the player info and the accumulated scores , it may also just save the damn entry 
    void Start()
    {
        _playerData = PersistantPlayerEntry.Instance;
        if (_playerData  == null) {
            Debug.Log("persitantplayerentry did not carry over");
        }


        _scoreGrabber = PersistantScoreGrabber.Instance;
        if (_scoreGrabber == null) {
            CurPlayerPoints = new Data_PlayerPoints(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        }
        else
        {
            if (CurPlayerPoints == null)
            {
                CurPlayerPoints = _scoreGrabber.Get_Data_Player();
            }
        }

        _playerData.GetCurPlayerDataObj().FinalScore = CurPlayerPoints.score;
        _playerData.GetCurPlayerDataObj().FinalKills = CurPlayerPoints.kills;
        _playerData.GetCurPlayerDataObj().FinalHeadShots = CurPlayerPoints.headshots;
        _playerData.GetCurPlayerDataObj().FinalDeaths = CurPlayerPoints.deaths;

        txt = "             Please Keep your Headset on." + "\n" +
                      "             Wait for Store Staff." + "\n" +
                      "       Final Score______  " + CurPlayerPoints.score + "\n" +
                      "       Zombies Killed___  " + CurPlayerPoints.kills + "\n" +
                      "       Points Lost______  " + CurPlayerPoints.pointslost + "\n";
        ButtonNewGame = new Rect(Xplace1, Line1, TextBoxDimentionsWidth, ToggleDimentions);
        ButtonAgain = new Rect(Xplace4, Line1, TextBoxDimentionsWidth, ToggleDimentions);
    }

    private void OnGUI()
    {
        if (GUI.Button(ButtonNewGame, "newGame"))
        {
            PlayNewGame();
        }
        if (GUI.Button(ButtonAgain, "Re play"))
        {
            PlayAgain();
        }
    }

    void PlayAgain() {

        SceneManager.LoadScene("RunThisVRGame");
    }

      void PlayNewGame()
    {
        _playerData.MakeNewPlayer();
        _scoreGrabber.Clear_DataPoints();
        SceneManager.LoadScene("PlayerEntry");
    }



}
