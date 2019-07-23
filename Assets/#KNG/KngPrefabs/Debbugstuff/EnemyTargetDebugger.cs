//#define ENABLE_LINEDEBUG
using UnityEngine;

public class EnemyTargetDebugger : MonoBehaviour
{







    IEnemyEntityComp m_Ieec;
    // Use this for initialization
    void Start()
    {
        m_Ieec = GetComponent<IEnemyEntityComp>();
        if (!GameManager.Instance.TESTON) return;
#if ENABLE_LINEDEBUG
        DrawLine_FromPosToPos();
#endif
    }
    #region line Debugger

    //public GameObject LRObjRef;

    //public GameObject Lines;

    LineRenderer LR;

    void DrawLine_FromPosToPos()
    {
        if (!GameManager.Instance.TESTON) return;


        LR = gameObject.AddComponent<LineRenderer>();
        LR.material = new Material(Shader.Find("Sprites/Default"));
        LR.widthMultiplier = 0.02f;
        LR.positionCount = 2;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;


        //  Lines = Instantiate(LRObjRef, this.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        //  LR = Lines.GetComponent<LineRenderer>();
        //LR.SetPosition(0, transform.position);
        //LR.SetPosition(1, _CurTargetNode.GetPos());
        // Lines.transform.parent = this.transform;
        // Lines = TempObjLine;
    }

    #endregion
#if ENABLE_LINEDEBUG
 
    // Update is called once per frame
    void Update()
    {
        if (m_Ieec.GetTargPos() != null)
        {
            Debug.DrawLine(m_Ieec.GEt_MyHIpsTans().position, m_Ieec.GetTargPos(), Color.green);
        }
        else {
            Debug.DrawLine(m_Ieec.GEt_MyHIpsTans().position, m_Ieec.GEt_MyHIpsTans().position + Vector3.forward, Color.red);

        }
        
        if (!GameManager.Instance.TESTON) return;
        if (m_Ieec.GetTargPos() != null) {
            LR.SetPosition(0, transform.position);
            LR.SetPosition(1, (m_Ieec.GetTargPos()));
        }


    }
#endif
}
