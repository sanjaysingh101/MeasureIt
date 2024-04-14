using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PermissionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if(PlayerPrefs.GetInt("First")==1)
        {
            SceneManager.LoadScene("MeasureIt");
        }
    }
    public void ChangeScene()
    {
        PlayerPrefs.SetInt("First", 1);
        SceneManager.LoadScene("MeasureIt");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
