using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject towerObj;
    private Turret turret;
    private Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        startColor = sr.color;
    }
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.Instance.IsHoveringUI()) return;
        if (towerObj != null)
        {
            turret.OpenUpgradeUI();
            return;
        }
        Tower towerToBuild = BuildManager.instance.GetSelectedTower();
        if(towerToBuild.cost > LevelManager.instance.currency)
        {
            return;
        }
        LevelManager.instance.SpendCurrency(towerToBuild.cost);
        towerObj = Instantiate(towerToBuild.prefab, transform.position,Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
