using System.Collections.Generic;

public interface ILevelProps
{
    void Set_TickerOverride(bool argonoff);
    bool Get_TickerOverride();


    void iSet_LevelTime(int t);
    int Get_LevelTime();

    void iSet_LevelNumber(int num);
    int Get_LevelNumber();


    int Get_LevelHP();
    void iSetLevelHP(int x);

    int Get_LevelHItStrength();
    void iSetLevelHItstrength(int x);

    int Get_LevelMax_Standard_OnScreen();
    void iSet_LevelMaxEnemiesOnScreen(int maxenemies);


    int Get_LevelMax_Gravers_OnScreen();
    void iSet_LevelMax_Gravers_OnScreen(int maxenemies);

    int Get_LevelMax_Sprinters_OnScreen();
    void iSet_LevelMax_Sprinters_OnScreen(int maxenemies);

    int Get_LevelMax_Axers_OnScreen();

    void iSet_LevelMax_Axers_OnScreen(int maxenemies);


    void iSet_LevelSeekSpeed(SeekSpeed argAgroLevel);
    SeekSpeed Get_LevelBaseSeekSpeed();

    void iSet_LevelGunType(GunType maingun, GunType secondarygun);
    GunType Get_LevelGunType(LeftMidRight argLMR);

    List<int> Get_ListSpawnPointIds();

    List<int> Get_ListGravePointIds();

    List<int> Get_ListTunnelSpawnPointIds();

    void iIncrease_LevelSeekSpeed();


}
