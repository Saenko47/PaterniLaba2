using PaterniLab2.Factories.Units;
using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Requests.Table;
using PaterniLab2.Tools;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var unitGenerator = new CreateUnitByRace(new CreateHuman(), new CreateOrc(), new CreateElf());


            IPlacementMode automatic = new PlacementMode(new AutomaticPlace2Units(unitGenerator), null);
            IPlacementMode handly = new PlacementMode(new HandlyPlace2Units(unitGenerator), null);
            IPlacementManager placementManager = new PlacementManager(automatic, handly);

            Table testTable = new Table(4, 4, placementManager);

            IGameManager gameManager = new GameManager(testTable, new Render());
            try
            {
                gameManager.StartGame();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }


        }
    }
}
