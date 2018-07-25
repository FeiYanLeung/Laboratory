using System;

namespace Laboratory.DesignPatterns.Observer
{

    /// <summary>
    /// 水壶加热功能
    /// </summary>
    public class Heater
    {
        public string type = "水壶型号";
        public string area = "产地";

        public delegate void BoilWaterHandler(object sender, BoiledEventArgs e);
        public event BoilWaterHandler Boiled;

        public class BoiledEventArgs : EventArgs
        {
            public readonly int temperature;
            public BoiledEventArgs(int temperature)
            {
                this.temperature = temperature;
            }
        }

        protected virtual void OnBoiled(BoiledEventArgs e)
        {
            // if (Boiled != null) Boiled(this, e);
            Boiled?.Invoke(this, e);
        }

        public void BoilWater()
        {
            int temperature = 0;
            while (temperature < 100)
            {
                temperature++;
                if (temperature > 95)
                {
                    OnBoiled(new BoiledEventArgs(temperature));
                }
            }
        }
    }

    /// <summary>
    /// 水壶报警器
    /// </summary>
    public class Alarm
    {
        public void MakeAlert(object sender, Heater.BoiledEventArgs e)
        {
            var heater = (Heater)sender;
            Console.WriteLine($"Alarm ({heater.area} - {heater.type}): 滴滴滴，水已经 {e.temperature}℃ 了");
        }
    }

    /// <summary>
    /// 水壶显示器
    /// </summary>
    public class Display
    {
        public void ShowMessage(object sender, Heater.BoiledEventArgs e)
        {
            var heater = (Heater)sender;
            Console.WriteLine($"Display ({heater.area} - {heater.type}): 水快烧开了，当前温度 {e.temperature}℃ ");
        }
    }

    /// <summary>
    /// 水壶
    /// </summary>
    public class Kettle
    {
        /// <summary>
        /// 烧开水
        /// </summary>
        public void TurnOn()
        {
            var heater = new Heater();
            var alarm = new Alarm();

            heater.Boiled += alarm.MakeAlert;
            heater.Boiled += (new Display()).ShowMessage;

            heater.BoilWater();
        }
    }
}
