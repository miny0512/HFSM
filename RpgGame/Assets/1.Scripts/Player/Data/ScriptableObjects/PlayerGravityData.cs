using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName ="Gravity", menuName ="Player/Data/Gravity")]
public class PlayerGravityData : ScriptableObject
{
    [field: SerializeField] public float Grounded { get; private set; }
    [field: SerializeField] public float Air { get; private set; }
    [field: SerializeField] public float Fall { get; private set; }
}
