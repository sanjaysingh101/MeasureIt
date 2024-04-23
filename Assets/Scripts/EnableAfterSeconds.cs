using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterSeconds : MonoBehaviour
{
    public float time = 2.5f;
    public GameObject objectToEnable;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndEnable());
    }
    IEnumerator WaitAndEnable()
    {
        yield return new WaitForSeconds(time);
        objectToEnable.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
