using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene scene;

    void Awake()
    {
        Init();
    }
    protected virtual IEnumerator Start()
    {
        yield return null;
    }
    protected abstract void Init();


    void OnDestroy()
    {
        Clear();
    }

    protected abstract void Clear();

}
