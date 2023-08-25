using System.Collections.Generic;
using System.Linq;

namespace Spider.Model
{
    public class Game
    {
        private const int TotalPiles = 10;
        private const int FullRunLength = 13;
        
        private readonly Deck _deck;
        private readonly List<Pile> _piles;

        public Game()
        {
            _deck = new Deck();
            _piles = Enumerable.Range(0, TotalPiles).Select(_ => new Pile()).ToList();
            DealInitialCards();
        }

        public void MoveCards(int sourcePileIndex, int cardIndex, int destPileIndex)
        {
            if (IsInvalidMove(sourcePileIndex, cardIndex, destPileIndex)) return;

            var cardsToMove = _piles[sourcePileIndex].RemoveCardsFrom(cardIndex);
            _piles[destPileIndex].AddCards(cardsToMove);

            CheckAndRemoveFullRunIfPresent(destPileIndex);
        }

        public void DrawFromStock()
        {
            if (HasEmptyPile) return;
            _piles.ForEach(pile => pile.AddCards(new[] { _deck.Draw() }));
        }

        public bool IsGameWon => AllPilesAreEmpty && _deck.IsEmpty;

        private void DealInitialCards()
        {
            foreach (var pile in _piles)
            {
                var cardsToDeal = _piles.IndexOf(pile) < 4 ? 6 : 5;
                pile.AddCards(Enumerable.Repeat(_deck.Draw(), cardsToDeal));
            }
        }

        private void CheckAndRemoveFullRunIfPresent(int pileIndex)
        {
            if (_piles[pileIndex].Cards.Count < FullRunLength) return;

            var startIndexOfRun = _piles[pileIndex].Cards.Count - FullRunLength;
            if (_piles[pileIndex].IsValidRun(startIndexOfRun))
            {
                _piles[pileIndex].RemoveCardsFrom(startIndexOfRun);
            }
        }

        private bool IsInvalidMove(int sourcePileIndex, int cardIndex, int destPileIndex)
            => sourcePileIndex == destPileIndex || !_piles[sourcePileIndex].IsValidRun(cardIndex);

        private bool AllPilesAreEmpty => _piles.All(pile => pile.IsEmpty);

        private bool HasEmptyPile => _piles.Any(pile => pile.IsEmpty);
    }
}
