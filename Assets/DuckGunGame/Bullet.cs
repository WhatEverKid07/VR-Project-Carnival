using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject collisionParticles;

    private GameObject localParticles;
    private MeshRenderer meshRenderer;
    private PointsLink pointsLink;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        pointsLink = collision.gameObject.GetComponent<PointsLink>();
        Animator animator = collision.gameObject.GetComponent<Animator>();

        localParticles = Instantiate(collisionParticles, gameObject.transform.localPosition, gameObject.transform.localRotation);
        meshRenderer.enabled = false;
        StartCoroutine(Destroy());

        if (collision.gameObject.CompareTag("Duck"))
        {
            animator.SetTrigger("Fall");
            pointsLink.AddPointsLink();
            //+ points on the ui thing
        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            pointsLink.RemovePointsLink();
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
