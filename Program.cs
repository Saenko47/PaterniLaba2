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
            Table testTable = new Table(8, 8, place);

            place.PlaceOnBoard(new PlaceOnBoardRequest { race = Race.Human, raceAmountFly = 0, raceAmountRide = 0, raceMeleeAmount = 1, raceRangeAmount = 1 }, testTable._board);

            for (int x = 0; x < testTable.xLen; x++) 
            {
                for (int y = 0; y < testTable.yLen; y++) 
                {
                    Console.Write(testTable._board[x, y].placeForUnit == null ? "-" : "+");
                }
                Console.WriteLine();
            }

            var move = new Move(testTable);

            move.MoveLeft(testTable._board[0, 0]);
            Console.WriteLine();
            for (int x = 0; x < testTable.xLen; x++)
            {
                for (int y = 0; y < testTable.yLen; y++)
                {
                    Console.Write(testTable._board[x, y].placeForUnit == null ? "-" : "+");
                }
                Console.WriteLine();
            }

        }
    }
}
