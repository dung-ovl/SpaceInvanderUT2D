using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Wall : GameMonoBehaviour
{
    [Header("LeftWall")]
    [SerializeField] protected BoxCollider2D boxCollider;
    [SerializeField] protected Rigidbody2D rigidbody2D;
    [SerializeField] protected float offsetPos = 0.1f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }

    protected void SetPosition(float x, float y)
    {
        this.transform.position = new Vector3(x, y, 0);
    }

    protected virtual void LoadCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider2D>();
        Debug.Log(transform.name + " LoadCollider", gameObject);
    }

    protected virtual void LoadRigid()
    {
        if (this.rigidbody2D != null) return;
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        Debug.Log(transform.name + " LoadRigid", gameObject);
    }
}
