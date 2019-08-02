using System.Collections.Generic;
using UnityEngine;

public class SwarmMaker : MonoBehaviour
{
    // Start is called before the first frame update
    public float MaxHorizontal;
    public float MaxVertical;
    public GameObject Fly1;
    public GameObject Fly2;
    Vector3 LocalPlaneFLyPos;
    //public List<GameObject> Swarms;
    List<FlyBehavior> MyLiveFlies;

    void Start()
    {
        //Swarms = new List<GameObject>();
        MyLiveFlies = new List<FlyBehavior>();
        Fly1 = Resources.Load<GameObject>("EnemiesRaw/FlyEnemies/Fly_1");
        Fly2 = Resources.Load<GameObject>("EnemiesRaw/FlyEnemies/Fly_2");

        GameObject SwarmObj = new GameObject();
        SwarmObj.transform.position = this.transform.position;
        SwarmObj.transform.localPosition = Vector3.zero;
        SwarmObj.transform.localEulerAngles = Vector3.zero;
        //swarmNum++;
        SwarmObj.name = "swarm_" + swarmNum.ToString();
        //Swarms.Add(SwarmObj);

        for (int x = 0; x < 10; x++)
        {
            GameObject fl = Instantiate(Fly1);
            fl.transform.parent = SwarmObj.transform;
            fl.transform.localPosition = Vector3.zero;
            fl.transform.localEulerAngles = Vector3.zero;
            MyLiveFlies.Add(fl.GetComponent<FlyBehavior>());

            float xpos = Random.Range(-MaxHorizontal, MaxHorizontal);
            float ypos = Random.Range(-MaxVertical, MaxVertical);
            float zpos = (float)x / 10f;
            LocalPlaneFLyPos = new Vector3(xpos, ypos, zpos);

            // Debug.DrawLine(Swarms[0].transform.position, LocalPlaneFLyPos, Color.blue, 200f);
        }

    }

    int swarmNum = 0;
    void CreateSwarm()
    {

        float xpos = Random.Range(-MaxHorizontal, MaxHorizontal);
        float ypos = Random.Range(-MaxVertical, MaxVertical);

        LocalPlaneFLyPos = new Vector3(xpos, ypos, 0);
        //  transform.position = Vector3.Lerp(transform.position, playerMarker.position, Time.deltaTime * 100);
        Instantiate(Fly1, LocalPlaneFLyPos, Quaternion.identity);
        // Debug.DrawLine(Swarms[0].transform.position, LocalPlaneFLyPos, Color.blue, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //  CreateSwarm();
        }
    }
}
