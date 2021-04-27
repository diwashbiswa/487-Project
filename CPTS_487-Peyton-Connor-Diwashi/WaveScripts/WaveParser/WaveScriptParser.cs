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

    public static class WaveScriptParser
    {
        static string exepath = "/CPTS_487-Peyton-Connor-Diwashi/WaveScripts/";
        static string[] acceptableEntityAttributes = { "spawner", "movement", "lifespan", "speed", "firerate", "health", "position", "timeseconds", "type" };
        public static SpriteWave Load(string file, string solution_directory = null)
        {
            XmlDocument doc = new XmlDocument();
            // Attempt to load the xml document, or throw an exception
            if (solution_directory == null)
            {
                try { doc.Load(VisualStudioProvider.TryGetSolutionDirectoryInfo() + exepath + file); }
                catch { throw new Exception("WaveScriptParser: Error Loading: " + file + " at: " + Environment.NewLine + VisualStudioProvider.TryGetSolutionDirectoryInfo() + exepath + file + Environment.NewLine); }
            }
            else
            {
                try { doc.Load(solution_directory + exepath + file); }
                catch { throw new Exception("WaveScriptParser: Error Loading: " + file + " at: " + Environment.NewLine + " " + solution_directory + " " + exepath + file + Environment.NewLine); }
            }

            // Validate the structure of all Waves
            foreach(XmlNode entity in doc.DocumentElement.ChildNodes)
            {
                string e;
                if(!ValidateWaveStructure(entity, out e)) { throw new Exception("WaveScriptParser: Wave structure invalid." + Environment.NewLine + e + Environment.NewLine); }
            }
            SpriteWave wave = new SpriteWave();
            LogConsole.Log("PARSER: --------------------------------");
            // Parse each entity and add to the Sprite Wave
            foreach(XmlNode entity in doc.DocumentElement.ChildNodes)
            {
                ParseEntity(entity, ref wave);
                LogConsole.Log(Environment.NewLine);
            }
            LogConsole.Log("----------------------------------------");
            return wave;
        }

        /// <summary>
        /// Parse an entity entry and add to the wave.
        /// </summary>
        /// <param name="ef"></param>
        /// <param name="sf"></param>
        /// <param name="wave"></param>
        private static void ParseEntity(XmlNode entity, ref SpriteWave wave)
        {
            // Factories
            StandardEntityFactory ef = new StandardEntityFactory(new Rectangle(50, 50, 1180, 600));
            StandardSpawnerFactory sf = new StandardSpawnerFactory();
            StandardMovementFactory mf = new StandardMovementFactory();
            // Default Values
            int timesec = 0, lifespan = 0, speed = 0, health = 1;
            double firerate = 1.0d;
            Vector2 pos = Vector2.Zero;
            SpawnerFactory.SpawnerType stype = SpawnerFactory.SpawnerType.None;
            EntityFactory.EntitiyType etype = EntityFactory.EntitiyType.Grunt1;
            MovementFactory.MovementType mtype = MovementFactory.MovementType.None;
            // Sprites
            BulletSpawner s = null;
            Entity e = null;
            Movement m = null;

            foreach (XmlNode att in entity.ChildNodes)
            {
                switch(att.Name)
                {
                    case "timeseconds":
                        timesec = TypeTable.Get(att.Name, att.InnerText);
                        LogConsole.Log("Parse: Wave time seconds: " + timesec.ToString());
                        break;
                    case "spawner":
                        stype = TypeTable.Get(att.Name, att.InnerText);
                        LogConsole.Log("Parse: Spawner type: " + stype.ToString());
                        break;
                    case "movement":
                        mtype = TypeTable.Get(att.Name, att.InnerText);
                        LogConsole.Log("Parse: Movement type: " + mtype.ToString());
                        break;
                    case "lifespan":
                        lifespan = TypeTable.Get(att.Name, att.InnerText);
                        LogConsole.Log("Parse: Lifespan seconds: " + lifespan.ToString());
                        break;
                    case "speed":
                        speed = TypeTable.Get(att.Name, att.InnerText);
                        LogConsole.Log("Parse: Speed: " + speed.ToString());
                        break;
                    case "firerate":
                        firerate = TypeTable.Get(att.Name, att.InnerText);
                        LogConsole.Log("Parse: Firerate: " + firerate.ToString());
                        break;
                    case "health":
                        health = TypeTable.Get(att.Name, att.InnerText);
                        LogConsole.Log("Parse: Health: " + health.ToString());
                        break;
                    case "position":
                        pos = TypeTable.Get(att.Name, att.InnerText);
                        LogConsole.Log("Parse: Initial position: " + pos.ToString());
                        break;
                    case "type":
                        etype = TypeTable.Get(att.Name, att.InnerText);
                        LogConsole.Log("Parse: Entity type: " + etype.ToString());
                        break;
                }
            }
            e = ef.CreateEnemy(etype);
            e.Speed = (uint)speed;
            e.LifeSpan = (uint)lifespan;
            e.Health = health;
            e.Position = pos;         
            e.WaveTimeSeconds = timesec;
            e.Movement = mf.CreateMovement(mtype, e);
            wave.AddEntitiy(e);
            if (stype != SpawnerFactory.SpawnerType.None)
            {
                s = sf.CreateSpawner(stype, e, SpawnerFactory.BulletType.Green);
                s.FireRateSeconds = firerate;
                s.WaveTimeSeconds = timesec;
                wave.AddSpawner(s);
            }
        }

        /// <summary>
        /// Validate a waves structure
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static bool ValidateWaveStructure(XmlNode entity, out string message)
        {
            message = string.Empty;
            List<string> attributes = new List<string>();
            attributes.AddRange(acceptableEntityAttributes);
            if (entity.Name != "entity")
            {
                message = "Wave child name unrecognized: " + entity.Name;
                return false;
            }
            foreach(XmlNode att in entity.ChildNodes)
            {
                if(!attributes.Contains(att.Name))
                {
                    message = "Entitiy Attribute: " + att.Name + " unrecognized";
                    return false;
                }
            }
            return true;
        }
    }
}
