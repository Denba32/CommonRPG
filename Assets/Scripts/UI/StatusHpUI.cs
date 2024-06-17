using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusHpUI : MonoBehaviour
{
    public Camera Cam => Camera.main;

    private void LateUpdate()
    {
        transform.LookAt(Cam.transform.position);
    }
}
