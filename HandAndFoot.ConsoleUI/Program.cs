using System;
using HandAndFoot.Logic;

namespace HandAndFoot;

    class Program
    {
        static void TupleThingyForTesting(string[] args)
        {
            DrawDeck drawDeck = new DrawDeck();
            List<Card> playerHand = drawDeck.DrawCards(11);
            Console.WriteLine("Player's Hand:");
            foreach (Card card in playerHand)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine("\nDrawing additional cards:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(drawDeck.DrawCard());
            }

            Console.WriteLine("\n=== Meld Testing Console UI ===");
            Console.Write("Enter team name for the new meld: ");
            string teamName = Console.ReadLine();
            Meld meld = new Meld(teamName);
            
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Add Card to Meld");
                Console.WriteLine("2. Validate Meld");
                Console.WriteLine("3. Show Meld Details");
                Console.WriteLine("4. Exit");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.Write("Enter card suit (Clubs, Diamonds, Hearts, Spades): ");
                            string suitInput = Console.ReadLine();
                            if (!Enum.TryParse(typeof(Suit), suitInput, true, out var suitObj))
                            {
                                Console.WriteLine("Invalid suit input.");
                                break;
                            }
                            Suit suit = (Suit)suitObj;
                            
                            Console.Write("Enter card rank (e.g., Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace, Joker): ");
                            string rankInput = Console.ReadLine();
                            if (!Enum.TryParse(typeof(Rank), rankInput, true, out var rankObj))
                            {
                                Console.WriteLine("Invalid rank input.");
                                break;
                            }
                            Rank rank = (Rank)rankObj;
                            
                            Card card = new Card(suit, rank);
                            meld.AddCard(card);
                            Console.WriteLine("Card added successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error adding card: " + ex.Message);
                        }
                        break;
                    case "2":
                        bool isValid = meld.ValidateMeld();
                        Console.WriteLine("Meld is " + (isValid ? "Valid" : "Invalid"));
                        break;
                    case "3":
                        Console.WriteLine("Current Meld Details:");
                        Console.WriteLine(meld.ToString());
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }
    }