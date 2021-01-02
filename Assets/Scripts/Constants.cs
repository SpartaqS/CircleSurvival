using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CircleSurvival
{
    public static class Constants
    {
        public const float STARTING_BOMB_INTERVAL = 2f;
        public const float STARTING_MIN_TIMER = 2f;
        public const float STARTING_MAX_TIMER = 4f;
        public const float NON_DISARMABLE_TIMER = 3f;
        public const float NON_DISARMABLE_CHANCE = 0.1f; // how many bombs should spawn as black bombs

        public const float MINIMUM_BOMB_INTERVAL = 0.5f;
        public const float LOWEST_MIN_TIMER = 0.4f;
        public const float LOWEST_MAX_TIMER = 0.9f;

        public const int UI_EXPLOSION_OFFSET = 4;
    }
}
