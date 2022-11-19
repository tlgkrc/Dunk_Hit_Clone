using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class AreaPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AreaManager manager;

        #endregion

        #region Private Variables

        private bool _isLeftSide;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CoreGameSignals.Instance.onInteractionWithBorder?.Invoke(_isLeftSide);
            }
        }

        public void SetSide(bool isLeft)
        {
            _isLeftSide = isLeft;
        }
    }
}