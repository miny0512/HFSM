using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderClapFlashAbility : AbilityBase
{
    [SerializeField] Queue<Transform> targetQ;

    Collider[] overlapColliders;
    PlayerController playerController;
    private void Awake()
    {
        InitializeAbilityParameter();
    }

    public override void InitializeAbilityParameter()
    {
        playerController = Owner.GetComponent<PlayerController>();
        overlapColliders = new Collider[20];
        targetQ = new Queue<Transform>();
    }

    public void FindTarget(float Range = 10f)
    {
        int result = Physics.OverlapSphereNonAlloc(Owner.transform.position, Range, overlapColliders, WhatIsEnemy);
        if (result == 0) return;

        Transform target = null;
        var forawrd = playerController.ModelTransform.forward;
        float min = float.MaxValue;
        float angle = 140f * 0.5f;
        for (int i = 0; i < result; i++)
        {
            float distance = Vector3.Distance(overlapColliders[i].transform.position, Owner.transform.position);
            if (distance < 2f) continue;
            var dir = overlapColliders[i].transform.position - Owner.transform.position;
            dir = dir.normalized;
            dir.y = 0;
            float targetAngle = Mathf.Abs(Mathf.Acos(Vector3.Dot(forawrd, dir)) * Mathf.Rad2Deg);
            if (targetAngle < angle && targetAngle < min)
            {
                min = targetAngle;
                target = overlapColliders[i].transform;
            }
            Debug.Log(targetAngle);
        }

        if (target != null)
            targetQ.Enqueue(target);
    }


    public override void Enter()
    {
        isPerformed = false;
        StartCoroutine(CooldownTimer());
        StartCoroutine(CoActivate());
    }

    public override IEnumerator CoActivate()
    {
        FindTarget(100F);

        if (targetQ.Count == 0)
        {
            isPerformed = true;
            yield break;
        }

        // Rotate To Enemy & Charge Energy
        var target = targetQ.Dequeue();

        yield return StartCoroutine(TP(target));
        Debug.Log("이동 완료");
        // Damaged
        // End
        isPerformed = true;
    }

    IEnumerator TP(Transform target)
    {
        playerController.Animator.CrossFade(playerController.Player.Data.Hash.ANIM_THUNDERCLAPSLASH_READY, 0.1f);
        yield return new WaitForSeconds(0.5f);
        playerController.Animator.CrossFade(playerController.Player.Data.Hash.ANIM_THUNDERCLAPSLASH_ATTACK, 0.1f);
        var targetDir = (target.position - Owner.transform.position).normalized;
        transform.position = target.position;
        yield return new WaitForSeconds(0.5f);
    }

    public override void Exit()
    {
        isPerformed = false;
        targetQ.Clear();
    }

    //public Vector3 EaseInOutExpo(Vector3 start, Vector3 end, float time, float duration)
    //{
    //    float t = EaseInOutExpoHelper(time, duration);
    //    return Vector3.Lerp(start, end, t);
    //}

    //private float EaseInOutExpoHelper(float time, float duration)
    //{
    //    if (time == 0f)
    //    {
    //        return 0f;
    //    }

    //    if (time == duration)
    //    {
    //        return 1f;
    //    }

    //    return ((time /= duration * 0.5f) < 1f) ? (0.5f * (float)Math.Pow(2.0, 10f * (time - 1f))) : (0.5f * (0f - (float)Math.Pow(2.0, -10f * (time -= 1f)) + 2f));
    //}
}
