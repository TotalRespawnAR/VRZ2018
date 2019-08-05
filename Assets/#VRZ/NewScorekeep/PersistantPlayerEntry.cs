using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantPlayerEntry : MonoBehaviour
{
   // Data_PlayerInfo _dPinfo;
    Data_VRZPlayerInfoScore _playerFullInfoScore;
    public static PersistantPlayerEntry Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }
    void Start()
    {
        MakeNewPlayer();


    }

    //public void UpdateFirstName(string argStr) { _dPinfo.PlayerFirstName = argStr; }
    //public void UpdateLasttName(string argStr) { _dPinfo.PlayerLastName = argStr; }
    //public void UpdateEmail(string argStr) { _dPinfo.PlayerEmail = argStr; }
    //public void UpdateUsertName(string argStr) { _dPinfo.PlayerUserName = argStr; }

    public void UpdateFirstName(string argStr) { _playerFullInfoScore.PlayerFirstName = argStr; }
    public void UpdateLasttName(string argStr) { _playerFullInfoScore.PlayerLastName = argStr; }
    public void UpdateEmail(string argStr) { _playerFullInfoScore.PlayerEmail = argStr; }
    public void UpdateUsertName(string argStr) { _playerFullInfoScore.PlayerUserName = argStr; }

    public Data_VRZPlayerInfoScore GetCurPlayerDataObj() { return this._playerFullInfoScore; }

    public override string ToString()
    {
        String thedata =_playerFullInfoScore.SessionTime +" "+ _playerFullInfoScore.FinalScore + " " + _playerFullInfoScore.FinalKills + " " + _playerFullInfoScore.FinalHeadShots + " " + _playerFullInfoScore.FinalDeaths + " " + _playerFullInfoScore.PlayerFirstName + " " + _playerFullInfoScore.PlayerLastName + " " + _playerFullInfoScore.PlayerEmail + " " + _playerFullInfoScore.PlayerUserName;
        return thedata;
    }

    public void MakeNewPlayer() {
        if (_playerFullInfoScore != null) {
            _playerFullInfoScore = null;
        }
        _playerFullInfoScore = new Data_VRZPlayerInfoScore(DateTime.Now, "fn", "ln", "un", "em", 0, 0, 0, 0);
    }
}
