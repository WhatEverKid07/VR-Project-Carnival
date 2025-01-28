using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform bulletLaunch;
    [SerializeField] private Transform smokeParticleLocation;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject gunSmokeParticles;

    [SerializeField] private Animator animatorForReload;
    [SerializeField] private Animator animatorForTrigger;

    [SerializeField] private InputActionAsset gunControlInputs;

    [SerializeField] private float coneAngle;
    [SerializeField] private float bulletSpeed;

    private bool canShoot;
    private bool reloadCoolDown;
    private bool shootCoolDown;

    [HideInInspector] public bool leftHandOn = false;
    [HideInInspector] public bool rightHandOn = false;

    private InputAction shootInput;
    private InputAction reloadInput;

    private void Start()
    {
        shootInput = gunControlInputs.FindActionMap("RightHand").FindAction("Shoot");
        reloadInput = gunControlInputs.FindActionMap("LeftHand").FindAction("Reload");

        shootInput.Enable();
        reloadInput.Enable();

        shootInput.performed += ctx => Shoot();
        reloadInput.performed += ctx => Reload();

        canShoot = true;
        shootCoolDown = false;
        reloadCoolDown = false;
    }

    public void Reload()
    {
        if (!canShoot && !shootCoolDown && leftHandOn && rightHandOn)
        {
            animatorForReload.SetTrigger("Reload");
            //Debug.Log("Reload");
            canShoot = true;
            StartCoroutine(ReloadCoolDown());
        }
    }

    public void Shoot()
    {
        if (canShoot && !reloadCoolDown && leftHandOn && rightHandOn)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletLaunch.position, bulletLaunch.rotation);
            GameObject smokeParticles = Instantiate(gunSmokeParticles, smokeParticleLocation.position, smokeParticleLocation.rotation);

            StartCoroutine(SmokeRemove(smokeParticles));

            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                animatorForTrigger.SetTrigger("Shoot");

                // Calculate random spread within a cone
                Vector3 bulletDirection = GetConeSpreadDirection(bulletLaunch.transform.right, coneAngle);

                bulletRb.velocity = bulletDirection * bulletSpeed;

                //Debug.Log("Shoot");
                canShoot = false;
                StartCoroutine(ShootCoolDown());
            }
        }
    }
    private Vector3 GetConeSpreadDirection(Vector3 forwardDirection, float maxAngle)
    {
        float maxAngleRad = maxAngle * Mathf.Deg2Rad;
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        float randomRadius = Mathf.Sin(maxAngleRad) * Random.Range(0f, 1f);

        Vector3 randomSpread = new Vector3(
            randomRadius * Mathf.Cos(randomAngle),
            randomRadius * Mathf.Sin(randomAngle),
            Mathf.Cos(maxAngleRad)
        );
        Quaternion rotation = Quaternion.LookRotation(forwardDirection);
        return (rotation * randomSpread).normalized;
    }
    IEnumerator SmokeRemove(GameObject Smoke)
    {
        yield return new WaitForSeconds(2f);
        Destroy(Smoke);
    }

    IEnumerator ReloadCoolDown()
    {
        reloadCoolDown = true;
        yield return new WaitForSeconds(0.5f);
        reloadCoolDown = false;
    }
    IEnumerator ShootCoolDown()
    {
        shootCoolDown = true;
        yield return new WaitForSeconds(0.1f);
        shootCoolDown = false;
    }
}