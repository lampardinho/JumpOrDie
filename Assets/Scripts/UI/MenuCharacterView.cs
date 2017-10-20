using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuCharacterView : MonoBehaviour
{
    public CharacterType Type;

    public event Action<CharacterType> Selected = type => { };

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
