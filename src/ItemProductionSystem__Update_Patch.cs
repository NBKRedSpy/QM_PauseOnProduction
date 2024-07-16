using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
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
        public static bool ShowProduction { get; set; } = false;


        public static void Prefix(MagnumCargo magnumCargo)
        {
            //Get list of items with only one item in the queue
            ProductionLinesStatus = magnumCargo.ItemProduceOrders.Values.Select(x => x.Count == 1).ToList();

        }

        public static void Postfix(MagnumCargo magnumCargo)
        {

            if (ShowProduction)
            {
                //Show the production in a different loop.
                //  Opening the SpaceshipScreen and MagnumProductionWindow in the same loop
                //  causes the production screen to show "Foo <1" instead of the item being completed.

                ShowProduction = false;
                SpaceshipScreen spaceship = SingletonMonoBehaviour<SpaceUI>.Instance.SpaceshipScreen;
                spaceship.MagnumProductionWindow.Show();
            }

            bool queueCompleted = magnumCargo.ItemProduceOrders.Values
                .Zip(ProductionLinesStatus, (current, previous) => new { current, previous })
                .Any(x => x.current.Count == 0 && x.previous == true);

            if (queueCompleted)
            {

                SpaceshipScreen spaceship = SingletonMonoBehaviour<SpaceUI>.Instance.SpaceshipScreen;
                spaceship.Show();

                ShowProduction = true;
            }

        }
    }
}