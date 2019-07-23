using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour, IEnemyInfo {

    IEnemyEntityComp m_ieec;
    Color tmc1olorORiginal;
    Color tmc2olorORiginal;
    Color tmc3olorORiginal;
    public TextMesh tm1;
    public TextMesh tm2;
    public TextMesh tm3;

    void Awake() {
    }
    //void OnEnable() this object gets instanciated by gm then attached to enemy. it can only get a ref to enemy at start
    //{
    //}
    void OnDisable()
    {
        m_ieec.OnModeUpdated -= ShowCurMode;
        m_ieec.OnBulletWoundInflicted -= SHowHP;
    }

    void Start() {

        tmc1olorORiginal = tm1.color;
        tmc2olorORiginal = tm2.color;
        tmc3olorORiginal = tm3.color;

        m_ieec = GetComponentInParent<IEnemyEntityComp>();

        //if (m_ieec != null)
        //{
        //    tm1.text = " Found Iebc";
        //    tm1.color = Color.green;
        //}
        //else
        //{
        //    tm1.text = " NO Iebc";
        //    tm1.color = Color.red;
        //}

        m_ieec.OnModeUpdated += ShowCurMode;
        m_ieec.OnBulletWoundInflicted += SHowHP;
        tm3.text = m_ieec.Get_HP().ToString();
    }

    int StateCounter = 0;
    //to make sure that the mode update is only sent once 
    void ShowCurMode(object o, ArgsEnemyMode e) {
            tm1.text = StateCounter+"."+e.Mode.ToString();
        StateCounter++;
    }

    void ShowID(int ID) {
        tm1.text = ID.ToString();
    }
    void ShowState(EBSTATE argstate) {
        tm2.color = Color.magenta;
        tm2.text = argstate.ToString();
    }
    void SHowHP(object o, ArgsBulletWoond e) {
        tm3.text = e.CurHpforHealthbarAndScoreSender.ToString();
    }

    void ToggleGroundedCOlorTM1(bool argG) {
        if (argG) { tm1.color = Color.green; tm1.text ="GROUNDED"; } else { tm1.color = Color.red; tm1.text = ":::::"; }
    }

     
}