using System;
using System.Collections.Generic;
using Controllers;
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
        [SerializeField] private List<GameObject> panels;
        [SerializeField] private Text scoreText;
        [SerializeField] private TextMeshProUGUI increaseText;
        [SerializeField] private TextMeshProUGUI perfectText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Image remainingTimeRatioImage;
        
        
        
        #endregion

        #region Private Variables
        
        private UIPanelController _uiPanelController;
        private float _currentTime;
        private const float _timeBorder = 70;
        private bool _changedColor = false;

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
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetScoreText += OnSetScoreText;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetScoreText -= OnSetScoreText;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            SetTimer();
        }

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
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
            perfectText.gameObject.SetActive(false);
            increaseText.gameObject.SetActive(false);
            _currentTime = _timeBorder;
        }

        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
            OnPlay();
        }

        private void OnSetScoreText(ushort score,ushort increaseFactor)
        {
            _currentTime = _timeBorder;
            var isPerfect = CoreGameSignals.Instance.onHasImpact?.Invoke();
            
            scoreText.transform.DOScale(Vector3.one * 1.3f, .3f).SetEase(Ease.InOutElastic).OnComplete(
                () => scoreText.transform.DOScale(Vector3.one, .3f));
            scoreText.text = score.ToString();
            increaseText.gameObject.SetActive(true);
            increaseText.text = "+" + (increaseFactor+1).ToString();
            increaseText.transform.DOLocalMoveY(increaseText.transform.localPosition.y+140f, 1f).
                OnComplete(ResetIncreaseText);
            
            if (isPerfect == true)
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

        private void SetTimer()
        {
            if (_currentTime<=0)
            {
                _currentTime -= Time.deltaTime;
                DisplayTimer(_currentTime);
                SetRatioImage();
            }
            else
            {
                CoreGameSignals.Instance.onGameFailed?.Invoke();
            }
        }

        private void DisplayTimer(float remainingTime)
        {
            float minutes = Mathf.FloorToInt(remainingTime / 60);  
            float seconds = Mathf.FloorToInt(remainingTime % 60); 
            timeText.text = $"{minutes:00}:{seconds:00}";
        }
        
        private void SetRatioImage()
        {
            remainingTimeRatioImage.fillAmount = _currentTime / _timeBorder;
            if (_currentTime/_timeBorder<.5f && !_changedColor)
            {
                remainingTimeRatioImage.material.color = new Color(255, 52, 84, 255);
                _changedColor = true;
            }
        }
    }
}