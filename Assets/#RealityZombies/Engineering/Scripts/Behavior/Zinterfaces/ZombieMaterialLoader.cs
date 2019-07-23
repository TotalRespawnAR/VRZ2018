using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMaterialLoader : MonoBehaviour
{

    #region dependencies
    #endregion

    #region PublicVars
    #endregion

    #region PrivateVars
    #endregion

    #region INIT
    //void OnEnable()
    //{

    //}

    //private void OnDisable()
    //{

    //}

    //private void Awake()
    //{

    //}

    //private void Start()
    //{

    //}
    #endregion

    #region PublicMethods
    public Material FetchSingleMaterial(int argZmodelNumber, int argZmaterialNumber)
    {
        string pathToExternalMAterial = MaterialNumber_Path(argZmodelNumber, argZmaterialNumber);
        return Resources.Load(pathToExternalMAterial, typeof(Material)) as Material;
    }

    
    /// <summary>
    /// this is leaking all the materials loaded from folder. try if not contains destroy object; to avoid doing resources unload in Zmaterial manager ondestroy()
    /// </summary>
    /// <param name="argZmodelNumber"></param>
    /// <param name="argSetNumber"></param>
    /// <returns></returns>
    public Material[] FetchSameSetMaterials(int argZmodelNumber, int argSetNumber)
    {
        //***********************************************************************************************************
        //inline cast does not work
        //Material[] allMatsInFOlder = Resources.LoadAll("EnemiesMaterial/Z1_Mats", typeof(Material)) as Material[]; 
        //***********************************************************************************************************
        Object[] Foldermats;
        string _pathToFolderForZombieNum = FolderForZombieNum_Path(argZmodelNumber);
        Foldermats = Resources.LoadAll(_pathToFolderForZombieNum, typeof(Material));

        List<Material> allMatsInFOlder = new List<Material>();
        string SetNameToFind = "Z" + argZmodelNumber.ToString();
        if (GameManager.Instance.KngGameState != ARZState.Pregame)
             SetNameToFind = SetNameToFind + "_set" + argSetNumber.ToString();
        else
            SetNameToFind = SetNameToFind + "_set4" ;


        foreach (Object t in Foldermats)
        {
            if (t.name.Contains(SetNameToFind)) {
                Material MatYEtanothercopy = t as Material;
                allMatsInFOlder.Add(MatYEtanothercopy);
            };
        }
        return allMatsInFOlder.ToArray();
    }



    string MaterialNumber_Path(int argZmodelNumber, int argZMaterialNumber)
    {
        if(GameManager.Instance.KngGameState == ARZState.Pregame)
            return "EnemiesMaterial/Z" + argZmodelNumber.ToString() + "_Mats/Z" + argZmodelNumber.ToString() + "_set4" ;

        else
            return "EnemiesMaterial/Z" + argZmodelNumber.ToString() + "_Mats/Z" + argZmodelNumber.ToString() + "_set" + argZMaterialNumber.ToString() ;
    }


    string FolderForZombieNum_Path(int argZmodelNumber)
    {
        return "EnemiesMaterial/Z" + argZmodelNumber.ToString() + "_Mats"; ;
    }


    #endregion

    #region PrivateMethods
    //void Update()
    //{

    //}

    //private void OnDestroy()
    //{

    //}
    #endregion
}
