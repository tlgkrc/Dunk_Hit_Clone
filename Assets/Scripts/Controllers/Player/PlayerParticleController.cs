using System.Collections;
using System.Collections.Generic;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerParticleController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private PlayerManager manager;

        #endregion

        #region Private Variables

        private List<GameObject> particleGamobjects = new List<GameObject>();

        #endregion

        #endregion

        public void SetPerfectCount(ushort count)
        {
            if (count>3)
            {
                StartCoroutine(CreateParticles());
            }
            else
            {
                StopAllCoroutines();
                ClearParticles();
            }
        }

        private IEnumerator CreateParticles()
        {
            var particleGameObject =
                PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.PerfectParticle.ToString(), transform);
            particleGamobjects.Add(particleGameObject);
            particleGameObject.transform.position = manager.transform.position;
            var particle = particleGameObject.GetComponent<ParticleSystem>();
            particle.Play();
            yield return new WaitForSeconds(.2f);
            particle.Stop();
            StartCoroutine(CreateParticles());
        }

        private void ClearParticles()
        {
            foreach (var element in particleGamobjects)
            {
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.PerfectParticle.ToString(), element);
            }
        }
    }
}