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

        static string[] acceptableEntityAttributes = { "spawners", "movement", "lifespan", "speed", "health", "position", "timeseconds", "type" };
        static string[] acceptableSpawnerAttributes = { "spawnertype", "spawnermovement", "bullettype", "timeseconds", "firerate" };

        /// <summary>
        /// Given a list of xml files, load all waves
        /// </summary>
        /// <param name="files"></param>
        /// <param name="solution_directory"></param>
        /// <returns></returns>
        public static List<SpriteWave> LoadAll(List<string> files, string solution_directory = null)
        {
            List<SpriteWave> waves = new List<SpriteWave>();
            foreach(string file in files)
            {
                waves.Add(Load(file, solution_directory));
            }
            return waves;
        }

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
            // Spawners for this entity
            List<BulletSpawner> new_spawners = null;
            // Factories
            StandardEntityFactory ef = new StandardEntityFactory();
            StandardMovementFactory mf = new StandardMovementFactory();
            // Default Values
            int timesec = 0, lifespan = 0, speed = 0, health = 1;
            Vector2 pos = Vector2.Zero;
            EntityFactory.EntitiyType etype = EntityFactory.EntitiyType.Grunt1;
            MovementFactory.MovementType mtype = MovementFactory.MovementType.None;
            // Sprites
            Entity e = null;

            foreach (XmlNode att in entity.ChildNodes)
            {
                switch (att.Name)
                {
                    case "timeseconds":
                        timesec = TypeTable.Get(att.Name, att.InnerText);
                        LogConsole.Log("Parse: Wave time seconds: " + timesec.ToString());
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
                    default:
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

            foreach (XmlNode att in entity.ChildNodes)
            {
                if (att.Name == "spawners")
                {
                    new_spawners = ParseSpawners(att, e);
                    break;
                }
            }
            foreach (BulletSpawner s in new_spawners)
                wave.AddSpawner(s);
        }

        /// <summary>
        /// Parse the spawners attribute of a wave
        /// </summary>
        /// <param name="spawners"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private static List<BulletSpawner> ParseSpawners(XmlNode spawners, Entity e)
        {
            List<BulletSpawner> new_spawners = new List<BulletSpawner>();
            StandardSpawnerFactory sf = new StandardSpawnerFactory();
            StandardMovementFactory mf = new StandardMovementFactory();
            SpawnerFactory.SpawnerType stype = SpawnerFactory.SpawnerType.None;
            MovementFactory.MovementType spawner_mtype = MovementFactory.MovementType.Mirror;
            SpawnerFactory.BulletType btype = SpawnerFactory.BulletType.Green;
            Movement spawnermovement = null;
            double firerate = 1.0d;
            int timesec = e.WaveTimeSeconds;
            BulletSpawner s = null;

            foreach (XmlNode att in spawners.ChildNodes)
            {
                LogConsole.Log(Environment.NewLine);
                foreach (XmlNode snode in att.ChildNodes)
                {
                    switch (snode.Name)
                    {
                        case "timeseconds":
                            timesec = TypeTable.Get(snode.Name, snode.InnerText);
                            LogConsole.Log("    Parse:Spawner: Wave time seconds: " + timesec.ToString());
                            break;
                        case "spawnertype":
                            stype = TypeTable.Get(snode.Name, snode.InnerText);
                            LogConsole.Log("    Parse:Spawner: Spawner type: " + stype.ToString());
                            break;
                        case "firerate":
                            firerate = TypeTable.Get(snode.Name, snode.InnerText);
                            LogConsole.Log("    Parse:Spawner: Firerate: " + firerate.ToString());
                            break;
                        case "bullettype":
                            btype = TypeTable.Get(snode.Name, snode.InnerText);
                            LogConsole.Log("    Parse:Spawner: Bullet type: " + btype.ToString());
                            break;
                        case "spawnermovement":
                            spawner_mtype = TypeTable.Get(snode.Name, snode.InnerText);
                            LogConsole.Log("    Parse:Spawner: Spawner movement type: " + spawner_mtype.ToString());
                            break;

                    }
                }
                if (stype != SpawnerFactory.SpawnerType.None)
                {
                    s = sf.CreateSpawner(stype, e, btype);
                    s.FireRateSeconds = firerate;
                    s.WaveTimeSeconds = timesec;
                    spawnermovement = mf.CreateMovement(spawner_mtype, e);
                    spawnermovement.ThisSprite = s;
                    s.Movement = spawnermovement;
                    new_spawners.Add(s);
                }
            }
            return new_spawners;
        }

        /// <summary>
        /// Validate a waves external structure
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static bool ValidateWaveStructure(XmlNode entity, out string message)
        {
            message = string.Empty;
            List<string> attributes = new List<string>();
            List<string> spawnerAttrbutes = new List<string>();
            attributes.AddRange(acceptableEntityAttributes);
            spawnerAttrbutes.AddRange(acceptableSpawnerAttributes);
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
            foreach(XmlNode att in entity.ChildNodes)
            {
                if(att.Name == "spawners")
                {
                    foreach(XmlNode snode in att.ChildNodes)
                    {
                        if(snode.Name != "spawner")
                        {
                            message = "Spawner Attribute: " + snode.Name + " unrecognized";
                            return false;
                        }
                        foreach(XmlNode isnode in snode.ChildNodes)
                        {
                            if(!spawnerAttrbutes.Contains(isnode.Name))
                            {
                                message = "Spawner Attribute: " + isnode.Name + " unrecognized";
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
