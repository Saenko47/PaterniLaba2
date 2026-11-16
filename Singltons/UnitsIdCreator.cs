using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Singltons
{
    internal class UnitsIdCreator
    {
        private static readonly UnitsIdCreator _instance = new UnitsIdCreator();

        private UnitsIdCreator() { }

        public static UnitsIdCreator Instance => _instance;

        private static int idCounter = 0;



        public static int CreateAndReturnId()
        {
            return Interlocked.Increment(ref idCounter);
        }
    }
}
