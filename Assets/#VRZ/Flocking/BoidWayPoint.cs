using UnityEngine;

public class BoidWayPoint : MonoBehaviour
{

    BoidController BC;
    bool hasBennTouched;
    Vector3[] MyPoses;
    int PosIndex = 0;
    Vector3 LocalRandompPlotPoint;
    int NumberOfFlyPlanePoints = 3;

    private void Awake()
    {
        BC = GetComponentInParent<BoidController>();
        MyPoses = new Vector3[NumberOfFlyPlanePoints];

        for (int x = 0; x < MyPoses.Length; x++)
        {
            if (x == 0)
                LocalRandompPlotPoint = new Vector3(Random.Range(-10, 0), Random.Range(0, 10), transform.localPosition.z);
            if (x == 1)
                LocalRandompPlotPoint = new Vector3(0, Random.Range(0f, 2f), transform.localPosition.z);
            if (x == 2)
                LocalRandompPlotPoint = new Vector3(Random.Range(0, 10), Random.Range(0, 10), transform.localPosition.z);
            PosIndex = x;
            MyPoses[PosIndex] = LocalRandompPlotPoint;
            Debug.DrawLine(this.transform.position, this.transform.position + MyPoses[PosIndex], Color.black, 200f);
        }

    }
    public void ResetTouched() { hasBennTouched = false; }
    private void OnTriggerEnter(Collider other)
    {
        //if (hasBennTouched) return;
        //Debug.Log("OOOOCH");
        //hasBennTouched = true;
        //BC.ActivateNext();
    }
    Vector3 GetNextPosRR()
    {
        PosIndex++;
        if (PosIndex >= MyPoses.Length) PosIndex = 0;

        return MyPoses[PosIndex];
    }

    public Transform NextPos;

    private void LateUpdate()
    {
        if (MyPoses[PosIndex] != null)
            transform.position = Vector3.Lerp(transform.position, MyPoses[PosIndex] + this.transform.parent.position, Time.deltaTime * 0.4f);

        if ((this.transform.localPosition - MyPoses[PosIndex]).sqrMagnitude < 0.9f * 0.9f)
        {
            GetNextPosRR();
        }
    }
}
