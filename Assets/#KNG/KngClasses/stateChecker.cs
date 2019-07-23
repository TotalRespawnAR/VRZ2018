using UnityEngine;

public class stateChecker : MonoBehaviour
{
    public TextMesh tx;
    // Use this for initialization
    void Start()
    {
        // tx.text = "strted";
    }



    void Update()
    {
        //if (GameManager.Instance)
        //{


        //    if (GameManager.Instance.KngGameState == ARZState.WavePlay || GameManager.Instance.KngGameState == ARZState.WaveOverTime)
        //    {
        //        textstr = GameManager.Instance.KngGameState.ToString() + "\n z on screen " + GameManager.Instance.GetLevelManager().GetCurrWaveObj().GetComponent<ILevelProps>().Get_LevelMaxEnemiesOnScreen().ToString() + "\n zhp" + GameManager.Instance.GetLevelManager().GetCurrWaveObj().GetComponent<ILevelProps>().Get_LevelHP().ToString();
        //    }
        //    else
        //        textstr = GameManager.Instance.KngGameState.ToString();


        //    tx.text = textstr;
        //}

    }
}
