using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName ="Player/Data/Camera/Camera")]
public class PlayerCameraData : ScriptableObject
{
    [field: SerializeField] public ThirdPersonCamera.CameraStyle DefaultCameraStyle { get; private set; }
    [field: SerializeField] public float TopClamp { get; private set; }
    [field: SerializeField] public float BottomClamp { get; private set; }
    [field: SerializeField] public float NormalSensitivity { get; private set; }
    [field: SerializeField] public float AimSensitivity { get; private set; }    
}
