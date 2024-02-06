using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [field: SerializeField] public Player Owner { get; set; }
    [field: SerializeField] public AbilityData Data { get; set; }
    [field: SerializeField] public LayerMask WhatIsEnemy { get; set; }

    protected float cooldownElapedTime;
    protected float duration;

    protected bool isPerformed;
    public bool IsPerformed => isPerformed;
    public abstract void InitializeAbilityParameter();
    public abstract void Enter();
    public abstract void Exit();
    public virtual void Activate() { }
    // 코루틴으로 실행할 액션
    public virtual IEnumerator CoActivate() { return null; }

    public float CurrentCooldownRatio => 1f - (cooldownElapedTime / Data.CooldownTime);
    public virtual bool CanUseAbility() => cooldownElapedTime == 0f;

    public IEnumerator CooldownTimer()
    {
        cooldownElapedTime = Data.CooldownTime;
        while (cooldownElapedTime >= 0f)
        {
            cooldownElapedTime -= Time.deltaTime;
            yield return null;
        }
        cooldownElapedTime = 0f;
    }

}
