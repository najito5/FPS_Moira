using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CM_CameraBehaviour : MonoBehaviour
{
    #region Parameters
    new Camera camera;
    protected Transform cameraTransform;
    public Action OnUpdateBehaviour = null;

    [SerializeField] protected CM_CameraSettings cameraSettings;
    [SerializeField] FollowVectorType fAxis;
    public bool IsValid => camera && cameraSettings.CameraTarget;

    public enum FollowVectorType
    {
        up, down, left, right, forward, backward
    }

    protected float x;
    protected float y;
    protected float z;
    #endregion

    public virtual void Init(CM_CameraSettings _cameraSettings, Camera _camera, FollowVectorType _fAxis)
    {
        if (!_camera) return;
        camera = _camera;
        cameraTransform = _camera.transform;
        cameraSettings = _cameraSettings;
        fAxis = _fAxis;
    }

    protected virtual void FollowTarget()
    {
        if (!IsValid) return;
        x = cameraSettings.CameraTarget.position.x;
        y = cameraSettings.CameraTarget.position.y;
        z = cameraSettings.CameraTarget.position.z;
        cameraTransform.position = new Vector3(x, y, z) + cameraSettings.CameraOffSet;
    }

    protected Vector3 GetFollowAxis()
    {
        if (!IsValid) return Vector3.zero;
        switch (fAxis)
        {
            case FollowVectorType.left:
                return -cameraSettings.CameraTarget.right;
            case FollowVectorType.right:
                return cameraSettings.CameraTarget.right;
            case FollowVectorType.up:
                return cameraSettings.CameraTarget.up;
            case FollowVectorType.down:
                return -cameraSettings.CameraTarget.up;
            case FollowVectorType.forward:
                return cameraSettings.CameraTarget.forward;
            case FollowVectorType.backward:
                return -cameraSettings.CameraTarget.forward;
        }
        return Vector3.zero;
    }

    protected virtual void OnDestroy() => OnUpdateBehaviour = null;
}
