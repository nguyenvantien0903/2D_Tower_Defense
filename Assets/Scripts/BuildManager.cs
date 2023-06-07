using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int selectedTower = 0;

    private void Awake()
    {
        instance= this;
    }

    public void SetSelctedTower(int selectedTower)
    {
        this.selectedTower= selectedTower;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }
}
