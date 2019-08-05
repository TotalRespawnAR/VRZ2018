using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntryUI : MonoBehaviour {

 
 
    //      y                                         .     y
    //      |                                         .     |                                         .
    //  ....x                                         . ....x                                         
    //      |-------------------|   ^                 .
    //      | <------width----->|   | height          .     | <------width----->|   | height          
    //      |___________________| ..v..........Xspace .     |___________________| ..v..........Xspace 
    //                         |            
    //                         | Yspace
    //                         |
    //      y                  |
    //      |                  |
    //  ....x                  |
    //      |---------------------|   ^
    //      | <------width------->|   | height
    //      |_____________________| ..v..........Xspace 
    //                         |            
    //                         | Yspace
    //                         |
    // Xspace
    float Xspace = 5f;
    float Yspace = 5f;
    float TextBoxDimentionsWidth = 120f; //=12 chars round to 10 
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
 
    Rect Lable_firstName;
    Rect Box_FirstName;

    Rect Lable_LastName;
    Rect Box_LastName;
    Rect Lable_UserName;
    Rect Box_UserName;
    Rect Lable_Email;
    Rect Box_Email;
    Rect ButtonNext;
    Rect ButtonClearAll;
    Rect ButtonPlayAgain;

    string stringToEdit;
    string str_fn;
    string str_ln;
    string str_email;
    string str_un;

    private void OnEnable()
    {
        stringToEdit = "";
        str_fn = "";
        str_ln = "";
        str_email = "";
        str_un = "";

        Lable_firstName = new Rect(Xplace1, Line1, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        Lable_LastName = new Rect(Xplace1, Line2, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        Lable_Email = new Rect(Xplace1, Line3, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        Lable_UserName = new Rect(Xplace1, Line4, TextBoxDimentionsWidth, TextBoxDimentionsHeight);

        Box_FirstName = new Rect(Xplace3, Line1, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        Box_LastName = new Rect(Xplace3, Line2, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        Box_Email = new Rect(Xplace3, Line3, TextBoxDimentionsWidth, TextBoxDimentionsHeight);
        Box_UserName = new Rect(Xplace3, Line4, TextBoxDimentionsWidth, TextBoxDimentionsHeight);

        ButtonNext = new Rect(Xplace1, Line5, TextBoxDimentionsWidth, ToggleDimentions);
        ButtonClearAll = new Rect(Xplace2, Line5, TextBoxDimentionsWidth, ToggleDimentions);
        ButtonPlayAgain = new Rect(Xplace3, Line5, TextBoxDimentionsWidth, ToggleDimentions);
        stringToEdit = "";
    }

    private void OnDisable()
    {
    

    }



    private void Start()
    {
 
    }
    private void OnGUI()
    {

        str_fn = GUI.TextField(Box_FirstName, str_fn);
        GUI.TextArea(Lable_firstName, "first name",GUIStyle.none);

        str_ln =GUI.TextField(Box_LastName, str_ln);
        GUI.TextArea(Lable_LastName, "last name", GUIStyle.none);

        str_email = GUI.TextField(Box_Email, str_email);
        GUI.TextArea(Lable_Email, "email", GUIStyle.none);

        str_un = GUI.TextField(Box_UserName, str_un);

        GUI.TextArea(Lable_UserName, "user name", GUIStyle.none);


        if (GUI.Button(ButtonNext, "next"))
        {
            GoToScene();
        }

        //if (GUI.Button(ButtonClearAll, "clearall"))
        //{
            
        //}

        if (GUI.Button(ButtonPlayAgain, "replay"))
        {
             
        }
    }
    void GoToScene()
    {
        PersistantPlayerEntry.Instance.UpdateFirstName(str_fn);
        PersistantPlayerEntry.Instance.UpdateLasttName(str_ln);
        PersistantPlayerEntry.Instance.UpdateEmail(str_email);
        PersistantPlayerEntry.Instance.UpdateUsertName(str_un);


       // Debug.Log(PersistantPlayerEntry.Instance.ToString() + stringToEdit);

        SceneManager.LoadScene("RunThisVRGame");
    }

 

}
