// @Author Nabil Lamriben ©2017

using UnityEngine;


public class ConsoleScreen : MonoBehaviour
{

    public TextMesh tm;
#if !UNITY_EDITOR && UNITY_WSA
         

    UnityEngine.XR.WSA.Persistence.WorldAnchorStore anchorStore;
#endif

    void Start()
    {
#if !UNITY_EDITOR && UNITY_WSA
         

         UnityEngine.XR.WSA.Persistence.WorldAnchorStore.GetAsync(AnchorStoreReady);
#endif

    }

#if !UNITY_EDITOR && UNITY_WSA
         

    void AnchorStoreReady(UnityEngine.XR.WSA.Persistence.WorldAnchorStore store)
    {
        anchorStore = store;
    }
#endif


    public void ShowAllClicked()
    {

#if !UNITY_EDITOR && UNITY_WSA
         
        tm.text += "\n showall";
        //#if !UNITY_EDITOR
        //        tm.text += "\n  no can show anchors cuz we in editor ";
        //        return;

        //#else

        string[] ids = anchorStore.GetAllIds();
        string s1 = "found " + ids.Length + "anchors locally \n";
        tm.text = s1;
        for (int x = 0; x < ids.Length; x++)
        {
            tm.text += ids[x].ToString();
            tm.text += "\n";
        }

        //#endif

#endif



    }
    public void ClearAllClicked()
    {

#if !UNITY_EDITOR && UNITY_WSA
         
        tm.text += "\n clearall";

        //#if !UNITY_EDITOR
        //        tm.text += " \n no can clear anchors cuz we in editor ";
        //        return;

        //#else

        anchorStore.Clear();


        //#endif

#endif


    }
    public void CallScreen(object o)
    {
        if (o is string)
        {
            if (o.ToString() == "LocalAnchorsOBJ") { ShowAllClicked(); }
            else
                 if (o.ToString() == "DeleteAllLocalOBJ") { ClearAllClicked(); }

        }
    }
}
