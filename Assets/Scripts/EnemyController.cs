using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;
    private bool isDestroyed = false;
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.OnEnemyDestroy.Invoke();
            LevelManager.instance.IncreaseCurrency(currencyWorth);
            Destroy(gameObject);
            isDestroyed = true;
        }
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
