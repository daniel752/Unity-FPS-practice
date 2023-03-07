using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private Transform cameraRotation;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = cameraRotation.rotation;
    }
}
