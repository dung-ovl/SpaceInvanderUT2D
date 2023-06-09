using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSkillShield : BaseButton
{
    protected override void OnClick()
    {
        if (SliderSkill2.Intance.isCountDown) return;
        Debug.Log("Skill Shield Click");
        SliderSkill2.Intance.StartCountDown();
        ShieldAbility shieldAbility = GameCtrl.Instance.CurrentShip.GetComponentInChildren<ShieldAbility>();
        if (shieldAbility != null)
        {
            shieldAbility.Active();
            Debug.Log("active shild");
        }
        else
        {
            Debug.LogError("Can not get PowerUpAbility");
        }
    }
}
