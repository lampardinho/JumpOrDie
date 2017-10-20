using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuWorldView : MonoBehaviour
{
    public SceneType Type;

    public event Action<SceneType> Selected = type => { };

    private void Start()
    {
        var toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                Selected(Type);
            }
        });
    }
}
