using Data.ValueObject;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rb;

        #endregion

        #region Private Variables

        private bool _isMoveRightSide;
        private bool _isSuitableForNewForce;
        private const float _maxVelocityMagnitude = 4.5f;
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
            ClampVelocity();
            if (!_isSuitableForNewForce) return;
            ApplyForce();
            _isSuitableForNewForce = false;
        }

        private void ApplyForce()
        {
            if (_isMoveRightSide)
            {
                rb.AddForce(new Vector3(_playerData.AppliedForce.x,_playerData.AppliedForce.y,0),ForceMode.Force);
            }
            else
            {
                rb.AddForce(new Vector3(-_playerData.AppliedForce.x,_playerData.AppliedForce.y,0),ForceMode.Force);
            }
        }

        public void ReturnLoopPos(bool isLeft)
        {
            var pos = transform.position;
            if (isLeft)
            {
                
                rb.transform.position = new Vector3(pos.x + _playerData.LoopDistance,
                    pos.y, pos.z);
            }
            else
            {
                rb.transform.position = new Vector3(pos.x - _playerData.LoopDistance,
                    pos.y, pos.z);
            }
        }

        public bool GetMoveDirection()
        {
            return _isMoveRightSide;
        }

        public void StopPlayer()
        {
            rb.velocity =Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        private void ClampVelocity()
        {
            if (rb.velocity.magnitude>_maxVelocityMagnitude)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, _maxVelocityMagnitude);
            }
        }
    }
}