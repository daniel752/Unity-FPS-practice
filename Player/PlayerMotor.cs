using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] private float speed = 5f;
    private bool grounded;
    private bool crouching;
    private bool lerpCrouch;
    private float crouchTimer;
    private bool sprinting;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float minimumDownForce = -2f;
    [SerializeField] private Weapon weapon;
    public bool firing;
    AudioSource walkingSfx;
    public bool walking;
    AudioSource jumpSfx;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        walkingSfx = GetComponents<AudioSource>()[1];
        jumpSfx = GetComponents<AudioSource>()[2];
        firing = false;
        crouching = false;
        sprinting = false;
        walking = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded;
        if (walking)
        {
            if (!walkingSfx.isPlaying)
            {
                // Debug.Log("play walk effect");
                walkingSfx.Play();
            }
            // else if (walkingSfx.isPlaying)
            // {
            //     Debug.Log("stop walk effect");
            //     walkingSfx.Stop();
            // }
        }

        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            
            if(crouching)
                controller.height = Mathf.Lerp(controller.height,1,p);
            else
                controller.height = Mathf.Lerp(controller.height,2,p);

            if(p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    // Receives input from InputManager.cs and applies it to character controller
    public void ProcessMove(Vector2 input) 
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (grounded && playerVelocity.y < 0)
            playerVelocity.y = minimumDownForce;
        controller.Move(playerVelocity * Time.deltaTime);
        // Debug.Log(playerVelocity.y);
    }
    public void Jump()
    {
        if (grounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.5f * gravity);
            jumpSfx.Play();
        }
    }
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
    public void Sprint()
    {
        sprinting = !sprinting;
        if(sprinting)
            speed = 10f;
        else
            speed = 5f;
    }
    public void Fire()
    {
        // Debug.Log("PlayerMotor fire");
        // firing = true;
        weapon.firing = true;
        weapon.Fire();
        // weapon.Fire();
        // firing = false;
    }
    public void StopFire()
    {
        weapon.firing = false;
    }
    public void Reload()
    {
        weapon.Reload();
    }
    private void OnWeaponEquipped(Weapon weapon)
    {
        this.weapon = weapon;
    }
    private void OnEnable()
    {
        WeaponManager.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDisable()
    {
        WeaponManager.OnWeaponEquipped -= OnWeaponEquipped;
    }

    // public void Walk(Vector2 movement)
    // {
    //     if (movement.magnitude > 0.01f)
    //     {
    //         if (!walkingSfx.isPlaying)
    //         {
    //             Debug.Log("play walk effect");
    //             walkingSfx.Play();
    //         }
    //         else if (walkingSfx.isPlaying)
    //         {
    //             Debug.Log("stop walk effect");
    //             walkingSfx.Stop();
    //         }
    //     }
    // }
}