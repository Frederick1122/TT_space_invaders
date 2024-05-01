using Rx;
using TMPro;
using UniRx;
using UnityEngine;

namespace UIElements
{
    [RequireComponent(typeof(TMP_Text))]
    public class Score : MonoBehaviour
    {
        private TMP_Text _text;

        private CompositeDisposable _disposables = new();

        private int _currentScore = 0;
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_DESTROY_ENEMY)
                .Subscribe(_ =>
                    UpdateScore(_currentScore + 1))
                .AddTo(_disposables);
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_RESET_LEVEL)
                .Subscribe(_ => UpdateScore(0))
                .AddTo(_disposables);
            
            UpdateScore(0);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void UpdateScore(int score)
        {
            _currentScore = score;
            _text.text = _currentScore.ToString();
        }
    }
}