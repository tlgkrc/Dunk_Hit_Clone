using Commands;
using UnityEngine;
using Controllers;
using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerMeshController meshController;

        #endregion

        #region Private Variables

        private Rigidbody _rb;
        private PlayerParticuleController _particuleController;


        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;
        
        private void Init()
        {
            var transform1 = transform;
            _rb = GetComponent<Rigidbody>();
            _particuleController = GetComponent<PlayerParticuleController>();
        }

        private void GetReferences()
        {
            Data = GetPlayerData();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactivateMovement;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactivateMovement;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Event Methods

        #region Movement Controller

        
        #endregion

        #region Others

        private void OnPlay()
        {
        }
        private void OnReset()
        {
        }

        private void OnActivateMovement()
        {
            
        }

        private void OnDeactivateMovement()
        {
            
        }

        #endregion

        #endregion

        private void ParticuleState(bool active, Transform instantiateTransform = null)
        {
            if (active)
            {
                _particuleController.StartParticule(instantiateTransform);
            }
            else
            {
                _particuleController.StopParticule();
            }
        }
    }
}
