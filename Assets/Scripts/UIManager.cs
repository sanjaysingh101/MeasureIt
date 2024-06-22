using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject clearButton;
    public GameObject undoButton;
    public GameObject addButton;
    public TextMeshProUGUI UnitText;
    public GameObject indicatorAnimation;
    public MeshRenderer indicatorMaterial;
    public GameObject NudgeObject;

    public Color32 snapIndicatorColour;
    public TextMeshProUGUI debugText;
    private PlaceManager pc;
    public int planeInitialized =0;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlaceManager>();
    }


    public void ClickShare()
    {
        StartCoroutine(TakeSSAndShare());
    }

    private IEnumerator TakeSSAndShare()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        yield return new WaitForEndOfFrame();
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        if (!Directory.Exists(GetAndroidExternalStoragePath() + "/MeasureIt"))
        {
            Directory.CreateDirectory(GetAndroidExternalStoragePath() + "/MeasureIt");
        }
        string filePath = Path.Combine(GetAndroidExternalStoragePath() + "/MeasureIt", "MeasureIt-" + timeStamp + ".jpg");
        //if (debugText)
        //{
        //    debugText.text = filePath;
        //}
        File.WriteAllBytes(filePath, ss.EncodeToJPG());
        FindObjectOfType<AudioManager>().Play("click");
        NudgeObject.GetComponent<Animation>().Play();
        Destroy(ss);


    }

    private string GetAndroidExternalStoragePath()
    {
        if (Application.platform != RuntimePlatform.Android)
            return Application.persistentDataPath;

        var jc = new AndroidJavaClass("android.os.Environment");
        var path = jc.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory",
            jc.GetStatic<string>("DIRECTORY_DCIM"))
            .Call<string>("getAbsolutePath");
        return path;
    }
    // Update is called once per frame
    void Update()
    {
        if (pc.placedLineRenderers.Count == 0)
        {
            undoButton.GetComponent<Button>().interactable = false;
            undoButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,255,69);
        }
        else
        {
            undoButton.GetComponent<Button>().interactable = true;
            undoButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        }
    }
}
