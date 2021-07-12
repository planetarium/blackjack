using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPoller : MonoBehaviour
{
    private Coroutine _pollCoroutine = null;
    private const float DELAY = 3f;
    private readonly string urlFormat = "https://jsonplaceholder.typicode.com/posts/{0}";
    public Subject<string> postSubject = new Subject<string>();

    public void StartPolling()
    {
        _pollCoroutine = StartCoroutine(CoPoll());
    }

    public void StopPolling()
    {
        StopCoroutine(_pollCoroutine);
        _pollCoroutine = null;
    }

    private IEnumerator CoPoll()
    {
        var count = 1;
        while (gameObject.activeSelf)
        {
            var url = string.Format(urlFormat, count);
            var www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.error is null)
            {
                var result = www.downloadHandler.text;
                postSubject.OnNext(result);
            }
            else
            {
                Debug.LogError("Error");
                postSubject.OnNext(null);
            }

            ++count;
            yield return new WaitForSecondsRealtime(DELAY);
        }
    }
}
