// @Author Nabil Lamriben ©2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDynamicLimbs : MonoBehaviour {

    public GameObject PARTS;
    public GameObject CLOTHES;
    public GameObject HEADHANDS;
    public GameObject Thirdobjs;
    public Transform ROOT_M;

    public bool HasNoWrists;

    public bool IsscaledUp;


    Dictionary<ARZMMeshBone, GameObject> DICMESH;
    GameObject FindPartsFromZparts(string ArgName)
    {
        if (PARTS == null) { Logger.Debug("problem with parts or locallist "); }
        string tofind = ArgName + "sm";
        if (HasNoWrists) { if (ArgName.Contains("Wrist")) { return null; } }
        
        if (ArgName == "Hiip_L") { tofind = "Hip_Lsm"; }
        if (ArgName == "Hiip_R") { tofind = "Hip_Rsm"; }
              //Logger.Debug("loooking for object " + ArgName + " in " + PARTS.name);
        GameObject zombiePartFound =  null;
        try
        {
            zombiePartFound = PARTS.transform.Find(tofind).gameObject;
        }
        catch (NullReferenceException e)
        {
            zombiePartFound = new GameObject();
        }
        return zombiePartFound;
    }

    Dictionary<ARZMMeshBone, Transform> DICBONE;

    Transform DeepSearch(Transform parent, string val)
    {
        // Logger.Debug("on " + parent.gameObject.name);
        foreach (Transform c in parent)
        {
            if (c.name == val) { return c; }
            var result = DeepSearch(c, val);
            if (result != null)
                return result;
        }
        return null;
    }

    Transform FindZombieBone(string ArgBoneName)
    {

        if (ROOT_M == null)
        {
            Logger.Debug("problem with root ");
        }
        string tofind = ArgBoneName;
        // Logger.Debug("loooking for bone " + tofind + " in " + ROOT_M.name);
        Transform zombieBoneFound = DeepSearch(ROOT_M, tofind);
        if (zombieBoneFound == null)
        {
            Logger.Debug("DID NOT FIND THE ZOMBIE BONEXXX " + tofind + " on zombie" + gameObject.name );
            return new GameObject().transform;
        }
       
        return zombieBoneFound;

    }
    public void PopulateDICS()
    {
        DICMESH = new Dictionary<ARZMMeshBone, GameObject>();
        DICBONE = new Dictionary<ARZMMeshBone, Transform>();
        foreach (ARZMMeshBone val in Enum.GetValues(typeof(ARZMMeshBone)))
        {
            if (val == ARZMMeshBone.DEFAULT) continue;
            //            Logger.Debug(val.ToString());
            if (HasNoWrists) { if (val == ARZMMeshBone.Wrist_L || val == ARZMMeshBone.Wrist_R) { continue; } }

            DICMESH.Add(val, FindPartsFromZparts(val.ToString()));
            DICBONE.Add(val, FindZombieBone(val.ToString()));
        }
    }


    ////void showenumvals()
    ////{
    ////    foreach (ARZMMeshBone val in Enum.GetValues(typeof(ARZMMeshBone)))
    ////    {
    ////        Logger.Debug(val.ToString() + " " + (int)val);
    ////    }
    ////}

    ////private void Start()
    ////{

    ////    //if (HEADHANDS != null)
    ////    //{
    ////    //    HEADHANDS.SetActive(false);
    ////    //}
    ////    //if (CLOTHES != null)
    ////    //{
    ////    //    CLOTHES.SetActive(false);
    ////    //}
    ////    //if (Thirdobjs != null)
    ////    //{
    ////    //    Thirdobjs.SetActive(false);
    ////    //}
      
    ////  //  PopulateDICS();

    ////}
 

    public void SET_MESHES_INACTIVE(List<ARZMMeshBone> argList)
    {

        //foreach (ARZMMeshBone amb in argList)
        //{
        //    DICMESH[amb].SetActive(false);
        //    DICBONE[amb].gameObject.GetComponent<BoxCollider>().enabled = false;
        //}
    }

    public void BuildAnObjectOutOfThese(List<ARZMMeshBone> argList, Vector3 direction)
    {
        Transform BonePArentOfTheObject = DICMESH[argList[0]].transform;


        if (argList.Count == 1)
        {
            GameObject RootOfConstructedlimb = ReBuildLimb(argList[0]);
            RootOfConstructedlimb.layer = 18;// LayerMask.NameToLayer("FeeLimb");


            //RootOfConstructedlimb.transform.position = ROOT_M.transform.position;
            //RootOfConstructedlimb.transform.rotation = ROOT_M.transform.rotation;
            RootOfConstructedlimb.transform.position = BonePArentOfTheObject.position;
            RootOfConstructedlimb.transform.rotation = BonePArentOfTheObject.rotation;
      
            RootOfConstructedlimb.AddComponent<Rigidbody>();
            RootOfConstructedlimb.AddComponent<BoxCollider>().size = new Vector3(0.1f, 0.1f, 0.1f);
           
            RootOfConstructedlimb.AddComponent<KillTimer>().StartTimer(5f);
            RootOfConstructedlimb.GetComponent<Rigidbody>().AddForce(-direction * 1.0f, ForceMode.Impulse);
            RootOfConstructedlimb.AddComponent<StopPhys>();
          

        }
        if (argList.Count == 2)
        {
            GameObject RootOfConstructedlimb = ReBuildLimb(argList[0]);
            RootOfConstructedlimb.layer = 18;// LayerMask.NameToLayer("FeeLimb");

            GameObject SecondChild = ReBuildLimb(argList[1]);
            SecondChild.transform.parent = RootOfConstructedlimb.transform;

            //            RootOfConstructedlimb.transform.position = ROOT_M.transform.position;
            //          RootOfConstructedlimb.transform.rotation = ROOT_M.transform.rotation;
            RootOfConstructedlimb.transform.position = BonePArentOfTheObject.position;
            RootOfConstructedlimb.transform.rotation = BonePArentOfTheObject.rotation;

          
            RootOfConstructedlimb.AddComponent<Rigidbody>();
            //RootOfConstructedlimb.AddComponent<BoxCollider>();
            RootOfConstructedlimb.AddComponent<BoxCollider>();
            RootOfConstructedlimb.AddComponent<StopPhys>();

            RootOfConstructedlimb.GetComponent<Rigidbody>().AddTorque(-direction * 4.0f, ForceMode.Impulse);

            RootOfConstructedlimb.GetComponent<Rigidbody>().AddForce(-direction * 4.0f, ForceMode.Impulse);
            RootOfConstructedlimb.AddComponent<KillTimer>().StartTimer(5f);

         
        }
        if (argList.Count == 3)
        {
            GameObject RootOfConstructedlimb = ReBuildLimb(argList[0]);
            RootOfConstructedlimb.layer = 18;// LayerMask.NameToLayer("FeeLimb");


            GameObject SecondChild = ReBuildLimb(argList[1]);
            SecondChild.transform.parent = RootOfConstructedlimb.transform;
            GameObject ThirdChild = ReBuildLimb(argList[2]);
            ThirdChild.transform.parent = SecondChild.transform;
            //GameObject MAdeObject = Instantiate(RootOfConstructedlimb, BonePArentOfTheObject.localPosition, BonePArentOfTheObject.rotation);

            //  MAdeObject.AddComponent<Rigidbody>();
            //MAdeObject.AddComponent<BoxCollider>();
            // RootOfConstructedlimb.transform.position = ROOT_M.transform.position;
            //RootOfConstructedlimb.transform.rotation = ROOT_M.transform.rotation;
            // GameObject MAdeObject = Instantiate(RootOfConstructedlimb, XXX.transform.localPosition, XXX.transform.rotation);
            RootOfConstructedlimb.transform.position = BonePArentOfTheObject.position;
            RootOfConstructedlimb.transform.rotation = BonePArentOfTheObject.rotation;
      
            RootOfConstructedlimb.AddComponent<Rigidbody>();
            RootOfConstructedlimb.AddComponent<BoxCollider>().size = new Vector3(0.1f, 0.1f, 0.1f);
            RootOfConstructedlimb.AddComponent<StopPhys>();

            RootOfConstructedlimb.GetComponent<Rigidbody>().AddForce(-direction * 4.0f, ForceMode.Impulse);
            RootOfConstructedlimb.GetComponent<Rigidbody>().AddTorque(-direction * 4.0f, ForceMode.Impulse);

            RootOfConstructedlimb.AddComponent<KillTimer>().StartTimer(5f);
        }
    }




    GameObject ReBuildLimb(ARZMMeshBone argLimenumb)
    {
        GameObject newLimbObj;
        Mesh limbMesh = DICMESH[argLimenumb].GetComponent<SkinnedMeshRenderer>().sharedMesh;
        if (IsscaledUp)
        {
           // limbMesh.sc gameobject.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
        }
        Material LimbMat = DICMESH[argLimenumb].GetComponent<SkinnedMeshRenderer>().materials[0];
        newLimbObj = new GameObject();
        newLimbObj.AddComponent<MeshFilter>().mesh = limbMesh;
        newLimbObj.AddComponent<MeshRenderer>().material = LimbMat;
        newLimbObj.name = "DET_" + argLimenumb;
        newLimbObj.AddComponent<ZombieFreeLimbCleaner>();
        return newLimbObj;
    }
}
