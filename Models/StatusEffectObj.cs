using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models
{
    internal class StatusEffectObj
    {
        public bool isFrendly { get; private set; } //that like - or + to parametrs
        
        public bool isOneTimer { get; private set; } //Something like "is activate 1 time", like we have fire that inflict damage every turn and heal, that do his thinh one time
        public StatusEffect statusEffect { get; private set; }

        public int turnsRamains { get; private set; }

        public int value { get; private set; }

        public StatusEffectObj(bool isFrendly, bool isOneTimer, StatusEffect statusEffect, int turnsRamains, int value) 
        {
            this.isFrendly = isFrendly;
            this.isOneTimer = isOneTimer;
            this.statusEffect = statusEffect;
            this.turnsRamains = turnsRamains;
            this.value = value;
        }

        public bool DecreamentTunsRamain() 
        {
            turnsRamains--;
            return turnsRamains > 0;
        }

        public void AddTurns(int increament) 
        {
            if (increament <= 0) return;

            turnsRamains += increament;
        }


    }
}
