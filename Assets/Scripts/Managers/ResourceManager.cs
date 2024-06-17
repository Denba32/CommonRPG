using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

// Load, LoadAll, LoadAsync 등
public class ResourceManager
{
    private Dictionary<string, object> sourceDict = new Dictionary<string, object>();

    public ResourceManager()
    {
        Init();
    }

    private void Init()
    {

    }


    public T Load<T>(string path) where T : UnityEngine.Object
    {
        int index = path.LastIndexOf('/');
        string sourceName = path.Substring(index + 1);

        if(sourceDict.ContainsKey(sourceName))
            return sourceDict[sourceName] as T;

        T source = Resources.Load<T>(path);

        if (source == null)
        {
            Debug.Log("리소스가 없습니다");
            return null;
        }
        sourceDict.Add(source.name, source);

        return source;
    }

    public T[] LoadAll<T>(string path) where T : UnityEngine.Object
    {
        T[] sources = Resources.LoadAll<T>(path);
        
        for(int i = 0; i < sources.Length; i++)
        {
            if (!sourceDict.ContainsKey(sources[i].name))
                sourceDict.Add(sources[i].name, sources[i]);
        }
        return sources;
    }

    public GameObject Instantiate(GameObject go, Vector3 origin, Quaternion rotation)
    {
        return UnityEngine.Object.Instantiate(go, origin, rotation);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        int index = path.LastIndexOf('/');
        string sourceName = path.Substring(index + 1);

        GameObject go = null;
        if (sourceDict.ContainsKey(sourceName))
        {
            GameObject obj = sourceDict[sourceName] as GameObject;
            go = UnityEngine.Object.Instantiate(obj, parent);
            go.name = obj.name;
            return go;
        }

        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        go = UnityEngine.Object.Instantiate(original, parent);
        go.name = original.name;

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        UnityEngine.Object.Destroy(go);
    }
}
