using UnityEngine;

public class QuadAnimatorCTRL : MonoBehaviour
{
    Animator mAnimator;
    public TriggersDamageEffects EffectType;
    string triggerName;
    bool animationFinished = true;
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


    public void TRIGGER_effectANimation()
    {

        if (animationFinished)
        {
            mAnimator.SetTrigger("trigHudAnim");
            animationFinished = false;
        }
    }

    public void SignalEndAnim()
    {
        animationFinished = true;
    }
}
