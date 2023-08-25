using System;
using System.Collections.Generic;
using System.Linq;

namespace Spider.Model
{
    public class Pile
    {
        private readonly List<Card> _cards = new();

        public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

        public bool IsEmpty => !_cards.Any();

        public void AddCards(IEnumerable<Card> cards)
        {
            if (cards == null) throw new ArgumentNullException(nameof(cards));
            _cards.AddRange(cards);
        }

        public IEnumerable<Card> RemoveCardsFrom(int index)
        {
            EnsureValidIndex(index);

            var cardsToBeRemoved = _cards.Skip(index).ToList();
            _cards.RemoveRange(index, _cards.Count - index);
            return cardsToBeRemoved;
        }

        public bool IsValidRun(int index)
        {
            EnsureValidIndex(index);

            var suit = _cards[index].Suit;
            var expectedRankValue = _cards[index].RankValue;

            for (var i = index; i < _cards.Count; i++)
            {
                if (!IsMatchingCard(_cards[i], suit, expectedRankValue))
                    return false;

                expectedRankValue--;
            }

            return true;
        }

        public Card? PeekTopCard() => _cards.LastOrDefault();

        public Card? PopTopCard()
        {
            if (IsEmpty) return null;
            var topCard = _cards.Last();
            _cards.RemoveAt(_cards.Count - 1);
            return topCard;
        }

        private bool IsMatchingCard(Card card, string suit, int rankValue) => 
            card.Suit == suit && card.RankValue == rankValue;
        

        private void EnsureValidIndex(int index)
        {
            if (index < 0 || index >= _cards.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }
    }
}