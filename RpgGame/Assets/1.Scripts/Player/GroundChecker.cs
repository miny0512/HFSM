using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public LayerMask GroundLayer;
    public Transform Owner;
    public Vector3 Offset;
    public Vector3 BoxSize;
    private Vector3 _halfExtents;
    public bool Debugging;
    public float DefaultDelayTime = 0.2f;
    private WaitForSeconds _wfs;
    private bool _isActive;
    public bool IsGrounded {get { return GroundedCheck(); } }

    private void Awake()
    {
        _isActive = true;
        _wfs = new WaitForSeconds(DefaultDelayTime);
        _halfExtents = BoxSize / 2f;
    }
    public void DelayOnOff()
    {
        StartCoroutine(DelayDisableCoroutine());
    }

    private IEnumerator DelayDisableCoroutine()
    {
        _isActive = false;
        yield return _wfs;
        _isActive = true;

    }

    private bool GroundedCheck()
    {
        if (_isActive == false) return false;

        bool result = Physics.CheckBox(Owner.position + Offset, _halfExtents, Quaternion.identity, GroundLayer);
        return result;
    }

    private void OnDrawGizmos()
    {
        if (Debugging)
        {
            Gizmos.DrawWireCube(Owner.position + Offset, BoxSize);
        }
    }
}
