using UnityEngine;

public class BerserkerCTRL : MonoBehaviour
{

    public Animator MyAnimator;
    // Start is called before the first frame update

    public AnimationClip OpeningScope;
    public AnimationClip ClosingScope;
    void Start()
    {

    }

    int State;
    bool LazorOn = false;
    bool ScopeOn = false;

    // Update is called once per frame
    void Update()
    {

        KeyboardStateChanger();

    }

    void KeyboardStateChanger()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { State = 1; } //shotgun
        else
        if (Input.GetKeyDown(KeyCode.Alpha2)) { State = 2; } //rifle
        else
        if (Input.GetKeyDown(KeyCode.Alpha3)) { State = 3; } //chainsaw


        MyAnimator.SetInteger("State", State);
        if (Input.GetKeyDown(KeyCode.Alpha4)) { ScopeOn = !ScopeOn; MyAnimator.SetBool("ScopeOn", ScopeOn); MyAnimator.SetTrigger("TrigScope"); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { LazorOn = !LazorOn; MyAnimator.SetBool("LazorOn", LazorOn); }






    }
}
