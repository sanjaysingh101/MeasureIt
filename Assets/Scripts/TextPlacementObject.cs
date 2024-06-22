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
    [SerializeField] private Vector3 directionToFace;
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
                distanceText.text = distance.ToString("F2") + " in”";

            }



            // Calculate position as midpoint between the two points
            Vector3 position = (finalpoint + initialpoint) / 2f;
            transform.position = position;

            //Quaternion lookRot = Quaternion.LookRotation(Camera.main.transform.position, transform.right);
            //Quaternion targetRotation = Quaternion.LookRotation(finalpoint - initialpoint, Vector3.up);      
            //directionToFace = new Vector3(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, lookRot.eulerAngles.z);

            transform.LookAt(Camera.main.transform);
        }

    }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, transform.position + transform.right);
            //Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward);
        }

    }
