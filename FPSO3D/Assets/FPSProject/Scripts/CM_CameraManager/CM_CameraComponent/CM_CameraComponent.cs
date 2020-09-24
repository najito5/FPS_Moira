using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CM_CameraComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField, Header("Camera ID name :")] string id = "mainCam";
    [SerializeField, Header("Camera Type")] CM_CameraType camType = CM_CameraType.FPS;
    [SerializeField, Header("Camera Settings")] CM_CameraSettings cameraSettings;

    new Camera camera;
    CM_CameraBehaviour cameraBehaviour;
    public enum CM_CameraType
    {
        FPS
    }
    #endregion

    #region Getter/Setter
    public Camera GetCamera => camera;
    public string ID => id;
    public bool IsValid => camera;
    public bool IsEnabled { get; set; } = true;
    public event Action OnregisterCamera = null;

    public void SetTarget(Transform _target) => cameraSettings.CameraTarget = _target;
    public void SetCameraType(CM_CameraType _camType) => camType = _camType;
    public void SetSettings(CM_CameraSettings _settings) => cameraSettings = _settings;
    #endregion

    public CM_CameraBehaviour Behaviour
    {
        get
        {
            if (!cameraBehaviour)
                Debug.Log("error");
            return cameraBehaviour;
        }
    }

    void InitBehaviourComponent()
    {
        CM_CameraBehaviour.FollowVectorType _fAxis = CM_CameraBehaviour.FollowVectorType.up;
        switch (camType)
        {
            case CM_CameraType.FPS: cameraBehaviour = gameObject.AddComponent<CM_CameraFPSBehaviour>(); break;
        }
        if (cameraBehaviour)
        {
            cameraBehaviour.Init(cameraSettings, camera, _fAxis);
            OnregisterCamera?.Invoke();
            OnregisterCamera = null;
        }
    }

    private void Start() => RegisterCamera();

    void RegisterCamera()
    {
        if (!CM_CameraManager.Instance) return;
        if (!camera)
        {
            camera = GetComponent<Camera>();
            if (!camera) Debug.Log("error404");
        }
        CM_CameraManager.Instance.AddCamera(this);
        name += "[CM]";
        InitBehaviourComponent();
    }

    void UnRegisterCamera()
    {
        if (!CM_CameraManager.Instance) return;
        CM_CameraManager.Instance.RemoveCamera(this);
    }

    public void SetViewActive(bool _active)
    {
        if (!IsValid) return;
        camera.enabled = _active;
    }

    /*
    [SerializeField, Header("Clamp Axis X")] bool clampAxisX = false;
    [SerializeField, Header("Clamp Axis Y")] bool clampAxisY = false;
    [SerializeField, Header("Value clamp Axis X"),Range(0,180)] int valueClampAxisX = 0;
    [SerializeField, Header("Value clamp Axis Y"), Range(0, 180)] int valueClampAxisY = 0;
    void ClampValue()
    {
        Vector3 eulerRotation = transform.eulerAngles;

        if (clampAxisX)
        {
            if (eulerRotation.x >= 0 && eulerRotation.x <= 180) eulerRotation.x = Mathf.Clamp(transform.eulerAngles.x, 0.0f, valueClampAxisX);
            else
                eulerRotation.x = Mathf.Clamp(transform.eulerAngles.x, (360 - valueClampAxisX), 360);
        }
        if (clampAxisY)
        {
            if (eulerRotation.y >= 0 && eulerRotation.y <= 180) eulerRotation.y = Mathf.Clamp(transform.eulerAngles.y, 0.0f, valueClampAxisY);
            else
                eulerRotation.y = Mathf.Clamp(transform.eulerAngles.y, (360 - valueClampAxisY), 360);
        }
        transform.eulerAngles = eulerRotation;
    }*/
}

[Serializable]
public class CM_CameraSettings
{
    #region Parameters
    [SerializeField, Header("Camera target")] Transform camTarget;
    [SerializeField, Header("Camera Speed"), Range(0.1f, 30)] float speed = 1;
    [SerializeField, Header("Camera Distance"), Range(0, 30)] float distance = 0;
    [SerializeField, Header("Camera offSet X"), Range(-20, 20)] float offsetX = 0;
    [SerializeField, Header("Camera offSet Y"), Range(-20, 20)] float offsetY = 0;
    #endregion

    #region Getter/Setter
    public Vector3 CameraOffSet => new Vector3(offsetX, offsetY, 0);
    public Transform CameraTarget { get { return camTarget; } set { camTarget = value; } }
    public bool IsValid => camTarget;
    public float CameraSpeed => speed;
    public float CameraDistance => distance;
    #endregion

    public CM_CameraSettings(float _speed, float _distance, float _offSetX, float _offSetY)
    {
        speed = _speed;
        distance = _distance;
        offsetX = _offSetX;
        offsetY = _offSetY;
    }
}
