using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cursorkiller : MonoBehaviour {

    public GameObject CursorToKill;
    private void Start()
    {
      ShouldIkillCursor();
    }
  public  void ShouldIkillCursor() {

        //   Destroy(CursorToKill);
        if (SceneManager.GetActiveScene().name.Contains("GameSolo"))
        {
            Destroy(CursorToKill);
        }
    }
}
