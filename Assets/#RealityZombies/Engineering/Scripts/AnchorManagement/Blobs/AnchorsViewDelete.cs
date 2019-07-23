using UnityEngine;

public class AnchorsViewDelete : MonoBehaviour
{

    public TextMesh _tm; //for now just use tm , later make scrolly ui container
                         //public Text txtbox;
#if !UNITY_EDITOR && UNITY_WSA
         

    UnityEngine.XR.WSA.Persistence.WorldAnchorStore anchorStore;
#endif

#if !UNITY_EDITOR && UNITY_WSA
         


    void Start()
    {
        UnityEngine.XR.WSA.Persistence.WorldAnchorStore.GetAsync(AnchorStoreReady);
    }
#endif

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Delete)) { ClearAllClicked(); }
    //    if (Input.GetKeyDown(KeyCode.Space)) { ShowAllClicked(); }

    //}
#if !UNITY_EDITOR && UNITY_WSA
         

    void AnchorStoreReady(UnityEngine.XR.WSA.Persistence.WorldAnchorStore store)
    {
        anchorStore = store;
    }

#endif

    public void ShowAllClicked()
    {
        ClearText();
        _tm.text += "\n showall";

#if !UNITY_EDITOR && UNITY_WSA
         

        string[] ids = anchorStore.GetAllIds();
        string s1 = "found " + ids.Length + "anchors locally \n";
        _tm.text = s1;
        for (int x = 0; x < ids.Length; x++)
        {
            _tm.text += ids[x].ToString();
            _tm.text += "\n";
        }

#endif
    }
    public void ClearAllClicked()
    {
        ClearText();
        _tm.text += "\n clearall";
        //  txtbox.text += "\n clearall";
#if !UNITY_EDITOR && UNITY_WSA
         
         anchorStore.Clear();

#endif

    }

    void ClearText()
    {
        _tm.text = "";
        // txtbox.text = "";
    }
}
