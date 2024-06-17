using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public RenderTexture renderTexture;

    [ContextMenu("ScreenShot")]
    public void Screenshot()
    {
        if (ShotScreen(renderTexture) != null)
        {
            Debug.Log("��ũ���� ����");
        }
        else
        {
            Debug.Log("��ũ���� ����");
        }
    }
    private Texture2D ShotScreen(RenderTexture externalTexture)
    {
        Texture2D myTexture2D = new Texture2D(externalTexture.width, externalTexture.height);
        if (myTexture2D == null)
        {
            myTexture2D = new Texture2D(externalTexture.width, externalTexture.height);
        }

        //Make RenderTexture type variable
        RenderTexture tmp = RenderTexture.GetTemporary(
            externalTexture.width,
            externalTexture.height,
            0,
            RenderTextureFormat.ARGB32,
            RenderTextureReadWrite.sRGB);

        Graphics.Blit(externalTexture, tmp);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tmp;

        myTexture2D.ReadPixels(new UnityEngine.Rect(0, 0, tmp.width, tmp.height), 0, 0);
        myTexture2D.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tmp);


        byte[] screenshot = myTexture2D.EncodeToPNG();

        File.WriteAllBytes(Application.dataPath + "/Screenshots/test.png", screenshot);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

        return myTexture2D;
    }
}
