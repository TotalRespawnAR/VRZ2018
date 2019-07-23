using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAnimatorTester : MonoBehaviour {

    public GameObject TheZombie;
    public Transform SpawnPoint;
 

    ////ZombieState StateToUse;
    Animator _animtr;


    
    void Start () {
  
     
 
    }

    //private void Update()
    //{
    //   // if (Input.GetKeyDown(KeyCode.Space)) {
    //        // _animtr.SetTrigger("trigLanding");
    //        // _animtr.SetInteger("state", 0);
    //        //_zbeh.Raggme();
    //    // }


    //    if (Input.GetKeyDown(KeyCode.Alpha0)) {
    //        _animtr.SetInteger("state", 20);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        _animtr.SetInteger("state", 21);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        _animtr.SetInteger("state", 22);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        _animtr.SetInteger("state", 23);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        _animtr.SetInteger("state", 24);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha5))
    //    {
    //        _animtr.SetInteger("state", 25);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha6))
    //    {
    //        _animtr.SetInteger("state", 26);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha7))
    //    {
    //        _animtr.SetInteger("state", 27);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha8))
    //    {
    //        _animtr.SetInteger("state", 0);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha9))
    //    {
    //        _animtr.SetInteger("state", 4);
    //    }
    //}

        #region INITS
        ////bool isNoHead;
        ////int chaseType04;
        ////int walktye04;
        //int Int_IDlE = 0;  
        //int Int_Walk = 0; 
        //int Int_Chase = 0; public InputField FieldIntChase; public Text LableIntChase;
        //int Int_Reach = 0; public InputField FieldIntReach; public Text LableIntReach;
        //int Int_Attack = 0; public InputField FieldIntAttack; public Text LableIntAttack;
        //int Int_HitDie = 0; public InputField FieldIntHitDie; public Text LableIntHitDie;
        //int Int_Die = 0; public InputField FieldIntDie; public Text LableIntDie;
        void InitCanvas() {

        //FieldIntIdle.ActivateInputField();
        //FieldIntIdle.onValueChanged.AddListener(delegate { Set_Idle_num(); });

        //FieldIntWalk.ActivateInputField();
        //FieldIntWalk.onValueChanged.AddListener(delegate { Set_Walk_num(); });

        //FieldIntChase.ActivateInputField();
        //FieldIntChase.onValueChanged.AddListener(delegate { Set_Chase_num(); });

        //FieldIntReach.ActivateInputField();
        //FieldIntReach.onValueChanged.AddListener(delegate { Set_Reach_num(); });

        //FieldIntAttack.ActivateInputField();
        //FieldIntAttack.onValueChanged.AddListener(delegate { Set_Attack_num(); });

        //FieldIntDie.ActivateInputField();
        //FieldIntDie.onValueChanged.AddListener(delegate { Set_Die_num(); });

        //FieldIntHitDie.ActivateInputField();
        //FieldIntHitDie.onValueChanged.AddListener(delegate { Set_HitDie_num(); });
    }
    //public void Set_Idle_num()
    //{
    //    if (FieldIntIdle.text == "") { Int_IDlE = 0; } else { Int_IDlE = int.Parse(FieldIntIdle.text); }
    //    LableIntIdle.text =(Int_IDlE).ToString();
    //}
    //public void Set_Walk_num()
    //{
    //    if (FieldIntWalk.text == "") { Int_Walk = 0; } else { Int_Walk = int.Parse(FieldIntWalk.text); }
    //    LableIntWalk.text = (Int_Walk).ToString();
    //}
    //public void Set_Chase_num()
    //{
    //    if (FieldIntChase.text == "") { Int_Chase = 0; } else { Int_Chase = int.Parse(FieldIntChase.text); }
    //    LableIntChase.text = (Int_Chase).ToString();
    //}
    //public void Set_Reach_num()
    //{
    //    if (FieldIntReach.text == "") { Int_Reach = 0; } else { Int_Reach = int.Parse(FieldIntReach.text); }
    //    LableIntReach.text = (Int_Reach).ToString();
    //}
    //public void Set_Attack_num()
    //{
    //    if (FieldIntAttack.text == "") { Int_Attack = 0; } else { Int_Attack = int.Parse(FieldIntAttack.text); }
    //    LableIntAttack.text = (Int_Attack).ToString();
    //}
    //public void Set_Die_num()
    //{
    //    if (FieldIntDie.text == "") { Int_Die = 0; } else { Int_Die = int.Parse(FieldIntDie.text); }
    //    LableIntDie.text = (Int_Die).ToString();
    //}
    //public void Set_HitDie_num()
    //{
    //    if (FieldIntHitDie.text == "") { Int_HitDie = 0; } else { Int_HitDie = int.Parse(FieldIntHitDie.text); }
    //    LableIntHitDie.text = (Int_HitDie).ToString();
    //}



    //public void Click_Idle() {
    //   // _animState.TestUpdateState(ZombieState.IDLE, Int_IDlE);
    //}

    //public void Click_Walk()
    //{

    //    //_animState.TestUpdateState(ZombieState.WALKING, Int_Walk);

    //}
    //public void Click_Chase()
    //{
    //  //  _animState.TestUpdateState(ZombieState.CHASING, Int_Chase);
    //}

    //public void Click_Reach()
    //{
    //    //_animState.TestUpdateState(ZombieState.REACHING, Int_Reach);

    //}

    //public void Click_Attack()
    //{
    //    //_animState.TestUpdateState(ZombieState.ATTACKING, Int_Attack);

    //}

    //public void Click_Die()
    //{
    //    //_animState.TestUpdateState(ZombieState.DEAD, Int_Die);

    //}

    //public void Click_HitDie()
    //{


    //}

    //public void Click_HitTrig()
    //{
    //   // _animState.Trigger_HeadShotAnim();
    //}
    #endregion
}
