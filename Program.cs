using PaterniLab2.Factories.Units;
using PaterniLab2.Interfaces;
using PaterniLab2.Interfaces.GameManager;
using PaterniLab2.Interfaces.Placement;
using PaterniLab2.Models.Table;
using PaterniLab2.Requests.Table;
using PaterniLab2.Services.AttackAndDamageProccesing;
using PaterniLab2.Services.Behaviour;
using PaterniLab2.Services.GameManager;
using PaterniLab2.Services.MoveHandlers;
using PaterniLab2.Services.Placement;
using PaterniLab2.Services.Render;
using PaterniLab2.Singltons;
using PaterniLab2.Tools;
using Spectre.Console;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2
{
    internal class Program
    {
        static CreateGameRequest ForTestPurpuses() 
        {
            var snippet = SaveLoadSystem.LoadGame();

            var req = new Requests.Table.CreateGameRequest
            {
               
                reqs = new PlaceOnBoardRequest[]
                      {

                    new PlaceOnBoardRequest
                    {
                    race = Race.Human,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 5,
                    raceRangeAmount = 0,
                    isAi = false,
                    isHandlyPlacement = false,

                    },


                    new PlaceOnBoardRequest
                    {
                    race = Race.Orc,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 3,
                    raceRangeAmount = 2,
                    isAi = true,
                    isHandlyPlacement = false,

                    }

                      }
            };
            return req;
        }
        static void Main(string[] args)
        {
            var unitGenerator = new CreateUnitByRace(new CreateHuman(), new CreateOrc(), new CreateElf());

            bool isLoad = true;

            IPlacementMode automatic = new PlacementMode(new AutomaticPlace2Units(unitGenerator), null);
            IPlacementMode handly = new PlacementMode(new HandlyPlace2Units(unitGenerator), null);
            IPlacementManager placementManager = new PlacementManager(automatic, handly);

           GameTable testTable = new GameTable(4, 4, placementManager);
            
            IRender render = new RenderTable(testTable);
            GlobalInputOutput.Instance.SetRender(render);

            AiUnitBehaviour aiBeha = new AiUnitBehaviour(new AiMoveHandler(new Move()), new Attack(new DamageCalculator()), new FindUnit());
            PlayerUnitBehaviour humanBeha = new PlayerUnitBehaviour(new HumanKeyboardMoveInput(new Move()), new Attack(new DamageCalculator()), new FindUnit());

            IGameManager gameManager = new GameManager(new SetTable(testTable, aiBeha, humanBeha), new CombatGenerator(testTable));
            try
            {
                gameManager.StartGame(ForTestPurpuses());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }



        }
    }
}
