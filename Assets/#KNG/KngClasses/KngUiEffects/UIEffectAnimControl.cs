using UnityEngine;

public class UIEffectAnimControl : MonoBehaviour
{

    Animator mAnimator;
    public string triggerName;

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start()
    {
        if (mAnimator == null)
        {
            Debug.LogError("no animatorfound");
        }

    }

    //curtain = 3 sec
    // MIRa = 2sec
    // MIRB =3sec
    public void TRIGGER_effectANimation()
    {
        mAnimator.SetTrigger(triggerName);
    }
}
