using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenProjectionSize : MonoBehaviour
{
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        
        this._camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _camera.orthographicSize += 1f * Time.deltaTime;
        if (_camera.orthographicSize > 17)
            enabled = false;
    }
}
