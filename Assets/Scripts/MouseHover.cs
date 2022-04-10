using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseHover : MonoBehaviour
{
    private TextMeshProUGUI text;
    private GameObject textGameObject;
    // Start is called before the first frame update
    void Start()
    {
        textGameObject = this.gameObject.transform.GetChild(0).gameObject;
        text = textGameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Open()
    {
   
        text.color = Color.black;

    }

    public void Close()
    {
        text.color = Color.white;
    }
}
