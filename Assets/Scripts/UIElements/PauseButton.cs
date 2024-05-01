using System;
using Rx;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private RestartButton _restartButton;
        [SerializeField] private Image _blackout;

        private CompositeDisposable _disposables = new();
        private Button _button;
        private bool _isPause = false;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => SetPause(!_isPause));
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_RESET_LEVEL)
                .Subscribe(_ => SetPause()).AddTo(_disposables);
            
            SetPause();
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
            _button?.onClick.RemoveAllListeners();
        }

        private void SetPause(bool isPause = false)
        {
            _isPause = isPause;
            _restartButton.gameObject.SetActive(isPause);
            _blackout.gameObject.SetActive(isPause);

            Time.timeScale = isPause ? 0 : 1;
        }
    }
}