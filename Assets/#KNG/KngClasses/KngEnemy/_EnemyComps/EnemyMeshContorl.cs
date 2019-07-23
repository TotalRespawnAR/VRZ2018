//#define  ENABLE_BUILDPROMITIVES_FIRSTTIME
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class EnemyMeshContorl : MonoBehaviour, IEnemyMeshControl
{
    #region MonoMethods
    void Awake()
    {

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
}



























/*


    // old OnEnabled for setting up prmitives
    //m_animator = GetComponent<Animator>();
    //if (m_animator == null)
    //{
    //    m_animator = GetComponentInChildren<Animator>(); //axe guyy
    //}

#if ENABLE_BUILDPROMITIVES_FIRSTTIME
        BoneTransWithColliders = GetComponentsInChildren<Collider>();
        ChildTransForms = new Transform[transform.childCount];
        for (int cc = 0; cc < transform.childCount; cc++)
        {
            ChildTransForms[cc] = transform.GetChild(cc);
            if (transform.GetChild(cc).tag.Contains("MeshHolo"))
            {
                MeshInternalObj = transform.GetChild(cc).gameObject;
                RendererInternal = MeshInternalObj.GetComponent<Renderer>();
             //   MeshInternalObj.SetActive(false);
            }
            if (transform.GetChild(cc).tag.Contains("MeshGeo"))
            {
                MeshExternalObj = transform.GetChild(cc).gameObject;
                RendererExternal = MeshExternalObj.GetComponent<Renderer>();
                //OriginalMaterialExternal = RendererExternal.material;
             //   RendererExternal.material = OuterTransParent;
            }
        }
       
          SeparateCollidertypes_andRigidBodies();
            CreateCusomePrimitives();
       
        ToggleAllKinematics(true); //must be true to let animator be able to controle the pos of each rigidbody , to allow ragdoll just turn kinematics offrigid bodies and disable animator
        
#endif

    //ChildTransForms = new Transform[transform.childCount];
    //for (int cc = 0; cc < transform.childCount; cc++)
    //{
    //    ChildTransForms[cc] = transform.GetChild(cc);
    //    if (transform.GetChild(cc).tag.Contains("MeshHolo"))
    //    {
    //        MeshInternalObj = transform.GetChild(cc).gameObject;
    //        RendererInternal = MeshInternalObj.GetComponent<Renderer>();
    //        //   MeshInternalObj.SetActive(false);
    //    }
    //    if (transform.GetChild(cc).tag.Contains("MeshGeo"))
    //    {
    //        MeshExternalObj = transform.GetChild(cc).gameObject;
    //        RendererExternal = MeshExternalObj.GetComponent<Renderer>();
    //        OriginalMaterialExternal = RendererExternal.material;
    //        RendererExternal.material = OuterTransParent;
    //    }
    //}

    //end Old OnEnable




#if ENABLE_BUILDPROMITIVES_FIRSTTIME
    void CreateCusomePrimitives()
    {
        if (gameObject.name.Contains("_7"))
        {
            CreatePrimitivesAbomination();
        }
        else
         if (gameObject.name.Contains("26"))//axeman
        {
            CreatePrimitivesAbomination();
        }
        else
         if (gameObject.name.Contains("14") ||
            gameObject.name.Contains("15") ||
            gameObject.name.Contains("16"))
        {
            CreatePrimitivesBigMutant_KnifeFighter_GAsMAsk();
        }
        else
        if (gameObject.name.Contains("21") ||
            gameObject.name.Contains("22") ||
            gameObject.name.Contains("23") ||
            gameObject.name.Contains("24") ||
            gameObject.name.Contains("25"))
        {
            CreatePrimitivesBigGuys();
        }
        else
            CreatePrimitives();
    }



    void SeparateCollidertypes_andRigidBodies()
    {
        foreach (Collider c in BoneTransWithColliders)
        {
            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                RigidBodies.Add(rb);
            }

            if (c is BoxCollider)
            {
                Boxcols.Add(c as BoxCollider);
            }
            else
                if (c is CapsuleCollider)
            {
                Capscols.Add(c as CapsuleCollider);
            }
            else
                if (c is SphereCollider)
            {
                SphereCols.Add(c as SphereCollider);
            }
        }
    }


    void CreatePrimitivesBigGuys()
    {

        foreach (BoxCollider bc in Boxcols)
        {
            Vector3 Boxsize = bc.size;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(cube.GetComponent<BoxCollider>());
            cube.transform.parent = bc.transform;
            cube.transform.localPosition = bc.center;//(bc.transform.localPosition) + (bc.center/100);
            cube.transform.localScale = Boxsize;
            cube.transform.rotation = Quaternion.Euler(0, 0, 0);
            VisualCaps.Add(cube);
        }
        foreach (SphereCollider sc in SphereCols)
        {
            float sphererad = sc.radius * 2;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Destroy(sphere.GetComponent<SphereCollider>());
            sphere.transform.parent = sc.transform;
            sphere.transform.localPosition = sc.center;
            sphere.transform.localScale = new Vector3(sphererad, sphererad, sphererad);
            VisualCaps.Add(sphere);

        }



        foreach (CapsuleCollider capsc in Capscols)
        {
            float caprad = capsc.radius * 2;
            float capsize = 0.2f;
            GameObject cap = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            Destroy(cap.GetComponent<CapsuleCollider>());

            cap.transform.parent = capsc.transform;
            cap.transform.localPosition = capsc.center;
            cap.transform.localScale = new Vector3(capsize, capsize, caprad);
            if (capsc.transform.tag.Contains("rm"))
            {
                cap.transform.localRotation = Quaternion.Euler(0, 0f, 90f);
            }
            else
                cap.transform.localRotation = Quaternion.Euler(0, 0f, 0);

            VisualCaps.Add(cap);
        }


        foreach (GameObject go in VisualCaps)
        {
            go.GetComponent<Renderer>().material = InnerTransparent;
        }
    }

    void CreatePrimitivesBigMutant_KnifeFighter_GAsMAsk()
    {

        foreach (BoxCollider bc in Boxcols)
        {
            Vector3 Boxsize = bc.size;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(cube.GetComponent<BoxCollider>());
            cube.transform.parent = bc.transform;
            cube.transform.localPosition = bc.center;//(bc.transform.localPosition) + (bc.center/100);
            cube.transform.localScale = Boxsize;
            cube.transform.rotation = Quaternion.Euler(0, 90f, 0);
            VisualCaps.Add(cube);
        }
        foreach (SphereCollider sc in SphereCols)
        {
            float sphererad = sc.radius * 2;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Destroy(sphere.GetComponent<SphereCollider>());
            sphere.transform.parent = sc.transform;
            sphere.transform.localPosition = sc.center;
            sphere.transform.localScale = new Vector3(sphererad, sphererad, sphererad);
            VisualCaps.Add(sphere);

        }



        foreach (CapsuleCollider capsc in Capscols)
        {
            float caprad = capsc.radius * 2;
            float capsize = 8f;
            GameObject cap = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            Destroy(cap.GetComponent<CapsuleCollider>());

            cap.transform.parent = capsc.transform;
            cap.transform.localPosition = capsc.center;
            cap.transform.localScale = new Vector3(caprad, capsize, caprad);
            cap.transform.localRotation = Quaternion.Euler(0, 90f, 0);

            VisualCaps.Add(cap);
        }


        foreach (GameObject go in VisualCaps)
        {
            go.GetComponent<Renderer>().material = InnerTransparent;
        }
    }

    void CreatePrimitivesAbomination()
    {

        foreach (BoxCollider bc in Boxcols)
        {
            Vector3 Boxsize = bc.size;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(cube.GetComponent<BoxCollider>());
            cube.transform.parent = bc.transform;
            cube.transform.localPosition = bc.center;//(bc.transform.localPosition) + (bc.center/100);
            cube.transform.localScale = Boxsize;
            cube.transform.rotation = Quaternion.Euler(0, 0f, 90f);
            VisualCaps.Add(cube);
        }
        foreach (SphereCollider sc in SphereCols)
        {
            float sphererad = sc.radius * 2;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Destroy(sphere.GetComponent<SphereCollider>());
            sphere.transform.parent = sc.transform;
            sphere.transform.localPosition = sc.center;
            sphere.transform.localScale = new Vector3(sphererad, sphererad, sphererad);
            VisualCaps.Add(sphere);

        }



        foreach (CapsuleCollider capsc in Capscols)
        {
            float caprad = capsc.radius * 2;
            float capsize = .2f;// capsc.height  -caprad;
            GameObject cap = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            Destroy(cap.GetComponent<CapsuleCollider>());

            cap.transform.parent = capsc.transform;
            cap.transform.localPosition = capsc.center;
            cap.transform.localScale = new Vector3(caprad, capsize, caprad);
            cap.transform.localRotation = Quaternion.Euler(0, 0f, 90f);

            VisualCaps.Add(cap);
        }


        foreach (GameObject go in VisualCaps)
        {
            go.GetComponent<Renderer>().material = InnerTransparent;
        }
    }

    void CreatePrimitives()
    {

        foreach (BoxCollider bc in Boxcols)
        {
            Vector3 Boxsize = bc.size;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(cube.GetComponent<BoxCollider>());
            cube.transform.parent = bc.transform;
            cube.transform.localPosition = bc.center;//(bc.transform.localPosition) + (bc.center/100);
            cube.transform.localScale = Boxsize;
            cube.transform.rotation = Quaternion.Euler(0, 90f, 0);
            VisualCaps.Add(cube);
        }
        foreach (SphereCollider sc in SphereCols)
        {
            float sphererad = sc.radius * 2;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Destroy(sphere.GetComponent<SphereCollider>());
            sphere.transform.parent = sc.transform;
            sphere.transform.localPosition = sc.center;
            sphere.transform.localScale = new Vector3(sphererad, sphererad, sphererad);
            VisualCaps.Add(sphere);

        }



        foreach (CapsuleCollider capsc in Capscols)
        {
            float caprad = capsc.radius * 2;
            float capsize = 18f;// capsc.height  -caprad;
            GameObject cap = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            Destroy(cap.GetComponent<CapsuleCollider>());

            cap.transform.parent = capsc.transform;
            cap.transform.localPosition = capsc.center;
            cap.transform.localScale = new Vector3(caprad, capsize, caprad);
            cap.transform.localRotation = Quaternion.Euler(0, 90f, 0);

            VisualCaps.Add(cap);
        }


        foreach (GameObject go in VisualCaps)
        {
            go.GetComponent<Renderer>().material = InnerTransparent;
        }
    }
#endif


#if ENABLE_BUILDPROMITIVES_FIRSTTIME
    void Start() {

        DeepSearchRemoveLAyer(this.transform);
    }

    public void DeepSearchRemoveLAyer(Transform parent)
    {
        if (parent == null) return;
        foreach (Transform c in parent)
        {
            if (c.gameObject.tag[0] == 'Z')
            {
                c.gameObject.layer = 9;

            }
            else
           if (c.gameObject.tag[0] == 'M')
            {
                Debug.Log("spatialmesh");

            }
            else { c.gameObject.layer =0; }
            DeepSearchRemoveLAyer(c);
            //if (result != null)
            //    return DeepSearchRemoveLAyer(c, val); ;
        }
        return;
    }
#endif

    */
