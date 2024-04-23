using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class snapObjectReciever : MonoBehaviour
{
    PlaceManager placeManager;
    UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        placeManager = FindObjectOfType<PlaceManager>();
        uIManager = FindObjectOfType<UIManager>();
        Vibration.Init();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("indicator"))
        {
            Debug.Log("SNAP!!");
            Vibration.VibrateAndroid(10);
            uIManager.indicatorAnimation.GetComponent<Animation>().Play();
            uIManager.indicatorMaterial.material.color = uIManager.snapIndicatorColour;
            //Indicator Ring Travels to SnapObject
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("indicator"))
        {
            placeManager.snapObject = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("indicator"))
        {
            placeManager.snapObject = null;
            uIManager.indicatorMaterial.material.color = new Color32(255, 255, 255, 255);
            //Indicator Ring Travels back to Indicator
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
