/*
- [ ] Accuracy = (totalshots-miss) / totalshots
- [ ] HeadshotAccuracy =  headshots / totalshots
- [ ] HeadshotRatio - number of kills that were headshots.
*/
using System;
[Serializable]
public class Data_PlayerPoints  {
    public int score;
    public int headshots;
    //
    public float Accuracy;//calculated in constructor
    public float HeadshotAccuracy;//calculated in constructor
    public float TorssoshotAccuracy;//calculated in constructor
    public float LimbshotAccuracy;//calculated in constructor
    public int torssoshots;
    public int limbshots;
    public int headshotkills;
    public int torssoshotkills;
    public int limbshotkills;
    //
    public int streakcount;
    public int maxstreak;
    public int kills;
    public int miss;
    public int totalshots;
    public int deaths;
    public int pointslost;
    public int numberofReloads;
    public int wavessurvived;
    //from wave 1 
    //flood zombie boss 2 0nly
    //
    public Data_PlayerPoints()
    {
        score = 0;
        headshots = 1;
        streakcount = 0;
        maxstreak = 0;
        kills = 1;
        miss = 1;
        totalshots = 1;
        deaths = 0;
        pointslost = 1;
        numberofReloads = 1;
        wavessurvived = 1;
        Accuracy = 0.0f;
        HeadshotAccuracy = 0.0f;

        torssoshots=0;
        limbshots=0;
        headshotkills = 0;
        torssoshotkills  = 0;
        limbshotkills = 0;
    }
    public Data_PlayerPoints(int sc, int hs, int st, int ms, int ki, int mi, int ts,int dt,int pl, int nr, int ws) {
        score=sc;
        headshots=hs;
        streakcount=st;
        maxstreak=ms;
        kills=ki;
        miss=mi;
        totalshots=ts;
        deaths = dt;
        pointslost = pl;
        numberofReloads = nr;
        wavessurvived = ws;
        CalcAccuracy(ts, mi);
        CalcHeadshotAccuracy(ts, hs);
        CalcTorssoshotAccuracy(ts, 0);
        CalcLimbshotAccuracy(ts, 0);

        torssoshots = 0;
        limbshots = 0;
        headshotkills = 0;
        torssoshotkills = 0;
        limbshotkills = 0;
    }
    //to do nabs
    //to do implement this
    public Data_PlayerPoints(int sc, int hs, int argTorssoShots, int argLimbshots, int argHeadShotKills, int argTorssoshotKills, int argLimbShotKills, int st, int ms, int ki, int mi, int ts, int dt, int pl, int nr, int ws)
    {
        score = sc;
        headshots = hs;
        streakcount = st;
        maxstreak = ms;
        kills = ki;
        miss = mi;
        totalshots = ts;
        deaths = dt;
        pointslost = pl;
        numberofReloads = nr;
        wavessurvived = ws;
        CalcAccuracy(ts, mi);
        CalcHeadshotAccuracy(ts, hs);
        CalcTorssoshotAccuracy(ts, argTorssoShots);
        CalcLimbshotAccuracy(ts, argLimbshots);

        torssoshots = argTorssoShots;
        limbshots = argLimbshots;
        headshotkills = argHeadShotKills;
        torssoshotkills = argTorssoshotKills;
        limbshotkills = argLimbShotKills;
    }
    public override string ToString()
    {
        return  " |score= " + score +

                " |Accuracy " + Accuracy +
                " |HeadShotAccuracy " + HeadshotAccuracy +

                " |headshots " + headshots +
                
                " |streakcount " + streakcount +
                " |maxstreak " + maxstreak +
                " |kills " + kills +
                " |miss " + miss +
                " |totalshots " + totalshots +
                " |deaths " + deaths +
                " |pointslost " + pointslost +
                " |reloads " + numberofReloads +
                " |waves " + wavessurvived +

                
                " |";
    }

    void CalcAccuracy(int allshots, int miss)
    {
        int landedcalced = allshots - miss;
        float fired = (float)allshots;
        float landed = (float)landedcalced;
        if (fired > 0)
        {
            Accuracy = (landed / fired) * 100;

        }
        else
            Accuracy = 0.0f;
    }

    void CalcHeadshotAccuracy(int allshots, int argHeadshots)
    {
        float fired = (float)allshots;
        float landed = (float)argHeadshots;
        if (fired > 0)
        {
            HeadshotAccuracy = (landed / fired) * 100;

        }
        else
            HeadshotAccuracy = 0.0f;
    }
    
    void CalcTorssoshotAccuracy(int allshots, int argTorssoShots)
    {
        float fired = (float)allshots;
        float landed = (float)argTorssoShots;
        if (fired > 0)
        {
            TorssoshotAccuracy = (landed / fired) * 100;

        }
        else
            TorssoshotAccuracy = 0.0f;
    }
    
    void CalcLimbshotAccuracy(int allshots, int argLimbshots)
    {
        float fired = (float)allshots;
        float landed = (float)argLimbshots;
        if (fired > 0)
        {
            LimbshotAccuracy = (landed / fired) * 100;

        }
        else
            LimbshotAccuracy = 0.0f;
    }
}
