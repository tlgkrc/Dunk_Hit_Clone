using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class HoopOrderManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private GameObject leftHook;
        [SerializeField] private GameObject rightHook;

        #endregion

        #region Private Variables

        private bool _isLeftSideActive;

        #endregion

        #endregion

        #region Event Supscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnSetDefaultSettings;
            ScoreSignals.Instance.onUpdateScore += OnChangeHookSide;
        }
        
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnSetDefaultSettings;
            ScoreSignals.Instance.onUpdateScore -= OnChangeHookSide;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void OnSetDefaultSettings()
        {
            rightHook.SetActive(false);
            float newPosY = Random.Range(-2, 2);
            leftHook.transform.DOMoveY(newPosY, .1f).OnComplete(
                () => leftHook.transform.DOMoveX(leftHook.transform.position.x + 1.5f, 1f).SetEase(Ease.InExpo));
            _isLeftSideActive = true;
        }
        
        private void OnChangeHookSide()
        {
            ChangeHookSide();
        }

        private void ChangeHookSide()
        {
            if (_isLeftSideActive)
            {
                leftHook.transform.DOMoveX(leftHook.transform.position.x - 1.5f, 1f).SetEase(Ease.InExpo).OnComplete(
                    ()=> leftHook.SetActive(false));
                rightHook.SetActive(true);
                float newPosY = Random.Range(-2, 2);
                rightHook.transform.DOMoveY(newPosY, .1f).OnComplete(
                    ()=>rightHook.transform.DOMoveX(rightHook.transform.position.x - 1.5f, 1f).SetEase(Ease.InExpo));
                _isLeftSideActive = false;
            }
            else
            {
                rightHook.transform.DOMoveX(rightHook.transform.position.x + 1.5f, 1f).SetEase(Ease.InExpo).OnComplete(()=>
                    rightHook.SetActive(false));
                leftHook.SetActive(true);
                float newPosY = Random.Range(-2, 2);
                leftHook.transform.DOMoveY(newPosY, .1f).OnComplete(
                    () => leftHook.transform.DOMoveX(leftHook.transform.position.x + 1.5f, 1f).SetEase(Ease.InExpo));
                _isLeftSideActive = true;
            }
        }
    }
}