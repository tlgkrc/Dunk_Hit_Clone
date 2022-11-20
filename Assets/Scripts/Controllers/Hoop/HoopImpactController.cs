using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Hoop
{
    public class HoopImpactController : MonoBehaviour
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
                CoreGameSignals.Instance.onHasImpact?.Invoke();
                manager.PlayImpactEffect();
            }
        }
    }
}