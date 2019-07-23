using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerHologramBars: MonoBehaviour
{
 public AudioSource EmitSource;
 
 const int TEXTURE_SIZE = 32;
 const int HALF_TEXTURE = TEXTURE_SIZE/2;
 
 Color[] PixelArray = new Color[TEXTURE_SIZE*TEXTURE_SIZE];
 Color   WhiteColour = Color.white;
 Color   BlackColour = Color.black;
 
 float[] BarsList         = new float[HALF_TEXTURE];
 
 float[] AudioSampleSlice = new float[16];
 
 Texture2D DynamicTexture;
 
 float DelayBetweenSlices = (1.0f/60.0f)*1.5f;
 float CurrentDelay       = 0.0f;
 
 // Use this for initialization
 void Start(/*void*/)
 {
  DynamicTexture = new Texture2D(TEXTURE_SIZE, TEXTURE_SIZE, TextureFormat.RGBA32, false, false);
  DynamicTexture.filterMode = FilterMode.Point;
  DynamicTexture.wrapMode = TextureWrapMode.Clamp;
  gameObject.GetComponent<Renderer>().material.mainTexture = DynamicTexture;
  
  DynamicTexture.SetPixels(PixelArray);
  DynamicTexture.Apply();
 }

 // Update is called once per frame
 void Update(/*void*/)
 {
  if (EmitSource == null)
    return;
 
  // Only update the bars periodically
  CurrentDelay += Time.deltaTime;
  if (CurrentDelay < DelayBetweenSlices)
    return;
  CurrentDelay = 0.0f;
  
  // Grab a slice of audio data      
  EmitSource.GetOutputData(AudioSampleSlice, 0);
  
  // Sum up the slice
  float NewSum = 0.0f;
  for (int i=0; i<AudioSampleSlice.Length; ++i)
    NewSum += AudioSampleSlice[i];
  NewSum /= AudioSampleSlice.Length;
  NewSum *= 3.0f;
  
  // Average out the curve
  if (NewSum < BarsList[(BarsList.Length/2)])
    BarsList[(BarsList.Length/2)] = BarsList[(BarsList.Length/2)]*0.995f;// + NewSum*0.75f;
  else
    BarsList[(BarsList.Length/2)] = NewSum;
  
  // Lerp the curve values
  for (int i=(BarsList.Length/2)-1; i>=0; --i)
    {
     BarsList[i]                   = BarsList[i+1]*0.9f;
     BarsList[BarsList.Length-i-1] = BarsList[BarsList.Length-i-2]*0.9f;
    }

  // Update the pixels
  for (int i=0; i<HALF_TEXTURE; ++i)
    {
     // BOTTOM SECTION
     int PixelHeightBottom = Mathf.FloorToInt((1.0f-BarsList[i])*HALF_TEXTURE);
     for (int j=0; j<HALF_TEXTURE; ++j)
       PixelArray[(j*TEXTURE_SIZE)+((HALF_TEXTURE-i-1)*2)] = ((j-1)>=PixelHeightBottom)?WhiteColour:BlackColour;
    
     // TOP SECTION
     int PixelHeightTop = HALF_TEXTURE + Mathf.FloorToInt(BarsList[i]*HALF_TEXTURE);
     for (int j=HALF_TEXTURE; j<TEXTURE_SIZE; ++j)
       PixelArray[(j*TEXTURE_SIZE)+((HALF_TEXTURE-i-1)*2)] = (j<=PixelHeightTop)?WhiteColour:BlackColour;
    }
  
  // Push the data to the texture
  DynamicTexture.SetPixels(PixelArray);
  DynamicTexture.Apply();
 }
}
