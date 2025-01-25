using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject collisionParticles;
    private GameObject localParticles;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnCollisionEnter(Collision collision)
    {

        Animator animator = collision.gameObject.GetComponent<Animator>();

        if (collision.gameObject.CompareTag("Duck"))
        {
            localParticles = Instantiate(collisionParticles, gameObject.transform.localPosition, gameObject.transform.localRotation);
            meshRenderer.enabled = false;
            StartCoroutine(Destroy());

            animator.SetTrigger("Fall");
            //+ points on the ui thing
        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            localParticles = Instantiate(collisionParticles, gameObject.transform.localPosition, gameObject.transform.localRotation);
            meshRenderer.enabled = false;
            StartCoroutine(Destroy());

            animator.SetTrigger("Fall");
            //- points on the ui thing
        }
    }

    IEnumerator Destroy()
    {
        Debug.Log("destroy");
        yield return new WaitForSeconds(0.2f);
        Destroy(localParticles);
        Destroy(gameObject);
    }
}
