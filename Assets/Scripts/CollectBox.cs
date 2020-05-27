using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBox : MonoBehaviour
{
    [SerializeField] private GameObject path;
    [SerializeField] private GameObject collectibleHolder; 
    
    private int _neededCollectibleCount;
    private int _collectibleCount;
    
    // Start is called before the first frame update
    void Start()
    {
        _collectibleCount = 0;
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
        }
        
        Debug.Log("Collectibles collected: " + _collectibleCount);
        
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
        Debug.Log("Checkpoint: " + _collectibleCount + "/" + _neededCollectibleCount);
        return _collectibleCount > _neededCollectibleCount;
    }

    public void SetNeededCollectibleCount(int neededCollectibleCount)
    {
        _neededCollectibleCount = neededCollectibleCount;
    }

    public IEnumerator GeneratePath()
    {
        var startTime = Time.time;
        var duration = 0.1f;
        while (Time.time < startTime + duration)
        {
            path.transform.localScale = Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(25f, 1f, 25f), (Time.time - startTime)/duration);
            yield return null;
        }
        
        path.transform.localScale = new Vector3(25f, 1f, 25f);
    }
}
