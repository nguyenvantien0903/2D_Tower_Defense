using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiveUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Health :" + LevelManager.instance.currentHealth.ToString();
    }
}
