using UnityEngine;

[CreateAssetMenu(fileName = "LandingData", menuName ="Player/Data/Land")]
public class PlayerLandData : ScriptableObject
{
    [field: SerializeField] public float LandingHeight { get; private set; }
}
