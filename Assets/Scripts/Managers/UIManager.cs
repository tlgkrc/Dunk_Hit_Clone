using System.Collections.Generic;
using Controllers;
using Controllers.UI;
using DG.Tweening;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private Text scoreText;
        [SerializeField] private TextMeshProUGUI bestScore;
        [SerializeField] private TextMeshProUGUI increaseText;
        [SerializeField] private TextMeshProUGUI perfectText;
        [SerializeField] private List<GameObject> panels;
        [SerializeField] private UITimeController timeController;

        #endregion

        #region Private Variables
        
        private UIPanelController _uiPanelController;
        
        #endregion

        #endregion

        private void Awake()
        {
            _uiPanelController = new UIPanelController();
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGameFailed += OnGameFailed;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetScoreText += OnSetScoreText;
            UISignals.Instance.onSetBestScore += OnSetBestScore;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onGameFailed -= OnGameFailed;
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetScoreText -= OnSetScoreText;
            UISignals.Instance.onSetBestScore -= OnSetBestScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnOpenPanel(UIPanels panelParam)
        {
            _uiPanelController.OpenPanel(panelParam , panels);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            _uiPanelController.ClosePanel(panelParam , panels);
        }

        private void OnPlay()
        {
            perfectText.gameObject.SetActive(false);
            increaseText.gameObject.SetActive(false);
            timeController.ResetTime();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnGameFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        private void OnSetScoreText(ushort score,ushort increaseFactor,bool isPerfect)
        {
            timeController.ResetTime();
            scoreText.transform.DOScale(Vector3.one * 1.3f, .3f).SetEase(Ease.InOutElastic).OnComplete(
                () => scoreText.transform.DOScale(Vector3.one, .3f));
            scoreText.text = score.ToString();
            increaseText.gameObject.SetActive(true);
            increaseText.text = "+" + (increaseFactor+1).ToString();
            increaseText.transform.DOLocalMoveY(increaseText.transform.localPosition.y+140f, 1f).
                OnComplete(ResetIncreaseText);

            if (isPerfect )
            {
                perfectText.gameObject.SetActive(true);
                perfectText.text = "Perfect x" + increaseFactor.ToString();
            }
            else
            {
                perfectText.gameObject.SetActive(false);
            }
        }

        private void ResetIncreaseText()
        {
            increaseText.gameObject.SetActive(false);
            increaseText.transform.DOLocalMoveY(increaseText.transform.localPosition.y - 140f, .3f);
        }

        private void OnSetBestScore(ushort best)
        {
            bestScore.text = "BEST " + best.ToString();
        }

        private void OnReset()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
        }

        public void TryAgain()
        {
            CoreGameSignals.Instance.onReset?.Invoke();
        }
        
    }
}