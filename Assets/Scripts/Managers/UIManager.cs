using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    List<PopupUI> popupUI = new List<PopupUI>();
    SceneUI sceneUI = null;

    private int currentOrder;


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

        popup.Init();
        popup.sortOrder = currentOrder;

        popupUI.Add(popup);
        popupUI.Sort((x, y) => x.sortOrder.CompareTo(y.sortOrder));
        go.transform.SetParent(Root.transform);

        Util.ActiveCursor(true);

        return popup;
    }


    public void ClosePopupUI(PopupUI popup)
    {
        if (popupUI.Count <= 0)
        {
            return;
        }

        popupUI.Remove(popup);


        currentOrder--;


        popupUI.Sort((x, y) => x.sortOrder.CompareTo(y.sortOrder));

        if (popupUI.Count <= 0)
        {
            Util.ActiveCursor(false);
        }
    }



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
        popupUI.Clear();
        sceneUI = null;
    }
}