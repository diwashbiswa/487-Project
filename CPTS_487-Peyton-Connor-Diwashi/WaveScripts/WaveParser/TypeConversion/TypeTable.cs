using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Xml;
using System.IO;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// Converts XML string specifiers to numerical types or enums.
    /// </summary>
    public static class TypeTable
    {
        public static dynamic Get(string attribute, string value)
        {
            switch(attribute)
            {
                case "timeseconds":
                    return Int32.Parse(value);
                case "spawner":
                    return GetSpawner(value);
                case "movement":
                    return GetMovement(value);
                case "lifespan":
                    return Int32.Parse(value);
                case "speed":
                    return Int32.Parse(value);
                case "firerate":
                    return Double.Parse(value);
                case "health":
                    return Int32.Parse(value);
                case "position":
                    return GetPosition(value);
                case "type":
                    return GetEntityType(value);
                default:
                    throw new Exception("TypeTable attribute: " + attribute + " could not be parsed.");
            }
        }

        private static EntityFactory.EntitiyType GetEntityType(string value)
        {
            switch(value)
            {
                case "grunt1":
                    return EntityFactory.EntitiyType.Grunt1;
                case "grunt2":
                    return EntityFactory.EntitiyType.Grunt2;
                case "boss1":
                    return EntityFactory.EntitiyType.Boss1;
                case "boss2":
                    return EntityFactory.EntitiyType.Boss2;
                case "player":
                    return EntityFactory.EntitiyType.Player;
                default:
                    throw new Exception("Entity Type: " + value + " could not be parsed.");
            }
        }

        private static Vector2 GetPosition(string value)
        {
            string[] xy = value.Split(',');
            return new Vector2(Int32.Parse(xy[0]), Int32.Parse(xy[1]));
        }

        private static MovementFactory.MovementType GetMovement(string value)
        {
            switch(value)
            {
                case "bounce":
                    return MovementFactory.MovementType.Bounce;
                case "cardinal":
                    return MovementFactory.MovementType.CardinalSouth;
                case "keyboard":
                    return MovementFactory.MovementType.Keyboard;
                case "none":
                    return MovementFactory.MovementType.None;
                default:
                    throw new Exception("Movement: " + value + " could not be parsed.");
            }
        }

        private static SpawnerFactory.SpawnerType GetSpawner(string value)
        {
            switch(value)
            {
                case "cardinalsouth":
                    return SpawnerFactory.SpawnerType.CardinalSouth;
                case "targeted":
                    return SpawnerFactory.SpawnerType.Targeted;
                case "keyboard":
                    return SpawnerFactory.SpawnerType.Keyboard;
                case "none":
                    return SpawnerFactory.SpawnerType.None;
                default:
                    throw new Exception("SpawnerType: " + value + " could not be parsed.");
            }
        }
    }
}
