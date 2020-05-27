using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PickerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float sidewaySpeed = 1f;

    private bool waitForCheckpoint;
    private Collider[] _colliders;
    
    // Start is called before the first frame update
    void Start()
    {
        waitForCheckpoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!waitForCheckpoint)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0f, 0f) * sidewaySpeed;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 1f) * forwardSpeed;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            _colliders = Physics.OverlapBox(transform.position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size / 2);
            
            int count = 0;
            for (int i = 0; i < _colliders.Length; i++)
            {
            
                if (_colliders[i].CompareTag("Collectible"))
                {
                    _colliders[i].GetComponent<Rigidbody>().AddForce(Vector3.forward * 5f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectBox"))
        {
            waitForCheckpoint = true;
        }
    }
}
