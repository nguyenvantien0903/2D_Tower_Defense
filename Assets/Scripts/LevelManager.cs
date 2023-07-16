using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform startPoint;
    public Transform[] path;

    [SerializeField] 
    private AudioSource audioSource;
    public TextMeshProUGUI messageText;
    public GameObject gameOverUI;
    public int currency;
    public float displayLogTime = 2f;

    public int maxHealth = 5; // Maximum health value
    public int currentHealth; // Current health value
    public int waves=0;

    private bool isGameEnded;

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
        isGameEnded = false;
        currency = 1000;
        currentHealth = maxHealth;
        messageText.gameObject.SetActive(false);
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnded) return;
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

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            isGameEnded = true;
            // Game over or other necessary actions when health reaches zero
            // You can add code here to handle game over logic, like reloading the level or showing a game over screen
            gameOverUI.SetActive(true);
        }
    }
}
