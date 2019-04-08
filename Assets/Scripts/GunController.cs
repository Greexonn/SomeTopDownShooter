using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Material rayMaterial;
    public float lineWidth;
    public ParticleSystem gun;

    private LineRenderer _lineRenderer;
    private RaycastHit _hitInfo;

    private bool _isFire;

    void Start()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.material = rayMaterial;
        _lineRenderer.widthMultiplier = lineWidth;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.up, out _hitInfo, 50f))
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _hitInfo.point);

            //fire
            if (Input.GetAxisRaw("Fire1") == 1)
            {
                if (!_isFire)
                {
                    _isFire = true;
                    gun.Play();
                }
            }
            else
            {
                _isFire = false;
                gun.Stop();
            }
        }
    }
}
