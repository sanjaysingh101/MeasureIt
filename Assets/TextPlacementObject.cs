using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class TextPlacementObject : MonoBehaviour
{
    public Vector3 initialpoint;
    public Vector3 finalpoint;
    public float distance;
    public TextMeshPro distanceText;
    public LineRenderer LR;
    public float distFromCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distFromCamera = Vector3.Distance(this.transform.position,Camera.main.transform.position);
        transform.localScale =  new Vector3(0.1f+ distFromCamera, 0.1f + distFromCamera, 0.1f + distFromCamera);
        if (LR.positionCount>1)
        {
            initialpoint = LR.GetPosition(0);
            finalpoint = LR.GetPosition(1);
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        if (finalpoint!=null && initialpoint!=null)
        {
            if(PlaceManager.isCm)
            {
                distance = 100*((finalpoint - initialpoint).magnitude);
                distanceText.text = distance.ToString("F2") + " cm";

            }
            else
            {
                distance = 39.370f*((finalpoint - initialpoint).magnitude);
                distanceText.text = distance.ToString("F2") + " inch";

            }

            transform.position =  new Vector3((finalpoint.x + initialpoint.x)/2,(finalpoint.y + initialpoint.y)/2,(finalpoint.z + initialpoint.z)/2);
            transform.LookAt(Camera.main.transform);
        }
        
    }
}
