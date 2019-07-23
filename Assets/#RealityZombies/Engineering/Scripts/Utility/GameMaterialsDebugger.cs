// @Author Nabil Lamriben ©2018
//very quick hack to rename the bone tags

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public class GameMaterialsDebugger : MonoBehaviour
{

   public  GameObject[] sceneZombs;
    //public Object[] AllOnemats;
     public List<GameObject> AllOnematsList = new List<GameObject>();
    public List<Transform> AllChests = new List<Transform>();


    private void Start()
    {
         

        sceneZombs = AllOnematsList.ToArray();

        ZombieChecker();




    }

    int headCnt = 1;
    int chestcnt = 1;
    int pelvCnt = 1;
    int armcnt = 2;
    int legcnt = 2;
    int TotalShouldbezero = 0;
    void resetAll()
    {
         headCnt = 1;
         chestcnt = 1;
         pelvCnt = 1;
         armcnt = 2;
         legcnt = 2;
         TotalShouldbezero = 0;
    }
    void ZombieChecker() {

        foreach (GameObject go in sceneZombs)
        {
            resetAll();
     
            DeepSearchCNT(go.transform.GetChild(1), "ZHeadTag", go.name);
            DeepSearchCNT(go.transform.GetChild(1), "ZPelvisTag", go.name);

            DeepSearchCNT(go.transform.GetChild(1), "ZChestTag", go.name);

            DeepSearchCNT(go.transform.GetChild(1), "ZHeadTag", go.name);

            DeepSearchCNT(go.transform.GetChild(1), "ZLegTag", go.name);
           // if((headCnt +  chestcnt + pelvCnt +   armcnt    + legcnt) > 0)
            Debug.Log(go.name + "H" + headCnt + " C" + chestcnt + " P" + pelvCnt + " A" + armcnt + " L" + legcnt);
        }
    }

    private void ReplaceTagInChest()
    {

        foreach (GameObject go in AllOnematsList.ToArray())
        {
            Transform chestt = DeepSearch(go.transform, "ZChestTag");
            AllChests.Add(chestt);
        }

        foreach (Transform Tgo in AllChests)
        {

            DeepSearch(Tgo, "ZLegTag", "ZArmTag");

        }



    }

    void ReplaceTagInLimbsFromRootObg()
    {
        foreach (GameObject go in sceneZombs)
        {

            DeepSearch(go.transform.GetChild(1), "ZombieHead", "ZHeadTag");
            //DeepSearch(go.transform.GetChild(1), "ZombieNonLethal", "ZPelvisTag");
            //DeepSearch(go.transform.GetChild(1), "ZombieTorso", "ZChestTag");
        }
    }


    Transform DeepSearch(Transform parent, string val, string Newtag)
    {
        foreach (Transform c in parent)
        {
            if (c.gameObject.CompareTag(val))
            {
                Debug.Log("renaming " + c.gameObject + " name ->" + Newtag);
                c.gameObject.tag = Newtag;
            }
            var result = DeepSearch(c, val, Newtag);
            if (result != null)
                return result;
        }
        return null;
    }



    Transform DeepSearch(Transform parent, string val)
    {
        foreach (Transform c in parent)
        {
            if (c.gameObject.CompareTag(val))
            {
                return c;
            }
            var result = DeepSearch(c, val);
            if (result != null)
                return result;
        }
        return null;
    }

    Transform DeepSearchCNT(Transform parent, string val ,string NameGo)
    {
        foreach (Transform c in parent)
        {
            if (c.gameObject.CompareTag(val))
            {

                if (val.Contains("ead")) { headCnt--; }
                if (val.Contains("est")) { chestcnt--; }
                if (val.Contains("elvis")) { pelvCnt--; }
                if (val.Contains("Arm")) { armcnt--; }
                if (val.Contains("Leg")) { legcnt--; }

            }
            var result = DeepSearchCNT(c, val, NameGo);
            if (result != null)
                return result;
        }

        TotalShouldbezero = headCnt + chestcnt + pelvCnt + armcnt + legcnt;
     
           // Debug.Log(  NameGo+ "H" + headCnt + " C" + chestcnt + " P" + pelvCnt+" A" + armcnt+" L" + "legcnt");
       

        return null;
    }

    /*
    Object[] AllObjectsMaterials;
    List<Object> RelevantObjectMaterials;


    public string InfoText = "msi";
    int xPos = 0;
    void Start()
    {
        FindAllMaterials_Filter_andPrint();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { FindAllMaterials_Filter_andPrint(); }
    }
      string dirPath = @"C:\Users\juliePC\Desktop\LoggingMaterials\";
    //string dirPath = @"C:\Users\Nabil\Desktop\LoggingMaterials\";
    string fileNameAndExtention = "List_matss.txt";
    void Write_MaterialsList(Object[] argObjArray, string argTitleInfo)
    {
        int fileCount = Directory.GetFiles(dirPath, "*.txt").Length;         //Directory.GetFiles(@"C:\Users\juliePC\Desktop\LoggingMaterials\", " *.txt", SearchOption.AllDirectories).Length;
        int fileCountPlusOne = fileCount + 1;
        int MaterialNumber = 0;
        string NumberedFileNameAndExtention = fileCountPlusOne.ToString() + "#_" + argObjArray.Length.ToString() + "_#" + fileNameAndExtention;
        string FullNumberedPath = dirPath + NumberedFileNameAndExtention;
        string ObjCountStr = argObjArray.Length.ToString();
        string[] title1 = { argTitleInfo, ObjCountStr, " mats found:", "\n" };
        System.IO.File.WriteAllLines(FullNumberedPath, title1);
        using (System.IO.StreamWriter MatsNameWriter =
        new System.IO.StreamWriter(FullNumberedPath, true))
        {
            foreach (Object obj in argObjArray)
            {
                MaterialNumber++;
                MatsNameWriter.WriteLine("mat_" + MaterialNumber.ToString() + ": " + obj.name.ToString());
            }
        }
    }

    string[] MaterialNamesToExcludeFromCount = new string[] { "", "", "" };
    bool CheckIsNotEditorMaterial(string argObjName)
    {
        if (argObjName == "SH" || argObjName == "InactiveGUI" || argObjName == "LightProbeLines")
            return false;
        if (argObjName.Contains("Hidden") ||
            argObjName.Contains("FrameDebugger") ||
            argObjName.Contains("Default") ||
            argObjName.Contains("andle") ||
            argObjName.Contains("Grid") ||
            argObjName.Contains("PreviewEncodedNormal") ||
            argObjName.Contains("SceneViewGrayscale") ||
            argObjName.Contains("ont Materia"))
            return false;
        return true;
    }

    void FindAllMaterials_Filter_andPrint()
    {
        AllObjectsMaterials = Resources.FindObjectsOfTypeAll(typeof(Material));
      //  print("Materials " + AllObjectsMaterials.Length);
        RelevantObjectMaterials = new List<Object>();
        for (int x = 0; x < AllObjectsMaterials.Length; x++)
        {
            if (CheckIsNotEditorMaterial(AllObjectsMaterials[x].name))
            {
                RelevantObjectMaterials.Add(AllObjectsMaterials[x]);
            }
        }
       // print("Relevant Mats " + RelevantObjectMaterials.Count);

        Write_MaterialsList(RelevantObjectMaterials.ToArray<Object>(), InfoText);
    }

    */


    //*******************************************************************************************************
    /*
    You can clean up the materials explicitly using 
    -Resources.Unload(renderer.material )
    -Resources.UnloadUnusedAssets, which should be faster.
    
    Either way loading and unloading resources is a relatively expensive operation.
    If you can share attributes between renderers, you can use 
    -renderer.sharedMaterial
    which will not create new instances but rather update the attributes on the material shared by all renderers.
    
    or you can make the object's ondestroy
    -void OnDestroy() { DestroyImmediate(renderer.material); }
    */
}