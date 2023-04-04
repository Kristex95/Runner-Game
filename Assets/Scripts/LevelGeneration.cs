using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    //TODO: make player a singelton

    [SerializeField] private Transform player;
    [SerializeField] private Transform[] levelPart;
    [SerializeField] private Transform lastLevelPart;
    private Vector3 lastLevelPartDesiredPos;

    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float spawnDepth = 200f;
    private const float LEVEL_PART_LENGTH = 30f;

    [SerializeField] private float spawnDistance = 100f;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            lastLevelPart = SpawnLevel(lastLevelPart.transform.position + new Vector3(0, 0, LEVEL_PART_LENGTH));
        }
    }

    private void Update()
    {
        if(lastLevelPart.transform.position.z - player.transform.position.z < spawnDistance)
        {
            lastLevelPart = SpawnLevel(lastLevelPart.transform.position + new Vector3(0, -spawnDepth, LEVEL_PART_LENGTH));
            lastLevelPartDesiredPos = lastLevelPart.position + new Vector3(0, spawnDepth, 0);
        }
        //MoveLevelPart(lastLevelPart);
        lastLevelPart.transform.position = Vector3.Lerp(lastLevelPart.position, lastLevelPartDesiredPos, Time.deltaTime * lerpSpeed);
    }

    private Transform SpawnLevel(Vector3 pos)
    {
        Transform newLevelPart = Instantiate(levelPart[Random.Range(0, levelPart.Length)], pos, Quaternion.identity);
        newLevelPart.transform.parent = GameObject.Find("Level").transform;
        return newLevelPart;
    }

    private void MoveLevelPart(Transform m_levelPart)
    {
        m_levelPart.position = Vector3.Lerp(m_levelPart.position, m_levelPart.position + new Vector3(0, spawnDepth, 0), lerpSpeed * Time.deltaTime);
    }
}
