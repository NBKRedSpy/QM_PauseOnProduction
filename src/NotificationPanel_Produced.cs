using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_PauseOnProduction
{


    [HarmonyPatch(typeof(NotificationPanel), nameof(NotificationPanel.Update))]
    public static class NotificationPanel_Produced_Update
    {

        public static void Postfix()
        {
            if (NotificationPanel_Produced.ShowProduction)
            {
                NotificationPanel_Produced.ShowProduction = false;
                SpaceshipScreen spaceship = SingletonMonoBehaviour<SpaceUI>.Instance.SpaceshipScreen;
                spaceship.MagnumProductionWindow.Show();

            }

        }
    }


    [HarmonyPatch(typeof(NotificationPanel), nameof(NotificationPanel.AddItemProducedNotify))]
    public static class NotificationPanel_Produced
    {

        public static bool ShowProduction { get; set; } = false;

        public static void Postfix()
        {
            SpaceshipScreen spaceship = SingletonMonoBehaviour<SpaceUI>.Instance.SpaceshipScreen;

            spaceship.Show();

            ShowProduction = true;
            
        }
    }

}
