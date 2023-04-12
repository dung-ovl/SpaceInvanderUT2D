using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : ShipAbstract
{
    [Header("ShipShooting")]
    [SerializeField] protected bool isShooting = true;
    [SerializeField] protected float shootDelay = 0.2f; //attackspeed
    [SerializeField] protected float shootTimer = 0f;
    [SerializeField] protected List<Transform> shipShootPoints;
    [SerializeField] string bulletName = "no-name";

    protected override void ResetValue()
    {
        base.ResetValue();
        this.SetupShootSpeed();  
    }

    protected override void Start()
    {
        base.Start();
        this.LoadShootPoints();
        this.LoadBulletName();
    }

    protected virtual void LoadBulletName()
    {
        this.bulletName = BulletSpawner.Instance.BulletOne;
    }

    private void Update()
    {
        this.Shooting();
        this.OnShootingAnimation();
    }

    private void FixedUpdate()
    {
        this.LoadShootPoints();
    }

    private void LoadShootPoints()
    {
        Transform currentShootPointObj = this.shipController.ShipModel.ShipShootPoint.CurrentShipShootPointObj();
        this.shipShootPoints.Clear();
        foreach (Transform shootPoint in currentShootPointObj)
        {
            this.shipShootPoints.Add(shootPoint);
        }
    }
    
    protected virtual void Shooting()
    {
        if (this.shipShootPoints.Count <= 0)
        {
            this.ShootingWithNoShootPoint();
            return;
        }
        this.ShootingWithShootPoint();
    }

    protected virtual void ShootingWithShootPoint()
    {
        if (!this.isShooting) return;
        shootTimer += Time.deltaTime;
        if (shootTimer < shootDelay) return;
        shootTimer = 0;
        foreach (Transform shootPoint in shipShootPoints)
        {
            Vector3 spawnPos = shootPoint.position;
            Quaternion rotation = shootPoint.rotation;
            Transform newBullet = BulletSpawner.Instance.Spawn(this.bulletName, spawnPos, rotation);
            if (newBullet == null) return;
            newBullet.gameObject.SetActive(true);
            BulletController bulletController = newBullet.GetComponent<BulletController>();
            bulletController.SetShooter(transform.parent);
            bulletController.BulletBouncy.startPos = spawnPos;
            Debug.Log("Shoot");
        }
    }

    protected virtual void ShootingWithNoShootPoint()
    {
        if (!this.isShooting) return;
        shootTimer += Time.deltaTime;
        if (shootTimer < shootDelay) return;
        shootTimer = 0;
        Vector3 spawnPos = transform.position;
        Quaternion rotation = transform.parent.rotation;
        Transform newBullet = BulletSpawner.Instance.Spawn(this.bulletName, spawnPos, rotation);
        if (newBullet == null) return;
        newBullet.gameObject.SetActive(true);
        Debug.Log("Shoot");
    }


    protected virtual void OnShootingAnimation()
    {
        if (!this.isShooting)
        {
            shipController.ShipModel.WeaponAnimator.SetBool("isShooting", false);
            return;
        }
        shipController.ShipModel.WeaponAnimator.SetBool("isShooting", true);
    }

    public virtual void SetupShootSpeed(int speedPercentAdd = 0)
    {
        this.shootDelay = shipController.ShipProfile.attackSpeed * (100f/(100 + speedPercentAdd));
        this.shootTimer = shootDelay;
    }
}
