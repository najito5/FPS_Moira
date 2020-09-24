using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_CameraFPSBehaviour : CM_CameraBehaviour
{
    public override void Init(CM_CameraSettings _cameraSettings, Camera _camera, FollowVectorType _fAxis)
    {
        base.Init(_cameraSettings, _camera, _fAxis);
        OnUpdateBehaviour += FollowTarget;
        OnUpdateBehaviour += SetTargetRotation;
        IM_InputManager.OnMouseAxis += SetCameraRotation;
    }

    protected override void FollowTarget() => base.FollowTarget();

    void SetCameraRotation(float _xAxis, float _yAxis) => cameraTransform.eulerAngles += new Vector3(-_yAxis, _xAxis, 0);

    void SetTargetRotation()
    {
        if (!IsValid) return;
        cameraSettings.CameraTarget.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    protected override void OnDestroy() => base.OnDestroy();
}
