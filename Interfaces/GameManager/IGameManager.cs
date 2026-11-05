using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.GameManager
{
    internal interface IGameManager
    {
        void StartGame(CreateGameRequest req);
    }
}
