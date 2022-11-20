using System.Collections;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class UITimeController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Image remainingTimeRatioImage;

        #endregion

        #region Private Variables

        private bool _changedColor;
        private float _currentTime;
        private const float _timeBorder = 70;

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
                remainingTimeRatioImage.color = new Color(255, 52, 84, 255);
                _changedColor = true;
                StartCoroutine(TimeScaleAnim());
            }
        }

        public void ResetTime()
        {
            _currentTime = _timeBorder;
            StopAllCoroutines();
            remainingTimeRatioImage.color = new Color(0, 235, 255, 255);
        }

        private IEnumerator TimeScaleAnim()
        {
            while ((_currentTime/_timeBorder)<.5f)
            {
                remainingTimeRatioImage.transform.DOScale(1.2f, 1.2f).SetEase(Ease.OutElastic).OnComplete(()
                    => remainingTimeRatioImage.transform.DOScale(1, .8f));
                yield return new WaitForSeconds(2f);
            }
        }
    }
}