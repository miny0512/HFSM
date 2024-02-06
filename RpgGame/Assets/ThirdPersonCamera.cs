using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class ThirdPersonCamera : MonoBehaviour
{
    public enum CameraStyle
    {
        Basic,
        Aim,
    }

    [HeaderColor("Prefabs", ColorType.WHITE, ColorType.GRAY)]
    public CinemachineVirtualCamera BasicVCamPrefab;
    public CinemachineVirtualCamera AimVCamPrefab;

    public CinemachineVirtualCamera basicVcam;
    public CinemachineVirtualCamera aimVcam;
    CinemachineVirtualCamera currentCam;

    [field: SerializeField] public PlayerCameraData CameraData { get; private set; }
    public CameraStyle cameraStyle;
    public bool LockCameraPosition { get; set; }

    public float Sensitivity;
    private float yaw;
    private float pitch;
    private Vector2 _look;
    public CameraStyle CurrentCamStyle => _currentCamStyle;
    public LayerMask whatIsAimRaycastable;
    [SerializeField] private float _threshold;
    [SerializeField] private CameraStyle _currentCamStyle;
    public GameObject FollowTarget;
    public GameObject PlayerObject;

    private void Awake()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        SwitchCameraStyle(_currentCamStyle);
    }

    public void SwitchCameraStyle(CameraStyle style)
    {
        basicVcam.gameObject.SetActive(false);
        aimVcam.gameObject.SetActive(false);

        if(style == CameraStyle.Basic)
        {
            basicVcam.gameObject.SetActive(true);
            currentCam = basicVcam;
            Sensitivity = CameraData.NormalSensitivity;
        }
        else if(style == CameraStyle.Aim)
        {
            aimVcam.gameObject.SetActive(true);
            currentCam = aimVcam;
            Sensitivity = CameraData.AimSensitivity;
        }
        _currentCamStyle = style;
    }

    private Vector3 currentVelocity;
    public void CharacterRotate(Vector2 input, bool isFreeze)
    {
        if (isFreeze) return;
        var f = FollowTarget.transform.forward;
        f.y = 0f;
        PlayerObject.transform.forward = Vector3.SmoothDamp(PlayerObject.transform.forward, f,ref currentVelocity, 0.05f);
    }

    public bool GetAimRaycastHit(out RaycastHit raycastHit, float maxDistance, LayerMask mask)
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out raycastHit, maxDistance, mask))
        {
            return true;
        }
        return false;
    }

    public void CameraRotate(Vector2 input, bool isFreeze)
    {
        if (isFreeze) return;
        if (input.sqrMagnitude >= _threshold)
        {
            yaw += input.x * Time.deltaTime * Sensitivity;
            pitch += input.y * Time.deltaTime * Sensitivity;
        }
        yaw = ClampAngle(yaw, float.MinValue, float.MaxValue);
        pitch = ClampAngle(pitch, CameraData.BottomClamp, CameraData.TopClamp);

        FollowTarget.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }


    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
