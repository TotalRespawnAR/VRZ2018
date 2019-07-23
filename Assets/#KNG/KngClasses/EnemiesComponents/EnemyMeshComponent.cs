////#define  ENABLE_DEBUGLOG
using System.Collections;
using UnityEngine;

public class EnemyMeshComponent : MonoBehaviour, IEnemyMeshComp
{


    #region PrivateVars
    private Transform[] MyTranChildren;
    private GameObject MeshInternalObj;
    private Renderer RendererInternal;
    private GameObject MeshExternalObj;
    private Renderer RendererExternal;
    private Material OriginalMaterialExternal;
    private Coroutine CoroutineCountFrom0To1;
    private Coroutine CounterCoroutingDown;
    private Coroutine CounterCoroutingUpDown;
    #endregion



    #region MonoMethods
    void Awake()
    {
        CoroutineCountFrom0To1 = null;
        CounterCoroutingDown = null;
        CounterCoroutingUpDown = null;

        MyTranChildren = new Transform[transform.childCount];
        for (int cc = 0; cc < transform.childCount; cc++)
        {
            MyTranChildren[cc] = transform.GetChild(cc);
            if (transform.GetChild(cc).tag.Contains("MeshHolo"))
            {
                MeshInternalObj = transform.GetChild(cc).gameObject;
                RendererInternal = MeshInternalObj.GetComponent<Renderer>();
                SetInteriorAllDissoled();
                MeshInternalObj.SetActive(false);
            }
            if (transform.GetChild(cc).tag.Contains("MeshGeo"))
            {
                MeshExternalObj = transform.GetChild(cc).gameObject;
                RendererExternal = MeshExternalObj.GetComponent<Renderer>();
                OriginalMaterialExternal = RendererExternal.material;
                MeshExternalObj.SetActive(false);

            }
        }

    }
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }

    void Start()
    {

    }
    #endregion

    #region Interface
    public void ToggleExternalMesh_inTime(bool argFadeOn, float timetotoggle)
    {
        StartCoroutine(WaitTime(argFadeOn, timetotoggle));
    }

    public void MeshDisolveToNothing()
    {
        StartCoroutine(SequenceKillDisolveOut());

    }
    public void MeshDisolveFromNothing()
    {
        StartCoroutine(SequenceStartFromNothing());
    }

    void MeshPOPfromNothing()
    {

        MeshInternalObj.SetActive(false);
        MeshExternalObj.SetActive(true);
    }


    void RunApplyDissovefactoactor()
    {
        foreach (Material mat in RendererInternal.materials)
        {
            mat.SetFloat("_Dis", DDDDDDDDISSSS);
        }
    }
    #endregion

    IEnumerator WaitTime(bool argDoFadeOn, float argTime)
    {
        yield return new WaitForSeconds(argTime);
        if (argDoFadeOn)
        {
            StartCoroutine(SequenceStartFromNothing());
        }
        else
        {
            MeshPOPfromNothing();
        }

    }


    void SetInteriorAllDissoled()
    {
        foreach (Material mat in RendererInternal.materials)
        {
            mat.SetFloat("_Dis", 1f);
        }
    }




    float DDDDDDDDISSSS = 0.0f;
    float disolveSpeedRatio = 0.6f;
    IEnumerator C1_FadInToExistance()
    {

        DDDDDDDDISSSS = 1f;
        MeshExternalObj.SetActive(false);

        MeshInternalObj.SetActive(true);

        while (DDDDDDDDISSSS > 0f)
        {
            Mathf.Lerp(1f, 0f, DDDDDDDDISSSS);
            DDDDDDDDISSSS -= disolveSpeedRatio * Time.deltaTime;

            RunApplyDissovefactoactor();
            yield return null;
        }
    }

    IEnumerator C2_PopExternalAndFadeOut()
    {
        DDDDDDDDISSSS = 0f;
        MeshExternalObj.SetActive(true);

        while (DDDDDDDDISSSS < 1f)
        {
            Mathf.Lerp(0f, 1f, DDDDDDDDISSSS);
            DDDDDDDDISSSS += disolveSpeedRatio * Time.deltaTime;
            RunApplyDissovefactoactor();
            yield return null;
        }
        MeshInternalObj.SetActive(false);
    }



    IEnumerator SequenceStartFromNothing()
    {
        yield return StartCoroutine(C1_FadInToExistance());
        yield return StartCoroutine(C2_PopExternalAndFadeOut());

    }



    IEnumerator C3_FadeOutFromExistance()
    {
        DDDDDDDDISSSS = 1f;
        MeshExternalObj.SetActive(true);
        MeshInternalObj.SetActive(true);

        while (DDDDDDDDISSSS > 0f)
        {
            Mathf.Lerp(1f, 0f, DDDDDDDDISSSS);
            DDDDDDDDISSSS -= disolveSpeedRatio * Time.deltaTime;
            RunApplyDissovefactoactor();
            yield return null;
        }
    }


    IEnumerator C4_FadeOutFromExistance()
    {
        DDDDDDDDISSSS = 0f;
        MeshExternalObj.SetActive(false);

        MeshInternalObj.SetActive(true);
        while (DDDDDDDDISSSS < 1f)
        {
            Mathf.Lerp(0f, 1f, DDDDDDDDISSSS);
            DDDDDDDDISSSS += disolveSpeedRatio * Time.deltaTime;
            RunApplyDissovefactoactor();
            yield return null;
        }
        MeshInternalObj.SetActive(false);
        MeshExternalObj.SetActive(false);
    }


    IEnumerator SequenceKillDisolveOut()
    {
        yield return StartCoroutine(C3_FadeOutFromExistance());
        yield return StartCoroutine(C4_FadeOutFromExistance());

    }
}
