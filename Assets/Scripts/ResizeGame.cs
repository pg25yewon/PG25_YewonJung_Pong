using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeGame : MonoBehaviour
{
    [SerializeField] private GameObject _resizeGame;
    [SerializeField] private float _shrinkSpeed = 0.1f;

    private bool _isResize = false; 
    private Vector2 _startScale;
    private Vector2 _targetScale;

    void Update()
    {
        // Check for input or any condition to trigger the resizing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Call a method to start the resizing process
            StartResizing();
        }
        
        if (_isResize)
        {
            UpdateResizing();
        }

    }

    private void StartResizing()
    {
        _startScale = _resizeGame.transform.localScale;
        _targetScale = _resizeGame.transform.localScale * 0.5f;

        _isResize = true;
    }

    private void UpdateResizing()
    {
        if (_isResize)
        {

        }
    }
}