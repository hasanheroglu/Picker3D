using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<GameObject> collectBoxes;

    private void Awake()
    {
        SetCollectBoxes();
    }

    private void SetCollectBoxes()
    {
        var i = 0;
        foreach (var collectBox in collectBoxes)
        {
            collectBox.GetComponent<CollectBox>().SetNeededCollectibleCount(10 * (i + 1) + 10 * (LevelManager.Instance.LevelIndex / 10));
            i++;
        }
    }
}
