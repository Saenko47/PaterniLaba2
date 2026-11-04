using PaterniLab2.Factories.Units;
using PaterniLab2.Interfaces;
using PaterniLab2.Models.Table;
using PaterniLab2.Requests.Table;
using PaterniLab2.Services.GameManager;
using PaterniLab2.Services.Placement;
using PaterniLab2.Services.Render;
using PaterniLab2.Tools;
using Spectre.Console;
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

           GameTable testTable = new GameTable(4, 4, placementManager);
            
            IRender render = new RenderTable(testTable);

            IGameManager gameManager = new NewGameManager(new SetTable(testTable), new CombatGenerator(render, testTable));
            try
            {
                gameManager.StartGame(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }



        }
    }
}
