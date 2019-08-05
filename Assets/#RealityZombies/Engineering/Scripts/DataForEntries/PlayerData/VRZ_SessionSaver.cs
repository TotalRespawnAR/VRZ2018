using System;
using System.IO;
using UnityEngine;

public class VRZ_SessionSaver : MonoBehaviour
{


    //string FileToCopy = null;
    //  \\DESKTOP-A8JUUIJ\Shared_MainPC
    string PlayerSessionName = "VRZ2018_";
    string Path_OGpc_LocalSharedFolder = @"C:\usr\Shared_MainPC";
    string Path_newpc_ExternalSharedFolder = @"\\DESKTOP-UCOVLTA\Share_NewPC";
    string Path_OGpc_ExternalSharedFolder = @"\\DESKTOP-A8JUUIJ\Shared_MainPC";


    string VRZ_SinglePlayerScoreDataPath
    {
        get
        {

            return Path_OGpc_ExternalSharedFolder ;

        }
    }

    string GetFullTimedPathFullFilePath(Data_VRZPlayerInfoScore argVRZdata)
    {
        string Path =    VRZ_SinglePlayerScoreDataPath + "\\"  + PlayerSessionName +"_"+ argVRZdata.PlayerFirstName + "_" + ".txt"; ;
        Debug.Log(Path);
        return Path;
    }
    //int fileCount = 0;
    //DirectoryInfo VrzDir;
    // Start is called before the first frame update
    void Start()
    {
        if (Environment.MachineName.Contains("A8JUUIJ"))
        {
            Debug.Log("I am OG PC");
        }
        else
        {
            Debug.Log("I'm NOT og pc");
        }
        //VrzDir = new DirectoryInfo(VRZ_SinglePlayerScoreDataPath);
        //FileInfo[] info = VrzDir.GetFiles("*.*");
    
        //foreach (FileInfo f in info)
        //{
        //    if (f.Name.Contains(PlayerSessionName))
        //    {
             
        //        fileCount++;
        //    }
        //}



     //   ProcessDirectory(VRZ_SinglePlayerScoreDataPath);


        //if not make it
        //Path_OGpc_LocalSharedFolder
    }
    public void ProcessDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        foreach (string fileName in fileEntries)
        {
            print(fileName);
        }
    }

    static void DirSearch(string dir, string rootDir = null)
    {
        if (rootDir == null)
        {
            rootDir = dir;
        }
        try
        {
            foreach (string f in Directory.GetFiles(dir))
            {
                string filename = f.Substring(rootDir.Length);
                print(filename);
            }
            foreach (string d in Directory.GetDirectories(dir))
            {
                print(d);
                DirSearch(d, rootDir);
            }

        }
        catch (System.Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    public void SaveIt(Data_VRZPlayerInfoScore argThisData)
    {


        string jsonData = JsonUtility.ToJson(argThisData);
        File.WriteAllText(GetFullTimedPathFullFilePath(argThisData), jsonData);
        //  File.WriteAllText(VRZ_SinglePlayerScoreDataPath+"\\yo.txt", "HELLO");
    }




    Data_VRZPlayerInfoScore Build_Data_FromJson(string argAllSess)
    {
        return JsonUtility.FromJson<Data_VRZPlayerInfoScore>(argAllSess);
    }

    
}
