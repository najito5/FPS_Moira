using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PM_Player : MonoBehaviour
{
    #region Parameters 
    [SerializeField] OB_Ball ball;
    [SerializeField] Transform PosSpawn;
    [SerializeField, Header("Player ID :")] int id = 1;
    [SerializeField, Header("Player Speed :"), Range(0.1f, 30)] int speed = 1;
    [SerializeField, Header("Player gravity :"), Range(0.1f, 30)] int gravity = 20;
    [SerializeField, Header("Player inventory")] INV_PlayerInventory playerInventory = null;
    [SerializeField, Header("Player stats :")] PM_PlayerSettings playerStats = new PM_PlayerSettings();
    [SerializeField, Header("Damage by time on player"), Range(0, 100)] int damageLife = 3;
    [SerializeField, Header("Power eaned by time on player"), Range(0, 100)] int earnPower = 20;

    Vector3 target = Vector3.zero;
    CharacterController characterController;
    OB_Ball _currentBall;
    #endregion
    #region Event
    public Action<PM_PlayerSettings> OnUpdatePlayerStats = null;
    #endregion

    #region Methods
    public int ID => id;
    public bool IsValid => this;
    public CharacterController GetCharacterController => characterController;
    public PM_PlayerSettings GetSettings => playerStats;
    public INV_PlayerInventory GetPlayerInventory => playerInventory;
    #endregion

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInventory = GetComponent<INV_PlayerInventory>();
    }

    public void Start()
    {
        PM_PlayerManager.Instance.AddPlayer(this);
        SetPlayerView();
        CreateBall();
        InitPlayer();
    }
    void quitGame(bool _start)
    {
        if (_start)
        {
            Application.Quit();
        }
    }
    void InitPlayer()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        IM_InputManager.OnKeyAxis += MovePlayer;
        IM_InputManager.OnShootInput += ShootPlayer;
        IM_InputManager.OnConsumeInput += ConsumePack;
        IM_InputManager.OnStartInput += quitGame;
        InvokeRepeating("UpdateStats", 2, 1);
        InvokeRepeating("Verif", 2, 1);
    }

    void ConsumePack(bool _inputKey)
    {
        if (_inputKey)
        {
            if (!playerInventory) return;
            if (!playerInventory.CheckID(0)) return;
            playerInventory.GetFromID(0).OnUseItem?.Invoke(playerInventory.GetFromID(0));
            int _suppLife = UnityEngine.Random.Range(15, 30);
            playerStats.Life += _suppLife;
        }
    }

    void SetPlayerView()
    {
        CM_CameraManager.Instance.SetViewCamera("mainCam");
        CM_CameraManager.Instance.SetCameraTarget("mainCam", transform);
    }

    void MovePlayer(float _horizontalAxis, float _verticalAxis)
    {
        Vector3 _move = (transform.forward * _verticalAxis) + (transform.right * _horizontalAxis);
        _move.y -= gravity * Time.deltaTime;
        characterController.Move(_move * Time.deltaTime * speed);
    }

    void Verif()
    {
        if (playerStats.Power <= 0)
            CreateBall();
        if (playerStats.Life <= 0)
            DeathScreen();
    }

    void UpdateStats()
    {
        playerStats.Life -= damageLife;
        playerStats.Power += earnPower;
        OnUpdatePlayerStats?.Invoke(playerStats);
    }

    void ShootPlayer(bool _mouseLeftInput)
    {
        if (playerStats == null) return;
        if (_mouseLeftInput)
        {
            if (playerStats.IsMaxPower())
            {
                _currentBall.Launch();
                _currentBall = null;
                playerStats.Power = 0;
                CreateBall();
            }
        }
    }

    public void CreateBall()
    {
        _currentBall = Instantiate(ball, PosSpawn);
        _currentBall.Init(this);
    }

    public void DeathScreen()
    {
        if (!playerStats.IsDead()) return;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        IM_InputManager.OnKeyAxis -= MovePlayer;
        IM_InputManager.OnShootInput -= ShootPlayer;
        //IM_InputManager.OnMouseAxis -= CM_CameraManager.Instance.GetCameraFromID("Main Camera");
        CancelInvoke("UpdateStats");
        CancelInvoke("Verif");
    }

    public void RestoreStats() => InitPlayer();

    public void GetItem(INV_InventoryItemComponent _compo) => playerInventory.AddItem(_compo);
}

