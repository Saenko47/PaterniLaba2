using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    using PaterniLab2.Interfaces;
    using PaterniLab2.Interfaces.Placement;
    using PaterniLab2.Models;
    using PaterniLab2.Models.Table;
    using PaterniLab2.Requests.Table;
    using PaterniLab2.Singltons;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static PaterniLab2.Tools.Enums;

    internal class CreatorCreateGameRequest: ICreatorCreateGameRequest
    {
        private readonly IPlacementManager _defaultPlacementManager;
        private readonly IFromJsonToArmy _fromJsonToArmy;

        public CreatorCreateGameRequest(IPlacementManager defaultPlacementManager, IFromJsonToArmy fromJsonToArmy)
        {
            _defaultPlacementManager = defaultPlacementManager;
            _fromJsonToArmy = fromJsonToArmy;
        }

        private void ShowStartGameOptions()
        {
            GlobalInputOutput.Instance.AddTextDataToBuffer("1. New Game");
            GlobalInputOutput.Instance.AddTextDataToBuffer("2. Load Game");
            GlobalInputOutput.Instance.AddTextDataToBuffer("Anything else - exit");
        }

        // 1. Выделенный метод для создания доски (стола)
        private GameTable CreateBoard()
        {
            GlobalInputOutput.Instance.AddTextDataToBuffer("=== Creating Board ===");

            GlobalInputOutput.Instance.AddTextDataToBuffer("Enter board Width (X length):");
            int.TryParse(GlobalInputOutput.Instance.Read(), out int xLen);
            if (xLen <= 0) xLen = 10;

            GlobalInputOutput.Instance.AddTextDataToBuffer("Enter board Height (Y length):");
            int.TryParse(GlobalInputOutput.Instance.Read(), out int yLen);
            if (yLen <= 0) yLen = 10;

            return new GameTable(xLen, yLen, _defaultPlacementManager);
        }

        // 2. Выделенный метод для настройки рас с учетом свободной полоски
        // 2. Выделенный метод для настройки ровно 2 рас с выбором расы, юнитов и флага isAi
        private PlaceOnBoardRequest[] ConfigureRaces(GameTable table)
        {
            Vector2Int frames = table.GetFrames();
            int totalCells = frames.X * frames.Y;

            // Требование: оставляем хотя бы 1 свободную полоску (резервируем целую строку = frames.X клеток)
            int maxAllowedCells = totalCells - frames.X;
            if (maxAllowedCells < 0) maxAllowedCells = 0;

            GlobalInputOutput.Instance.AddTextDataToBuffer($"Board size: {frames.X}x{frames.Y} (Max capacity with 1 free row: {maxAllowedCells} units)");

            List<PlaceOnBoardRequest> createdReqs = new List<PlaceOnBoardRequest>();
            PlaceOnBoardRequest[] reqsArray = new PlaceOnBoardRequest[4];

            // Жестко делаем ровно 2 итерации для двух рас
            for (int i = 0; i < 2; i++)
            {
                GlobalInputOutput.Instance.AddTextDataToBuffer($"--- Configure Race #{i + 1} of 2 ---");

                // Вывод доступных рас из enum
                GlobalInputOutput.Instance.AddTextDataToBuffer("Choose Race:");
                GlobalInputOutput.Instance.AddTextDataToBuffer("0. Human");
                GlobalInputOutput.Instance.AddTextDataToBuffer("1. Elf");
                GlobalInputOutput.Instance.AddTextDataToBuffer("2. Orc");
                GlobalInputOutput.Instance.AddTextDataToBuffer("3. Dwarf");

                Race selectedRace = Race.Human; // Дефолт
                if (int.TryParse(GlobalInputOutput.Instance.Read(), out int raceIndex) && Enum.IsDefined(typeof(Race), raceIndex))
                {
                    selectedRace = (Race)raceIndex;
                }
                else
                {
                    GlobalInputOutput.Instance.AddTextDataToBuffer("Invalid choice, defaulting to Human.");
                }

                var placeReq = new PlaceOnBoardRequest();
                placeReq.race = selectedRace;

                GlobalInputOutput.Instance.AddTextDataToBuffer($"Enter Melee amount for {selectedRace}:");
                int.TryParse(GlobalInputOutput.Instance.Read(), out int melee);

                GlobalInputOutput.Instance.AddTextDataToBuffer($"Enter Range amount for {selectedRace}:");
                int.TryParse(GlobalInputOutput.Instance.Read(), out int range);

                GlobalInputOutput.Instance.AddTextDataToBuffer($"Enter Ride amount for {selectedRace}:");
                int.TryParse(GlobalInputOutput.Instance.Read(), out int ride);

                GlobalInputOutput.Instance.AddTextDataToBuffer($"Enter Fly amount for {selectedRace}:");
                int.TryParse(GlobalInputOutput.Instance.Read(), out int fly);

                placeReq.raceMeleeAmount = melee;
                placeReq.raceRangeAmount = range;
                placeReq.raceAmountRide = ride;
                placeReq.raceAmountFly = fly;

                // Выбор: управляется ли раса ИИ
                GlobalInputOutput.Instance.AddTextDataToBuffer($"Is {selectedRace} controlled by AI? (1 - Yes, 0 - No):");
                if (int.TryParse(GlobalInputOutput.Instance.Read(), out int aiChoice))
                {
                    placeReq.isAi = (aiChoice == 1);
                }
                else
                {
                    placeReq.isAi = false; // По умолчанию игрок
                }

                createdReqs.Add(placeReq);
            }

            // Проверка и пропорциональное ограничение юнитов
            int totalRequestedUnits = createdReqs.Sum(r => r.entireAmount + r.raceAmountRide + r.raceAmountFly);

            if (totalRequestedUnits > maxAllowedCells && totalRequestedUnits > 0)
            {
                GlobalInputOutput.Instance.AddTextDataToBuffer($"Warning: Too many units ({totalRequestedUnits}). Scaling down to fit within {maxAllowedCells} cells (leaving 1 free strip).");

                float scale = (float)maxAllowedCells / totalRequestedUnits;

                foreach (var req in createdReqs)
                {
                    req.raceMeleeAmount = (int)(req.raceMeleeAmount * scale);
                    req.raceRangeAmount = (int)(req.raceRangeAmount * scale);
                    req.raceAmountRide = (int)(req.raceAmountRide * scale);
                    req.raceAmountFly = (int)(req.raceAmountFly * scale);
                }
            }

            // Заполняем результирующий массив (вставляем обе расы)
            for (int i = 0; i < createdReqs.Count && i < reqsArray.Length; i++)
            {
                reqsArray[i] = createdReqs[i];
            }

            return reqsArray;
        }

        private CreateGameRequest CreateNewGame()
        {
            GlobalInputOutput.Instance.AddTextDataToBuffer("=== Creating New Game ===");

            // Создаем доску
            GameTable table = CreateBoard();

            // Настраиваем расы
            PlaceOnBoardRequest[] reqs = ConfigureRaces(table);

            return new CreateGameRequest()
            {
                table = table,
                reqs = reqs
            };
        }

        private CreateGameRequest LoadGameRequest()
        {
            var lastSaveGame = SaveLoadSystem.LoadGame();
            if (lastSaveGame == null)
                throw new Exception("U cant load game!!!");

            GlobalInputOutput.Instance.AddTextDataToBuffer("=== Loading Game ===");

            // Создаем таблицу на основе сохраненных Frames
            GameTable loadedTable = new GameTable(lastSaveGame.Frames.X, lastSaveGame.Frames.Y, _defaultPlacementManager);

            return new CreateGameRequest()
            {
                armies = _fromJsonToArmy.ConvertFromJsonToArmy(lastSaveGame.Armies),
                table = loadedTable
            };
        }

        public CreateGameRequest Create()
        {
            ShowStartGameOptions();
            if (int.TryParse(GlobalInputOutput.Instance.Read(), out int decision))
            {
                switch (decision)
                {
                    case 1: return CreateNewGame();
                    case 2: return LoadGameRequest();
                    default: throw new Exception("U exit");
                }
            }
            else throw new Exception("Wrong format!!!");
        }
    }
}
