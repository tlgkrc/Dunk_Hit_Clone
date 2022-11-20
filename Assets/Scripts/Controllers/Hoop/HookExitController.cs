using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Hoop
{
    public class HookExitController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private HoopManager manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CoreGameSignals.Instance.onInteractionWithHookExit?.Invoke(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CoreGameSignals.Instance.onInteractionWithHookExit?.Invoke(false);
            }
        }
    }
}