using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DashAbility : AbilityBase
{
    public ParticleSystem ParticleEffect;
    public Material DashMaterial;
    private Dictionary<SkinnedMeshRenderer, Material[]> originMat;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;

    private void Awake()
    {

        originMat = new();
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var i in skinnedMeshRenderers)
        {
            var mats = i.materials;
            originMat.Add(i, mats);
        }

   
        InitializeAbilityParameter();
    }

    public override void InitializeAbilityParameter()
    {

    }

    public override void Activate()
    {
        if (isPerformed == true) return;

        duration -= Time.deltaTime;
        float durationRatio = (Data.Duration - duration) / Data.Duration;
        Debug.Log(durationRatio);
        ca.intensity.value = Mathf.Lerp(0, 0.5f, durationRatio);
        ld.intensity.value = Mathf.Lerp(0, -0.5f, durationRatio);
        float sin = Mathf.Sin(Mathf.Lerp(0, Mathf.PI, durationRatio));
        // 0 -> 2  2->0
        // sin : 0 ~ 1
        Debug.Log($"Sin : {sin}");
        Debug.Log($"SetFloat : {Mathf.Lerp(0, 2, sin)}");
        // DashMaterial.SetFloat("_RimWidth", Mathf.Lerp(0, 2, sin));
        DashMaterial.SetFloat("_RimWidth", Mathf.Lerp(2, 0, durationRatio));

        Owner.ReuseableData.ExpectedVelocityY = 0f;
        Owner.ReuseableData.ExpectedVelocityX += direction.x * Time.deltaTime * 300f;
        Owner.ReuseableData.ExpectedVelocityZ += direction.z * Time.deltaTime * 300f;

        if (duration <= 0f) isPerformed = true;
    }

    Vector3 direction;
    LensDistortion ld;
    ChromaticAberration ca;
    public override void Enter()
    {
        // Materai Change
        foreach(var i in skinnedMeshRenderers)
        {
            var materials = i.materials;
            for(int mat = 0; mat < materials.Length; mat++)
            {
                materials[mat] = DashMaterial;
            }
            i.materials = materials;
        }

        VolumeProfile vp = Camera.main.GetComponent<Volume>().profile;
        vp.TryGet<LensDistortion>(out ld);
        vp.TryGet<ChromaticAberration>(out ca);

        Owner.Controller.Animator.Play(Owner.Data.Hash.ANIM_DASH);
        isPerformed = false;
        cooldownElapedTime = Data.CooldownTime;
        duration = Data.Duration;
        StartCoroutine(CooldownTimer());
        if(Owner.ReuseableData.IsMovementPressed == false)
        {
            direction = Owner.Controller.ModelTransform.forward;
            //Owner.ReuseableData.ExpectedVelocityX = direction.x * 40f;
            //Owner.ReuseableData.ExpectedVelocityZ = direction.z * 40f;

        }
        else
        {
            direction = Owner.ReuseableData.CurrentMovementInput.x * Owner.Controller.ModelTransform.right + Owner.ReuseableData.CurrentMovementInput.y * Owner.Controller.ModelTransform.forward;
            //Owner.ReuseableData.ExpectedVelocityX = direction.x * 40f;
            //Owner.ReuseableData.ExpectedVelocityZ = direction.z * 40f;
        }
    }

    public override void Exit()
    {
        ParticleEffect.Play();
        // Materai Change
        // Materai Change
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            skinnedMeshRenderers[i].materials = originMat[skinnedMeshRenderers[i]];
        }
        duration = 0;
        ca.intensity.value = 0;
        ld.intensity.value = 0;
        isPerformed = false;
    }

}
