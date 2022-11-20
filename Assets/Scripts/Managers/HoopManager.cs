using System;
using Controllers;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class HoopManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject hoopEntry;
        [SerializeField] private HoopPhysicController physicController;
        [SerializeField] private HoopImpactController impactController;
        
        #endregion

        #region Private Variables

        private bool _isPerfect;

        #endregion

        #endregion

        #region Event Supscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onHasImpact += OnHasImpact;
            ScoreSignals.Instance.onUpdateScore += OnResetPerfectState;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onHasImpact -= OnHasImpact;
            ScoreSignals.Instance.onUpdateScore -= OnResetPerfectState;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void PlayImpactEffect()
        {
            _isPerfect = false;
            hoopEntry.transform.DOLocalRotate(new Vector3(3, 0, 0), .5f);
            Invoke(nameof(StopImpactEffect),1);
        }

        public void StopImpactEffect()
        {
            hoopEntry.transform.DOLocalRotate(Vector3.zero, .5f);
        }

        private bool OnHasImpact()
        {
            return _isPerfect;
        }

        private void OnResetPerfectState()
        {
            _isPerfect = true;
        }
    }
}