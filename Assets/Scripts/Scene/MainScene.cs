using System.Collections;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Clear()
    {

    }

    protected override void Init()
    {
        UIManager.Instance.ShowSceneUI<MainSceneUI>();
        SoundManager.Instance.Play("Sound/BGM/MainBGM");
    }

    protected override IEnumerator Start()
    {
        return base.Start();

        
    }
}