using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] public Transform bulletLaunch;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Rigidbody bulletPrefabRb;
    [SerializeField] public GameObject impactParticle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            Debug.Log("shoot");
        }
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletLaunch.transform.position, bulletLaunch.transform.rotation);
        bulletPrefabRb.velocity = bulletLaunch.transform.forward * 10;
    }
}
