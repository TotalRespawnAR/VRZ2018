using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SysDiag = System.Diagnostics;
using System.Linq;
using System.Text;
#if !UNITY_EDITOR && UNITY_METRO
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
#endif



 
 

public static class StaticHexLogger
{

    public static string DirTarget
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

    public static string FileName_BatchSaveHexText
    {
        get
        {
            return "/" + "ARZ_SaveBatch_HEX.txt";
        }
    }

    public static string FileName_BatchSaveBytesWACt
    {
        get
        {
            return  "ARZ_SaveBatch_HEX.wac";
        }
    }

    public static void WriteFast_32BitLinesText(List<byte> argByteList, string path, string filenameExtenssion)
    {
        string fullpath = path + filenameExtenssion;
        using (StreamWriter strmwriter = new StreamWriter(File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite)))
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < argByteList.Count; x++)
            {
                if (x % 4 == 0) { sb.Append(Environment.NewLine); }
                sb.Append(" " + argByteList[x].ToString("X2") + " ");
            }
            strmwriter.Write(sb);
            strmwriter.Dispose();
        }
    }


    public static string ShowFast_32BitLinesText(byte[] argByteArra )
    {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < argByteArra.Length; x++)
            {
                if (x % 4 == 0) { sb.Append(Environment.NewLine); }
                sb.Append(" " + argByteArra[x].ToString("X2") + " ");
            }
        return sb.ToString();
    }


    public static void SaveWATB(byte[] rawfrombatch, string path, string filenameExtenssion)
    {
        string fullpath = path + filenameExtenssion;
        using (Stream stream = OpenFileForWrite(path, filenameExtenssion))
        {
            // Serialize and write the meshes to the file.
            byte[] data = rawfrombatch;
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }
    }

     
    static public List<byte> LoadWATB(string folderPath, string filenameExtenssion)
    {
        List<byte> AncBytes = new List<byte>();
        using (Stream stream = OpenFileForRead(folderPath, filenameExtenssion))
        {
            // Read the file and deserialize the meshes.
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);

            AncBytes.AddRange(data);
        }
        return AncBytes;
    }

    static private Stream OpenFileForWrite(string folderPath, string fileName)
    {
        Stream stream = null;

#if !UNITY_EDITOR && UNITY_WSA
        Task<Task> task = Task<Task>.Factory.StartNew(
                        async () =>
                        {
                            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(folderPath);
                            StorageFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                            IRandomAccessStream randomAccessStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                            stream = randomAccessStream.AsStreamForWrite();
                        });
        task.Wait();
        task.Result.Wait();
#else
        stream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create, FileAccess.Write);
#endif
        return stream;
    }




    static private Stream OpenFileForRead(string folderPath, string fileName)
    {
        Stream stream = null;

#if !UNITY_EDITOR && UNITY_WSA
        Task<Task> task = Task<Task>.Factory.StartNew(
                        async () =>
                        {
                            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(folderPath);
                            StorageFile file = await folder.GetFileAsync(fileName);
                            IRandomAccessStreamWithContentType randomAccessStream = await file.OpenReadAsync();
                            stream = randomAccessStream.AsStreamForRead();
                        });
        task.Wait();
        task.Result.Wait();
#else
        stream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Open, FileAccess.Read);
#endif
        return stream;
    }
}
