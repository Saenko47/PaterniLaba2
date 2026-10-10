using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models
{
    internal class Table
    {

       public Cell[,] _board { get; set; }
        private readonly IPlaceRaceOnBoard _placeRaceOnBoard;

        public int xLen;
        public int yLen;
        public Table(int xLen, int yLen, IPlaceRaceOnBoard placeRaceOnBoard)
        {
            _placeRaceOnBoard = placeRaceOnBoard;
            _board = new Cell[xLen, yLen];

            this.xLen = xLen;
            this.yLen = yLen;

           InitializeBoard(xLen, yLen);

        }
        private void InitializeBoard(int xLen, int yLen)
        {
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    _board[i, j] = new Cell(i, j);
                }
            }
        }

        


        public UnitOnTheBoardByRace FirstPlacementOfUnits(CreateGameRequest req) 
        { 
        List<BaseUnit> raceA = _placeRaceOnBoard.PlaceOnBoard(new PlaceOnBoardRequest { 
             race = req.raceA.race, raceAmountFly = req.raceA.raceAmountFly, raceAmountRide = req.raceA.raceAmountRide, raceMeleeAmount = req.raceA.raceMeleeAmount, raceRangeAmount = req.raceA.raceRangeAmount }, _board);

        List<BaseUnit> raceB = _placeRaceOnBoard.PlaceOnBoard(new PlaceOnBoardRequest { 
            race = req.raceB.race, raceAmountFly = req.raceB.raceAmountFly, raceAmountRide = req.raceB.raceAmountRide, raceMeleeAmount = req.raceB.raceMeleeAmount, raceRangeAmount = req.raceB.raceRangeAmount }, _board);

         
        var response = new UnitOnTheBoardByRace { raceA = raceA, raceB = raceB };

            return response;



        }
    }
}
