using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    //TODO: make player a singelton

    [SerializeField] private Transform player;
    [SerializeField] private Transform levelPart;
    [SerializeField] private Transform lastLevelPart;
    private float levelPartLength;

    [SerializeField] private float spawnDistance = 100f;

    private void Awake()
    {
        levelPartLength = levelPart.transform.localScale.z;
        
    }

    private void Update()
    {
        if(Vector3.Distance(player.transform.position, lastLevelPart.transform.position) < spawnDistance)
        {
            lastLevelPart = SpawnLevel();
        }
    }

    private Transform SpawnLevel()
    {
        Transform newLevelPart = Instantiate(levelPart, lastLevelPart.transform.position + new Vector3(0, 0, levelPartLength), Quaternion.identity);
        newLevelPart.transform.parent = GameObject.Find("Level").transform;
        return newLevelPart;
    }
}
