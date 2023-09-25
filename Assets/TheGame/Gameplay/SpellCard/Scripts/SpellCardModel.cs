namespace Gameplay
{
    public interface ISpellCardModel : IModel
    {
        public CardData Data { get; }
    }


    public class SpellCardModel : ISpellCardModel
    {
        private readonly CardData _data;
        public CardData Data => _data;

        public SpellCardModel(CardData data)
        {
            _data = data;
        }
    }
}

