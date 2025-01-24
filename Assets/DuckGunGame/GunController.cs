using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform bulletLaunch;
    [SerializeField] private Transform smokeParticleLocation;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject gunSmokeParticles;

    [SerializeField] private Animator animatorForReload;
    [SerializeField] private Animator animatorForTrigger;

    [SerializeField] private InputActionAsset gunControlInputs;

    private bool canShoot;
    private bool coolDown;

    private InputAction shootInput;
    private InputAction reloadInput;

    private void Start()
    {
        // Find the input actions for shooting and reloading
        shootInput = gunControlInputs.FindActionMap("RightHand").FindAction("Shoot");
        reloadInput = gunControlInputs.FindActionMap("LeftHand").FindAction("Reload");

        // Enable the input actions
        shootInput.Enable();
        reloadInput.Enable();

        // Attach event handlers for input actions
        shootInput.performed += ctx => Shoot();
        reloadInput.performed += ctx => Reload();

        // Initialize state variables
        canShoot = true;
        coolDown = false;
    }
    public void Reload()
    {
        if (!canShoot && !coolDown)
        {
            animatorForReload.SetTrigger("Reload"); // Trigger the reload animation
            Debug.Log("Reload");
            canShoot = true; // Gun is ready to shoot again
            StartCoroutine(CoolDown()); // Start the cooldown period
        }
    }
    public void Shoot()
    {
        if (canShoot && !coolDown)
        {
            // Instantiate bullet and smoke particle objects
            GameObject bullet = Instantiate(bulletPrefab, bulletLaunch.position, bulletLaunch.rotation);
            GameObject smokeParticles = Instantiate(gunSmokeParticles, smokeParticleLocation.position, smokeParticleLocation.rotation);

            // Schedule smoke particles for removal after a delay
            StartCoroutine(SmokeRemove(smokeParticles));

            // Apply velocity to the bullet if it has a Rigidbody component
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                animatorForTrigger.SetTrigger("Shoot"); // Trigger the shoot animation
                bulletRb.velocity = bulletLaunch.transform.right * 25f; // Set bullet velocity
                Debug.Log("Shoot");
                canShoot = false; // Gun is not ready to shoot until reloaded
                StartCoroutine(CoolDown()); // Start the cooldown period
            }
        }
    }
    IEnumerator SmokeRemove(GameObject Smoke)
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        Destroy(Smoke); // Destroy the smoke particles
    }
    IEnumerator CoolDown()
    {
        coolDown = true; // Gun is cooling down
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        coolDown = false; // Cooldown complete
    }
}