using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System;
using UnityEngine.UI;

public class ScreenCapture : MonoBehaviour
{
    public int captureWidth = 1080;
    public int captureHeight = 1920;
    
    // Start is called before the first frame update

    public enum Format
    {
        RAW,
        JPG,
        PNG,
        PPM
    };

    public Format format = Format.JPG;


    private string outputFolder;

    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;
   
    public bool isProcessing;
    private byte[] currentTexture; 
    
    public Image showImage;
    public UnityEvent OnShowImage;

    public string currentFilePath;
    
    
    void Start()
    {
        outputFolder = Application.persistentDataPath + "/Screenshots/";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
            Debug.Log("Save Path will be: " + outputFolder);
        }

    }
    
    private string CreateFileName(int width, int height)
    {
        string timestamp = DateTime.Now.ToString("yyyyMMddTHHmmss");

        var filename = string.Format("{0}/screen_{1}x{2}_{3}.{4}", outputFolder, width, height, timestamp, format.ToString().ToLower());

        return filename;
    }

    private void CaptureScreenshot()
    {
        isProcessing = true;
        if(renderTexture == null)
        {
            rect = new Rect(0, 0, captureWidth, captureHeight);
            renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
            screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        }
        
        Camera camera = Camera.main;
        camera.targetTexture = renderTexture;
        camera.Render();

        //
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(rect,0,0);

        camera.targetTexture = null;
        RenderTexture.active = null;

        string filename = CreateFileName((int)rect.width, (int) rect.height);

        byte[] fileHeader = null;
        byte[] fileData = null;

        switch (format)
        {
            case Format.RAW:
                fileData = screenShot.GetRawTextureData();
                break;
            case Format.PNG:
                fileData = screenShot.EncodeToPNG();
                break;
            case Format.JPG:
                fileData = screenShot.EncodeToJPG();
                break;
            case Format.PPM:
            {
                string headerStr = string.Format("P6\n{0} {1}\n255\n", rect.width, rect.height);
                fileHeader = System.Text.Encoding.ASCII.GetBytes(headerStr);
                fileData = screenShot.GetRawTextureData();
                break;
            }
        }

        currentTexture = fileData;
        
        new System.Threading.Thread(() =>
        {
            var file = System.IO.File.Create(filename);

            if (fileHeader != null)
            {
                file.Write(fileHeader, 0, fileHeader.Length);
            }
            file.Write(fileData, 0, fileData.Length);
            file.Close();
            Debug.Log(string.Format("Screenshot Saved {0}, size {1}, ", filename, fileData.Length));
            isProcessing = false;
        }
            ).Start();

        currentFilePath = filename;
        StartCoroutine(ShowImage());
        
        Destroy(renderTexture);
        renderTexture = null;
        screenShot = null;


    }

    public void TakeScreenShot()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (!isProcessing)
        {
            CaptureScreenshot();
        }
        else
        {
            Debug.Log("Currently Processing");
        }
    }


    public IEnumerator ShowImage()
    {
    
        yield return new WaitForEndOfFrame();

        showImage.material.mainTexture = null;
        Texture2D texture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        texture.LoadImage(currentTexture);
        showImage.material.mainTexture = texture;

        OnShowImage?.Invoke();
    }

    public void ShareImage()
    {
        new NativeShare()
            .AddFile(currentFilePath)
            .SetSubject("This is my screenshot")
            .SetText("share your screenshot with your friend")
            .Share();
            
    }


}
