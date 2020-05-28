using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int LevelIndex { get; set; }

    
    [SerializeField] private List<GameObject> levels;
    [SerializeField] private GameObject picker;
    
    private GameObject _currentLevel;
    
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
        
        LevelIndex = 0;
        GenerateLevel();
    }
    
    public void SetLevel()
    {
        UIManager.Instance.SetLevelTexts(LevelIndex);        
        UIManager.Instance.ClearWorldSpaceCanvas();
        UIManager.Instance.ResetCheckpointIndicators();
        ClearLevel();
        MovePickerToStartPosition();
        GenerateLevel();
    }

    private void MovePickerToStartPosition()
    {
        picker.transform.position = new Vector3(picker.transform.position.x, picker.transform.position.y, -75f);

    }
    
    private void GenerateLevel()
    {
        if (LevelIndex >= levels.Count) return; 
        
        _currentLevel = Instantiate(levels[LevelIndex], levels[LevelIndex].transform.position, levels[LevelIndex].transform.rotation);
    }

    private void ClearLevel()
    {
        Destroy(_currentLevel);
    }
}
