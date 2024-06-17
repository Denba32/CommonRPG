using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : BaseUI 
{
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        UIManager.Instance.SetCanvas(gameObject, true);
    }
    public virtual void ClosePopUI()
    {
        UIManager.Instance.ClosePopupUI(this);
    }
}