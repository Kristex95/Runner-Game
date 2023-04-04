using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartRenderingControl : MonoBehaviour
{ 
    private float renderDistance = 100f;
    void Update()
    {
        if (PlayerMovement.instance.transform.position.z - transform.position.z > renderDistance)
        {
            Destroy(gameObject);
        }
    }
}
