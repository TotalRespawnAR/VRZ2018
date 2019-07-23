using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Screenshot : MonoBehaviour
{
    int foldPathNum = 0;
    int screenshotNumber = 0;
    string originalFileName = "screenShot";
    string newFileName = "";
    string folderPath;

    public Text printText;

    [Tooltip("If this is off, no folders will be made for the photos")]
    public bool wantToTakePictures = false;
    [Tooltip("If this is off, press 4 to take screenshots")]
    public bool autoTakePictures = false;



    // start
    private void Start()
    {
        if(wantToTakePictures == true)
        {
            CreateScreenshotFolder();
        }
        
    }// end of start

    //update
    private void Update()
    {
        if(wantToTakePictures == true)
        {
            // if we want to take our own photos
            if (autoTakePictures == false)
            {
                if (Input.GetKeyDown("4") || Input.GetKey("4"))
                {
                    Debug.Log("pressed 4");
                    // Capture and store the screenshot
                    newFileName = originalFileName + "_" + screenshotNumber + ".png";
                    screenshotNumber++;
                    ScreenCapture.CaptureScreenshot(folderPath + newFileName);
                    if (printText != null)
                        printText.text = "Screenshot: " + screenshotNumber + " saved to: " + folderPath;

                }

            }// end of take our own photos
            else
            // if we want auto take photos
            {
                Debug.Log("Computer is taking screenshots");
                // Capture and store the screenshot
                newFileName = originalFileName + "_" + screenshotNumber + ".png";
                screenshotNumber++;
                ScreenCapture.CaptureScreenshot(folderPath + newFileName);
                if (printText != null)
                    printText.text = "Screenshot: " + screenshotNumber + " saved to: " + folderPath;
            }

        }// end of wantToTakePictures
        
    }// end of Update()



    private void CreateScreenshotFolder()
    {
        folderPath = "C:/" + foldPathNum + "_NEW_SCREENSHOTS/";

        // if it does exist create a 2nd one
        if (System.IO.Directory.Exists(folderPath))
        {
            Debug.Log("A folder exisists with the number " + foldPathNum);
            foldPathNum++;
            //folderPath = "C:/" + foldPathNum + "_NEW_SCREENSHOTS/";
            //System.IO.Directory.CreateDirectory(folderPath);
            CreateScreenshotFolder();
        }

        // Create the folder beforehand if not exists
        if (!System.IO.Directory.Exists(folderPath))
        {
            Debug.Log("No folder with the number " + foldPathNum + " exisists yet");
            System.IO.Directory.CreateDirectory(folderPath);
        }

        Debug.Log("saving location is: " + folderPath);

    }// end of screenshot folder function



}// end of screenshot script