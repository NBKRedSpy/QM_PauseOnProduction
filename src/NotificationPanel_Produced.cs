using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_PauseOnProduction
{

    [HarmonyPatch(typeof(NotificationPanel), nameof(NotificationPanel.AddItemProducedNotify))]
    public static class NotificationPanel_Produced
    {
        public static void Postfix()
        {
            SpaceshipScreen spaceship = SingletonMonoBehaviour<SpaceUI>.Instance.SpaceshipScreen;

            spaceship.Show();
            spaceship.MagnumProductionWindow.Show();
        }
    }

}
