
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SysDiag = System.Diagnostics;

public class DataSerializeDeserializer : MonoBehaviour {

    private  int ListStringsHeaderSize = sizeof(int) * 1;
    public  byte[] Serialize_ListOfStrings(IEnumerable<string> textdata)
    {
        byte[] data = null;

        using (MemoryStream stream = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                foreach (string str in textdata)
                {
                    WriteTextData(writer, str);
                }

                stream.Position = 0;
                data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
            }
        }

        return data;
    }

    public  IEnumerable<string> Deserialize_ListOfStrings(byte[] data)
    {
        List<string> newstringtext = new List<string>();

        using (MemoryStream stream = new MemoryStream(data))
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                while (reader.BaseStream.Length - reader.BaseStream.Position >= ListStringsHeaderSize)
                {
                    newstringtext.Add(ReadTextData(reader));
                }
            }
        }

        return newstringtext;
    }



    private  void WriteTextData(BinaryWriter writer, string textDataToWriteTofile)
    {
        SysDiag.Debug.Assert(writer != null);
        int numLines = textDataToWriteTofile.Split('\n').Length;
        string[] lines = textDataToWriteTofile.Split('\n');
        // Write the mesh data.
        Write_dataText_Header(writer, numLines);
        Write_String_Indicies(writer, lines);
   
    }
    private  string ReadTextData(BinaryReader reader)
    {
        SysDiag.Debug.Assert(reader != null);

        int lineCount = 0;
 
        // Read the mesh data.
        Read_dataText_Header(reader, out lineCount);
        string[] liesFromTextData = Read_String_Indicies(reader, lineCount);

        // Create the text.
        string TextOutput = "";
        foreach (string str in liesFromTextData) {
            TextOutput += str;
        }

        return TextOutput;
    }


    private  void Write_dataText_Header(BinaryWriter writer, int lineCount)
    {
        SysDiag.Debug.Assert(writer != null);
        writer.Write(lineCount);
    }
    private  void Read_dataText_Header(BinaryReader reader, out int lineCount )
    {
        SysDiag.Debug.Assert(reader != null);
        lineCount = reader.ReadInt32();
    }

    /// Writes the Line indices from the full text to the data stream
    private  void Write_String_Indicies(BinaryWriter writer, string[] argLinesIndicies)
    {
        SysDiag.Debug.Assert(writer != null);
        foreach (string str in argLinesIndicies)
        {
            writer.Write(str);
        }
    }



    //   Reads the string indices  
    private  string[] Read_String_Indicies(BinaryReader reader, int indexOfLine)
    {
        SysDiag.Debug.Assert(reader != null);

        string[] StringIndices = new string[indexOfLine];

        for (int i = 0; i < StringIndices.Length; i++)
        {
            StringIndices[i] = reader.ReadString();
        }

        return StringIndices;
    }
}
