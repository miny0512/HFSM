using Assets._1.Scripts.Player;
using HFSM;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, PlayerInputAction.IDefaultActions
{
    public HierarchicalStateMachine<Player.RootState, Player.SubState, Player> stateMachine;
    [HeaderColor("References: Transform", ColorType.WHITE, ColorType.GRAY)]
    public Transform ModelTransform;
    public Transform FollowTransform;

    [field: Space(20)]
    [field: HeaderColor("References: Component", ColorType.WHITE, ColorType.GRAY)]
    [field: SerializeField] public ThirdPersonCamera ThirdPersonCamera { get; private set; }
    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }



    public Player Player { get; private set; }
    public PlayerWallChecker WallChecker { get; private set; }
    public CharacterController CharacterController{ get; private set; }
    public AbilityHolder AbilityHolder { get; private set; }
    public Animator Animator { get; private set; }

    private PlayerInputAction _inputAction;


    private void Awake()
    {
        Player = GetComponent<Player>();

        Animator = GetComponent<Animator>();
        
        // Ability
        AbilityHolder = GetComponent<AbilityHolder>();
        
        WallChecker = GetComponent<PlayerWallChecker>();
        // Set Input Action
        _inputAction = new PlayerInputAction();
        _inputAction.Default.SetCallbacks(this);
        CharacterController = GetComponent<CharacterController>();

    }

    private void Start()
    {
        Player.ReuseableData.PlayerHeight = GetComponent<CharacterController>().height;
        stateMachine = new PlayerHStateMachine(Player);
        stateMachine.SetDefaultState();
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }

    private void OnAbilityButtonPressed()
    {
        Player.ReuseableData.IsAbilityButtonPressed = true;
    }

    private void Update()
    {
        HandleRotation(Player.ReuseableData.CurrentMovementInput, Player.ReuseableData.Look);
        stateMachine.UpdateState();
        HandleGravity();
        CharacterController.Move(Player.ReuseableData.ExpectedVelocity * Time.deltaTime);
        Debug.Log(stateMachine.CurrentState?.ToString());
        Debug.Log(stateMachine.CurrentState?.CurrentSubstate?.ToString());
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdateState();
    }

    private void HandleGravity()
    {
        Player.ReuseableData.ExpectedVelocityY = Mathf.Max(Player.ReuseableData.ExpectedVelocityY + Player.ReuseableData.CurrentGravity * Time.deltaTime, -20f);
    }

    public void HandleJump(float velocity)
    {
        Player.ReuseableData.ExpectedVelocityY = velocity;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Player.ReuseableData.IsJumpPressed = context.ReadValueAsButton();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Player.ReuseableData.Look = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var moveInput = context.ReadValue<Vector2>();
        Player.ReuseableData.CurrentMovementInput = moveInput;
        Player.ReuseableData.IsMovementPressed = moveInput.x == 0 && moveInput.y == 0 ? false : true;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        Player.ReuseableData.IsRunPressed = context.ReadValueAsButton();
    }

    private void HandleRotation(Vector2 moveInput, Vector2 lookInput)
    {
        CameraRotate(lookInput);
        CharacterRotate(moveInput);
    }

    private void CharacterRotate(Vector2 input)
    {
        ThirdPersonCamera.CharacterRotate(input, Player.ReuseableData.FreezePlayerRoation);
    }

    private void CameraRotate(Vector2 input)
    {
        ThirdPersonCamera.CameraRotate(input, Player.ReuseableData.FreezeCameraRoation);
    }

    // 착지 후 IsLanding 파라미터 => false
    private void OnLandingEnd()
    {
        Player.ReuseableData.IsLanding = false;
    }

    private void OnAttackEnd()
    {
        Player.ReuseableData.IsAttackEnd = true;
    }
}
