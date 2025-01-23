using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] public Transform bulletLaunch;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject impactParticle;

    [SerializeField] public Animator animatorForReload;
    [SerializeField] public Animator animatorForTrigger;

    private bool canShoot;
    private bool coolDown;

    private void Start()
    {
        canShoot = true;
        coolDown = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot && !coolDown)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !canShoot && !coolDown)
        {
            Reload();
        }
    }

    public void Reload()
    {
        animatorForReload.SetTrigger("Reload");
        Debug.Log("Reload");
        canShoot = true;
        StartCoroutine(CoolDown());
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletLaunch.position, bulletLaunch.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            animatorForTrigger.SetTrigger("Shoot");
            bulletRb.velocity = bulletLaunch.transform.right * 25f;
            Debug.Log("shoot");
            canShoot = false;
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        coolDown = true;
        yield return new WaitForSeconds(0.5f);
        coolDown = false;
    }
}
