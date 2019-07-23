// @Author Nabil Lamriben ©2017

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ILevel  {
    void StartLevel_WAVEPLAY();
    void FinishLevel();
    void ReloadLevel();
    void On_TickerTime(int argSecondTicked);
    void ControllTunnel(LeftMidRight argLeftMdRight, bool argOenClose);


}

