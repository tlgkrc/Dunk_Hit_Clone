using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class AreaBorderController
    {
        public void OpenBorder(Borders border,List<GameObject> borders)
        {
            borders[(int) border].SetActive(true);
        }

        public void CloseBorder(Borders border,List<GameObject> borders)
        {
            borders[(int) border].SetActive(false);
        }
    }
}