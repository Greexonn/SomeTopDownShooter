using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    public float speed;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //move
        _rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;

        //rotate
        Vector3 _pointerPos = Input.mousePosition;
        _pointerPos.z = Camera.main.transform.position.y - transform.position.y;
        Vector3 _lookPos = Camera.main.ScreenToWorldPoint(_pointerPos) - transform.position;
        transform.rotation = Quaternion.LookRotation(_lookPos, Vector3.up);
    }
}
