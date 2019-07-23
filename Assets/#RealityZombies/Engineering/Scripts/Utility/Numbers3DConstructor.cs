using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numbers3DConstructor : MonoBehaviour {

    #region dependencies
    #endregion

    #region PublicVars
    public GameObject StreakContainerRef; //should have script for flying up, and destroy on timer, and billboard
    public GameObject[] ALLDigits0_9;//for now take th whole alpabet, later trim t to digits only and use modulo method to parse integers and get array at index  1203 ->getarra[1] getarra[2] getarra[0]....

    #endregion

    #region PrivateVars
    float xNextLetterxPosition = 0f;
    float OffsetNormal = -0.22f;
    float OffsetShort = -0.12f;
    float calculatedXoffset = 0f; //this is the actual length of the word on the x axis. divid that by 2 and you can center the word 

    #endregion

    #region INIT

    private void Start()
    {
        // MakeanEmptyWordHolder();
        // Inst3dFromChar('N');

        // MakeWordFromString("175", MakeanEmptyWordHolder());
    }
    #endregion

    #region PublicMethods
    public GameObject Get_Build_NEW_3Dnumber(int argInts)
    {


        GameObject OutputObject = Make_StreakContainer("_3DstreakObj");
       // OutputObject.AddComponent<DestroyTimer>().timeInSeconds = GameSettings.Instance.FadeOutStartIn + GameSettings.Instance.FadeInStartIn;
        string argString = argInts.ToString(); //this needs refactoring later . just use modulo to extract each digit , and 
        calculatedXoffset = CalculateOffsetBasedOnStringLength(argString);
        string AllCaps = argString.ToUpper();
        for (int i = 0; i < AllCaps.Length; i++)
        {
            if (AllCaps[i] == ' ')
            {
                xNextLetterxPosition += OffsetNormal;
                continue;
            }
            GameObject curletter = Inst3dFromChar(AllCaps[i]);
            curletter.name = "letter_" + AllCaps[i];

            curletter.transform.position = new Vector3(xNextLetterxPosition, 0f, 0f);
            curletter.transform.parent = OutputObject.transform;
            if (AllCaps[i] == 'i' || AllCaps[i] == ',' || AllCaps[i] == ';' || AllCaps[i] == '1')
            {
                xNextLetterxPosition += OffsetShort;
            }
            else
            {
                xNextLetterxPosition += OffsetNormal;
            }
        }
       // OutputObject.transform.parent = this.transform;
        OutputObject.transform.localPosition = new Vector3(calculatedXoffset, 0, 0);
        OutputObject.transform.localEulerAngles = new Vector3(0, 90, 0);

        xNextLetterxPosition = 0f;
        //detach the 3d word object to destroy it alone
        //OutputObject.transform.parent = null;
        return OutputObject;
    }
    #endregion

    #region PrivateMethods


    GameObject Make_StreakContainer(string argObjName)
    {
        GameObject EmptyWordRootObject = Instantiate(StreakContainerRef);
        EmptyWordRootObject.name = argObjName;
        return EmptyWordRootObject;
    }
    //handle spaces
    float CalculateOffsetBasedOnStringLength(string argString)
    {
        if (string.IsNullOrEmpty(argString)) { return 0.0f; }
        else
        {
            return (float)argString.Length / -10;
        }
    }
  
    GameObject Inst3dFromChar(char argChar)
    {
        return Instantiate(ALLDigits0_9[(int)argChar - 48]);
    }
    #endregion
}
    

//unused func
    //void MakeWordFromString(string argString, GameObject argWordRoot)
    //{
    //    string AllCaps = argString.ToUpper();
    //    for (int i = 0; i < AllCaps.Length; i++)
    //    {
    //        if (AllCaps[i] == ' ')
    //        {
    //            xNextLetterxPosition += OffsetNormal;
    //            continue;
    //        }
    //        GameObject curletter = Inst3dFromChar(AllCaps[i]);
    //        curletter.name = "letter_" + AllCaps[i];

    //        curletter.transform.position = new Vector3(xNextLetterxPosition, 0f, 0f);
    //        curletter.transform.parent = argWordRoot.transform;
    //        if (AllCaps[i] == 'i' || AllCaps[i] == ',' || AllCaps[i] == ';' || AllCaps[i] == '1')
    //        {
    //            xNextLetterxPosition += OffsetShort;
    //        }
    //        else
    //        {
    //            xNextLetterxPosition += OffsetNormal;
    //        }
    //    }
    //    argWordRoot.transform.parent = this.transform;
    //    argWordRoot.transform.localPosition = Vector3.zero;
    //}
