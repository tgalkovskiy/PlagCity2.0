using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslatableText : MonoBehaviour
{
    public int TextId;

    [HideInInspector] public Text textUI;
    [HideInInspector] public TooltipText textTooltip;

    private void Awake()
    {
        textUI = GetComponent<Text>();
        if (textUI == null)
            textTooltip = GetComponent<TooltipText>();
    }

    private void Start()
    {
        UIManager.AddTextToTranslate(this);
        UIManager.UpdateText();
    }
}
