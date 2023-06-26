using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int point;
    public TMP_Text text;

    public void StartSetting()
    {
        point = 0;
        TextSetting();
    }

    public void TextSetting()
    {
        text.text = point.ToString();
    }
}
