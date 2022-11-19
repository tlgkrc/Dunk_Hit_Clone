using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class HoopEntryController : MonoBehaviour
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
                CoreGameSignals.Instance.onInteractionWithHookEntry?.Invoke(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CoreGameSignals.Instance.onInteractionWithHookEntry?.Invoke(false);
            }
        }
    }
}