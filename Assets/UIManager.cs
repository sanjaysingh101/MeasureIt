using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject clearButton;
    public GameObject undoButton;
    public GameObject addButton;
    public TextMeshProUGUI UnitText;
    public GameObject indicatorAnimation;
    public MeshRenderer indicatorMaterial;

    public Color32 snapIndicatorColour;

    private PlaceManager pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.placedLineRenderers.Count == 0)
        {
            undoButton.GetComponent<Button>().interactable = false;
            undoButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,255,69);
        }
        else
        {
            undoButton.GetComponent<Button>().interactable = true;
            undoButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        }
    }
}
