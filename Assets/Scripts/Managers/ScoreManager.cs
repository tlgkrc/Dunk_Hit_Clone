using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshPro greatText;

        #endregion

        #region Private Variables

        private ushort _score;
        private ushort _bestScore;
        private ushort _perfectCounter;
        private Vector3 _greatTextPos;

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
            ScoreSignals.Instance.onGetHookPos += OnGetHookPosition;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onGameFailed += OnGameFailed;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateScore -= OnUpdateScore;
            ScoreSignals.Instance.onGetHookPos -= OnGetHookPosition;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onGameFailed -= OnGameFailed;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnPlay()
        {
            greatText.gameObject.SetActive(false);
            UISignals.Instance.onSetScoreText?.Invoke(_score,_perfectCounter);
        }

        private void OnUpdateScore()
        {
            bool isPerfect = (bool)CoreGameSignals.Instance.onHasImpact?.Invoke();
            if (isPerfect)
            {
                _perfectCounter++;
                greatText.gameObject.SetActive(true);
                greatText.transform.position = _greatTextPos;
                greatText.text = "GREAT\n+" + _perfectCounter.ToString();
                greatText.transform.DOMoveY(greatText.transform.position.y + 3f, 1.2f).
                    OnComplete(ResetGreatText);
            }
            else
            {
                _perfectCounter = 0;
            }

            _score += (ushort)(_perfectCounter+1);
            UISignals.Instance.onSetScoreText?.Invoke(_score,_perfectCounter);
            ScoreSignals.Instance.onGetPecfectCount?.Invoke((ushort)(_perfectCounter+1));
        }

        private void OnGetHookPosition(Vector3 hookPos)
        {
            _greatTextPos = hookPos;
        }
        
        private void ResetGreatText()
        {
            greatText.gameObject.SetActive(false);
        }

        private void OnGameFailed()
        {
            ushort oldScore = SaveManager.LoadValue("BestScore", _score);
            if (oldScore >= _score) return;
            SaveManager.SaveValue("BestScore",_score);
            UISignals.Instance.onSetBestScore?.Invoke(_score);
        }
    }
}