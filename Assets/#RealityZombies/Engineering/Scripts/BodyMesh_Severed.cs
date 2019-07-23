using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMesh_Severed : MonoBehaviour
{

    /// this script is to remove limbs testing a heiarchy system


    // a bool for if this object can be severed or not
    public bool canSever;
    // the object this bone is linked to
    public GameObject linkedObj;
    // a spot for a prefab with the neccessary components (mesh filter, mesh renderer), box collider, rigidbody
    public GameObject severedLimbObj;
    // the stored mesh
    private Mesh limb;
    // the stored material
    private Material diffuse;
    // the variable for scale size
    private float scale = 0.0104f;


    public void CutMeOffBro() {

        // if we can sever this
        if (canSever == true)
        {
            // call sever limbs function
            GameObject FullLimbObj = new GameObject("LimbObj");

            FullLimbObj.AddComponent<Rigidbody>();

            SeverLimbs(FullLimbObj);

        }// end of can sever check
        else
        // if we cannot sever it
        {
            // display message
            Debug.Log("Cannot Sever " + transform.name);

        }// end of cannot sever
    }



    // when the mouse clicks this object
    private void OnMouseDown()
    {

        //// if we can sever this
        //if (canSever == true)
        //{
        //    // call sever limbs function
        //    GameObject FullLimbObj = new GameObject("LimbObj");

        //    FullLimbObj.AddComponent<Rigidbody>();

        //    SeverLimbs(FullLimbObj);

        //}// end of can sever check
        //else
        //// if we cannot sever it
        //{
        //    // display message
        //    Debug.Log("Cannot Sever " + transform.name);

        //}// end of cannot sever

    }// end of mouse clicks this obj


    // a public function to sever limbs
    public void SeverLimbs(GameObject parent)
    {
        // if this limb was alreayd cut off
        if (this.gameObject.activeSelf == false)
        {
            // display messgae
            Debug.Log(transform.name + " is not active");
            //end the function
            return;
        }// end of this limb was cut off
        else
        // if this limb has not been cut off already
        if (this.gameObject.activeSelf == true)
        {
            // display messgae
            Debug.Log(transform.name + " is active");

            // grab desired mesh
            limb = linkedObj.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            // grab desired material
            diffuse = linkedObj.GetComponent<Renderer>().material;

            // spawn a clone of the severed limb
            severedLimbObj = Instantiate(severedLimbObj, linkedObj.transform.position, linkedObj.transform.rotation);
            // set the scale of the cloned obj
            severedLimbObj.transform.localScale = new Vector3(scale, scale, scale);

            // if there is a parent,
            if (parent != null)
            {
                //parent it
                severedLimbObj.transform.parent = parent.transform;

            }// end of if there is a parent

            // assign mesh
            severedLimbObj.GetComponent<MeshFilter>().sharedMesh = limb;
            // assign collider of mesh
            severedLimbObj.GetComponent<MeshCollider>().sharedMesh = limb;
            // assign material
            severedLimbObj.GetComponent<Renderer>().material = diffuse;

            // set this GameObject's limb (mesh) to be false
            linkedObj.SetActive(false);
            // turn off this boneCollider
            transform.GetComponent<BoxCollider>().enabled = false;


            Transform[] Children = GetComponentsInChildren<Transform>();

            // do a loop check
            //foreach (Transform child in Children)
            //{
            //
            //}
            if (transform.childCount == 0) return;
            // there is a child to this object 
            if (transform.GetChild(0))
            {
                // check the child's name
                Debug.Log("child name is: " + transform.GetChild(0).name + " || and active status is: " + transform.GetChild(0).gameObject.activeInHierarchy);

                // if that object has a script and it is active
                if (transform.GetChild(0).GetComponent<BodyMesh_Severed>() != null && transform.GetChild(0).gameObject.activeSelf == true)
                {
                    // store a temporary for the child
                    GameObject severedLimbObjChild = transform.GetChild(0).gameObject;
                    // run that child's script
                    severedLimbObjChild.GetComponent<BodyMesh_Severed>().SeverLimbs(severedLimbObj);

                }// end of obj has script

                // if that object has a script and it is active
                if (transform.GetChild(0).GetChild(0).GetComponent<BodyMesh_Severed>() != null && transform.GetChild(0).GetChild(0).gameObject.activeSelf == true)
                {
                    print("child's child name is: " + transform.GetChild(0).GetChild(0).name);
                    print("unity test 02: " + transform.GetChild(0).GetChild(0).gameObject.activeInHierarchy);
                    // store a temporary for the child
                    GameObject severedLimbObjChild = transform.GetChild(0).GetChild(0).gameObject;
                    // run that child's script
                    severedLimbObjChild.GetComponent<BodyMesh_Severed>().SeverLimbs(severedLimbObj);

                }// end of obj has script


            }// end of there is a child
            else
            {
                Debug.Log("no child");
            }
            // turn off sever for this object
            canSever = false;
            // set the bone to be false too
            this.gameObject.SetActive(false);

        }// end of limb not cut off already

    }// end of sever limbs function

}// end of bodymesh severed
