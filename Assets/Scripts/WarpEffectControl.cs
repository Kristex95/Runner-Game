using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpEffectControl : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;

    private ParticleSystem particleSys;

    private void Awake()
    {
        particleSys = GetComponent<ParticleSystem>(); 
    }

    private void Start()
    {
        GameManager.instance.OnGameStateChange.AddListener(ChangeState);
    }

    private void Update()
    {
        Vector3 desiredPosition = new Vector3(0, target.position.y, target.position.z) + offset;
        transform.position = desiredPosition;
    }

    public void ChangeState()
    {
        var em = particleSys.emission;
        switch (GameManager.instance.gameState)
        {
            case GameState.Playing:
                em.enabled = true;
                break;
            case GameState.GameOver:
                em.enabled = false;
                break;
            case GameState.PreStart:
                em.enabled = false;
                break;
            default:
                break;
        }
    }
}
