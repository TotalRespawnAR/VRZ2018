using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimationListener : MonoBehaviour {

    public AxeEnemyBehavior _Abeh;

    public void OnAnimeThrowApex()
    {
        _Abeh.DO_AnimeThrowApex();
    }
}
