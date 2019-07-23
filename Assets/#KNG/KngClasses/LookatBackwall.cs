using UnityEngine;

public class LookatBackwall : MonoBehaviour
{

    public Transform backwallLookPos;
    bool FoundWall = false;

    private void OnEnable()
    {
        //Debug.Log(" lookat absk" + gameObject.name);
        GameEventsManager.OnGameObjectAnchoredPlaced += ListenTo_AnchoresLANDPlaced;
    }
    private void OnDisable()
    {
        GameEventsManager.OnGameObjectAnchoredPlaced -= ListenTo_AnchoresLANDPlaced;
    }
    private void Start()
    {
        backwallLookPos = GameManager.Instance.Get_SceneObjectsManager().Mist_PlaceHolder;
        Vector3 lookPos = backwallLookPos.position - transform.position;
        lookPos.x = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
        Goodrotation = rotation;
        FoundWall = true;

    }

    void ListenTo_AnchoresLANDPlaced(string argPlacedAnhorName)
    {
        print("noneed" + argPlacedAnhorName);
        //if (argPlacedAnhorName.Contains(GameSettings.Instance.AncName_WindowBasedLand()))
        //{

        //    var lookPos = GameManager.Instance.Get_SceneObjectsManager().Mist_PlaceHolder.position - transform.position;
        //    lookPos.x = 0;
        //    var rotation = Quaternion.LookRotation(lookPos);
        //    transform.rotation = rotation;
        //    Goodrotation = rotation;
        //    FoundWall = true;
        //}
    }

    Quaternion Goodrotation;

    void LateUpdate()
    {
        //nabil fix this  
        transform.position = Camera.main.transform.position - new Vector3(0, 0, 1f);
        transform.rotation = Goodrotation;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("FlyTag"))
        {
            //do avoided fly
        }
        else
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            other.gameObject.GetComponentInParent<EnemySpinnerProjectile>().TellAxeItPassedPlayer();
        }
    }


}
