using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text3DContructor : MonoBehaviour {



    #region dependencies
    #endregion

    #region PublicVars
    public GameObject[] AllCharacters;
    #endregion

    #region PrivateVars
    float xNextLetterxPosition = 0f;
    float OffsetNormal = -0.22f;
    float OffsetShort = -0.12f;
    float calculatedXoffset = 0f; //this is the actual length of the word on the x axis. divid that by 2 and you can center the word 
    GameObject CurrentWordObj=null;

    #endregion
 

    #region PublicMethods
    public void Build_NEW_3DWord(string argString, bool isFinal)
    {

        calculatedXoffset = CalculateOffsetBasedOnStringLength(argString);
        if (CurrentWordObj != null) {
            Destroy(CurrentWordObj);
        }
        CurrentWordObj = null;

        CurrentWordObj = MakeaNamedEmptyWordHolder(argString+"_WordObj");
        if (isFinal)
        {
            CurrentWordObj.AddComponent<DestroyTimer>().timeInSeconds = GameSettings.Instance.GET_FadeFinalInStartIn() + GameSettings.Instance.GET_FadeFinalInStartIn();
        }
        else {
            CurrentWordObj.AddComponent<DestroyTimer>().timeInSeconds = GameSettings.Instance.GET_FadeFinalInStartIn() + GameSettings.Instance.GET_SHORTTIME_4seconds();
        }
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
            curletter.transform.parent = CurrentWordObj.transform;
            if (AllCaps[i] == 'i' || AllCaps[i] == ',' || AllCaps[i] == ';' || AllCaps[i] == '1')
            {
                xNextLetterxPosition += OffsetShort;
            }
            else
            {
                xNextLetterxPosition += OffsetNormal;
            }
        }
        CurrentWordObj.transform.parent = this.transform;
        CurrentWordObj.transform.localPosition = new Vector3(calculatedXoffset, 0,0);
        CurrentWordObj.transform.localEulerAngles = new Vector3(0,180,0);

        xNextLetterxPosition = 0f;
        //detach the 3d word object to destroy it alone
        CurrentWordObj.transform.parent = null;

    }
    #endregion

    #region PrivateMethods
    GameObject MakeanEmptyWordHolder() {
        GameObject EmptyWordRootObject = new GameObject();
        EmptyWordRootObject.name = "myWordRoot";
        return EmptyWordRootObject;
    }

    GameObject MakeaNamedEmptyWordHolder(string argObjName)
    {
        GameObject EmptyWordRootObject = new GameObject();
        EmptyWordRootObject.name = argObjName;
        return EmptyWordRootObject;
    }
    //handle spaces
    void MakeWordFromString(string argString, GameObject argWordRoot) {
        string AllCaps = argString.ToUpper();
        for (int i = 0; i < AllCaps.Length; i++) {
            if (AllCaps[i] == ' ')
            {
                xNextLetterxPosition += OffsetNormal;
                continue; }
            GameObject curletter = Inst3dFromChar(AllCaps[i]);
            curletter.name = "letter_" + AllCaps[i];

            curletter.transform.position = new Vector3(xNextLetterxPosition, 0f, 0f);
            curletter.transform.parent = argWordRoot.transform;
            if (AllCaps[i] == 'i' || AllCaps[i] == ',' || AllCaps[i] == ';' || AllCaps[i] == '1') {
                xNextLetterxPosition += OffsetShort;
            }
            else {
            xNextLetterxPosition += OffsetNormal;
            }
        }
        argWordRoot.transform.parent = this.transform;
        argWordRoot.transform.localPosition = Vector3.zero;
    }
    GameObject Inst3dFromChar(char argChar) {
        return Instantiate(AllCharacters[(int)argChar-48]);
    }

    float CalculateOffsetBasedOnStringLength(string argString) {
        if (string.IsNullOrEmpty(argString)) { return 0.0f; }
        else
        {
            return (float)argString.Length / -10;
        }    
    }
    //void Update()
    //{

    //}

    //private void OnDestroy()
    //{

    //}
    #endregion
}
