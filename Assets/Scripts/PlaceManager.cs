using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    [Serializable]
    public class PlacedLineRenderer
    {
        public GameObject LineRenderer;
        public List<Transform> Points;
    }
    public GameObject inconpleteLR;
    public LineRenderer incompleteLRObject;
    public Indicator indicator;
    public GameObject LineRendererPrefab;
    public List<PlacedLineRenderer> placedLineRenderers;
    public GameObject snapObject=null;
    UIManager uIManager;
    public AddPointButton apb;
    public bool testBool=false;

    public static bool isCm = true;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        Vibration.Init();
    }
    public void ClickToPlace()
    {
        Vibration.VibratePop();
        FindObjectOfType<AudioManager>().Play("Place");

        if (inconpleteLR==null)
        {
            PlacedLineRenderer placedLR = new PlacedLineRenderer();
            placedLR.LineRenderer = Instantiate(LineRendererPrefab, indicator.transform.GetChild(0).transform.position, indicator.transform.GetChild(0).transform.rotation);
            inconpleteLR = placedLR.LineRenderer;
            if(snapObject!=null)
            {
                placedLR.LineRenderer.GetComponent<LineRenderManager>().AddPoints(snapObject.transform);

            }
            else
            {
                placedLR.LineRenderer.GetComponent<LineRenderManager>().AddPoints(indicator.transform);         
            }
            placedLR.Points = placedLR.LineRenderer.GetComponent<LineRenderManager>().Points;
            placedLineRenderers.Add(placedLR);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("Completing the previous one");
            if (snapObject != null)
            {
                inconpleteLR.GetComponent<LineRenderManager>().AddPoints(snapObject.transform);

            }
            else
            {
                inconpleteLR.GetComponent<LineRenderManager>().AddPoints(indicator.transform);
            }
            inconpleteLR = null;
        }
        
    }

    public void SwitchUnit()
    {
        if(isCm)
        {
            //switchToInch
            uIManager.UnitText.text = "inch";
            isCm = false;
        }
        else
        {
            //switchToCm
            uIManager.UnitText.text = "cm";
            isCm = true;
        }
        FindObjectOfType<AudioManager>().Play("SwitchUnit");
    }
    public void Clear()
    {
        Vibration.VibratePop();
        FindObjectOfType<AudioManager>().Play("UIClick");

        for (int i=0;i<placedLineRenderers.Count;i++)
        {
            Destroy(placedLineRenderers[i].LineRenderer);
        }
        if(inconpleteLR!=null)
        {
            Destroy(inconpleteLR);
            inconpleteLR = null;
        }
        placedLineRenderers.Clear();
        uIManager.clearButton.SetActive(false);
    }

    public void Undo()
    {
        
         Vibration.VibrateNope();
      
          FindObjectOfType<AudioManager>().Play("Undo");


         if (inconpleteLR == null) //means latest Linerender is completed and need to remove one point from this
         {
                Destroy(placedLineRenderers[placedLineRenderers.Count - 1].Points[1].gameObject);
                placedLineRenderers[placedLineRenderers.Count - 1].Points.RemoveAt(1);
                placedLineRenderers[placedLineRenderers.Count - 1].LineRenderer.GetComponent<LineRenderer>().positionCount -= 1;
                inconpleteLR = placedLineRenderers[placedLineRenderers.Count - 1].LineRenderer;
         }
         else // entire line renderer needed to be removed
         {
                Destroy(placedLineRenderers[placedLineRenderers.Count - 1].LineRenderer);
                placedLineRenderers.RemoveAt(placedLineRenderers.Count - 1);
                inconpleteLR = null;
         }
    }
   
    private void Update() 
    {
        if(inconpleteLR!=null)
        {
            incompleteLRObject.enabled = true;
            incompleteLRObject.SetPosition(0, inconpleteLR.GetComponent<LineRenderManager>().Points[inconpleteLR.GetComponent<LineRenderManager>().Points.Count-1].transform.position);
            incompleteLRObject.SetPosition(1, indicator.transform.position);
        }
        else
        {
            incompleteLRObject.enabled = false;
        }

        if(placedLineRenderers.Count==0)
        {
            uIManager.clearButton.SetActive(false);
        }
        else
        {
            uIManager.clearButton.SetActive(true);
        }


        if(snapObject!=null)
        {
            if(apb.longPress)
            {
                snapObject.transform.parent.position = uIManager.indicatorAnimation.transform.parent.position;
            }
            else
            {
                uIManager.indicatorAnimation.transform.position = snapObject.transform.position;
            }
        }
        else
        {
            uIManager.indicatorAnimation.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
