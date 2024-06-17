using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class LoginUI : PopupUI
{
    [Header("InputFields")]
    public TMP_InputField inputId;
    public TMP_InputField inputPass;

    [Header("Buttons")]
    public Button btnLogin;
    public Button btnRegist;
    
    public override void ClosePopUI()
    {
        base.ClosePopUI();
    }

    private void OnEnable()
    {
        inputPass.onValueChanged.AddListener(FinishInput);
        btnRegist.onClick.AddListener(OpenRegistUI);
        btnLogin.onClick.AddListener(Login);
    }


    private void OnDisable()
    {
        inputPass.onValueChanged.RemoveListener(FinishInput);
        btnRegist.onClick.RemoveListener(OpenRegistUI);
        btnLogin.onClick.RemoveListener(Login);

    }
    private void OpenRegistUI()
    {
        UIManager.Instance.ShowPopupUI<RegistUI>();
    }

    private void FinishInput(string pass)
    {
        if(pass.Length < 9 || pass.Equals(string.Empty))
        {
            btnLogin.interactable = false;
            return;
        }
        btnLogin.interactable = true;
    }


    public override void Init()
    {
        base.Init();

        Reset();

    }

    
    private async void Login()
    {
        if (CheckInputEmpty())
            return;

        bool result = await Managers.GetService<NetworkManager>().Login(inputId.text, inputPass.text);
        
        if(result)
        {
            SceneManagerEX.Instance.ChangeScene(Define.Scene.Main);
        }
        else
        {
            UIManager.Instance.ShowAlert("���̵� �Ǵ� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
        }
    }

    private bool CheckInputEmpty() => inputId.text == string.Empty && inputPass.text == string.Empty;

    private void Reset()
    {
        inputId.text = string.Empty;
        inputPass.text = string.Empty;

        btnLogin.interactable = false;
    }
    private void ActiveRegist()
    {
        UIManager.Instance.ShowPopupUI<RegistUI>();
    }
}
