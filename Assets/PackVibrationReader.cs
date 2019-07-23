using HoloToolkit.Unity;
using SixenseCore;
using System.Text;
using UnityEngine;

public class PackVibrationReader : MonoBehaviour {


    public TextMesh VibrationReaderPAckLeft;
 //   protected SixenseCore.TrackerVisual[] m_trackers;
    public SixenseCore.TrackerVisual LeftPackVis;
    StringBuilder sb;
    // Use this for initialization
    void Start () {
        sb = new StringBuilder();
     }
	
	// Update is called once per frame
	void Update () {
        ReadStemInputs();
    }
    float cume = 0f;
    void ReadStemInputs() {
      //  Debug.Log("YIYIYIIY");

        //if (Device.GetTracker(TrackerID.PACK_LEFT) != null)
        //{
        //    cume += Device.GetTracker(TrackerID.PACK_LEFT).GetVibration();

        //    //sb.Append(cume.ToString());
        //    //sb.Append(" ");
        //    //sb.Append();
        //   // Debug.Log(Device.GetTracker(TrackerID.PACK_LEFT).Position.x.ToString());
        //    VibrationReaderPAckLeft.text = cume.ToString();
        //}
    }
}
