using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControl : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody[] rigidbodies;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private TrailRenderer trailRenderer;

    private void Awake()
    {
        foreach(Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach(Collider col in colliders)
        {
            col.enabled = false;
        }
    }
    
    public void MakePhysical()
    {
        animator.enabled = false;
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(GetComponent<CapsuleCollider>());
            GetComponent<Rigidbody>().isKinematic = true;
            MakePhysical();
            GetComponentInParent<PlayerMovement>().enabled = false;
            trailRenderer.time = Mathf.Infinity;

            //vibration
            Handheld.Vibrate();

            GameManager.instance.SetGameState(GameState.GameOver);
        }
    }
}
