using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SysDiag = System.Diagnostics;
using System.Linq;
#if !UNITY_EDITOR && UNITY_METRO
using System.Threading.Tasks;
using Windows.Storage;
#endif

public class FilePathTest : MonoBehaviour {
    /*
    string dirPath = @"C:\Users\Nabil\Desktop\"; 
    string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + @"\RZDataFolder";

    DirectoryInfo ArzDir;
    string DataFolderName = @"\RZDataFolder";


    string ArzDirPath
    {
        get
        {
#if !UNITY_EDITOR && UNITY_METRO
                    return ApplicationData.Current.RoamingFolder.Path;
#else
            return Application.persistentDataPath;
#endif
        }
    }

    void Start()
    {
        //check if folder is there

        //Windows.Storage.StorageFolder folder = await picker.PickSingleFolderAsync();
        //StorageFolder targetFolder = await folder.CreateFolderAsync("Documents", CreationCollisionOption.OpenIfExists)
        //var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".exe" });
        //var query = folder.CreateFileQueryWithOptions(queryOptions);
        //var files = await query.GetFilesAsync();
        //foreach (var file in files)
        //{
        //    await file.MoveAsync(targetFolder);
        //}
        //if not make it 

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    */
}
