using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSeparate : BulletAbstract
{
    public float timeCount;

    [SerializeField] protected int timesSeparation;
    public int TimesSeparation => timesSeparation;

    [SerializeField] protected int quantityOfEachTimes;
    public int QuantityOfEachTimes => quantityOfEachTimes;

    [SerializeField] protected int baseQuantity = 2;
    public int BaseQuantity => baseQuantity;

    [SerializeField] protected float timeWait;
    public float TimeWait => timeWait;

    [SerializeField] protected float angleSeparation;
    public float AngleSeparation => angleSeparation;

    [SerializeField] protected bool isSeparating = false;
    public bool IsSeparating => isSeparating;  

    protected override void OnEnable()
    {
        base.OnEnable();
        this.timeWait = 0.2f;
        this.quantityOfEachTimes = this.baseQuantity;
        this.timesSeparation = 2;
        this.angleSeparation = 10;
        this.timeCount = 0;
    }

    protected virtual void FixedUpdate()
    {
        if (!this.isSeparating) return;
        if (timesSeparation >= 1)
        {
            this.BulletController.isSendDamage = false;
            Separate();
        }
    }

    protected virtual void Separate()
    {
        this.timeCount += Time.fixedDeltaTime;
        if (this.timeCount < this.timeWait) return;
        this.CreateImpactFX();
        float tempAngle = Mathf.Abs(angleSeparation) * 2 / (quantityOfEachTimes - 1);
        float angle = angleSeparation;
        for (int i = 0; i < quantityOfEachTimes; i++)
        {
            Vector3 rot = transform.parent.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y, rot.z + angle);
            Debug.Log(rot);
            Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.Instance.BulletOne, transform.position, Quaternion.Euler(rot));
            if (newBullet == null) return;
            newBullet.gameObject.SetActive(true);
            BulletController bulletController = newBullet.GetComponent<BulletController>();
            bulletController.BulletPower.timesSeparation = timesSeparation - 1;
            bulletController.BulletPower.isSeparating = true;
            bulletController.SetShooter(GameCtrl.Instance.CurrentShip);
            bulletController.BulletBouncy.startPos = transform.parent.position;
            Debug.Log("Separate " + i);
            angle -= tempAngle;
        }
        this.bulletController.BulletDespawn.DespawnObject();
    }

    protected virtual void CreateImpactFX()
    {
        string fxImpactName = GetImpactFXName();
        Quaternion hitRot = transform.rotation;
        Transform newFxImpact = FXSpawner.Instance.Spawn(fxImpactName, transform.position, hitRot);
        if (newFxImpact == null) return;
        newFxImpact.gameObject.SetActive(true);
    }

    protected virtual string GetImpactFXName()
    {
        return FXSpawner.Instance.Impact1;
    }
}