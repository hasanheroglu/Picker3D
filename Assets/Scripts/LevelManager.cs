using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    [SerializeField] private List<GameObject> paths;
    [SerializeField] private int pathCount;

    [SerializeField] private GameObject picker;
    
    private List<CollectBox> collectBoxes;
    private int checkpointIndex;
    private int levelIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        
        pathCount = 3;
        checkpointIndex = 1;
        levelIndex = 1;
        collectBoxes = new List<CollectBox>();
        
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateLevel()
    {
        for (int i = 0; i < pathCount; i++)
        {
            var selectedPathPrefab = paths[Random.Range(0, paths.Count)];
            var path = Instantiate(selectedPathPrefab, selectedPathPrefab.transform.position + new Vector3(0f, 0f, selectedPathPrefab.transform.position.z) * i, selectedPathPrefab.transform.rotation);
            path.GetComponentInChildren<CollectBox>().SetNeededCollectibleCount(10 * (i + 1) + 10 * (levelIndex / 10));
        }
    }
}
