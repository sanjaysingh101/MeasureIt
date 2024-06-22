using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    Indicator indicator; 
    public float smoothTime = 0.25f;
    public Vector3 rotOffset;
    // Start is called before the first frame update
    void Start()
    {
        indicator = FindObjectOfType<Indicator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(indicator)
        {
            //Vector3 desigredPosition = indicator.transform.position;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position,desigredPosition,smoothTime);

            transform.position = indicator.transform.position;
            transform.localEulerAngles = indicator.transform.localEulerAngles + rotOffset;
        }
        
    }
}
