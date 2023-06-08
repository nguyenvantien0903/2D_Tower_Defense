using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private bool isHoveringUI;

    private void Awake()
    {
        Instance= this;
    }

    public void SetHoveringState(bool state)
    {
        isHoveringUI= state;
    }

    public bool IsHoveringUI()
    {
        return isHoveringUI;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
