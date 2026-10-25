using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests.Table;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PaterniLab2.Models
{
    internal class Table: ITable
    {

       public Cell[,] _board { get; set; }
        private readonly IPlaceRaceOnBoard _placeRaceOnBoard;
        public Action<Vector2Int, Vector2Int>? OnSomeoneMove { get; private set; }

        private Vector2Int frames;
        public Table(int xLen, int yLen, IPlaceRaceOnBoard placeRaceOnBoard)
        {
            _placeRaceOnBoard = placeRaceOnBoard;
            _board = new Cell[xLen, yLen];

            frames = new Vector2Int(xLen, yLen);

            for (int x = 0; x < xLen; x++)
            {
                for (int y = 0; y < yLen; y++)
                {
                    _board[x, y] = new Cell {isEmpty = true };
                }
            }


        }

        public void ClearCell(int x, int y) 
        { 
            if(x >= 0 && x < frames.X && y >= 0 && y < frames.Y) _board[x, y].isEmpty = true;
            
        }

        private void SetCell(int x, int y)
        {
            if (x >= 0 && x < frames.X && y >= 0 && y < frames.Y) _board[x, y].isEmpty = false;
        }
        public bool IsSellFree(int x, int y) 
        {
            if (x < 0 || y < 0) return false;
            if (x >= frames.X || y >= frames.Y) return false;

            return _board[x, y].isEmpty;
        }
        public void MoveUnit(Vector2Int from, Vector2Int to)
        {
    
            ClearCell(from.X, from.Y);

            SetCell(to.X, to.Y);

            OnSomeoneMove?.Invoke(from, to);
        }

        public Vector2Int GetFrames() 
        {
            return frames;
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
