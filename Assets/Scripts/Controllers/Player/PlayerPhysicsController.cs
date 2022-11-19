using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;

        #endregion

        #region Private Variables

        private bool _isEnterPlayer;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hook"))
            {
                _isEnterPlayer = true;
            }
            else if (other.CompareTag("Border"))
            {
                manager.SetLoopPos();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Hook") && _isEnterPlayer)
            {
                ScoreSignals.Instance.onUpdateScore?.Invoke();
                _isEnterPlayer = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Hook"))
            {
                _isEnterPlayer = false;
            }
        }
    }
}