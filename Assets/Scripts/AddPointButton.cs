using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddPointButton : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler
{
    bool pressing = false;
    [SerializeField]
    [Tooltip("How long must pointer be down on this object to trigger a long press")]
    private float holdTime = 1f; 
    public bool longPress=false;
    float timer; 
    PlaceManager pm;
    private void Start() {
        pm = FindObjectOfType<PlaceManager>();
    }
    public void OnPointerDown (PointerEventData eventData)
    {
        pressing = true;
    }
   
   
    public void OnPointerUp (PointerEventData eventData)
    {
        if(!longPress)
        {
            pm.ClickToPlace();
        }
        pressing = false;
    }
 
    void Update()
    {
        if ( pressing )
        {
            // do continuous stuff here
            timer+=Time.deltaTime;
            if(timer>holdTime)
            {
                longPress= true;
            }
        }
        else
        {
            longPress=false;
            timer = 0;
        }
    }

    
}
