using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerParticleController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject effect;
        [SerializeField] private PlayerManager manager;

        #endregion

        #region Private Variables

        private ParticleSystem _particle;

        #endregion

        #endregion

        private void Awake()
        {
            _particle = effect.GetComponent<ParticleSystem>();
        }

        public void SetDirection(bool isRight)
        {
            SetParticlePosition(isRight);
        }

        public void SetPerfectCount(ushort count)
        {
            if (count>=3)
            {
                effect.SetActive(true);
                _particle.Play();
            }
            else
            {
                _particle.Stop();
                effect.SetActive(false);
            }
        }

        private void SetParticlePosition(bool isRight)
        {
            if (isRight == true) 
            {
                effect.transform.position  = manager.transform.position - new Vector3(0, 0, -.5f);
            }
            else
            {
                effect.transform.position = manager.transform.position - new Vector3(0, .3f, -.5f);
            }
        }

    }
}