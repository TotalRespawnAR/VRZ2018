using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZStatModel
{

    private int _curHealth;

    public int CurrHealth
    {
        get { return _curHealth; }
        set { _curHealth = value; }
    }


    /// <summary>
    ///      Dictionary holds the following map
    ///      
    ///      meshbone Name  ||   was mesh/colider already disabled? 0= NO , they are not dissabled, they are alive
    /// </summary>
    private int _MaxLIMBHIT;
    //private int _MaxHitsArms;
    //private int _MaxHitsLegs;
    // private int _CurHits_Head_M;


    Dictionary<ARZMMeshBone, int> DICINT;

    Dictionary<ARZMMeshBone, bool> DICBOOL;
    public ZStatModel(int argCurHealth)
    {
        InitDiCs();
        _curHealth = argCurHealth;
        _MaxLIMBHIT = 0;
        //_MaxHitsArms=3;
        //_MaxHitsLegs=3;
    }


    void InitDiCs()
    {
        DICBOOL = new Dictionary<ARZMMeshBone, bool>();
        DICINT = new Dictionary<ARZMMeshBone, int>();

        foreach (ARZMMeshBone val in Enum.GetValues(typeof(ARZMMeshBone)))
        {
            // Logger.Debug(val.ToString() + " " + (int)val);
            DICBOOL.Add(val, true);
            DICINT.Add(val, 0);
        }
    }

    public bool IsLimbOn(ARZMMeshBone argLimbName)
    {
        bool OutVal = false;
        //  if (DICBOOL.ContainsKey(argLimbName)) {
        OutVal = DICBOOL[argLimbName];
        // }
        return OutVal;
    }

    public void MarkLimbAsDeleted(ARZMMeshBone argStr)
    {
        DICBOOL[argStr] = false;
    }
    public bool CanIncrement(ARZMMeshBone ArgLimbnameTotakeHit)
    {
        if (DICINT[ArgLimbnameTotakeHit] < _MaxLIMBHIT)
        {
            DICINT[ArgLimbnameTotakeHit]++;
            //  Logger.Debug(ArgLimbnameTotakeHit +" hit rgistered " + DICINT[ArgLimbnameTotakeHit]  );
            return true;
        }
        return false;
    }


    public List<ARZMMeshBone> MakePropperListOfLimbsInvolved(ARZMMeshBone argBoneMEshNAme_BoneThaWasHit)
    {


        //-------------------
        //  zer child SINGLE LEAFE NoNeed, this will always have a first bone., the oen that was hit
        //------------------
      
        List<ARZMMeshBone> ListOfBones_mustHaveAtLeastOneElemnt = new List<ARZMMeshBone>();
        ListOfBones_mustHaveAtLeastOneElemnt.Add(argBoneMEshNAme_BoneThaWasHit);
        int IndexEnum = (int)argBoneMEshNAme_BoneThaWasHit;

        //-------------------
        // ONE CJHILD 
        //------------------

        if (IndexEnum > 4 && IndexEnum < 9)
        {

            return Add_Double_ElemntList(argBoneMEshNAme_BoneThaWasHit);
        }

        //-------------------
        // 2 children shoulders only have 2 children 
        //------------------
        if ( IndexEnum > 8  && IndexEnum <11)
        {

            return Add_Triple_ElemntList(argBoneMEshNAme_BoneThaWasHit);

        }

        //foreach (ARZMMeshBone m in NamesOfBones) {
        //    string OUTSTR = (int)m + "  " + m;
        //    Logger.Debug(OUTSTR);

        //}

        return ListOfBones_mustHaveAtLeastOneElemnt;
    }

    List<ARZMMeshBone> Add_singleElemntList(ARZMMeshBone argBoneMEshNAme_BoneThaWasHit) {
        List<ARZMMeshBone> ListOfBonesInChain = new List<ARZMMeshBone>();
        ListOfBonesInChain.Add(argBoneMEshNAme_BoneThaWasHit);
        return ListOfBonesInChain;
    }

    List<ARZMMeshBone> Add_Double_ElemntList(ARZMMeshBone argBoneMEshNAme_BoneThaWasHit)
    {
        List<ARZMMeshBone> ListOfBonesInChain = new List<ARZMMeshBone>();
        ListOfBonesInChain.Add(argBoneMEshNAme_BoneThaWasHit);

        if (argBoneMEshNAme_BoneThaWasHit == ARZMMeshBone.Hip_L)
        {
            if (IsLimbOn(ARZMMeshBone.Knee_L))
            {
                ListOfBonesInChain.Add(ARZMMeshBone.Knee_L);
            }
        }
        if (argBoneMEshNAme_BoneThaWasHit == ARZMMeshBone.Hip_R)
        {
            if (IsLimbOn(ARZMMeshBone.Knee_R))
            {
                ListOfBonesInChain.Add(ARZMMeshBone.Knee_R);
            }
        }

        if (argBoneMEshNAme_BoneThaWasHit == ARZMMeshBone.Elbow_L)
        {
            if (IsLimbOn(ARZMMeshBone.Wrist_L))
            {
                ListOfBonesInChain.Add(ARZMMeshBone.Wrist_L);
            }
        }


        if (argBoneMEshNAme_BoneThaWasHit == ARZMMeshBone.Elbow_R)
        {
            if (IsLimbOn(ARZMMeshBone.Wrist_R))
            {
                ListOfBonesInChain.Add(ARZMMeshBone.Wrist_R);
            }
        }

        return ListOfBonesInChain;
    }

    List<ARZMMeshBone> Add_Triple_ElemntList(ARZMMeshBone argBoneMEshNAme_BoneThaWasHit)
    {
        List<ARZMMeshBone> ListOfBonesInChain = new List<ARZMMeshBone>();
        ListOfBonesInChain.Add(argBoneMEshNAme_BoneThaWasHit);

        if (argBoneMEshNAme_BoneThaWasHit == ARZMMeshBone.Shoulder_L)
        {
            if (IsLimbOn(ARZMMeshBone.Elbow_L))
            {
                ListOfBonesInChain.Add(ARZMMeshBone.Elbow_L);
                if (IsLimbOn(ARZMMeshBone.Wrist_L))
                {
                    ListOfBonesInChain.Add(ARZMMeshBone.Wrist_L);
                }
            }
        }
        if (argBoneMEshNAme_BoneThaWasHit == ARZMMeshBone.Shoulder_R)
        {
            if (IsLimbOn(ARZMMeshBone.Elbow_R))
            {
                ListOfBonesInChain.Add(ARZMMeshBone.Elbow_R);
                if (IsLimbOn(ARZMMeshBone.Wrist_R))
                {
                    ListOfBonesInChain.Add(ARZMMeshBone.Wrist_R);
                }
            }
        }

        return ListOfBonesInChain;
    }


    public void MarkAsDeleted_Delet_Create(List<ARZMMeshBone> arglistbm)
    {
        foreach (ARZMMeshBone amb in arglistbm)
        {
            MarkLimbAsDeleted(amb);

        }
    }


    public bool DoWeNeedToCrawm(List<ARZMMeshBone> arglistbm)
    {
        foreach (ARZMMeshBone amb in arglistbm)
        {
            if ((int)amb > 2 && (int)amb < 7) return true;

        }
        return false;
    }

}
