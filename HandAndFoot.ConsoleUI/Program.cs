using HandAndFoot.Logic;

DrawDeck drawDeck = new DrawDeck();

List<Card> playerHand = drawDeck.DrawCards(11);
Console.WriteLine("Player's Hand:");
foreach (Card card in playerHand)
{
    Console.WriteLine(card);
}

Console.WriteLine("\nDrawing additional cards:");
for (int i = 0; i < 60; i++) 
{
    Console.WriteLine(drawDeck.DrawCard());
}