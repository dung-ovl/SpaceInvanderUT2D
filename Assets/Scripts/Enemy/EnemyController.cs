using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GameMonoBehaviour
{
    [SerializeField] protected EnemyDespawn enemyDespawn;
    public EnemyDespawn EnemyDespawn => enemyDespawn;

    [SerializeField] protected EnemyModel enemyModel;
    public EnemyModel EnemyModel => enemyModel;

    [SerializeField] protected EnemyProfileSO enemyProfile;
    public EnemyProfileSO EnemyProfile => enemyProfile;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyDespawn();
        this.LoadEnemyModel();
        this.LoadEnemyProfile();
    }

    private void LoadEnemyProfile()
    {
        if (this.enemyProfile != null) return;
        string resPath = "Enemy/" + transform.name;
        this.enemyProfile = Resources.Load<EnemyProfileSO>(resPath);
        Debug.Log(transform.name + ": LoadEnemyProfile", gameObject);
    }

    private void LoadEnemyModel()
    {
        if (this.enemyModel != null) return;
        this.enemyModel = transform.GetComponentInChildren<EnemyModel>();
        Debug.Log(transform.name + ": LoadEnemyModel", gameObject);
    }

    protected virtual void LoadEnemyDespawn()
    {
        if (this.enemyDespawn != null) return;
        this.enemyDespawn = transform.GetComponentInChildren<EnemyDespawn>();
        Debug.Log(transform.name + ": LoadEnemyDespawn", gameObject);
    }

}
