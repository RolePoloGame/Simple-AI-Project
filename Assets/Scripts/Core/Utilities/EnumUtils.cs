using System;

namespace Assets.Scripts.Core.Utilities
{
    public static class EnumUtils
    {
        public static int GetEnumLength<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return Enum.GetNames(typeof(T)).Length;
        }

        public static T GetRandomEnum<T>(int seed) where T : struct, IConvertible
        {
            Random random = new(seed);
            return (T)(object)random.Next(GetEnumLength<T>());
        }
    }
}