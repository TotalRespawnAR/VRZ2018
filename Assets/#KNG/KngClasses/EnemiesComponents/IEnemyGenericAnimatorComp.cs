using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyGenericAnimatorComp  {

    void Do_IDLE_Anim();
    void Do_GRAVEEMERGE_Anim();
    void Do_WALK_Anim();
    void Do_RUN_Anim();
    void Do_SPRINT_Anim();
    void Do_DIE_Anim();
    void Do_COMBATCROUCH_Anim(bool argcrouch);
    void Do_BLOCK_Anim(bool argBlock);
    void DO_COMBATPUNCH_Anim();
    void DO_COMBATwalkLEFT_Anim();
    void DO_COMBATwalkRIGHT_Anim();
    void DO_COMBATwalkFORWARD_Anim();
    void DO_COMBATwalkBACK_Anim();
    void DO_SpringRoll_Anim();
    void DO_HEADSHOT_Anim();
    void Do_GUTSHOT_Anim();
    void DO_POINTING_Anim();
    void DO_PICKUP1_ANIM();
    void DO_PICKUP2_Anim();
    void DoSwingAnim();
    void DoThrowNaim();

}
