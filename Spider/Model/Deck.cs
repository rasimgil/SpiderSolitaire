using System;
using System.Collections.Generic;
using System.Linq;

namespace Spider.Model;

public class Deck
{
    private readonly Stack<Card> _cards;
    private static readonly Random Rng = new Random();

    public Deck()
    {
        var suits = new[] { "C", "D", "H", "S" };
        var tempCards = new List<Card>();

        foreach (var suit in suits)
        {
            foreach (var rank in Card.Ranks)
            {
                tempCards.Add(new Card(rank, suit));
                tempCards.Add(new Card(rank, suit));
            }
        }         
        _cards = new Stack<Card>(tempCards.OrderBy(_ => Rng.Next()));
    }

    public Card Draw()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Deck is empty!");
        }
        return _cards.Pop();
    }
    
    public bool IsEmpty => !_cards.Any();
}