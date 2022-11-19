using System;
using Data.ValueObject;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class HookPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private HookManager manager;

        #endregion

        #region Private Variables

        private bool _isPassedPlayer;
        private bool _isEnterPlayer;

        #endregion

        #endregion
        
    }
}