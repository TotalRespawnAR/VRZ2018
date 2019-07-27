using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentCTRL : MonoBehaviour
{


    NavMeshAgent _agent;
    public Transform PlayerBaseTrans;
    bool ToggleAgentOn;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        ToggleAgentOn = false;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleAgentOn = !ToggleAgentOn;
        }
        if (ToggleAgentOn)
        {
            _agent.isStopped = false;
            _agent.SetDestination(PlayerBaseTrans.position);
        }
        else
        {
            _agent.isStopped = true;
        }
    }
}
