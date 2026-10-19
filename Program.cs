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
            var place = new PlaceRaceOnBoard(new CreateUnitByRace(new CreateHuman(), new CreateOrc(), new CreateElf()));
            Table testTable = new Table(4, 4, place);
            
            IGameManager gameManager = new GameManager(testTable,
                new Move(testTable), 
                new FindEnemyToAttack(), 
                new Render(),
                new Ai(new Attack(new DamageCalculator()),
                new Move(testTable), new FindEnemyToAttack()));
            gameManager.StartGame();


        }
    }
}
