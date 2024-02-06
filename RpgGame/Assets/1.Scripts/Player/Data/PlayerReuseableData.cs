using Assets._1.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class PlayerReuseableData
{
    public PlayerReuseableData() { Initialize(); }

    [field: SerializeField] public float PlayerHeight { get; set; }
    [field: SerializeField] public bool FreezeXZMovement { get; set; }
    [field: SerializeField] public bool FreezeCameraRoation { get; set; }
    [field: SerializeField] public bool FreezePlayerRoation { get; set; }

    [field: SerializeField] public float CurrentGravity{ get; set; }
    [SerializeField] private Vector3 expectedVelocity;
    public Vector3 ExpectedVelocity { get { return expectedVelocity; } set { expectedVelocity = value; } }
    public float ExpectedVelocityX { get { return expectedVelocity.x;} set{ expectedVelocity.x = value;} }
    public float ExpectedVelocityY { get { return expectedVelocity.y;} set{ expectedVelocity.y = value;} }
    public float ExpectedVelocityZ { get { return expectedVelocity.z;} set{ expectedVelocity.z = value;} }

    [field: SerializeField] public Vector2 CurrentMovementInput { get; set; }
    [field: SerializeField] public Vector3 CurrentMovementVector { get; set; }
    [field: SerializeField] public Vector2 Look { get; set; }
    [field: SerializeField] public float CurrentMoveSpeed { get; set; }
    [field: SerializeField] public bool IsRunPressed { get; set; }
    [field: SerializeField] public bool IsFlyingMode { get; set; }
    [field: SerializeField] public bool IsMovementPressed { get; set; }
    [field: SerializeField] public int CurrentAttackCombo { get; set; }
    [field: SerializeField] public bool IsAttackPressed { get; set; }
    [field: SerializeField] public bool IsAttackEnd { get; set; }
    [field: SerializeField] public bool OnSlope { get; set; }
    [field: SerializeField] public RaycastHit SlopeHit { get; set; }
    [field: SerializeField] public bool IsJumpPressed { get;  set; }
    [field: SerializeField] public bool IsJumpAnimating { get; set; }
    [field: SerializeField] public bool IsAir { get; set; }
    [field: SerializeField] public bool IsLanding { get; set; }
    [field: SerializeField] public bool IsJumping { get; set; }
    [field: SerializeField] public bool IsFalling { get; set; }
    [field: SerializeField] public bool IsGrounded { get; set; }

    [field: SerializeField] public bool IsAbilityButtonPressed { get; set; }
    [field: SerializeField] public bool IsActiveAbility { get; set; }

    [field: SerializeField] public bool IsWallCollided { get; set; }

    public void Initialize()
    {
        IsAttackEnd = true;
    }
}
