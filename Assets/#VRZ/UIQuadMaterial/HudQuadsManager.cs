using System.Collections.Generic;
using UnityEngine;

public class HudQuadsManager : MonoBehaviour
{


    public List<QuadAnimatorCTRL> TheQuads;
    public Dictionary<TriggersDamageEffects, QuadAnimatorCTRL> HudQuad_DICT;
    void SetRefs()
    {
        TheQuads = new List<QuadAnimatorCTRL>();
        HudQuad_DICT = new Dictionary<TriggersDamageEffects, QuadAnimatorCTRL>();
        int childCNT = this.transform.childCount;
        for (int x = 0; x < this.transform.childCount; x++)
        {
            QuadAnimatorCTRL temp = this.transform.GetChild(x).gameObject.GetComponent<QuadAnimatorCTRL>();
            if (temp != null)
            {
                TheQuads.Add(temp);
                if (HudQuad_DICT.ContainsKey(temp.EffectType))
                {
                    Debug.Log("key " + temp.EffectType.ToString() + " already exixst");

                }

                else
                {
                    HudQuad_DICT.Add(temp.EffectType, temp);

                }
            }

        }

        if (TheQuads.Count != HudQuad_DICT.Count) Debug.LogError(Mathf.Abs(TheQuads.Count - HudQuad_DICT.Count).ToString() + "Quads type is Missing");


    }

    private void Awake()
    {
        SetRefs();
    }

    public void Trig_DamageEffect(TriggersDamageEffects argDamageTrig)
    {
        if (HudQuad_DICT.ContainsKey(argDamageTrig))
        {
            HudQuad_DICT[argDamageTrig].TRIGGER_effectANimation();
        }
        else
        {
            Debug.LogError("NO such Key " + argDamageTrig.ToString());
        }
    }
    /*
    private void Update()
    {
        int num = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            num = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            num = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            num = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            num = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            num = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            num = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            num = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            num = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            num = 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            num = 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            num = (int)TriggersDamageEffects.GlassBreak1;
        }

        if (num > -1)

            if (HudQuad_DICT.ContainsKey((TriggersDamageEffects)num))
            {
                HudQuad_DICT[(TriggersDamageEffects)num].TRIGGER_effectANimation();
            }
            else
            {
                //  Debug.LogError("NO such Key " + ((TriggersDamageEffects)num).ToString());
            }

    }

    */
}
