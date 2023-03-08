using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform weaponTransform;
    [SerializeField] GameObject reticle;
    private float range = 30;

    private void Awake() 
    {
        playerCamera = Camera.main;
        weaponTransform = transform;    
    }

    private void Update() 
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction * range);
        weaponTransform.LookAt(ray.GetPoint(range));
        // RaycastHit hit;
        // if (Physics.Raycast(ray, out hit))
        // {
        //     weaponTransform.LookAt(hit.point);
        // }
        // else
        // {
        //     weaponTransform.LookAt(ray.direction * range,);
        // }
    }
}
