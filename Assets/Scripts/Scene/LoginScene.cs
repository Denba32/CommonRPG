using System.Collections;
using UnityEngine;

public class LoginScene : BaseScene
{
    LoginUI loginUI = null;
    protected override void Clear()
    {

    }

    protected override void Init()
    {
        if(loginUI == null)
        {
            loginUI = UIManager.Instance.ShowPopupUI<LoginUI>();
            SoundManager.Instance.Play("Sound/BGM/LoginBGM");
        }
    }
    protected override IEnumerator Start()
    {
        yield return StartCoroutine(SceneManagerEX.Instance.LoadManagerScene());
    }
}