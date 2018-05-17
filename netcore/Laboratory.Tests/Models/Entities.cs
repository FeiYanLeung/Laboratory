using System;
using System.Collections.Generic;
using System.Threading;

namespace Laboratory.NetCore.Tests.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Patient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int HeartBeatRate { get; set; }
        public bool IsNew { get; set; } = true;

        public float BloodSugar { get; set; } = 5.0f;

        public void IncreaseHeartBeatRate()
        {
            HeartBeatRate = CalculateHeartBeatRate() + 2;
        }

        private int CalculateHeartBeatRate()
        {
            var random = new Random();
            return random.Next(1, 100);
        }

        public void HaveDinner()
        {
            var random = new Random();
            BloodSugar += (float)random.Next(1, 1000) / 100; //  应该是1000
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class Worker
    {
        public string Name { get; set; }

        public abstract double TotalReward { get; }
        public abstract double Hours { get; }
        public double Salary => TotalReward / Hours;

        public List<string> Tools { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Plumber : Worker
    {
        public Plumber()
        {
            Tools = new List<string>()
            {
                "螺丝刀",
                "扳子",
                "钳子"
            };
        }

        public override double TotalReward => 200;
        public override double Hours => 3;
    }


    public class PlayerCharacter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public int Health { get; set; } = 100;

        public void TakeDamage(int earthquake_damage)
        {
            this.Health -= 75;
        }
    }
    public class GameState
    {
        public static readonly int EarthquakeDamage = 25;
        public List<PlayerCharacter> Players { get; set; } = new List<PlayerCharacter>();
        public Guid Id { get; } = Guid.NewGuid();

        public GameState()
        {
            CreateGameWorld();
        }

        public void Earthquake()
        {
            foreach (var player in Players)
            {
                player.TakeDamage(EarthquakeDamage);
            }
        }

        public void Reset()
        {
            Players.Clear();
        }

        /// <summary>
        /// 模拟耗时操作
        /// </summary>
        private void CreateGameWorld()
        {
            // Simulate expensive creation
            Thread.Sleep(2000);
        }
    }

    /// <summary>
    /// 测试时共享上下文
    /// </summary>
    public class GameStateFixture : IDisposable
    {
        public GameState State { get; private set; }

        public GameStateFixture()
        {
            State = new GameState();
        }

        public void Dispose()
        {
            // Cleanup
        }
    }
}
