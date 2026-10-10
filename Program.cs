using PaterniLab2.Factories.Units;
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
            Table testTable = new Table(2, 2, place);

            place.PlaceOnBoard(new PlaceOnBoardRequest { race = Race.Human, raceAmountFly = 0, raceAmountRide = 0, raceMeleeAmount = 2, raceRangeAmount = 0 }, testTable._board);
            place.PlaceOnBoard(new PlaceOnBoardRequest { race = Race.Orc, raceAmountFly = 0, raceAmountRide = 0, raceMeleeAmount = 1, raceRangeAmount = 0 }, testTable._board);

            for (int x = 0; x < testTable.xLen; x++) 
            {
                for (int y = 0; y < testTable.yLen; y++) 
                {
                    Console.Write(testTable._board[x, y].placeForUnit == null ? "-" : "+");
                }
                Console.WriteLine();
            }

            var attacFinder = new FindEnemyToAttack();

            var unitToAttack = attacFinder.FindToAttack(testTable._board[0, 0], testTable._board);
            Console.WriteLine(unitToAttack);
            var move = new Move(testTable);

          

        }
    }
}
