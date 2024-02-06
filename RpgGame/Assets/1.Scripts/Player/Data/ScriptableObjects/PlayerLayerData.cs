using UnityEngine;

[CreateAssetMenu(fileName ="PlayerLayerData", menuName ="Player/Data/Layer")]
public class PlayerLayerData : ScriptableObject
{
    [field: SerializeField] public LayerMask WhatIsGround {get;private set;}
    [field: SerializeField] public LayerMask WhatIsWall {get;private set;}
}
