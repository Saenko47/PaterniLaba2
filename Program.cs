using PaterniLab2.Factories;
using PaterniLab2.Factories.Items;
using PaterniLab2.Factories.Skills;
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
       
        static void Main(string[] args)
        {
            var unitGenerator = new CreateUnitByRace(new CreateHuman(), new CreateOrc(), new CreateElf());
            var itemGenerator = new BaseCreateItem(new CreateBoots(), new CreateLeggins(), new CreateChestPlate(), new CreateHelmet(), new CreateJewery());
            var skillCreator = new BaseCreateSkill(new CreateHealthSkill(), new CreateArmorSkill(), new CreateAvoidChanceSkill(), new CreateAttackSkill());
            var stateCreator = new BaseCreateUnitState();
            


           

            IPlacementMode automatic = new PlacementMode(new AutomaticPlace2Units(unitGenerator), null);
            IPlacementMode handly = new PlacementMode(new HandlyPlace2Units(unitGenerator), null);
            IPlacementManager placementManager = new PlacementManager(automatic, handly);

           
            
           

            AiUnitBehaviour aiBeha = new AiUnitBehaviour(new AiMoveHandler(new Move()), new Attack(new DamageCalculator()), new FindUnit());
            PlayerUnitBehaviour humanBeha = new PlayerUnitBehaviour(new HumanKeyboardMoveInput(new Move()), new Attack(new DamageCalculator()), new FindUnit());

            var unitFromJsonCreator = new FromJsonToUnit(unitGenerator, itemGenerator, skillCreator, aiBeha, humanBeha);
            var armyFromJson = new FromJsonToArmy(unitFromJsonCreator);

            ICreatorCreateGameRequest creator = new CreatorCreateGameRequest(placementManager, armyFromJson);

            var requestOnStartGame = creator.Create(); 

            IRender render = new RenderTable(requestOnStartGame.table);
            GlobalInputOutput.Instance.SetRender(render);


            IGameManager gameManager = new GameManager(new SetTable(requestOnStartGame.table, aiBeha, humanBeha), new CombatGenerator(requestOnStartGame.table));
            try
            {
                gameManager.StartGame(requestOnStartGame);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }



        }
    }
}
