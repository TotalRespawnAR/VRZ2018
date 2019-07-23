using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using SysDiag = System.Diagnostics;
using System.Linq;
#if !UNITY_EDITOR && UNITY_METRO
using System.Threading.Tasks;
using Windows.Storage;
#endif

public class DataIoScript : MonoBehaviour
{
#if !UNITY_EDITOR && UNITY_METRO
    // This example code can be used to read or write to an ApplicationData folder of your choice.

    // Change this to Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
    // to use the RoamingFolder instead, for example.
    Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;

    // Write data to a file
   // async void WriteTimestamp()
   // {
        //Windows.Globalization.DateTimeFormatting.DateTimeFormatter formatter =
        //    new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longtime");

        //StorageFile sampleFile = await localFolder.CreateFileAsync("RZdataFile.txt",
        //    CreationCollisionOption.ReplaceExisting);
        //await FileIO.WriteTextAsync(sampleFile, formatter.Format(DateTime.Now));
   // }

    // Read data from a file
    async Task ReadTimestamp()
    {
        try
        {
            StorageFile sampleFile = await localFolder.GetFileAsync("RZdataFile.txt");
            String timestamp = await FileIO.ReadTextAsync(sampleFile);
            // Data is contained in timestamp
        Debug.LogFormat("{0}\n", timestamp); 
        tm.text = "time stamp =" + timestamp; 
        }
        catch (FileNotFoundException e)
        {
            // Cannot find file
    Debug.LogFormat("{0}\n", "No File");
            tm.text = "time stamp = No File";
        }
        catch (IOException e)
        {
            // Get information from the exception, then throw
            // the info to the parent method.
            if (e.Source != null)
            {
                Debug.LogFormat("IOException source: {0}", e.Source);
            }
            throw;
        }
    }
#else
    // This example code can be used to read or write to an ApplicationData folder of your choice.

    // Change this to Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
    // to use the RoamingFolder instead, for example.

    //// DirectoryInfo ArzDir = new DirectoryInfo(Application.persistentDataPath);
    // Write data to a file
    void WriteTimestamp()
    {
        string DataString = DateTime.Now.ToLongTimeString();

        File.WriteAllText(Application.persistentDataPath + @"\RZdataFile.txt", DataString);

    }
    private void ReadTimestamp()
    {
        // Handle any problems that might arise when reading the text
        try
        {
            string line;
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            StreamReader theReader = new StreamReader(Application.persistentDataPath + @"\RZdataFile.txt", Encoding.Default);
            // Immediately clean up the reader after this block of code is done.
            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            // (Do not confuse this with the using directive for namespace at the 
            // beginning of a class!)
            using (theReader)
            {
                // While there's lines left in the text file, do this:
                do
                {
                    line = theReader.ReadLine();

                    if (line != null)
                    {
                        // Do whatever you need to do with the text line, it's a string now
                        // In this example, I split it into arguments based on comma
                        // deliniators, then send that array to DoStuff()
                        string[] entries = line.Split(',');
                        if (entries.Length > 0)
                            Debug.LogFormat("{0}\n", entries);

                        tm.text = line;
                    }
                }
                while (line != null);
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
                //return true;
            }
        }
        // If anything broke in the try block, we throw an exception with information
        // on what didn't work
        catch (Exception e)
        {
           Debug.LogFormat("{0}\n", e.Message);
          //  return false;
        }
    }
#endif


 
    public TextMesh tm;
    DataSerializeDeserializer dsdds;

    private void Awake()
    {
        dsdds = GetComponent<DataSerializeDeserializer>();
    }
    private void Start()
    {
        Debug.Log("I am DataIoScript on " + this.gameObject.name); 
        StartCoroutine(Waitabit());   
    }
    IEnumerator Waitabit() {
       // WriteTimestamp();
        tm.text = "writing \n"; 
        yield return new WaitForSeconds(2);
        tm.text = "reading \n";
        ReadTimestamp();
    }


}

//// Read data from a file
//void ReadTimestamp()
//{
//    try
//    {
//        StorageFile sampleFile = await localFolder.GetFileAsync("dataFile.txt");
//        String timestamp = await FileIO.ReadTextAsync(sampleFile);
//        // Data is contained in timestamp
//    }
//    catch (FileNotFoundException e)
//    {
//        // Cannot find file
//    }
//    catch (IOException e)
//    {
//        // Get information from the exception, then throw
//        // the info to the parent method.
//        if (e.Source != null)
//        {
//            Debug.WriteLine("IOException source: {0}", e.Source);
//        }
//        throw;
//    }


