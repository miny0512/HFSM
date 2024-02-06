using System.Collections;
using UnityEngine;

public enum DamageType
{
    None,
    Physical,
    Magical,
    Absolute,
}

[CreateAssetMenu(fileName = "AbilityData", menuName = "Player/Ability/Data")]
public class AbilityData : ScriptableObject
{
    [field: SerializeField] public Sprite Icon{ get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public KeyCode Key { get; set; }
    [field: SerializeField] public float  Duration { get; private set; }
    [field: SerializeField] public float  CooldownTime { get; private set; }
    [field: SerializeField] public float  Damage { get; private set; }
    [field: SerializeField] public DamageType DamageType { get; private set; }
}
