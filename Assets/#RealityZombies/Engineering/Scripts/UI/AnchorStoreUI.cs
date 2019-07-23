using UnityEngine;
using UnityEngine.UI;

public class AnchorStoreUI : MonoBehaviour
{

    public TextMesh _tm; //for now just use tm , later make scrolly ui container
    public Text txtbox;

#if !UNITY_EDITOR && UNITY_WSA
         

    UnityEngine.XR.WSA.Persistence.WorldAnchorStore anchorStore;
    void Start () {
        UnityEngine.XR.WSA.Persistence.WorldAnchorStore.GetAsync(AnchorStoreReady);
    }
#endif



    public void DoShowAnchors()
    {
#if !UNITY_EDITOR && UNITY_WSA
         

        ShowAllClicked();
#endif

    }
    public void DoClearAllAnchors()
    {
#if !UNITY_EDITOR && UNITY_WSA
         

        ClearAllClicked();
#endif

    }


#if !UNITY_EDITOR && UNITY_WSA
         
    void AnchorStoreReady(UnityEngine.XR.WSA.Persistence.WorldAnchorStore store)
    {
        anchorStore = store;
    }


#endif



#if !UNITY_EDITOR && UNITY_WSA
         


    public void ShowAllClicked()
    {
        ClearText();
        _tm.text += "\n showall";
        string[] ids = anchorStore.GetAllIds();
        string s1 = "found " + ids.Length + "anchors locally \n";
        _tm.text = s1;
        for (int x = 0; x < ids.Length; x++)
        {
            _tm.text += ids[x].ToString();
            _tm.text += "\n";
        }
    }
    public void ClearAllClicked()
    {
        ClearText();
        _tm.text += "\n clearall";
        txtbox.text += "\n clearall";
        anchorStore.Clear();
    }

#endif

    void ClearText()
    {
        _tm.text = "";
        txtbox.text = "";
    }
}
