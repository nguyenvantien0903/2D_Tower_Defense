using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform startPoint;
    public Transform[] path;

    public int currency;

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if(amount<=currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough money");
            return false;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currency = 1500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
