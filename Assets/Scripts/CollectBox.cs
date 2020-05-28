using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CollectBox : MonoBehaviour
{
    [SerializeField] private GameObject path;
    [SerializeField] private GameObject collectibleHolder;
    [SerializeField] private GameObject checkpointLeftDoor;
    [SerializeField] private GameObject checkpointRightDoor;
    
    private int _neededCollectibleCount;
    private int _collectibleCount;
    private GameObject _collectBoxText;
    
    // Start is called before the first frame update
    void Start()
    {
        _collectibleCount = 0;
        _collectBoxText = UIManager.Instance.GenerateCollectBoxText(transform.position.z);
        _collectBoxText.GetComponent<TextMeshProUGUI>().text = _collectibleCount + "/" + _neededCollectibleCount;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            _collectibleCount++;
            _collectBoxText.GetComponent<TextMeshProUGUI>().text = _collectibleCount + "/" + _neededCollectibleCount;
        }
        
        if (other.CompareTag("Picker"))
        {
            other.GetComponent<PickerController>().WaitForCheckpoint = true;
            StartCoroutine(Evaluate(other.GetComponent<PickerController>()));
        }  
        
    }

    

    private IEnumerator Evaluate(PickerController pickerController)
    {
        yield return new WaitForSeconds(2f);
        
        if (DidReachNeededCollectibleCount())
        {        
            yield return GeneratePath();
            yield return OpenDoors();
            UIManager.Instance.SetCheckpointIndicator();
            collectibleHolder.SetActive(false);
            pickerController.WaitForCheckpoint = false;
        }
        else
        {
            //Restart Level
        }
    }

    public bool DidReachNeededCollectibleCount()
    {
        return _collectibleCount > _neededCollectibleCount;
    }

    public void SetNeededCollectibleCount(int neededCollectibleCount)
    {
        _neededCollectibleCount = neededCollectibleCount;
    }

    public IEnumerator GeneratePath()
    {
        var startTime = Time.time;
        var duration = 0.3f;
        while (Time.time < startTime + duration)
        {
            path.transform.localScale = Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(25f, 1f, 25f), (Time.time - startTime)/duration);
            yield return null;
        }
        
        path.transform.localScale = new Vector3(25f, 1f, 25f);
    }

    public IEnumerator OpenDoors()
    {
        var startTime = Time.time;
        var duration = 0.3f;
        while (Time.time < startTime + duration)
        {
            checkpointLeftDoor.transform.rotation = Quaternion.Lerp(Quaternion.identity, new Quaternion(0f, 0f, 0.5f, 1f), (Time.time - startTime)/duration);
            checkpointRightDoor.transform.rotation = Quaternion.Lerp(Quaternion.identity, new Quaternion(0f, 0f, -0.5f, 1f), (Time.time - startTime)/duration);
            
            yield return null;
        }

        checkpointLeftDoor.transform.rotation = new Quaternion(0f, 0f, 0.5f, 1f);
        checkpointRightDoor.transform.rotation = new Quaternion(0f, 0f, -0.5f, 1f);
    }
}
