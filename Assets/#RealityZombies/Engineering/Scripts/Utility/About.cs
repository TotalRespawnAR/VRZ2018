// @Author Jeffrey M. Paquette ©2016

using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class About : MonoBehaviour {


    //ARZMALL5p5p2p0
    //ARZPACKhfmf
    [Tooltip("Text field with version information.")]
    public Text titleText;
    public Text versionText;

	// Use this for initialization
	void Start () {
        //string ApplicationVersionName = "version :" + Application.version.ToString();
        //titleText.text = Application.productName.ToString(); // ApplicationVersionName;
        //versionText.text = Application.version.ToString();

        // StartCoroutine(ShowinOneSec());
        if (GameSettings.Instance)
        {
            versionText.text = GameSettings.Instance.GameVersion;

            titleText.text = GameSettings.Instance.GameName;
        }
        else
            Debug.LogError("Must have a gamesettings");

    }

    //IEnumerator ShowinOneSec() {
    //    yield return new WaitForSeconds(1);
    //}
}



