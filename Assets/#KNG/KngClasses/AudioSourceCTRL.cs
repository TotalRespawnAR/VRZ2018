using UnityEngine;

public class AudioSourceCTRL : MonoBehaviour
{

    public EnemySpeakerTypes MySpeakType;

    public AudioSource AudioFootStep;
    public AudioSource AudioHeadShot;
    public AudioSource AudioBodyShot;
    public AudioSource AudioSmallGrowl;
    public AudioSource AudioBigGrowl;
    public AudioSource AudioSmallMoan;
    public AudioSource AudioBigMoan;
    public AudioSource AudioSmallHurt;
    public AudioSource AudioBigHurt;
    public AudioSource AudioSmallAttack;
    public AudioSource AudioBigAttack;
    public AudioSource AudioSmallDeath;
    public AudioSource AudioBigDeath;


    IEnemyEntityComp m_ieec;


    void LoadTypeBasedAudio(EnemySpeakerTypes argspeakertype)
    {

        switch (argspeakertype)
        {
            case EnemySpeakerTypes.Male:
                Load_Male();
                break;
            case EnemySpeakerTypes.Female:
                Load_Female();
                break;
            case EnemySpeakerTypes.Creature:
                Load_Creature();
                break;
            case EnemySpeakerTypes.Giant:
                Load_Giant();
                break;
            case EnemySpeakerTypes.Bone:
                Load_Bone();
                break;
        }

    }




    private void Start()
    {
        m_ieec = GetComponentInParent<IEnemyEntityComp>();
        if (m_ieec != null)
        {



        }
        else
        {
            Debug.LogWarning("No IentityComp");
        }

        LoadTypeBasedAudio(MySpeakType);



    }
    void OnDisable()
    {
        if (m_ieec != null)
        {

        }
        else
        {
            Debug.LogWarning("No IentityComp");
        }

    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha0)) { PlayKngAudioClip(AudioClipType.FootStep); }
    //    if (Input.GetKeyDown(KeyCode.Alpha1)) { PlayKngAudioClip(AudioClipType.HeadShot); }
    //    if (Input.GetKeyDown(KeyCode.Alpha2)) { PlayKngAudioClip(AudioClipType.BodyShot); }
    //    if (Input.GetKeyDown(KeyCode.Alpha3)) { PlayKngAudioClip(AudioClipType.MoanSmall); }
    //    if (Input.GetKeyDown(KeyCode.Alpha4)) { PlayKngAudioClip(AudioClipType.MoanBig); }
    //    if (Input.GetKeyDown(KeyCode.Alpha5)) { PlayKngAudioClip(AudioClipType.GrowlSmall); }
    //    if (Input.GetKeyDown(KeyCode.Alpha6)) { PlayKngAudioClip(AudioClipType.GrowlBig); }
    //    if (Input.GetKeyDown(KeyCode.Alpha7)) { PlayKngAudioClip(AudioClipType.HurtSmall); }
    //    if (Input.GetKeyDown(KeyCode.Alpha8)) { PlayKngAudioClip(AudioClipType.HurtBig); }
    //    if (Input.GetKeyDown(KeyCode.Alpha9)) { PlayKngAudioClip(AudioClipType.AttackSmall); }
    //    if (Input.GetKeyDown(KeyCode.P)) { PlayKngAudioClip(AudioClipType.DeathSmall); }
    //    if (Input.GetKeyDown(KeyCode.O)) { PlayKngAudioClip(AudioClipType.DeathBig); }
    //}

    public void PlayKngAudioClip(AudioClipType argType)
    {

        switch (argType)
        {

            case AudioClipType.FootStep:
                if (AudioFootStep != null)
                {
                    AudioFootStep.Play();
                }
                else
                {
                    Debug.LogError("No FootstepSource");
                }
                break;
            case AudioClipType.HeadShot:
                if (AudioHeadShot != null)
                {
                    AudioHeadShot.Play();
                }
                else
                {
                    Debug.LogError("No AudioHeadShot");
                }
                break;

            case AudioClipType.BodyShot:
                if (AudioBodyShot != null)
                {
                    AudioBodyShot.Play();
                }
                else
                {
                    Debug.LogError("No AudioBodyShot");
                }
                break;
            case AudioClipType.MoanSmall:
                if (AudioSmallMoan != null)
                {
                    AudioSmallMoan.Play();
                }
                else
                {
                    Debug.LogError("No AudioSmallMoan");
                }
                break;
            case AudioClipType.MoanBig:
                if (AudioBigMoan != null)
                {
                    AudioBigMoan.Play();
                }
                else
                {
                    Debug.LogError("No AudioBigMoan");
                }
                break;
            case AudioClipType.GrowlSmall:
                if (AudioSmallGrowl != null)
                {
                    AudioSmallGrowl.Play();
                }
                else
                {
                    Debug.LogError("No AudioSmallGrowl");
                }
                break;
            case AudioClipType.GrowlBig:
                if (AudioBigGrowl != null)
                {
                    AudioBigGrowl.Play();
                }
                else
                {
                    Debug.LogError("No AudioBigGrowl");
                }
                break;
            case AudioClipType.HurtSmall:
                if (AudioSmallHurt != null)
                {
                    AudioSmallHurt.Play();
                }
                else
                {
                    Debug.LogError("No AudioSmallHurt");
                }
                break;
            case AudioClipType.HurtBig:
                if (AudioBigHurt != null)
                {
                    AudioBigHurt.Play();
                }
                else
                {
                    Debug.LogError("No AudioBigHurt");
                }
                break;
            case AudioClipType.AttackSmall:
                if (AudioSmallAttack != null)
                {
                    AudioSmallAttack.Play();
                }
                else
                {
                    Debug.LogError("No AudioSmallAttack");
                }
                break;
            case AudioClipType.AttackBig:
                if (AudioBigAttack != null)
                {
                    AudioBigAttack.Play();
                }
                else
                {
                    Debug.LogError("No AudioBigAttack");
                }
                break;
            case AudioClipType.DeathSmall:
                if (AudioSmallDeath != null)
                {
                    AudioSmallDeath.Play();
                }
                else
                {
                    Debug.LogError("No AudioSmallDeath");
                }
                break;
            case AudioClipType.DeathBig:
                if (AudioBigDeath != null)
                {
                    AudioBigDeath.Play();
                }
                else
                {
                    Debug.LogError("No AudioBigDeath");
                }
                break;


        }

    }

    #region LoadAudios
    void Load_Male()
    {

        AudioFootStep.clip = GameSettings.Instance.FootStepBasicclip;
        AudioHeadShot.clip = GameSettings.Instance.HeadShotCrushclip;
        AudioBodyShot.clip = GameSettings.Instance.Bodyshotclip;
        AudioSmallGrowl.clip = GameSettings.Instance.GrowlSmallclipMale;
        AudioBigGrowl.clip = GameSettings.Instance.GrowlBigclipMale;
        AudioSmallMoan.clip = GameSettings.Instance.MoanSmallclipMale;
        AudioBigMoan.clip = GameSettings.Instance.MoanBigclipMale;
        AudioSmallHurt.clip = GameSettings.Instance.HurtSmallclipMale;
        AudioBigHurt.clip = GameSettings.Instance.HurtBigclipMale;
        AudioSmallAttack.clip = GameSettings.Instance.AttackSmallclipMale;
        AudioBigAttack.clip = GameSettings.Instance.AttackBigclipMale;
        AudioSmallDeath.clip = GameSettings.Instance.DeathSmallclipMale;
        AudioBigDeath.clip = GameSettings.Instance.DeathBigclipMale;
    }



    void Load_Female()
    {

        AudioFootStep.clip = GameSettings.Instance.FootStepBasicclip;
        AudioHeadShot.clip = GameSettings.Instance.HeadShotCrushclip;
        AudioBodyShot.clip = GameSettings.Instance.Bodyshotclip;
        AudioSmallGrowl.clip = GameSettings.Instance.GrowlSmallclipFemale;
        AudioBigGrowl.clip = GameSettings.Instance.GrowlBigclipFemale;
        AudioSmallMoan.clip = GameSettings.Instance.MoanSmallclipFemale;
        AudioBigMoan.clip = GameSettings.Instance.MoanBigclipFemale;
        AudioSmallHurt.clip = GameSettings.Instance.HurtSmallclipFemale;
        AudioBigHurt.clip = GameSettings.Instance.HurtBigclipFemale;
        AudioSmallAttack.clip = GameSettings.Instance.AttackSmallclipFemale;
        AudioBigAttack.clip = GameSettings.Instance.AttackBigclipFemale;
        AudioSmallDeath.clip = GameSettings.Instance.DeathSmallclipFemale;
        AudioBigDeath.clip = GameSettings.Instance.DeathBigclipFemale;
    }


    void Load_Creature()
    {

        AudioFootStep.clip = GameSettings.Instance.FootStepBasicclip;
        AudioHeadShot.clip = GameSettings.Instance.HeadShotCrushclip;
        AudioBodyShot.clip = GameSettings.Instance.Bodyshotclip;
        AudioSmallGrowl.clip = GameSettings.Instance.GrowlSmallclipCreature;
        AudioBigGrowl.clip = GameSettings.Instance.GrowlBigclipCreature;
        AudioSmallMoan.clip = GameSettings.Instance.MoanSmallclipCreature;
        AudioBigMoan.clip = GameSettings.Instance.MoanBigclipCreature;
        AudioSmallHurt.clip = GameSettings.Instance.HurtSmallclipCreature;
        AudioBigHurt.clip = GameSettings.Instance.HurtBigclipCreature;
        AudioSmallAttack.clip = GameSettings.Instance.AttackSmallclipCreature;
        AudioBigAttack.clip = GameSettings.Instance.AttackBigclipCreature;
        AudioSmallDeath.clip = GameSettings.Instance.DeathSmallclipCreature;
        AudioBigDeath.clip = GameSettings.Instance.DeathBigclipCreature;
    }


    void Load_Giant()
    {

        AudioFootStep.clip = GameSettings.Instance.FootStepPumpkinclip;
        AudioHeadShot.clip = GameSettings.Instance.HeadShotCrushclip;
        AudioBodyShot.clip = GameSettings.Instance.Bodyshotclip;
        AudioSmallGrowl.clip = GameSettings.Instance.GrowlSmallclipGiant;
        AudioBigGrowl.clip = GameSettings.Instance.GrowlBigclipGiant;
        AudioSmallMoan.clip = GameSettings.Instance.MoanSmallclipGiant;
        AudioBigMoan.clip = GameSettings.Instance.MoanBigclipGiant;
        AudioSmallHurt.clip = GameSettings.Instance.HurtSmallclipGiant;
        AudioBigHurt.clip = GameSettings.Instance.HurtBigclipGiant;
        AudioSmallAttack.clip = GameSettings.Instance.AttackSmallclipGiant;
        AudioBigAttack.clip = GameSettings.Instance.AttackBigclipGiant;
        AudioSmallDeath.clip = GameSettings.Instance.DeathSmallclipGiant;
        AudioBigDeath.clip = GameSettings.Instance.DeathBigclipGiant;
    }


    void Load_Bone()
    {

        AudioFootStep.clip = GameSettings.Instance.BoneSmallBreakclip;
        AudioHeadShot.clip = GameSettings.Instance.DeathBigclipBone;
        AudioBodyShot.clip = GameSettings.Instance.BoneBodyShotclip;
        AudioSmallGrowl.clip = GameSettings.Instance.GrowlSmallclipBone;
        AudioBigGrowl.clip = GameSettings.Instance.GrowlBigclipBone;
        AudioSmallMoan.clip = GameSettings.Instance.MoanSmallclipBone;
        AudioBigMoan.clip = GameSettings.Instance.MoanBigclipBone;
        AudioSmallHurt.clip = GameSettings.Instance.HurtSmallclipBone;
        AudioBigHurt.clip = GameSettings.Instance.HurtBigclipBone;
        AudioSmallAttack.clip = GameSettings.Instance.AttackSmallclipBone;
        AudioBigAttack.clip = GameSettings.Instance.AttackBigclipBone;
        AudioSmallDeath.clip = GameSettings.Instance.DeathSmallclipBone;
        AudioBigDeath.clip = GameSettings.Instance.DeathBigclipBone;

    }
    #endregion
}
