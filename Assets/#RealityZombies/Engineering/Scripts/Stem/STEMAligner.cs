using UnityEngine;


public class STEMAligner : MonoBehaviour
{

    public GameObject virtualObject;
    public GameObject trackerObject;
    public GameObject visualiser;
    public GameObject coreDevice;

    private SixenseCore.Device device;
#if !UNITY_EDITOR && UNITY_WSA
         

     private UnityEngine.XR.WSA.Persistence.WorldAnchorStore anchorStore;
#endif


    private void Start()
    {
        device = coreDevice.GetComponent<SixenseCore.Device>();

#if !UNITY_EDITOR && UNITY_WSA
         

        UnityEngine.XR.WSA.Persistence.WorldAnchorStore.GetAsync(AnchorStoreReady);
#endif

    }

    public void Align()
    {
        // delete visualiser world anchor
#if !UNITY_EDITOR && UNITY_WSA
         

 

        UnityEngine.XR.WSA.WorldAnchor anchor = visualiser.GetComponent<UnityEngine.XR.WSA.WorldAnchor>();
        if (anchor != null)
        {
            DestroyImmediate(anchor);
        }
 
         

        anchorStore.Delete(GameSettings.Instance.AncName_ArenaStemBase());
#endif


        // align rotation
        Vector3 visualiserRot = visualiser.transform.rotation.eulerAngles;
        Vector3 trackerRot = trackerObject.transform.rotation.eulerAngles;
        Vector3 virtualRot = virtualObject.transform.rotation.eulerAngles;
        float rRotx = 0f; // virtualRot.x - trackerRot.x;
        float rRoty = virtualRot.y - trackerRot.y;
        float rRotz = 0f; // virtualRot.z - trackerRot.z;
        Vector3 relativeRot = new Vector3(rRotx, rRoty, rRotz);

        //Logger.Debug("Visualiser Rotation: " + visualiserRot);
        //Logger.Debug("Tracker Rotation: " + trackerRot);
        //Logger.Debug("Virtual Rotation: " + virtualRot);
        //Logger.Debug("Relative Rotation: " + relativeRot);

        visualiser.transform.Rotate(relativeRot, Space.World);

        // force update tracker after rotation before aligning position
        device.ForceUpdateTrackers();

        // align position
        float xoffset, yoffset, zoffset;
        xoffset = virtualObject.transform.position.x - trackerObject.transform.position.x;
        yoffset = virtualObject.transform.position.y - trackerObject.transform.position.y;
        zoffset = virtualObject.transform.position.z - trackerObject.transform.position.z;

        //Logger.Debug("Visualiser Position Before: " + visualiser.transform.position);
        //Logger.Debug("Translating: (" + xoffset + ", " + yoffset + ", " + zoffset + ")");

        visualiser.transform.Translate(new Vector3(xoffset, yoffset, zoffset), Space.World);

        //Logger.Debug("Visualiser Position After: " + visualiser.transform.position);

        //save new position

#if !UNITY_EDITOR && UNITY_WSA
         


        UnityEngine.XR.WSA.WorldAnchor attachingAnchor = visualiser.AddComponent<UnityEngine.XR.WSA.WorldAnchor>();
        if (attachingAnchor.isLocated)
        {
            anchorStore.Save(GameSettings.Instance.AncName_ArenaStemBase(), attachingAnchor);
        }
        else
        {
            attachingAnchor.OnTrackingChanged += AttachingAnchor_OnTrackingChanged;
        }
#endif
    }

#if !UNITY_EDITOR && UNITY_WSA
         

     private void AnchorStoreReady(UnityEngine.XR.WSA.Persistence.WorldAnchorStore store)
    {
        // save instance
        anchorStore = store;

        anchorStore.Load(GameSettings.Instance.AncName_ArenaStemBase(), visualiser);
    }
#endif

#if !UNITY_EDITOR && UNITY_WSA
         


    private void AttachingAnchor_OnTrackingChanged(UnityEngine.XR.WSA.WorldAnchor self, bool located)
    {
        if (located)
        {
            anchorStore.Save(GameSettings.Instance.AncName_ArenaStemBase(), self);
            self.OnTrackingChanged -= AttachingAnchor_OnTrackingChanged;
        }
    }
#endif

}
