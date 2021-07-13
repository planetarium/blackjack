using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    [SerializeField]
    private NetworkPoller poller = null;

    [SerializeField]
    private Text text = null;

    private void Awake()
    {
        poller.StartPolling();
        poller.postSubject.Subscribe(value =>
        {
            text.text = value;
        }).AddTo(gameObject);
    }
}
