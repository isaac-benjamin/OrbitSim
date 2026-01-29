using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{

    private UIDocument uiDoc;
    [SerializeField] private VisualTreeAsset planetWindow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        uiDoc = gameObject.GetComponent<UIDocument>();
    }

}
