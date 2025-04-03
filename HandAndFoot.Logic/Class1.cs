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
    Two = 2,
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

        // Shuffle the new deck (I copy and pasted this algorithm)
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
            // Automatically add a fresh deck to the draw pile if it runs out
            AddFreshDeck();
        }

        // Remove and return the top card.
        Card drawnCard = drawDeck[0];
        drawDeck.RemoveAt(0);
        return drawnCard;
    }

    /// If the deck runs out during the draw, a fresh deck is added.
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