using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PickerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float sidewaySpeed = 1f;

    private Collider[] _colliders;
    private Rigidbody _rigidbody;
    private bool _started;
    private bool _enteredFinish;
    
    public bool WaitForCheckpoint { get; set; }
    public bool WaitForNewLevel { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        WaitForCheckpoint = false;
        WaitForNewLevel = false;
        _rigidbody = GetComponent<Rigidbody>();
        _enteredFinish = false;
        _started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_started)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                _started = true;
                UIManager.Instance.RemoveInstructionMessage();
            }
        }
    }

    private void LateUpdate()
    {
        if (!WaitForCheckpoint && _started)
        {
            Move();   
        }
        
        if(WaitForCheckpoint)
        {
            _rigidbody.velocity = Vector3.zero;
            PushCollectiblesInPicker(FindCollectiblesInPicker());
        }
    }

    private void Move()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0f, 0f) * sidewaySpeed;
        _rigidbody.velocity = new Vector3(0f, 0f, 1f) * forwardSpeed;
    }

    private List<Collider> FindCollectiblesInPicker()
    {
        List<Collider> collectibles = new List<Collider>();

        _colliders = Physics.OverlapBox(transform.position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size / 2);
            
        foreach (var coll in _colliders)
        {
            if (coll.CompareTag("Collectible"))
            {
                collectibles.Add(coll);
            }
        }

        return collectibles;
    }

    private void PushCollectiblesInPicker(List<Collider> collectibles)
    {
        foreach (var collectible in collectibles)
        {
            collectible.GetComponent<Rigidbody>().AddForce(Vector3.forward * 50f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Start") && _enteredFinish)
        {
            _enteredFinish = false;
            _started = false;
            _rigidbody.velocity = Vector3.zero;
            UIManager.Instance.DisplayInstructionMessage();
        }
        
        if (other.CompareTag("Finish") && !_enteredFinish)
        {
            LevelManager.Instance.LevelIndex++;
            LevelManager.Instance.SetLevel();
            WaitForNewLevel = true;
            _enteredFinish = true;
        }
    }
}
