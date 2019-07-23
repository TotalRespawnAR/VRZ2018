using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEffect : MonoBehaviour {
    #region PublicVars
    public List<GameObject> SubMeshObjs;

    //public Renderer rend;
    #endregion

    float _dissolveFactor = 0.0f;
    float _dissolveFactor2 = 1.0f;

    float _lerpDuration = 3f;
    float _lerpDuration2 = 1.2f;
    float _elapsedTime = 0f;
    float _elapsedTime2 = 0f;

    float _lerp_From_value = 0.0f;
    float _lerp_To_value = 1.0f;
    private void ResetLerpValues()
    {
        _dissolveFactor2 = 1.0f;
        _dissolveFactor = 0.0f;
        _lerpDuration = 3f;
        _elapsedTime = 0f;
        _elapsedTime2 = 0f;
        _lerp_From_value = 0.0f;
        _lerp_To_value = 1.0f;

    }


    IEnumerator Lerp_dissolveFactor()
    {
        _elapsedTime = 0f;

        while (_elapsedTime < _lerpDuration)
        {
            _dissolveFactor = Mathf.Lerp(_lerp_From_value, _lerp_To_value, (_elapsedTime / _lerpDuration));
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
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
        yield return StartCoroutine(Lerp_dissolveFactor01());
        //last
    }


    IEnumerator Lerp_dissolveFactor01()
    {
        _elapsedTime = 0f;
        _dissolveFactor2 = 0;
        while (_elapsedTime < _lerpDuration)
        {
            _dissolveFactor2 = Mathf.Lerp(0f, 1f, (_elapsedTime / _lerpDuration));
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    void DISOLVEME_new()
    {
        //SetSubMeshShader(0f);
        //SetSubMeshShader(1f);

        StartCoroutine(Lerp_dissolveFactor10());
    }
    public bool IsOneWayDissolv = false;

    // Update
    private void Update()
    {
        SetFloatDisolve();
    }// end of Update

    public void DISOLVEME()
    {


        DISOLVEME_new();

    }









    void SetSubMeshShader(float arVal)
    {
        foreach (GameObject go in SubMeshObjs)
        {
            go.GetComponent<Renderer>().material.SetFloat("_Dis", arVal);
        }
    }
    void SetFloatDisolve()
    {


        foreach (GameObject go in SubMeshObjs)
        {
            go.GetComponent<Renderer>().material.SetFloat("_Dis", _dissolveFactor2);
        }



    }




    //IEnumerator DisolveIn3() {
    //    yield return new WaitForSeconds(3);
    //    SetMaterial(HoloMat0_Diss_Light);
    //   // Mathf.PingPong((Time.time * 0.75f), 1.0F);
    //   // rend.material.SetFloat("_Dis", dissolve);
    //}
    #region PrivateVars
    BloodFX _bloodFX;
    bool useHex;
    #endregion

    #region Dependencies
    ////ZombieDynamicLimbs _Zdyn;
    #endregion
    #region INIT
    void Start()
    {
        ResetLerpValues();


        SetSubMeshShader(1f);

    }
    #endregion
    void OnDestroy()
    {
        //print("Materials DELETE");
        if (IsOneWayDissolv)
        {
            foreach (GameObject go in SubMeshObjs)
            {
                Destroy(go.GetComponent<Renderer>().material);
            }
        }


    }
    #region PublicMethods
    public void Boold_On_Head(Bullet bullet)
    {
        if (_bloodFX != null)
            _bloodFX.HeadShotFX(bullet.hitInfo, useHex);
    }

    public void Blood_On_HEadCheat(Transform argHEadTRans)
    {
        _bloodFX.HeadShotFX_CHEAT(argHEadTRans);

    }

    public void Boold_On_Torso(Bullet bullet)
    {
        if (_bloodFX != null)
            _bloodFX.TorsoShotFX(bullet.hitInfo, useHex);
    }
    public void Boold_On_Limb(Bullet bullet)
    {
        if (_bloodFX != null)
            _bloodFX.LimbShotFX(bullet.hitInfo, useHex);
    }
    #endregion

}
