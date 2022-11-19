using Signals;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        

        #endregion

        #region Private Variables

        private ushort _score;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateScore += OnUpdateScore;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateScore -= OnUpdateScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnUpdateScore()
        {
            bool isPerfect = (bool)CoreGameSignals.Instance.onHasImpact?.Invoke();
            if (isPerfect)
            {
                _score += 2;
                Debug.Log("Implement boost effect");
            }
            else
            {
                _score += 1;
            }
            UISignals.Instance.onSetScoreText?.Invoke(_score);
        }
    }
}