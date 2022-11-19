using System;
using Data.ValueObject;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Private Variables

        private bool _isMoveRightSide;
        private bool _isSuitableForNewForce;
        private PlayerData _playerData;

        #endregion

        #endregion


        private void Awake()
        {
            _isMoveRightSide = true;
        }

        public void SetData(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void SetSuitableSituation(bool isSuit)
        {
            _isSuitableForNewForce = isSuit;
        }

        public void SetMoveDirection()
        {
            if (_isMoveRightSide)
            {
                _isMoveRightSide = false;
            }
            else
            {
                _isMoveRightSide = true;
            }
        }

        private void FixedUpdate()
        {
            if (!_isSuitableForNewForce) return;
            ApplyForce();
            _isSuitableForNewForce = false;
        }

        private void ApplyForce()
        {
            if (_isMoveRightSide)
            {
                rigidbody.AddForce(new Vector3(_playerData.AppliedForce.x,_playerData.AppliedForce.y,0),ForceMode.Impulse);
            }
            else
            {
                rigidbody.AddForce(new Vector3(-_playerData.AppliedForce.x,_playerData.AppliedForce.y,0),ForceMode.Impulse);
            }
        }

        public void ReturnLoopPos(bool isLeft)
        {
            var pos = transform.position;
            if (isLeft)
            {
                
                rigidbody.transform.position = new Vector3(pos.x + _playerData.LoopDistance,
                    pos.y, pos.z);
            }
            else
            {
                rigidbody.transform.position = new Vector3(pos.x - _playerData.LoopDistance,
                    pos.y, pos.z);
            }
        }
    }
}