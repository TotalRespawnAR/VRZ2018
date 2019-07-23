using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class EnemyAudioManager : MonoBehaviour
{

    /*
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\coughing blood death");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\damage1");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\damage2");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\damage3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\damage4");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\deathlastbreath");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\death quick1");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\deathquick2");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\deathquick3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\doggrowl");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\dognosesnort");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\exert1");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\exert2");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\exert3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\exert4");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\exert5");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\exert6");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\exert7");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\exertdry heave");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\ffalling-bones");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\Footstep01.wav
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\Footstep02.wav
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\Footstep03.wav
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\Footstep04.wav
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\hiss");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\horse1");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\horse2longer");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\horse3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\hungoverdisgust");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\hungovergrowl");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\lowexert");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\LOWgrowlmutant");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\OneThump");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\OneThumpLow");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\OneThumpOriginal");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\ZombieChaseSound\predator");


Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieAttackExert_01_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieAttackExert_02_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieAttackExert_03_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieAttackExert_04_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieAttackExert_05_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieAttackExert_06_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieAttackExert_07_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieAttackExert_08_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieGrowl_01_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieGrowl_02_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieGrowl_03_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieGrowl_04_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieGrowl_05_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieGrowl_06_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHitExert_01_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHitExert_02_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHitExert_03_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHitExert_04_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHitExert_05_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHitExert_06_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHurtExert_01_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHurtExert_02_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHurtExert_03_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHurtExert_04_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieHurtExert_05_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieMoan_01_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieMoan_02_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieMoan_03_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieMoan_04_Male3");
Resources.Load<AudioClip>("AudioClips\Enemy\Audio_Zombies\Male3_Zombie_Todd\ZombieMoan_05_Male3");
 */
    //AudioClip clip_Step;
    //AudioClip clip_Growl;
    //AudioClip clip_step1; //Footstep01
    //AudioClip clip_step2;
    //AudioClip clip_step3;

    //bool isSkeleton;

    //AudioSource audioSource;
    // Use this for initialization
    void Start()
    {

        //clip_Growl = Resources.Load<AudioClip>("AudioClips/Enemy/Audio_Zombies/Female1_Zombie_Becka/ZombieGrowl_04_Female1");
        //clip_step1 = Resources.Load<AudioClip>("AudioClips/Enemy/Audio_Zombies/ZombieChaseSound/Footstep01"); //Footstep01
        //clip_step2 = Resources.Load<AudioClip>("AudioClips/Enemy/Audio_Zombies/ZombieChaseSound/Footstep01");
        //clip_step3 = Resources.Load<AudioClip>("AudioClips/Enemy/Audio_Zombies/ZombieChaseSound/Footstep01");

        //if (GetComponent<MainEntityComponent>()._EnemyType == ARZombieypes.GRAVESKELETON)
        //{
        //    clip_Step = Resources.Load<AudioClip>("AudioClips/Enemy/Audio_Zombies/ZombieChaseSound/ffalling-bones");
        //    isSkeleton = true;
        //}
        //else
        //    clip_Step = Resources.Load<AudioClip>("AudioClips/Enemy/Audio_Zombies/ZombieChaseSound/Footstep01");
        //audioSource = GetComponent<AudioSource>();
        //audioSource.clip = clip_Step;
        ////audioSource.PlayOneShot(clip_Step);

    }


    //EVENTS in animations:
    // _reachGrunt2 
    // _step
    AudioSourceCTRL myAidioController;
    private void Awake()
    {
        for (int x = 0; x < transform.childCount; x++)
        {
            if (transform.GetChild(x).name.Contains("AudioSourcesObj"))
            {
                myAidioController = transform.GetChild(x).gameObject.GetComponent<AudioSourceCTRL>();
                break;
            }
        }

        if (myAidioController == null)
        {
            Debug.LogError("we haz a problem on " + transform.parent.gameObject.name);
        }

        InvokeRepeating("PlayMoan", 5, 10);
    }

    public void PlayMoan()
    {

        //print("moan");
        myAidioController.PlayKngAudioClip(AudioClipType.MoanBig);
    }

    public void PlayGrowl()
    {

        //print("moan");
        myAidioController.PlayKngAudioClip(AudioClipType.GrowlBig);
    }

    public void PlayHurt()
    {

        myAidioController.PlayKngAudioClip(AudioClipType.HurtBig);
    }
    public void PlayDeath()
    {

        myAidioController.PlayKngAudioClip(AudioClipType.DeathBig);
    }

    public void PlayBodyEffects()
    {

        myAidioController.PlayKngAudioClip(AudioClipType.BodyShot);
    }

    public void PlayHeadEffects()
    {

        myAidioController.PlayKngAudioClip(AudioClipType.HeadShot);
    }

    public void PlayEvent()
    {
        //if (isSkeleton)
        //    print("stepBone");
        //else
        //    print("stepFoot");

        // audioSource.PlayOneShot(clip_Step);
        // myAidioController.PlayKngAudioClip(AudioClipType.FootStep);
        // print("step");
    }
    public void PlayEvent(string str)
    {

        //print(str);
        //if (str.Contains("tep"))
        //{
        //    myAidioController.PlayKngAudioClip(AudioClipType.FootStep);

        //}
    }

}
