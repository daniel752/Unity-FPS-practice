using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    public PlayerInput.UIActions UI;
    private PlayerMotor motor;
    private PlayerLook look;
    private GameObject inventoryUI;
    private bool inventoryOpen;
    WeaponManager weaponManager;
    [SerializeField] InventoryManager inventoryManager;


    // private CameraSwitch cameraSwitch;
    void Awake()
    {
        inventoryOpen = false;
        inventoryUI = GameObject.FindWithTag("InventoryUI");
        inventoryUI.SetActive(false);
        inventoryManager.Init();
        inventoryManager.GetComponent<EquipmentManager>().Init();
        // InventoryManager.instance.gameObject.SetActive(false);
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        UI = playerInput.UI;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        weaponManager = GetComponent<WeaponManager>();
        // cameraSwitch = GetComponent<CameraSwitch>();
        // inventoryManager = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryManager>();
        // inventoryManager.gameObject.GetComponent<Image>().enabled = false;
        // inventoryManager.gameObject.SetActive(false);

        onFoot.Jump.performed += ctx =>
        {
            if (!inventoryOpen)
                motor.Jump();
        };
        onFoot.Crouch.performed += ctx =>
        { 
            if (!inventoryOpen)
                motor.Crouch();
        };
        onFoot.Sprint.performed += ctx => 
        {
            if (!inventoryOpen)
                motor.Sprint();
        };
        onFoot.Fire.performed += ctx => 
        {
            if (!inventoryOpen && WeaponEquipped())
                motor.Fire();
        };
        onFoot.FireHold.performed += ctx =>
        {
            if (!inventoryOpen && WeaponEquipped())
                 motor.Fire();
        };
        onFoot.FireHold.canceled += ctx =>
        {
            if (!inventoryOpen && WeaponEquipped())
                motor.StopFire();
        };
        UI.OpenInventory.performed += ctx => 
        {
            if (!inventoryOpen) 
            {
                look.enabled = false;
                // cameraSwitch.SwitchCameras();
                inventoryManager.OpenInventory();
                inventoryOpen = true;
            }
            else 
            {
                look.enabled = true;
                // cameraSwitch.SwitchCameras();
                inventoryManager.CloseInventory();
                inventoryOpen = false;
            }
        };
    }
    void FixedUpdate()
    {
        if (!inventoryOpen)
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    void LateUpdate()
    {
        if (!inventoryOpen)
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();
        UI.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
        UI.Disable();    
    }
    private bool WeaponEquipped()
    {
        return (weaponManager.GetCurrentWeapon() != null) ? true : false;
    }
}
