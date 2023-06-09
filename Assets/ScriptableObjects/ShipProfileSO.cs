using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "ScriptableObject/Ship")]

public class ShipProfileSO : ScriptableObject
{
    public string shipName = "Ship";
    public float maxHeath = 10f;
    public float shieldTimeUp = 0f;
    public float powerTimeUp = 0f;


    public float mainDamage = 1f;
    public float mainAttackSpeed = 0.2f;
    public List<ShipPointLevelInfo> mainBulletList;
    public float subDamage = 1f;
    public float subAttackSpeed = 0.2f;
    public List<ShipPointLevelInfo> subBulletList;

    public float countDownSkill1 = 10f;
    public float countDownSkill2 = 10f;

}
