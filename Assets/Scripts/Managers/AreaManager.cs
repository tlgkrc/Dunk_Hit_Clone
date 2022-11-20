using Controllers;
using UnityEngine;

namespace Managers
{
    public class AreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AreaPhysicController physicController;

        #endregion

        #endregion
        
        private void Start()
        {
            if (transform.position.x <0)
            {
                physicController.SetSide(true);
            }
            else 
            {
                physicController.SetSide(false);
            }
        }
    }
}