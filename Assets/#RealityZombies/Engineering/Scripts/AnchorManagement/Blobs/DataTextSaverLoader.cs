using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_WSA
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
#endif
public class DataTextSaverLoader : MonoBehaviour {


    public TextMesh tm;
    DataSerializeDeserializer dsdds;

    private void Awake()
    {
        dsdds = GetComponent<DataSerializeDeserializer>();
    }
    private   string fileExtension = ".wak";

 
    public   string MeshFolderName
    {
        get
        {
#if !UNITY_EDITOR && UNITY_WSA
                return ApplicationData.Current.RoamingFolder.Path;
#else
            return Application.persistentDataPath;
#endif
        }
    }
    public   IList<string> Load(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            tm.text = "Must specify a valid fileName.";
            throw new ArgumentException("Must specify a valid fileName.");
        }

        List<string> listoflines = new List<string>();

        // Open the mesh file.
        String folderName = MeshFolderName;
        string s = String.Format("Loading mesh file: {0}", Path.Combine(folderName, fileName + fileExtension));
        Debug.Log(s);
        tm.text += "\n";
        tm.text +=  s;
        using (Stream stream = OpenFileForRead(folderName, fileName + fileExtension))
        {
            // Read the file and deserialize the meshes.
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);

            listoflines.AddRange(dsdds.Deserialize_ListOfStrings(data));
        }

        Debug.Log("Mesh file loaded.");
        tm.text += "\n";
        tm.text += "Mesh file loaded.";
        return listoflines;
    }

    public List<byte> Load()
    {
        string fileName = "ANCRS";
        if (string.IsNullOrEmpty(fileName))
        {
            tm.text = "Must specify a valid fileName.";
            throw new ArgumentException("Must specify a valid fileName.");
        }

        List<byte> AncBytes = new List<byte>();

        // Open the mesh file.
        String folderName = MeshFolderName;
        string s = String.Format("Loading anc file: {0}", Path.Combine(folderName, fileName + fileExtension));
        Debug.Log(s);
        tm.text += "\n";
        tm.text += s;
        using (Stream stream = OpenFileForRead(folderName, fileName + fileExtension))
        {
            // Read the file and deserialize the meshes.
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);

            AncBytes.AddRange(data);
        }

        Debug.Log("ancs file loaded.");
        tm.text += "\n";
        tm.text += "ancs file loaded.";
        return AncBytes;
    }

    public   string Save(string fileName, IEnumerable<string> argListOfStrings)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            tm.text = "Must specify a valid fileName.";
            throw new ArgumentException("Must specify a valid fileName.");
        }

        if (argListOfStrings == null)
        {
            tm.text = "Value of lines cannot be null.";
            throw new ArgumentNullException("Value of meshes cannot be null.");
        }

        // Create the mesh file.
        String folderName = MeshFolderName;
        string s = String.Format("Saving mesh file: {0}", Path.Combine(folderName, fileName + fileExtension));
        Debug.Log(s);
        tm.text += "\n";
        tm.text += s;
        using (Stream stream = OpenFileForWrite(folderName, fileName + fileExtension))
        {
            // Serialize and write the meshes to the file.
            byte[] data = dsdds.Serialize_ListOfStrings(argListOfStrings);
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        Debug.Log("Mesh file saved.");
        tm.text += "\n";
        tm.text += "Mesh file saved.";
        return Path.Combine(folderName, fileName + fileExtension);
    }


    public string Save( byte[] rawfrombatch)
    {
        string fileName = "ANCRS";
        if (string.IsNullOrEmpty(fileName))
        {
            tm.text = "Must specify a valid fileName.";
            throw new ArgumentException("Must specify a valid fileName.");
        }

        if (rawfrombatch == null)
        {
            tm.text = "Value of lines cannot be null.";
            throw new ArgumentNullException("Value of meshes cannot be null.");
        }

        // Create the mesh file.
        String folderName = MeshFolderName;
        string s = String.Format("Saving mesh file: {0}", Path.Combine(folderName, fileName + fileExtension));
        Debug.Log(s);
        tm.text += "\n";
        tm.text += s;
        using (Stream stream = OpenFileForWrite(folderName, fileName + fileExtension))
        {
            // Serialize and write the meshes to the file.
            byte[] data = rawfrombatch;
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        Debug.Log("Mesh file saved.");
        tm.text += "\n";
        tm.text += "Mesh file saved.";
        return Path.Combine(folderName, fileName + fileExtension);
    }



    private  Stream OpenFileForRead(string folderName, string fileName)
    {
        Stream stream = null;

#if !UNITY_EDITOR && UNITY_WSA
            Task<Task> task = Task<Task>.Factory.StartNew(
                            async () =>
                            {
                                StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(folderName);
                                StorageFile file = await folder.GetFileAsync(fileName);
                                IRandomAccessStreamWithContentType randomAccessStream = await file.OpenReadAsync();
                                stream = randomAccessStream.AsStreamForRead();
                            });
            task.Wait();
            task.Result.Wait();
#else
        stream = new FileStream(Path.Combine(folderName, fileName), FileMode.Open, FileAccess.Read);
#endif
        return stream;
    }


    private   Stream OpenFileForWrite(string folderName, string fileName)
    {
        Stream stream = null;

#if !UNITY_EDITOR && UNITY_WSA
            Task<Task> task = Task<Task>.Factory.StartNew(
                            async () =>
                            {
                                StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(folderName);
                                StorageFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                                IRandomAccessStream randomAccessStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                                stream = randomAccessStream.AsStreamForWrite();
                            });
            task.Wait();
            task.Result.Wait();
#else
        stream = new FileStream(Path.Combine(folderName, fileName), FileMode.Create, FileAccess.Write);
#endif
        return stream;
    }

}
