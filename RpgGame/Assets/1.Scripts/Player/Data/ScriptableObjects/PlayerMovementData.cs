using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Player/Data/Movement")]
public class PlayerMovementData : ScriptableObject
{
    [field:SerializeField] public float MoveSpeed { get; private set; } 
    [field:SerializeField] public float RunMultiplier { get; private set; }
    [field:SerializeField] public float RotationPower { get; private set; } 
    [field:SerializeField] public float MaxSlopeAngle { get; private set; }
}
