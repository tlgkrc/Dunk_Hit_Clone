using System.Collections;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class UITimeController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Image remainingTimeRatioImage;
        [SerializeField] private Image timeBackground;

        #endregion

        #region Private Variables

        private bool _changedColor;
        private float _currentTime;
        private const float _timeBorder = 15;

        #endregion
        
        #endregion

        private void Awake()
        {
            _currentTime = _timeBorder;
            _changedColor = false;
        }
        
        private void Update()
        {
            SetTimer();
        }
        
        private void SetTimer()
        {
            if (_currentTime>0)
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
            if ((_currentTime/_timeBorder)<.5f && !_changedColor)
            {
                remainingTimeRatioImage.color = new Color(220, 0, 24, 255);
                _changedColor = true;
                StartCoroutine(TimeScaleAnim());
            }
        }

        public void ResetTime()
        {
            _currentTime = _timeBorder;
            _changedColor = false;
            StopAllCoroutines();
            remainingTimeRatioImage.color = new Color(0, 235, 255, 255);
        }

        private IEnumerator TimeScaleAnim()
        {
            while ((_currentTime/_timeBorder)<.5f)
            {
                remainingTimeRatioImage.transform.DOScale(1.1f, 0.6f).SetEase(Ease.OutElastic).OnComplete(()
                    => remainingTimeRatioImage.transform.DOScale(1, .4f));
                timeBackground.transform.DOScale(1.1f, 0.6f).SetEase(Ease.OutElastic).OnComplete(()
                    => timeBackground.transform.DOScale(1, .4f));
                yield return new WaitForSeconds(1f);
            }
        }
    }
}