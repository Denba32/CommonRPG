using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : SceneUI
{
    public TMP_Text TxtLevel;
    public TMP_Text TxtClock;
    public TMP_Text TxtCash;

    public Button btnHome;
    public Button btnCharacter;
    public Button btnQuest;
    public Button btnGacha;
    public Button btnSetting;
    public override void Init()
    {
        base.Init();
    }

    private void LateUpdate()
    {
        TxtClock.text = Util.GetTime();
    }


}
