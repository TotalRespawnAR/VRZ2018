using UnityEngine;

public class KngAudioManager : MonoBehaviour
{
    //this is static 2d sounds, do NOT place on player
    public static KngAudioManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    //suddendeath



    //gameover

    //ding

    //woosh

    //takingdamage crunsh

    //takingdamage scratch

    //takingdamge glass crack



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
