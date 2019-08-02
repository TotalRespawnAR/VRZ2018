using System.Collections.Generic;
using UnityEngine;

public class SwarmControler : MonoBehaviour
{
    public float MaxHorizontal;
    public float MaxVertical;
    public GameObject Fly1;
    public GameObject Fly2;
    Vector3 LocalPlaneFLyPos;

    List<FlyBehavior> MyLiveFlies;
    bool released;
    void Start()
    {

        MyLiveFlies = new List<FlyBehavior>();
        Fly1 = Resources.Load<GameObject>("EnemiesRaw/FlyEnemies/Fly_3");
        Fly2 = Resources.Load<GameObject>("EnemiesRaw/FlyEnemies/Fly_4");

        // Destroy(gameObject, 160);

        this.gameObject.name = "swarm_" + swarmNum.ToString();
        //Swarms.Add(SwarmObj);

        for (int x = 0; x < 10; x++)
        {


            float xpos = Random.Range(-MaxHorizontal, MaxHorizontal);
            float ypos = Random.Range(0, MaxVertical);
            float zpos = (float)x / 10f;
            LocalPlaneFLyPos = new Vector3(xpos, ypos, zpos);

            GameObject fl = Instantiate(Fly1);
            fl.transform.parent = this.transform;
            fl.transform.localPosition = LocalPlaneFLyPos;
            fl.transform.localEulerAngles = new Vector3(0, 180, 0);
            MyLiveFlies.Add(fl.GetComponent<FlyBehavior>());


            // Debug.DrawLine(Swarms[0].transform.position, LocalPlaneFLyPos, Color.blue, 200f);
        }

    }

    int swarmNum = 0;
    void ReleaseAllFly()
    {

        foreach (FlyBehavior fb in MyLiveFlies)
        {
            fb.ReleaseMe();
        }
    }

    // Update is called once per frame
    void Update()
    {


        if ((this.transform.position.z < -10f))
        {
            Destroy(gameObject);
        }


        if (released) return;
        transform.Translate(transform.forward * Time.deltaTime * -2.2f);

        if ((this.transform.position.z < 4f))
        {
            if (released) return;
            ReleaseAllFly();
            Debug.Log("fy my lovelies");
            released = true;
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{

        //}
    }
}
