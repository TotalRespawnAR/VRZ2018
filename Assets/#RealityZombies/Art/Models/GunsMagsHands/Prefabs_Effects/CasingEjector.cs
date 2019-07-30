using System.Collections.Generic;
using UnityEngine;

public class CasingEjector : MonoBehaviour
{
    public GameObject CasingGo;
    // Use this for initialization

    Vector3 torque;
    ConstantForce cf;
    Rigidbody rb;
    List<GameObject> casings;
    void Awake()
    {


    }

    void Start()
    {

        torque.x = Random.Range(-200, 200);
        torque.y = Random.Range(-200, 200);
        torque.z = Random.Range(-200, 200);
        cf = CasingGo.GetComponent<ConstantForce>();
        rb = CasingGo.GetComponent<Rigidbody>();
        cf.torque = torque;
        rb.AddForce(Vector3.up * Random.Range(2, 5), ForceMode.Impulse);
        casings = new List<GameObject>();
        for (int x = 0; x < 60; x++)
        {

            GameObject go = (GameObject)Instantiate(CasingGo);
            go.SetActive(false);
            casings.Add(go);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        torque.x = Random.Range(-200, 200);
    //        torque.y = Random.Range(-200, 200);
    //        torque.z = Random.Range(-200, 200);
    //        GameObject _casing = Instantiate(CasingGo, this.transform.position, Quaternion.identity) as GameObject;
    //        _casing.GetComponent<ConstantForce>().torque = torque;
    //        _casing.GetComponent<Rigidbody>().AddForce(this.transform.forward * Random.Range(2, 5), ForceMode.Impulse);
    //    }
    //}

    //public void EjectCasing() {
    //        torque.x = Random.Range(-200, 200);
    //        torque.y = Random.Range(-200, 200);
    //        torque.z = Random.Range(-200, 200);
    //        GameObject _casing = Instantiate(CasingGo, this.transform.position, Quaternion.identity) as GameObject;
    //        _casing.GetComponent<ConstantForce>().torque = torque;
    //        _casing.GetComponent<Rigidbody>().AddForce(this.transform.forward * Random.Range(2, 5), ForceMode.Impulse);
    //}

    int curShellIndex = 0;
    public void EjectCasing()
    {

        curShellIndex++;

        if (curShellIndex >= casings.Count) { curShellIndex = 0; }
        //for (int x = 0; x < casings.Count; x++)
        //{
        //if (!casings[curShellIndex].activeInHierarchy)
        //{
        casings[curShellIndex].transform.position = this.transform.position;
        casings[curShellIndex].transform.rotation = Quaternion.identity;
        casings[curShellIndex].SetActive(true);
        torque.x = Random.Range(-200, 200);
        torque.y = Random.Range(-200, 200);
        torque.z = Random.Range(-200, 200);

        casings[curShellIndex].GetComponent<ConstantForce>().torque = torque;
        casings[curShellIndex].GetComponent<Rigidbody>().AddForce(this.transform.forward * Random.Range(2, 5), ForceMode.Impulse);
        //break;
        //}
        //   }
    }
}
