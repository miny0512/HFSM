using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName="Player/Data/Master")]
public class PlayerData : ScriptableObject
{
    [field:HeaderColor("Grounded", ColorType.WHITE, ColorType.GRAY)]
    [field: SerializeField] public PlayerMovementData Movement { get; private set; }

    [field: Space(10),HeaderColor("Air", ColorType.WHITE, ColorType.GRAY)]
    [field: SerializeField] public PlayerJumpData Jump { get; private set; }
    [field: SerializeField] public PlayerFallData Fall{ get; private set; }
    [field: SerializeField] public PlayerLandData Land { get; private set; }
    [field: SerializeField] public PlayerGravityData Gravity { get; private set; }

    [field: Space(10), HeaderColor("ETC", ColorType.WHITE, ColorType.GRAY)]
    [field: SerializeField] public PlayerLayerData Layer { get; private set; }
    [field: SerializeField] public PlayerAnimatorHashData Hash{ get; private set; }

    public void Initialize()
    {
        Hash = new PlayerAnimatorHashData();
    }
}
