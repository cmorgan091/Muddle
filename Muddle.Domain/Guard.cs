using System;

namespace Muddle.Domain
{
    public static class Guard
    {
        /// <summary>
        /// Guard against the given value being null
        /// </summary>
        public static void NotNull<T>(T value, string name)
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>The not null.</summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">the param type</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull<T>(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        public static void AllNotNull(params object[] value)
        {
            foreach (object obj in value)
            {
                NotNull(obj);
            }
        }
    }
}
