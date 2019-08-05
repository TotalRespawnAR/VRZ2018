 
 using System;

[Serializable]
public class Data_VRZPlayerInfoScore
{
    public string PlayerFirstName;
    public string PlayerLastName;
    public string PlayerUserName;
    public string PlayerEmail;
    public int FinalScore;
    public int FinalKills;
    public int FinalHeadShots;
    public int FinalDeaths;
    public string GameTime;

    public Data_VRZPlayerInfoScore()
    {
        PlayerFirstName = "fn";
        PlayerLastName = "ln";
        PlayerUserName = "un";
        PlayerEmail = "em";
    }
    public Data_VRZPlayerInfoScore(DateTime dt, string fn, string ln, string un, string em, int vrzScore, int vrzKills, int vrzHeadshot, int vrzDeaths)
    {
        PlayerFirstName = fn;
        PlayerLastName = ln;
        PlayerUserName = un;
        PlayerEmail = em;
        GameTime = SessionTimeStampFormatter(dt);
        FinalScore = vrzScore;
        FinalKills = vrzKills;
        FinalHeadShots = vrzHeadshot;
        FinalDeaths = vrzDeaths;
    }

    public override string ToString()
    {
        return "|pinfo= " + PlayerFirstName + " " + PlayerLastName + " " + PlayerUserName + " " + PlayerEmail + "|";
    }


  
    string SessionTimeStampFormatter(DateTime argDatetime)
    {
        //'s' is "Sortable" date format. Output looks like 2008-04-10T06:30:00
        return  argDatetime.ToString("s");
    }
}
