using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum RootState
    {
        None,
        Grounded,
        Jump,
        Air,
        Ability,
    }
    public enum SubState
    {
        None,
        Idle,
        Walk,
        Run,
    }

    [field: HeaderColor("Datas", ColorType.WHITE, ColorType.GRAY)]
    [field:SerializeField]  public PlayerData Data { get; private set; }
    [field:SerializeField, Readonly] public PlayerReuseableData ReuseableData { get; private set; }

    [HideInInspector] public PlayerController Controller { get; private set; }
    private void Awake()
    {
        Data.Initialize();
        ReuseableData = new PlayerReuseableData();
        Controller = GetComponent<PlayerController>();  
    }

    private void Update()
    {
    }
}
