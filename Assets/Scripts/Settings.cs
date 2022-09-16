using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Slider AudioSlider;
    [SerializeField]
    private DropdownMenu QualityLevel;

    private float AudioLevel;


    void OnDropdownValueChanged (DropdownMenu changed)
    {
    }

    private void Start()
    {
        AudioSlider.maxValue = 10;
        AudioSlider.minValue = 0;
        AudioLevel = 5;
        AudioSlider.value = AudioLevel;
    }

    public void Update()
    {
        AudioLevel = AudioSlider.value;

    }

    



}
