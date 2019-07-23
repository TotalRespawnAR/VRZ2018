//using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.UI;

public class CountDownAudioControl : MonoBehaviour
{

    //Note:
    //This will start as soon as the player is in scene

    //private UAudioManager audioManager;
    public Text CountdownTextBox;
    public TextMesh CountDownText;
    void OnEnable()
    {
        GameEventsManager.CountDownHandler += DOCountDouwn;

    }
    private void OnDisable()
    {
        GameEventsManager.CountDownHandler -= DOCountDouwn;
    }
    void Start()
    {
        Debug.Log("Need AUDIO COuntDown" + gameObject.name);
        // audioManager = GetComponent<UAudioManager>();
    }

    public void DOCountDouwn(int argNumber)
    {
        if (argNumber > 0 && argNumber < 11)
        {
            // audioManager.PlayEvent("_" + argNumber.ToString());
            CountDownText.text = argNumber.ToString();
        }
        if (argNumber == 1)
        {
            TimerBehavior t = gameObject.AddComponent<TimerBehavior>();
            t.StartTimer(1, HideCountDown);
        }
    }

    public void HideCountDown()
    {
        CountDownText.text = "";
    }
}
