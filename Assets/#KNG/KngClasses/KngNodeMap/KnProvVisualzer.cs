#define ENABLE_LINEDEBUG
using System.Collections.Generic;
using UnityEngine;

public class KnProvVisualzer : MonoBehaviour
{

    private void Start()
    {
        Debug.Log("yo i m on " + gameObject.name);
    }

    public GameObject LRObjRef;

    public List<GameObject> AllLines;

    private void DrawLine_FromPosToPos(Vector3 startPos, Vector3 EndPos)
    {
        Debug.Log("Drawing line");
        GameObject TempObjLine = Instantiate(LRObjRef, startPos, Quaternion.identity);
        LineRenderer LR = TempObjLine.GetComponent<LineRenderer>();
        LR.SetPosition(0, startPos);
        LR.SetPosition(1, EndPos);
        TempObjLine.transform.parent = this.transform;
        AllLines.Add(TempObjLine);
    }


    public void EraseAll()
    {
        Debug.Log("ERRASING");

        // if (!GameManager.Instance.TESTON) return;
#if ENABLE_LINEDEBUG


        if (AllLines != null)
        {

            for (int x = AllLines.Count - 1; x >= 0; x--)
            {
                DestroyImmediate(AllLines[x]);
                AllLines.RemoveAt(x);
            }
            AllLines.Clear();
        }
#endif
    }
    public void UpdateNExtNeighborsVisually(List<KNode> ArgAllKnodes)
    {
        Debug.Log("drawnext ");
        //if (!GameManager.Instance.TESTON) return;
#if ENABLE_LINEDEBUG
        if (AllLines == null)
        {
            AllLines = new List<GameObject>();
        }

        foreach (KNode sp in ArgAllKnodes)
        {
            if (sp.IsFree)
            {
                KNode thepotentialnextnode = KnodeProvider.Instance.RequestNextKnode(sp);
                if (thepotentialnextnode != null)
                {
                    if (thepotentialnextnode.IsFree)
                    {
                        DrawLine_FromPosToPos(sp.GetPos(), KnodeProvider.Instance.RequestNextKnode(sp).GetPos());
                    }
                }
            }
        }
#endif
    }


    public void DrawPath(List<int> argIDS)
    {
        if (argIDS.Count < 2) { return; }

        DrawLine_FromPosToPos(KnodeProvider.Instance.GetNodeByID(argIDS[0]).GetPos(), KnodeProvider.Instance.GetNodeByID(argIDS[1]).GetPos());
        DrawLine_FromPosToPos(KnodeProvider.Instance.GetNodeByID(argIDS[argIDS.Count - 1]).GetPos(), KnodeProvider.Instance.GetNodeByID(argIDS[0]).GetPos());
        for (int x = 1; x < argIDS.Count; x++)
        {
            DrawLine_FromPosToPos(KnodeProvider.Instance.GetNodeByID(argIDS[x - 1]).GetPos(), KnodeProvider.Instance.GetNodeByID(argIDS[x]).GetPos());
        }
    }
}
