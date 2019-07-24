using UnityEngine;
using System.Collections;
using ArenaLiveAPI;

public class B_AutomaticWeapon : BlasterBehaviour {

    public float FireRate;

    public int MaxAmmo;
    public int Ammo;

    private bool IsWaitingInitial = false;
    private bool IsFiring = false;

    private float LastShotTime = 0f;
    public float InitialDelay;
    private float StartFiringTime = 0f;

    private bool WillChangeMode = false;

    public Blaster blaster;
    public BlasterBehaviour NextBehaviour;

    // Use this for initialization
    void Start()
    {
        Ammo = MaxAmmo;
    }

    protected override void OnActivated()
    {
        ChangeMode(InitialModeId);
        WillChangeMode = false;

        if (Ammo > 0)
        {
            SetUserConditions(~BlasterUserConditions.OutOfAmmo);
        }
        else
        {
            SetUserConditions(BlasterUserConditions.OutOfAmmo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsConnected || WillChangeMode)
        {
            return;
        }

        if (BlasterInput.GetButtonDown(BlasterButtons.Trigger))
        {
            Fire();
        }

        if (BlasterInput.GetButtonUp(BlasterButtons.Trigger))
        {
            IsFiring = false;
        }

        if (BlasterInput.GetButtonUp(BlasterButtons.Bottom) && !IsFiring && !IsWaitingInitial)
        {
            PlayModeSlot(1);
            UpdateAmmo(MaxAmmo);
        }

        if (BlasterInput.GetButtonUp(BlasterButtons.LeftBack)
            || BlasterInput.GetButtonUp(BlasterButtons.RightBack))
        {
            PrepareChangeMode();
        }

        if (IsWaitingInitial)
        {
            if ((Time.time - StartFiringTime) > InitialDelay)
            {
                IsWaitingInitial = false;
                if (Ammo > 0)
                {
                    Ammo--;
                    LastShotTime = Time.time;
                    if (Ammo == 0)
                    {
                        SetUserConditions(BlasterUserConditions.OutOfAmmo);
                    }
                    
                
                    else
                    {
                        IsFiring = true;
                    }
                }
            }
        }

        if (IsFiring)
        {
            if ((Time.time - LastShotTime) > FireRate)
            {
                if (Ammo > 0)
                {
                    LastShotTime = Time.time;
                    Ammo--;
                    if (Ammo == 0)
                    {
                        SetUserConditions(BlasterUserConditions.OutOfAmmo);
                        IsFiring = false;
                    }
                }
            }

        }
    }

    void Fire()
    {
        //if(Ammo > 0)
        //{
            FireAuto();
       // }
    }

    void FireAuto()
    {
        PlayModeSlot(0);
            StartFiringTime = Time.time;
        IsWaitingInitial = true;

    }

    void AmmoDecrementer()
    {
        if(BlasterInput.GetButton(BlasterButtons.Trigger))
            UpdateAmmo(Ammo - 1);
    }

    void UpdateAmmo(int newAmmoCnt)
    {
        Ammo = newAmmoCnt;

        if(Ammo <= 0)
        {
            SetUserConditions(BlasterUserConditions.OutOfAmmo);
        }

        else
        {
            SetUserConditions(~BlasterUserConditions.OutOfAmmo);
        }
    }

    void PrepareChangeMode()
    {
        WillChangeMode = true;

        StartCoroutine(ChangeModeRoutine());
    }

    IEnumerator ChangeModeRoutine()
    {
        yield return new WaitForSeconds(0.1f);

        blaster.Behaviour = NextBehaviour;
    }
}
