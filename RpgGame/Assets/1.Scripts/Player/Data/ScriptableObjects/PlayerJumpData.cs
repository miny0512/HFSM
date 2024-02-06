using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable,CreateAssetMenu(fileName = "JumpData", menuName = "Player/Data/Jump")]
public class PlayerJumpData : ScriptableObject
{
    [field:SerializeField] public float JumpHeight { get; private set; }
    [field:SerializeField] public float MinMoveSpeed { get; private set; }
    [field:SerializeField] public float MaxMoveSpeed { get; private set; }
}
