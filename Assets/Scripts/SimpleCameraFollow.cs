using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Vector3 offset;

    public float velocity;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, (_player.transform.position + offset), 0.01f * velocity);
    }
}
