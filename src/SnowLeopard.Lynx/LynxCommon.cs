using System;
using System.Collections.Generic;

namespace SnowLeopard.Lynx
{
    /// <summary>
    /// Lynx common extensions.
    /// </summary>
    public static class LynxCommonExtensions
    {
        #region 洗牌算法

        /// <summary>
        /// 洗牌算法
        /// Shuffle the specified input.
        /// </summary>
        /// <returns>The shuffle.</returns>
        /// <param name="input">Input.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T[] Shuffle<T>(this T[] input)
        {
            var rand = new Random();
            for (var i = input.Length - 1; i >= 0; i--)
            {
                var randomIndex = rand.Next(0, i + 1);
                var itemAtIndex = input[randomIndex];

                input[randomIndex] = input[i];
                input[i] = itemAtIndex;
            }
            return input;
        }

        /// <summary>
        /// 洗牌算法
        /// Shuffle the specified input.
        /// </summary>
        /// <returns>The shuffle.</returns>
        /// <param name="input">Input.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static List<T> Shuffle<T>(this List<T> input)
        {
            var rand = new Random();
            for (var i = input.Count - 1; i >= 0; i--)
            {
                var randomIndex = rand.Next(0, i + 1);
                var itemAtIndex = input[randomIndex];

                input[randomIndex] = input[i];
                input[i] = itemAtIndex;
            }
            return input;
        }

        #endregion
    }
}
