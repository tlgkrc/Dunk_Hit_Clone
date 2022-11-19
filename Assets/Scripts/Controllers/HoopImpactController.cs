using System;
using Managers;
using UnityEngine;

namespace Controllers
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
                manager.PlayImpactEffect();
            }
        }
    }
}