using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkManager 
{
    private const string registUrl = "http://denba.store/member/Regist.php";
    private const string checkIdUrl = "http://denba.store/member/CheckId.php";
    private const string loginUrl = "http://denba.store/member/Login.php";

    public async Task<bool> Login(string id, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", id);
        form.AddField("userpass", password);

        using (UnityWebRequest request = UnityWebRequest.Post(loginUrl, form))
        {
            var tcs = new TaskCompletionSource<bool>();
            request.SendWebRequest().completed += (asyncOperation) =>
            {
                tcs.SetResult(true);
            };

            await tcs.Task;

            if (request.responseCode != 200)
            {
                Debug.Log(request.error);
                return false;
            }
            else
            {
                string responseData = request.downloadHandler.text;
                ResponseData response = JsonUtility.FromJson<ResponseData>(responseData);

                if (response.success)
                {
                    Debug.Log("성공");
                    return true;
                }
                else
                {
                    Debug.Log("실패");
                    return false;
                }
            }
        }
    }


    public async Task<bool> Regist(string id, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", id);
        form.AddField("userpass", password);

        using (UnityWebRequest request = UnityWebRequest.Post(registUrl, form))
        {
            var tcs = new TaskCompletionSource<bool>();
            request.SendWebRequest().completed += (asyncOperation) =>
            {
                tcs.SetResult(true);
            };

            await tcs.Task;

            if (request.responseCode != 200)
            {
                Debug.Log(request.error);
                return false;
            }
            else
            {
                string responseData = request.downloadHandler.text;
                ResponseData response = JsonUtility.FromJson<ResponseData>(responseData);

                if (response.success)
                {
                    Debug.Log("성공");
                    return true;
                }
                else
                {
                    Debug.Log("실패");
                    return false;
                }
            }
        }
    }

    public async Task<bool> CheckId(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", id);

        using (UnityWebRequest request = UnityWebRequest.Post(checkIdUrl, form))
        {
            var tcs = new TaskCompletionSource<bool>();
            request.SendWebRequest().completed += (asyncOperation) =>
            {
                tcs.SetResult(true);
            };

            // 작업이 끝나길 기다림
            await tcs.Task;

            if (request.responseCode != 200)
            {
                Debug.Log(request.error);
                return false;
            }
            else
            {
                string responseData = request.downloadHandler.text;
                ResponseData response = JsonUtility.FromJson<ResponseData>(responseData);

                if (response.success)
                {
                    Debug.Log("성공");
                    return true;
                }
                else
                {
                    Debug.Log("실패");
                    return false;
                }
            }
        }
    }

    [System.Serializable]
    private struct ResponseData
    {
        public bool success;
    }
}
