using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertUI : PopupUI
{
    public TMP_Text alertTxt;
    public Button btnOk;

    private void OnEnable()
    {
        btnOk.onClick.AddListener(CloseAlertUI);
    }
    private void OnDisable()
    {
        btnOk.onClick.RemoveListener(CloseAlertUI);
    }
    public override void ClosePopUI()
    {
        base.ClosePopUI();
    }

    public override void Init()
    {
        base.Init();
    }
    private void CloseAlertUI()
    {
        ClosePopUI();
    }
    public void SetText(string message)
    {
        if(alertTxt != null) alertTxt.text = message;
    }
}
