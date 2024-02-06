using UnityEngine;

[CreateAssetMenu(fileName ="Fall",menuName ="Player/Data/Fall")]
public class PlayerFallData : ScriptableObject
{
    [field: SerializeField] public float FallMultiplier { get; private set; }
}
