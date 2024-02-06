using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAnimatorHashData
{
    // Params
    public readonly int Parameter_RunGuage = Animator.StringToHash("runGuage");
    public readonly int Parameter_DirX = Animator.StringToHash("dirX");
    public readonly int Parameter_DirY = Animator.StringToHash("dirY");
    
    // States
    public readonly int ANIM_LOCOMOTION = Animator.StringToHash("Locomotion");
    public readonly int ANIM_FALL = Animator.StringToHash("Fall");
    public readonly int ANIM_JUMP = Animator.StringToHash("Jump");
    public readonly int ANIM_LAND = Animator.StringToHash("Land");
    public readonly int ANIM_GRAPPLING_READY = Animator.StringToHash("GrapplingReady");
    public readonly int ANIM_GRAPPLING_MOVE = Animator.StringToHash("GrapplingMove");
    public readonly int ANIM_SWINGING = Animator.StringToHash("Swinging");
    public readonly int ANIM_AIR = Animator.StringToHash("Air");
    public readonly int ANIM_DASH = Animator.StringToHash("Dash");
    public readonly int ANIM_THUNDERCLAPSLASH_READY = Animator.StringToHash("ThunderClapFlashReady");
    public readonly int ANIM_THUNDERCLAPSLASH_ATTACK = Animator.StringToHash("ThunderClapFlashAttack");
    public readonly int[] ANIM_ATTACK = new int[5]{ Animator.StringToHash("Attack_5_1"),
        Animator.StringToHash("Attack_5_2"),
        Animator.StringToHash("Attack_5_3"),
        Animator.StringToHash("Attack_5_4"),
        Animator.StringToHash("Attack_5_5")};
}
