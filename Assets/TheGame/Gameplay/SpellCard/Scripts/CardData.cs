using System;

namespace Gameplay
{
    public struct CardData
    {
        public CardType Type;
        public SpellType Spell;
        public ElementType Element;
        public int Power;
        public int CritChance;
        public float CritMultiplier;
        public int ElementalPower;

        public static CardData operator + (CardData a, CardData b)
        {
            return new CardData
            {
                Element = StatsHelper.AddElements(a.Element, b.Element),
                Power = a.Power + b.Power,
                CritChance = a.CritChance + b.CritChance,
                CritMultiplier = a.CritMultiplier + b.CritMultiplier,
                ElementalPower = StatsHelper.CalculateElementalPower(a.Element, b.Element, a.ElementalPower, b.ElementalPower)
            };
        }


    }

    public static class StatsHelper
    {
        public static ElementType AddElements(ElementType a, ElementType b)
        {
            var tempResult = (ElementType)((int)a + (int)b);
            if (Enum.IsDefined(typeof(ElementType), tempResult))
            {
                return tempResult;
            }
            return a;
        }

        public static int CalculateElementalPower(ElementType elementA, ElementType elementB, int powerA, int powerB)
        {
            ElementType resultType = AddElements(elementA, elementB);
            if (resultType.Equals(ElementType.none) || resultType.Equals(ElementType.Fail))
            {
                return 0;
            }
            int result = powerA + powerB;
            return result;
        }
    }
}

