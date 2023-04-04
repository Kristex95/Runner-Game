using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextGenerator : MonoBehaviour
{
    [SerializeField] private Transform floatingText;
    [SerializeField] private Transform pivotPoint;

    public void CreateText()
    {
        Transform newFloatingText = Instantiate(floatingText, pivotPoint.position, Quaternion.identity);
        newFloatingText.parent = null;
    }
}
