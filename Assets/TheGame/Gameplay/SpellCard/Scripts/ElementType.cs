namespace Gameplay
{
    public enum ElementType
    {
        none = 0,
        Fire = 1,
        Earth = 11,
        Wind = 21,
        Water = 31,
        Dark = 41,
        Light = 51,

        //experimental
        Magma = 12, //Fire = 1 + Earth = 11
        Ice = 52, //Wind = 21 + Water = 31
        Blood = 72, //Water = 31 + Dark = 41

        Fail = 32, //Fire = 1 + Water = 31 || Earth = 11 + Wind = 21
    }
}

