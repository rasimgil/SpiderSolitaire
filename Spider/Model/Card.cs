using System;

namespace Spider.Model;

public class Card
{
    public static readonly string[] Ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        
    public string Suit { get; }
    private string Rank { get; }

    public static string BackImage => "BACK.png";
        
    public string ImageName => $"{Rank}{Suit}.png";

    public int RankValue => Array.IndexOf(Ranks, Rank);

    public Card(string rank, string suit)
    {
        Rank = rank ?? throw new ArgumentNullException(nameof(rank));
        Suit = suit ?? throw new ArgumentNullException(nameof(suit));
    }

    public override string ToString() => $"{Rank} of {Suit}";
}
