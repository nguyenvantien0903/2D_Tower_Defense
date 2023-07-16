using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform startPoint;
    public Transform[] path;

    public TextMeshProUGUI messageText;

    public int currency;
    public float displayLogTime = 2f;

    public void IncreaseCurrency(int amount,string msg)
    {
        currency += amount;
        StartCoroutine(DisplayMessage(msg));
    }

    public bool SpendCurrency(int amount,string msg)
    {
        StartCoroutine(DisplayMessage(msg));
        if (amount<=currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
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
        currency = 1000;
        messageText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.y += 100f;
        messageText.transform.position = mousePosition;
    }

    public IEnumerator DisplayMessage(string msg)
    {
        if (msg == string.Empty) yield return 0;
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        // Wait for the specified delay
        yield return new WaitForSeconds(displayLogTime);

        // Disable the messageText object
        messageText.gameObject.SetActive(false);
    }
}
