using System;
using System.Collections.Generic;
using System.Linq;

namespace HandAndFoot.Logic;

public enum Suit
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}

public enum Rank
{
    Joker = 1,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

public class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }
    
    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
    
    public bool IsWild()
    {
        return (this.Rank == Rank.Two || this.Rank == Rank.Joker);
    }
}

public class CardDeck
{
    public static List<Card> GenerateDeck()
    {
        List<Card> deck = new List<Card>();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                deck.Add(new Card(suit, rank));
            }
        }
        return deck;
    }
}

public class DrawDeck
{
    private List<Card> drawDeck;
    private Random random;

    public DrawDeck()
    {
        random = new Random();
        drawDeck = new List<Card>();
        AddFreshDeck();
    }

    public void AddFreshDeck()
    {
        List<Card> newDeck = CardDeck.GenerateDeck();

        // Shuffle the new deck
        for (int i = newDeck.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            Card temp = newDeck[i];
            newDeck[i] = newDeck[j];
            newDeck[j] = temp;
        }

        drawDeck.AddRange(newDeck);
    }

    public Card DrawCard()
    {
        if (drawDeck.Count == 0)
        {
            // Automatically add a fresh deck if it runs out.
            AddFreshDeck();
        }

        // Remove and return the top card.
        Card drawnCard = drawDeck[0];
        drawDeck.RemoveAt(0);
        return drawnCard;
    }

    // If the deck runs out during the draw, a fresh deck is added.
    public List<Card> DrawCards(int count)
    {
        List<Card> drawnCards = new List<Card>();
        for (int i = 0; i < count; i++)
        {
            drawnCards.Add(DrawCard());
        }
        return drawnCards;
    }
}

public class Meld
{
    public List<Card> Cards { get; private set; } = new List<Card>();
    public Rank? MeldRank { get; private set; } = null; // Determined by the first natural card added.
    public string Team { get; private set; }
    
    public bool IsClosed => Cards.Count == 7;
    
    public Meld(string team)
    {
        Team = team;
    }
    
    public bool AddCard(Card card)
    {
        if (card.Rank == Rank.Three)
            throw new InvalidOperationException("Cannot add Threes to a meld.");

        if (Cards.Count >= 7)
            throw new InvalidOperationException("Meld is full (closed pile/book).");

        bool isWild = card.IsWild();

        if (!isWild)
        {
            if (MeldRank == null)
            {
                MeldRank = card.Rank;
            }
            else if (card.Rank != MeldRank)
            {
                throw new InvalidOperationException($"Natural card rank must match the meld rank {MeldRank}.");
            }
        }
        
        Cards.Add(card);
        return true;
    }
    
    public bool ValidateMeld()
    {
        int naturalCount = Cards.Count(card => !card.IsWild());
        bool hasWild = Cards.Any(card => card.IsWild());
        
        if (Cards.Count < 3)
            return false;
            
        if (hasWild && naturalCount < 4)
            return false;
            
        return true;
    }
    
    public override string ToString()
    {
        string status = IsClosed ? "Closed Pile (Book)" : "Open Meld";
        string meldRankStr = MeldRank.HasValue ? MeldRank.ToString() : "Not set";
        return $"{status} for team {Team} (Meld Rank: {meldRankStr}): {string.Join(", ", Cards)}";
    }
}
