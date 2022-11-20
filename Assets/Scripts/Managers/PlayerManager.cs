using UnityEngine;
using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject;
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

        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerParticleController particleController;

        #endregion
        
        #endregion

        private void Awake()
        {
            GetReferences();
            SendDataToControllers();
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
            CoreGameSignals.Instance.onInteractionWithBorder += OnInteractionWithBorder;
            CoreGameSignals.Instance.onInteractionWithHookEntry += OnInteractionWithEntry;
            CoreGameSignals.Instance.onInteractionWithHookExit += OnInteractionWithHookExit;
            ScoreSignals.Instance.onGetPecfectCount += OnGetPerfectCount;
            ScoreSignals.Instance.onUpdateScore += OnChangeMoveDirection;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactivateMovement;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onInteractionWithBorder -= OnInteractionWithBorder;
            CoreGameSignals.Instance.onInteractionWithHookEntry -= OnInteractionWithEntry;
            CoreGameSignals.Instance.onInteractionWithHookExit -= OnInteractionWithHookExit;
            ScoreSignals.Instance.onUpdateScore -= OnChangeMoveDirection;
            ScoreSignals.Instance.onGetPecfectCount -= OnGetPerfectCount;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_PlayerData").Data;

        private void GetReferences()
        {
            Data = GetPlayerData();
        }

        private void SendDataToControllers()
        {
            movementController.SetData(Data);
        }

        private void OnPlay()
        {
            movementController.SetMoveDirection();
        }
        private void OnReset()
        {
            movementController.StopPlayer();
        }

        private void OnActivateMovement()
        {
            movementController.SetSuitableSituation(true);
        }

        private void OnDeactivateMovement()
        {
            movementController.SetSuitableSituation(true);
        }

        private void OnChangeMoveDirection()
        {
            movementController.SetMoveDirection();
        }

        private void OnInteractionWithBorder(bool isLeft)
        {
            movementController.ReturnLoopPos(isLeft);
        }

        private void OnInteractionWithEntry(bool isInEntry)
        {
            physicsController.SetEntrySituation(isInEntry);
        }
        
        private void OnInteractionWithHookExit(bool isInExit)
        {
            physicsController.SetExitSituation(isInExit);
        }

        private void OnGetPerfectCount(ushort count)
        {
            particleController.SetPerfectCount(count);
        }
    }
}
