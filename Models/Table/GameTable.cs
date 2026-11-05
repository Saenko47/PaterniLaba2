using PaterniLab2.Interfaces;
using PaterniLab2.Interfaces.Placement;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Xml;

namespace PaterniLab2.Models.Table
{
    internal class GameTable: ITable
    {

       public Cell[,] _board { get; set; }
        private readonly IPlacementManager _placeRaceOnBoard;
        public Action<Vector2Int, Vector2Int>? OnSomeoneMove { get; private set; }

        private Vector2Int frames;
        public GameTable(int xLen, int yLen, IPlacementManager placeRaceOnBoard)
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
            if (req.amountOfRacesOnBoard < 2 && req.amountOfRacesOnBoard > 4) throw new Exception("Not correct amount of races on board");
            if (req.reqs.Sum(a => a.entireAmount) > frames.X * frames.Y) throw new Exception("To many units!!!");

            int count = req.amountOfRacesOnBoard;
            bool isTwo = count <= 2;

            _placeRaceOnBoard.SetMode(isTwo);

          
            List<Army> placedArmiesList = new List<Army>();

            for (int k = 0; k < count; ++k)
            {
                
                if (req.reqs[k] != null)
                {
                    var army = _placeRaceOnBoard.Place(req.reqs[k], req.reqs[k].isHandlyPlacement, _board);
                    if (army != null)
                    {
                        placedArmiesList.Add(army);
                    }
                }
            }

            var res = new UnitOnTheBoardByRace { placedArmies = placedArmiesList };

            return res;
        }
    }
}
