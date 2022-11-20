using System;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private Camera cam;

        #endregion

        #endregion

        #region Event Supscirption

        private void OnEnable()
        { 
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onActivePerfectScoreEffect += OnCameraShake;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onActivePerfectScoreEffect -= OnCameraShake;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnCameraShake()
        {
            cam.DOShakePosition(.5f, .5f);
        }
    }
}