using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Indicator : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject indicatorObject;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public Vector3 cross;
    UIManager uIManager;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        uIManager = FindObjectOfType<UIManager>();
        indicatorObject = gameObject.transform.GetChild(0).gameObject;
        indicatorObject.SetActive(false);
        Vibration.Init();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Vector2(Screen.width/2,Screen.height/2);
        if(raycastManager.Raycast(ray,hits,TrackableType.PlaneWithinBounds))
        { 
            Pose hitPose = hits[0].pose;
            transform.position = hitPose.position;
            transform.rotation = hitPose.rotation;
            
            if (!indicatorObject.activeInHierarchy)
            {
                indicatorObject.SetActive(true);
                uIManager.addButton.GetComponent<Button>().interactable = true;
                uIManager.addButton.GetComponent<AddPointButton>().enabled=true;
            }
            
        }

        #if UNITY_EDITOR
                indicatorObject.SetActive(true);
                uIManager.addButton.GetComponent<Button>().interactable = true;
                uIManager.addButton.GetComponent<AddPointButton>().enabled=true;
        #endif        
    }
}
