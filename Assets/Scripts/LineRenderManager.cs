using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<Transform> Points;
    public GameObject objectToPlace;
    public Indicator indicator;


    // Start is called before the first frame update
    void Start()
    {
        indicator = FindObjectOfType<Indicator>();
    }

    public void AddPoints(Transform newPointPosition)
    {
        GameObject newPoint = Instantiate(objectToPlace, newPointPosition.position, newPointPosition.rotation);
        newPoint.transform.parent = this.gameObject.transform;
        Points.Add(newPoint.transform);
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, newPointPosition.position);
    }

    // Update is called once per frame
    
    void Update()
    {
        if (lineRenderer.positionCount > 0)
        {
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                lineRenderer.SetPosition(i, Points[i].position);
            }
        }

    }
}
