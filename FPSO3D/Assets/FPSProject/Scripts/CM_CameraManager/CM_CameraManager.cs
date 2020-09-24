using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_CameraManager : Singleton<CM_CameraManager>
{
    [SerializeField, Header("Camera base")] CM_CameraComponent cameraComponent = null;
    Dictionary<string, CM_CameraComponent> allCameras = new Dictionary<string, CM_CameraComponent>();

    bool CheckID(string _id) => allCameras.ContainsKey(_id);

    protected override void Awake()
    {
        base.Awake();
    }

    private void LateUpdate() => UpdateCameraComponent();

    void UpdateCameraComponent()
    {
        foreach (KeyValuePair<string, CM_CameraComponent> cam in allCameras)
        {
            if (cam.Value.IsEnabled)
            {
                cam.Value.Behaviour.OnUpdateBehaviour?.Invoke();
            }
        }
    }

    void CameraManagerHandleBehaviour(bool _addCam, CM_CameraComponent _camera)
    {
        if (_addCam)
            allCameras.Add(_camera.ID, _camera);
        else
            allCameras.Remove(_camera.ID);
    }

    public void AddCamera(CM_CameraComponent _camera) => CameraManagerHandleBehaviour(true, _camera);
    public void RemoveCamera(CM_CameraComponent _camera) => CameraManagerHandleBehaviour(false, _camera);

    public void EnabledCamera(string _id)
    {
        if (CheckID(_id))
            GetCameraFromID(_id).IsEnabled = true;
    }

    public void DisabledCamera(string _id)
    {
        if (CheckID(_id))
            GetCameraFromID(_id).IsEnabled = false;
    }

    public void SetViewCamera(string _id)
    {
        if (!CheckID(_id)) return;
        foreach (KeyValuePair<string, CM_CameraComponent> c in allCameras)
        {
            c.Value.SetViewActive(false);
        }
        GetCameraFromID(_id).SetViewActive(true);
    }

    public void SetCameraTarget(string _id, Transform _target)
    {
        if (!CheckID(_id) || (!_target)) return;
        GetCameraFromID(_id).SetTarget(_target);
    }

    public CM_CameraComponent GetCameraFromID(string _id)
    {
        for (int i = 0; i < allCameras.Count; i++)
        {
            if (allCameras[_id] == null) return null;
            if (allCameras.ContainsKey(_id))
                return allCameras[_id];
        }
        return null;
    }

    public void DeleteCamera(string _id)
    {
        if (CheckID(_id))
            Destroy(GetCameraFromID(_id).gameObject);
    }
}
