using HarmonyLib;
using MGSC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_PauseOnProduction
{

    [HarmonyPatch(typeof(ItemProductionSystem), nameof(ItemProductionSystem.Update))]
    public static class ItemProductionSystem__Update_Patch
    {

        /// <summary>
        /// The list of production lines that had an order processing before the production loop
        /// </summary>
        public static List<bool> ProductionLinesStatus { get; set; }


        /// <summary>
        /// If true, will wait for the space screen to be displayed.
        /// The purpose is to fix the issue where the after screen raid is inoperable due to the production screen being displayed.
        /// </summary>
        public static bool ShowProductionWhenOnSpaceScreen { get; set; } = false;


        public static void Prefix(MagnumCargo magnumCargo)
        {
            //Get list of items with only one item in the queue
            ProductionLinesStatus = magnumCargo.ItemProduceOrders.Values.Select(x => x.Count == 1).ToList();

        }


        private static IEnumerator ShowScreen()
        {
            yield return UI.Chain<MagnumProductionWindow>().Show();
        }

        public static void Postfix(MagnumCargo magnumCargo)
        {

            if(ShowProductionWhenOnSpaceScreen)
            {
                if(IsOnSpaceScreen())
                {
                    ShowProductionWhenOnSpaceScreen = false;

                    ShowProductionScreen();
                   
                }

                return;
            }

            bool queueCompleted = magnumCargo.ItemProduceOrders.Values
                .Zip(ProductionLinesStatus, (current, previous) => new { current, previous })
                .Any(x => x.current.Count == 0 && x.previous == true);

            if (queueCompleted)
            {

                if (IsOnSpaceScreen())
                {
                    ShowProductionScreen();
                }
                else
                {
                    ShowProductionWhenOnSpaceScreen = true;
                }
            }

        }

        private static void ShowProductionScreen()
        {
            //This logic is from the main Space Hud Screen, and how the button for that
            //opens the ship screen, which opens the production screen.

            UI.Chain<SpaceshipScreen>().HideAll().Invoke(delegate (SpaceshipScreen v)
            {
                v.ResetFocus();
            })
            .Show();

            UI.Get<SpaceshipScreen>().HideAllWindows(hideSideWindow: false);
            UI.Chain<MagnumProductionWindow>().Show();
        }

        private static bool IsOnSpaceScreen()
        {
            return UI.IsShowing<SpaceHudScreen>();
        }
    }
}