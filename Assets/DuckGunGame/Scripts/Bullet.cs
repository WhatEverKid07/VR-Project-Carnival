using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject collisionParticles;

    private GameObject localParticles;
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private PointsLink pointsLink;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        boxCollider.enabled = false;
        pointsLink = collision.gameObject.GetComponent<PointsLink>();
        Animator animator = collision.gameObject.GetComponent<Animator>();

        localParticles = Instantiate(collisionParticles, gameObject.transform.localPosition, gameObject.transform.localRotation);
        meshRenderer.enabled = false;
        StartCoroutine(Destroy());

        if (collision.gameObject.CompareTag("Duck"))
        {
            animator.SetTrigger("Fall");
            pointsLink.AddPointsLink();
            pointsLink.DuckDie();
        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            animator.SetTrigger("Fall");
            pointsLink.RemovePointsLink();
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
