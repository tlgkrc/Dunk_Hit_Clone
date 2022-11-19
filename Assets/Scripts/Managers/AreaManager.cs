using System;
using System.Collections.Generic;
using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> borders;

        #endregion

        #region Private Variables

        private AreaBorderController _areaBorderController;
        private bool _isLeftSideActive;

        #endregion

        #endregion
        
        private void Awake()
        {
            _areaBorderController = new AreaBorderController();
        }

        #region Event Supscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnSetDefaultSettings;
            ScoreSignals.Instance.onUpdateScore += OnChangeBorderSide;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnSetDefaultSettings;
            ScoreSignals.Instance.onUpdateScore -= OnChangeBorderSide;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSetDefaultSettings()
        {
            _areaBorderController.OpenBorder(Borders.LeftBorder, borders);
            _areaBorderController.CloseBorder(Borders.RightBorder, borders);
            _isLeftSideActive = true;
        }

        private void OnChangeBorderSide()
        {
            if (_isLeftSideActive)
            {
                _areaBorderController.OpenBorder(Borders.RightBorder, borders);
                _areaBorderController.CloseBorder(Borders.LeftBorder, borders);
                _isLeftSideActive = false;
            }
            else
            {
                _areaBorderController.OpenBorder(Borders.LeftBorder, borders);
                _areaBorderController.CloseBorder(Borders.RightBorder, borders);
                _isLeftSideActive = true;
            }
        }
        
    }
}