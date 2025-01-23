using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject collisionParticles;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Duck"))
        {
            GameObject localParticles = Instantiate(collisionParticles, gameObject.transform.localPosition, gameObject.transform.localRotation);
            StartCoroutine(Destroy());
        }
        else if (collision.gameObject.CompareTag("Duck"))
        {

        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        Destroy();
    }
}
