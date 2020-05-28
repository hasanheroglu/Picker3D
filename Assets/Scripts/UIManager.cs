using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }
    
    [SerializeField] private GameObject worldSpaceCanvas;
    [SerializeField] private GameObject screenSpaceCanvas;
    [SerializeField] private GameObject collectBoxTextPrefab;
    [SerializeField] private GameObject instructionMessageText;
    
    [Header("Level and Checkpoint")] 
    [SerializeField] private GameObject currentLevelText;
    [SerializeField] private GameObject nextLevelText;
    [SerializeField] private List<GameObject> checkpointIndicators;

    
    
    private int _checkpointIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _checkpointIndex = 0;
    }

    public GameObject GenerateCollectBoxText(float zPos)
    {
        var collectBoxText = Instantiate(collectBoxTextPrefab, worldSpaceCanvas.transform);
        collectBoxText.GetComponent<RectTransform>().SetPositionAndRotation(collectBoxText.transform.position + new Vector3(0f, 0f, zPos), collectBoxText.transform.rotation);
        return collectBoxText;
    }

    public void SetLevelTexts(int levelIndex)
    {
        currentLevelText.GetComponent<TextMeshProUGUI>().text = (levelIndex + 1).ToString();
        nextLevelText.GetComponent<TextMeshProUGUI>().text = (levelIndex + 2).ToString();
    }
    
    public void ClearWorldSpaceCanvas()
    {
        for (int i = 0; i < worldSpaceCanvas.transform.childCount; i++)
        {
            Destroy(worldSpaceCanvas.transform.GetChild(i).gameObject);
        }
    }

    public void SetCheckpointIndicator()
    {
        if (_checkpointIndex >= checkpointIndicators.Count) return;
        
        checkpointIndicators[_checkpointIndex].GetComponent<Image>().color = Color.green;
        _checkpointIndex++;
    }

    public void ResetCheckpointIndicators()
    {
        _checkpointIndex = 0;
        
        foreach(var checkpointIndicator in checkpointIndicators)
        {
            checkpointIndicator.GetComponent<Image>().color = Color.white;
        }
    }

    public void DisplayInstructionMessage()
    {
        instructionMessageText.SetActive(true);

    }

    public void RemoveInstructionMessage()
    {
        instructionMessageText.SetActive(false);

    }
}
