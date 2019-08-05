﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class PersistantDataChecker : MonoBehaviour
{
    public VRZ_SessionSaver _sessionSaver;
    public string txt;
    PersistantPlayerEntry _playerData;
    PersistantScoreGrabber _scoreGrabber;
    Data_PlayerPoints CurPlayerPoints;
    Data_VRZPlayerInfoScore _TESTPlayerScoreobj;
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
        if (_playerData == null)
        {
            Debug.Log("persitantplayerentry did not carry over");
            _TESTPlayerScoreobj = new Data_VRZPlayerInfoScore(DateTime.Now, "joe", "shmo", "js", "j@s.com", 0, 0, 0, 0);
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

        if (_playerData != null)
        {
            _playerData.GetCurPlayerDataObj().FinalScore = CurPlayerPoints.score;
            _playerData.GetCurPlayerDataObj().FinalKills = CurPlayerPoints.kills;
            _playerData.GetCurPlayerDataObj().FinalHeadShots = CurPlayerPoints.headshots;
            _playerData.GetCurPlayerDataObj().FinalDeaths = CurPlayerPoints.deaths;

            txt = "             Please Keep your Headset on." + "\n" +
                          "             Wait for Store Staff." + "\n" +
                          " Final Score______  " + CurPlayerPoints.score + "\n" +
                          " Zombies Killed___  " + CurPlayerPoints.kills + "\n" +
                          " headshots______  " + CurPlayerPoints.headshots + "\n"+
                          " deaths  " + CurPlayerPoints.deaths + "\n";

            SAVE_playerDataToFile(_playerData.GetCurPlayerDataObj());

        }
        else {

            _TESTPlayerScoreobj.FinalScore = CurPlayerPoints.score;
            _TESTPlayerScoreobj.FinalKills = CurPlayerPoints.kills;
            _TESTPlayerScoreobj.FinalHeadShots = CurPlayerPoints.headshots;
            _TESTPlayerScoreobj.FinalDeaths = CurPlayerPoints.deaths;

            txt = "             Please Keep your Headset on." + "\n" +
                          "             Wait for Store Staff." + "\n" +
                          " Final Score______  " + _TESTPlayerScoreobj.FinalScore + "\n" +
                          " Zombies Killed___  " + _TESTPlayerScoreobj.FinalKills + "\n" +
                          " headshots______  " + CurPlayerPoints.headshots + "\n" +
                          " deaths  " + CurPlayerPoints.deaths + "\n";

            SAVE_playerDataToFile(_TESTPlayerScoreobj);
        }



            ButtonNewGame = new Rect(Xplace1, Line1, TextBoxDimentionsWidth, ToggleDimentions);
            ButtonAgain = new Rect(Xplace4, Line1, TextBoxDimentionsWidth, ToggleDimentions);


    }

    void SAVE_playerDataToFile(Data_VRZPlayerInfoScore argThisData) {
       _sessionSaver.SaveIt(argThisData);
    }

    private void OnGUI()
    {
        GUI.TextField ( new Rect(20,200,300,400),txt, GUIStyle.none);
      //  if(_playerData!=null)
        if (GUI.Button(ButtonNewGame, "newGame"))
        {
            PlayNewGame();
        }
      //  if(_scoreGrabber!=null)
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
