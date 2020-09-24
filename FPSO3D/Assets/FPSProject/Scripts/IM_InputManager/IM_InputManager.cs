using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IM_InputManager : Singleton<IM_InputManager>
{
    #region Inputs
    //KeyBoard
    [SerializeField, Header("Vertical axis")] string vAxis = "Vertical";
    [SerializeField, Header("Horizontal axis")] string hAxis = "Horizontal";
    [SerializeField, Header("FeedBack vAxis"), Range(-1, 1)] float vAxisValue;
    [SerializeField, Header("FeedBack hAxis"), Range(-1, 1)] float hAxisValue;

    [SerializeField, Header("Input key consume")] KeyCode consumeInputKey = KeyCode.E;

    //Mouse
    [SerializeField, Header("Mouse X Axis")] string xAxis = "Mouse X";
    [SerializeField, Header("FeedBack mxAxis"), Range(-100, 100)] float mxAxisValue;
    [SerializeField, Header("Mouse Y Axis")] string yAxis = "Mouse Y";
    [SerializeField, Header("FeedBack myAxis"), Range(-100, 100)] float myAxisValue;

    [SerializeField, Header("Mouse Left Input")] bool leftmouseInput = false;
    //Controller
    [SerializeField, Header("Controller A")] KeyCode joystickInputA = KeyCode.JoystickButton0;
    [SerializeField, Header("Controller X")] KeyCode joystickInputX = KeyCode.JoystickButton2;
    [SerializeField, Header("Controller X")] KeyCode joystickInputStart = KeyCode.JoystickButton7;
    #endregion

    #region Events
    public static event Action<float, float> OnKeyAxis = null;
    public static event Action<float, float> OnMouseAxis = null;
    public static event Action<bool> OnConsumeInput = null;
    public static event Action<bool> OnShootInput = null;
    public static event Action<bool> OnStartInput = null;
    #endregion

    #region Methods
    //KeyBoard
    public float GetVertical => vAxisValue = Input.GetAxis(vAxis);
    public float GetHorizontal => hAxisValue = Input.GetAxis(hAxis);
    public bool GetKeyConsume => Input.GetKeyDown(consumeInputKey);

    //Mouse
    public float GetMouseX => mxAxisValue = Input.GetAxis(xAxis);
    public float GetMouseY => myAxisValue = Input.GetAxis(yAxis);
    public bool GetLeftMouseValueDown => leftmouseInput = Input.GetKeyDown(KeyCode.Mouse0);

    //Controller
    public bool GetControlA => Input.GetKeyDown(joystickInputA);
    public bool GetControlX => Input.GetKeyDown(joystickInputX);
    public bool GetControlStart => Input.GetKeyDown(joystickInputStart);
    #endregion

    protected override void Awake() => base.Awake();

    void Update()
    {
        //mouse
        OnKeyAxis?.Invoke(GetHorizontal, GetVertical);
        OnMouseAxis?.Invoke(GetMouseX, GetMouseY);
        OnConsumeInput?.Invoke(GetKeyConsume);
        OnShootInput?.Invoke(GetLeftMouseValueDown);
        //controller
        OnConsumeInput?.Invoke(GetControlX);
        OnShootInput?.Invoke(GetControlA);
        OnStartInput?.Invoke(GetControlStart);
    }
}
