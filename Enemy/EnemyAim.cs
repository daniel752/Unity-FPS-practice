using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    [SerializeField] Transform weaponTransform;
    private float range = 30;

    void Awake()
    {
        weaponTransform = transform;
    }

    private void Update() 
    {
        Ray ray = new Ray(transform.position, transform.forward);
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
    public void AimAtTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        weaponTransform.rotation = rotation;
    }
}
