using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5.0f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float bps = 1f;//Bullet per second
    [SerializeField] private int baseUpgradeCost = 100;
    [SerializeField] private float upgradeScalingFactor = 0.8f;

    private Transform target;
    private float timeUntilFire;

    private float bpsBase;
    private float targetingRangeBase;
    private int level = 1;
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position,transform.forward, targetingRange);
    }
    // Start is called before the first frame update
    void Start()
    {
        bpsBase = bps;
        targetingRangeBase = targetingRange;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();
        if (!IsTargetInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;
            if(timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab,firingPoint.position,Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private bool IsTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y-transform.position.y,target.position.x-transform.position.x)*Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,targetRotation,rotationSpeed*Time.deltaTime);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if(hits.Length > 0 )
        {
            target = hits[0].transform;
        }
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.Instance.SetHoveringState(false);
    }

    public void Upgrade()
    {
        int upgradeCost = CalculateUpgradeCost();
        if (upgradeCost > LevelManager.instance.currency)
        {
            LevelManager.instance.SpendCurrency(upgradeCost, "Not enough money");
        }
        else
        {
            LevelManager.instance.SpendCurrency(upgradeCost, "Upgrade successfully");
        }
        level++;
        bps=CalculateUpgradeBPS();
        targetingRange=CalculateUpgradeRange();
        CloseUpgradeUI();
    }

    public void Sell()
    {
        int upgradeCost = CalculateUpgradeCost();
        LevelManager.instance.IncreaseCurrency(upgradeCost/2, "Sell successfully");
        Destroy(gameObject);
        CloseUpgradeUI();
    }

    private int CalculateUpgradeCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, upgradeScalingFactor));
    }

    private float CalculateUpgradeBPS()
    {
        return bpsBase*Mathf.Pow(level, upgradeScalingFactor);
    }

    private float CalculateUpgradeRange()
    {
        return targetingRangeBase * Mathf.Pow(level, upgradeScalingFactor);
    }
}
