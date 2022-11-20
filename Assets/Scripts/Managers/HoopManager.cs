using System;
using Controllers;
using Controllers.Hoop;
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
        [SerializeField] private HoopEntryController entryController;
        
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
            ScoreSignals.Instance.onUpdateScore += OnResetPerfectState;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateScore -= OnResetPerfectState;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void PlayImpactEffect()
        {
            hoopEntry.transform.DOLocalRotate(new Vector3(3, 0, 0), .5f);
            Invoke(nameof(StopImpactEffect),1);
        }

        public void StopImpactEffect()
        {
            hoopEntry.transform.DOLocalRotate(Vector3.zero, .5f);
        }

        private void OnResetPerfectState()
        {
            entryController.ResetEntryState();
        }
    }
}