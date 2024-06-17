using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Stack<PopupUI> popupStack = new Stack<PopupUI>();
    SceneUI sceneUI = null;

    private int currentOrder = 0;


    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }

    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            currentOrder++;
            canvas.sortingOrder = currentOrder;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }


    public T ShowPopupUI<T>(string name = null) where T : PopupUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.GetService<ResourceManager>().Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        popupStack.Push(popup);


        go.transform.SetParent(Root.transform);

        return popup;
    }


    public void ClosePopupUI(PopupUI popup)
    {
        if (popupStack.Count == 0)
            return;

        if (popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }
    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        PopupUI popup = popupStack.Pop();
        Managers.GetService<ResourceManager>().Destroy(popup.gameObject);
        popup = null;

        currentOrder--;
    }


    // T는 스크립트, name은 prefab의 이름, 팝업을 여는 기능
    public T ShowSceneUI<T>(string name = null) where T : SceneUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.GetService<ResourceManager>().Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        this.sceneUI = sceneUI;


        go.transform.SetParent(Root.transform);

        return sceneUI;
    }
    public void Clear()
    {
        popupStack.Clear();
        sceneUI = null;
    }

    public void ShowAlert(string text)
    {
        ShowPopupUI<AlertUI>().SetText(text);
    }
}