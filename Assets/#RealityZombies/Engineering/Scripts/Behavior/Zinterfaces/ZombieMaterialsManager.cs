// @Author Nabil Lamriben ©2018
//#define RAGGTEST

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMaterialsManager : MonoBehaviour {
    #region dependencies
   // ZombieBehavior _ZBEH;
    #endregion

    #region PublicVars
    #endregion

    #region PrivateVars
    GameObject MeshInternalObj;
    Renderer RendererInternal;
    GameObject MeshExternalObj;
    Renderer RendererExternal;
    #endregion

    #region INIT
    void OnEnable()
    {
        //_ZBEH = GetComponent<ZombieBehavior>();
        //_ZBEH.OnZombieDied += DissolveInternal;
    }

    private void OnDisable()
    {
       // _ZBEH.OnZombieDied -= DissolveInternal;

    }

    private void Awake()
    {
    }

    //private void Start()
    //{

    //}
    #endregion

    #region PublicMethods

    public void SetMaterialSarra(Material[] argBaseMaterials, Material[] argHOLOMaterials, int argZModelNumber)
    {
        if (GameManager.Instance.KngGameState == ARZState.EndGame ||
            GameManager.Instance.KngGameState == ARZState.ReachedAllowedTime ||
            GameManager.Instance.KngGameState == ARZState.WaveEnded) { return; }

        RetRenderers();
        //RendererInternal.materials = new Material[2];
        RendererInternal.materials = argHOLOMaterials;
        RendererExternal.materials = argBaseMaterials;

        Shader Sref = Shader.Find("RM/Holographic/LB Transparent CutOut");
       
            RendererInternal.materials[0].shader = Sref;
            Color blueHolo = new Color(0.2f, 0.3f, 0.5f, 1f);//new Color(3f, 88f, 142f, 255f);
            RendererInternal.materials[0].SetColor("_Color", blueHolo);
            RendererInternal.materials[0].SetFloat("Dissolve", 1.0f);
            RendererInternal.materials[0].SetFloat("_Dis", 1.0f);
       
            RendererInternal.materials[0].SetTexture("_MainTex", argBaseMaterials[0].mainTexture);

    }
    #endregion

    #region PrivateMethods
    // Update
    private void Update()
    {
#if !RAGGTEST

        //Console3D.Instance.LOGit("_diss  "+ _dissolveFactor2);
        SetFloatDisolve();
#endif
    }// end of Update
    void SetFloatDisolve()
    {
#if !RAGGTEST

        foreach(Material mat in RendererInternal.materials)
        {
            mat.SetFloat("_Dis", _dissolveFactor2);
        }
#endif
    }
    float _dissolveFactor2 = 1.0f;
    float _lerpDuration2 = 1.0f;
    float _lerpDuration20 = 1.0f;
    float _elapsedTime = 0f;
    float _elapsedTime2 = 0f;
    //*******************************************************************
   public  void DissolveInternal()
    {
        //Debug.Log("mats Dissolve");
        StartCoroutine(Lerp_dissolveFactor10());
    }
    IEnumerator Lerp_dissolveFactor10()
    {

        _dissolveFactor2 = 1;
        while (_elapsedTime2 < _lerpDuration2)
        {
            _dissolveFactor2 = Mathf.Lerp(1f, 0f, (_elapsedTime2 / _lerpDuration2));
            _elapsedTime2 += Time.deltaTime;
            yield return null;
        }
        HideExternalMeshes();
        yield return StartCoroutine(Lerp_dissolveFactor01());
        //last
    }
    void HideExternalMeshes() {
       MeshExternalObj.SetActive(false);
    }
    IEnumerator Lerp_dissolveFactor01()
    {
        _elapsedTime = 0f;
        _dissolveFactor2 = 0;
        while (_elapsedTime < _lerpDuration20)
        {
            _dissolveFactor2 = Mathf.Lerp(0f, 1f, (_elapsedTime / _lerpDuration20));
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


    //***********************************************************************

    Transform DeepSearch(Transform parent, string val)
    {
        foreach (Transform c in parent)
        {
            if (c.name == val) { return c; }
            var result = DeepSearch(c, val);
            if (result != null)
                return result;
        }
        return null;
    }

    Transform DeepSearchTag(Transform parent, string val)
    {
        foreach (Transform c in parent)
        {
            if (c.gameObject.CompareTag(val)) { return c; }
            var result = DeepSearch(c, val);
            if (result != null)
                return result;
        }
        return null;
    }
    //these renderers cannot be called on awake since we instantiate the object later 
    void RetRenderers() {

        MeshInternalObj = DeepSearchTag(this.transform, "MeshHoloTag").gameObject;
        RendererInternal = MeshInternalObj.GetComponent<Renderer>();


        MeshExternalObj = DeepSearchTag(this.transform, "MeshGeoTag").gameObject;
        RendererExternal = MeshExternalObj.GetComponent<Renderer>();
    }
    //void Update()
    //{

    //}
    /*
    private void OnDestroy()
    {
#if !RAGGTEST

        if (RendererInternal.material != null) { Destroy(RendererInternal.material); }
        if (RendererExternal.material != null) { Destroy(RendererExternal.material); }

        if (RendererInternal.materials[0] != null) { Destroy(RendererInternal.materials[0]); }
        if (RendererExternal.materials[0] != null) { Destroy(RendererExternal.materials[0]); }
      
        Resources.UnloadUnusedAssets();
#endif
    }
    */
    #endregion
}
