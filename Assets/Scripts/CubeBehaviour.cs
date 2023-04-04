using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    private GameObject stickmanBody;
    private GameObject player;

    private Animator stickmanAnim;


    public void Awake()
    {
        stickmanBody = GameObject.Find("Body");
        player = GameObject.Find("Player");

        stickmanAnim = stickmanBody.GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            if (transform.position.y < collision.transform.position.y + collision.transform.localScale.y / 2)
            {
                Destroy(collision.gameObject);

                stickmanBody.transform.position += Vector3.up;
                GameObject stackCube = Instantiate(Resources.Load("StackCube"), stickmanBody.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity) as GameObject;
                stackCube.transform.parent = this.transform.parent;

                //player jumping
                stickmanAnim.SetTrigger("Jump");

                //Floating text generation
                player.GetComponent<FloatingTextGenerator>().CreateText();

                //particle effect
                stickmanBody.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
        else if(collision.gameObject.layer == 6)
        {
            if ((transform.position.y < collision.transform.position.y + collision.transform.localScale.y && transform.position.y > collision.transform.position.y) || (transform.position.y < collision.transform.position.y && transform.position.y - collision.transform.position.y > 0.25f))
            {
                transform.parent = collision.gameObject.transform;
                Destroy(GetComponent<CubeBehaviour>());
            }
        }
    }

}
