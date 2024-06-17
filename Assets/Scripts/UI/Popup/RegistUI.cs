using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RegistUI : PopupUI
{
    public TMP_InputField inputId;
    public TMP_InputField inputPass;
    public TMP_InputField inputCheckPass;

    public Button btn_CheckID;
    public Button btn_Regist;
    public Button btn_Close;

    public Image imgIdChecker;

    public TMP_Text txtAlert;

    private Coroutine coRegist;

    private bool canUseId = false;
    private bool canUsePassword = false;

    private void OnEnable()
    {
        btn_Close.onClick.AddListener(ClosePopUI);
        btn_CheckID.onClick.AddListener(CheckID);
        btn_Regist.onClick.AddListener(Regist);

        inputId.onValueChanged.AddListener(OnIdChange);
        inputCheckPass.onValueChanged.AddListener(CheckPass);
    }



    private void OnDisable()
    {
        btn_CheckID.onClick.RemoveListener(CheckID);
        btn_Regist.onClick.RemoveListener(Regist);
        btn_Close.onClick.RemoveListener(ClosePopUI);
        inputCheckPass.onValueChanged.RemoveListener(CheckPass);

    }
    public override void Init()
    {
        base.Init();
        btn_Regist.interactable = false;
        SetAlert("");
    }

    public override void ClosePopUI()
    {
        base.ClosePopUI();
    }

    private void CheckPass(string checkPass)
    {
        if(inputPass.text.Equals(checkPass))
        {
            SetAlert("비밀번호가 일치합니다.", Color.green);
            canUsePassword = true;
            if (canUseId) btn_Regist.interactable = true;
        }
        else
        {
            SetAlert("비밀번호가 일치하지 않습니다.", Color.red);
            canUsePassword = false;
            btn_Regist.interactable = false;
        }
    }

    private void OnIdChange(string checkId)
    {
        canUseId = false;
    }

    private async void Regist()
    {
        if(canUseId && canUsePassword)
        {
            bool result = await Managers.GetService<NetworkManager>().Regist(inputId.text, inputPass.text);

            if(result)
            {
                ClosePopUI();
            }
            else
            {
                UIManager.Instance.ShowAlert("회원가입에 실패했습니다.");
            }
        }
    }


    private async void CheckID()
    {
        if(inputId.text != string.Empty)
        {
            canUseId = await Managers.GetService<NetworkManager>().CheckId(inputId.text);
            
            if(canUseId)
            {
                UIManager.Instance.ShowAlert("사용 가능한 아이디 입니다.");
                imgIdChecker.gameObject.SetActive(true);
                
            }
            else
            {
                UIManager.Instance.ShowAlert("중복된 아이디 입니다.");
                imgIdChecker.gameObject.SetActive(false);
            }
        }
    }

    private void SetAlert(string message)
    {
        if(message.Equals(""))
        {
            txtAlert.text = "";
            txtAlert.gameObject.SetActive(false);
        }
        txtAlert.text = message;
        txtAlert.gameObject.SetActive(true);
    }

    private void SetAlert(string message, Color color)
    {
        if (message.Equals(""))
        {
            txtAlert.text = "";
            txtAlert.gameObject.SetActive(false);
        }
        txtAlert.text = message;
        txtAlert.color = color;
        txtAlert.gameObject.SetActive(true);
    }
}
