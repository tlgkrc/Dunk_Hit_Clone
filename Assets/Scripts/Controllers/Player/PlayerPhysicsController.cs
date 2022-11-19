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
            if (other.CompareTag("Hook") && _isEnterPlayer)
            {
                ScoreSignals.Instance.onGetHookPos.Invoke(other.transform.position);
                ScoreSignals.Instance.onUpdateScore?.Invoke();
                _isEnterPlayer = false;
            }
        }

        public void SetEntrySituation(bool isInEntry)
        {
            _isEnterPlayer = isInEntry;
        }
    }
}