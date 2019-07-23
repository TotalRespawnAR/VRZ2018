using System.Collections.Generic;
using UnityEngine;

public class KNode : MonoBehaviour
{



    // public bool IsFree = true; 
    public int KnokeID;
    public Transform KnodeTransform;
    public List<KNode> NextNeighbors = new List<KNode>();
    public List<KNode> PrevNeighbors = new List<KNode>();
    public KNode Right_fromPlayerView = null;
    public KNode Left_fromPlayersview = null;

    //for ordering list of nodes around a column
    public int DistToCol = 0;
    public int MyColumn = 0;
    public int MyRow = 0;
    //------------------------------------
    Material m_Material;
    Color originalColor;
    public Vector3 PosOfTrans;//public for debugging
    public Vector3 GetPos() { return PosOfTrans; }
    bool _isFree = true;

    public bool IsFree
    {
        get
        {
            return _isFree;
        }

        set
        {
            _isFree = value;
            if (!_isFree)
            {
                SetColor(Color.red);
            }
            else
            {
                SetColor(originalColor);
            }
        }
    }

    bool _isFinal = false;

    public bool IsFinal
    {
        get
        {
            return _isFinal;
        }

        set
        {
            _isFinal = value;
            if (!_isFinal)
            {
                SetColor(Color.green);
            }
            else
            {
                SetColor(originalColor);
            }
        }
    }
    #region init colors by nodeprovider once level is loaded. init knode pos when event room and stem placed done by knodemanger
    public void SetColor(Color argColor)
    {
        m_Material.color = argColor;
    }
    public void Set_Original_Color(Color argColor)
    {
        m_Material.color = originalColor = argColor;
    }
    public void SetPos()
    {
        //   PosOfTrans = this.transform.position;
        PosOfTrans = transform.TransformPoint(Vector3.zero);
    }
    #endregion
    public KNode BestNextNode(KNodeNExtDir argDir, int argBest0)
    {
        KNode Bestnode = null;
        // List<KNode> ListREf = null ;
        for (int neighborIndex = 0; neighborIndex < NextNeighbors.Count; neighborIndex++)
        {
            if (NextNeighbors[neighborIndex].IsFree)
            {
                Bestnode = NextNeighbors[neighborIndex];
                break;
            }
        }
        //could be null, make sure ther IS a path
        return Bestnode;
    }

    void BestNExt()
    {

    }



    void Awake()
    {
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<Renderer>().material;
        originalColor = m_Material.color;
    }



    //void OnMouseOver()
    //{
    //    // Change the Color of the GameObject when the mouse hovers over it
    //    m_Material.color = Color.red;
    //}

    //void OnMouseExit()
    //{
    //    //Change the Color back to white when the mouse exits the GameObject
    //    m_Material.color = Color.white;
    //}

    //void OnDestroy()
    //{
    //    //Destroy the instance
    //    Destroy(m_Material);
    //    //Output the amount of materials to show if the instance was deleted
    //    print("Materials " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
    //}
}
