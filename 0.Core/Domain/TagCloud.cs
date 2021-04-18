using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class TagCloud
    {
        public string ID { get; set; }

        public string Name { get; set; }

        /// <summary>大小(顯示用)</summary>
        public double Weight
        {
            get
            {
                Random random = new Random();
                double result = 0;
                result += random.NextDouble();
                result += random.Next(5, 12);
                return Math.Round(result, 1, MidpointRounding.AwayFromZero);
            }
        }
    }
}
