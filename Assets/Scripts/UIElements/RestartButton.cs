using Rx;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    [RequireComponent(typeof(Button))]

    public class RestartButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                MessageBroker.Default
                    .Publish (MessageBase.Create (
                        this, 
                        ServiceShareData.MSG_RESET_LEVEL
                    ));
            });
        }
        
        private void OnDestroy()
        {
            _button?.onClick.RemoveAllListeners();
        }
    }
}