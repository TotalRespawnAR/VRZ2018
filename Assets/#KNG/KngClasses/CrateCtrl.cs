using UnityEngine;

public class CrateCtrl : MonoBehaviour, IShootable
{


    public int crateID = 0;
    public int CrateRow = 0;
    public int CrateCol = 0;
    public int CrateState = 4;
    public KngNode MyNode;
    //this is on the parent of the collider. collider gets hit by bullet, bullet checks if this object has an ishootable parent.
    public GameObject CrateFull;
    public GameObject CrateBig;
    public GameObject CrateMed;
    public GameObject CrateSmall;

    int CrateHealth = 80;
    int curShotstaken;

    void Show_Full()
    {

        CrateState = 4;
        CrateFull.SetActive(true);
        CrateBig.SetActive(false);
        CrateMed.SetActive(false);
        CrateSmall.SetActive(false);
    }

    void Show_Big()
    {
        CrateState = 3;
        CrateFull.SetActive(false);
        CrateBig.SetActive(true);
        CrateMed.SetActive(false);
        CrateSmall.SetActive(false);
    }

    void Show_Med()
    {
        CrateState = 2;
        CrateFull.SetActive(false);
        CrateBig.SetActive(false);
        CrateMed.SetActive(true);
        CrateSmall.SetActive(false);
    }

    void Show_Small()
    {
        CrateState = 1;
        CrateFull.SetActive(false);
        CrateBig.SetActive(false);
        CrateMed.SetActive(false);
        CrateSmall.SetActive(true);
    }


    void Show_None()
    {
        CrateState = 0;
        MyNode.IsOpen = true;
        CrateFull.SetActive(false);
        CrateBig.SetActive(false);
        CrateMed.SetActive(false);
        CrateSmall.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        Show_Full();

    }

    public void UpdateCrateVisual_fromReceivingUDP(int argState)
    {
        if (argState == 0) { Show_None(); }
        else
        if (argState == 1) { Show_Small(); }
        else
        if (argState == 2) { Show_Med(); }
        else
        if (argState == 3) { Show_Big(); }
        else
        if (argState == 4) { Show_Full(); }


    }



    public void Shot(Bullet argBullet)
    {
        CrateHealth -= 5;
        Debug.Log("Crate Shot" + CrateHealth);
        if (CrateHealth <= 0) { Show_None(); }
        else if (CrateHealth > 1 && CrateHealth <= 20) { Show_Small(); }
        else if (CrateHealth > 20 && CrateHealth <= 40) { Show_Med(); }
        else if (CrateHealth > 40 && CrateHealth <= 60) { Show_Big(); }
        else if (CrateHealth > 60) { Show_Full(); }

        GameEventsManager.Instance.Call_LocalCrateChangedState(crateID, CrateState);
    }

    public void aimed(bool argTF)
    {
        throw new System.NotImplementedException();
    }
}
