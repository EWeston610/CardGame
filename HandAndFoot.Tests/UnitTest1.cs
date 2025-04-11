namespace HandAndFoot.Tests;
using HandAndFoot.Logic;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
     [Test]
        public void ValidateMeld_WithLessThanThreeCards_ReturnsFalse()
        {
            Meld meld = new Meld("TeamA");
            meld.AddCard(new Card(Suit.Spades, Rank.Ace));
            meld.AddCard(new Card(Suit.Hearts, Rank.Ace));
            Assert.IsFalse(meld.ValidateMeld(), "Meld with fewer than 3 cards should be invalid.");
        }

        // Test that a meld with three natural cards is valid.
        [Test]
        public void ValidateMeld_WithThreeNaturalCards_ReturnsTrue()
        {
            Meld meld = new Meld("TeamA");
            meld.AddCard(new Card(Suit.Spades, Rank.Ace));
            meld.AddCard(new Card(Suit.Hearts, Rank.Ace));
            meld.AddCard(new Card(Suit.Clubs, Rank.Ace));
            Assert.IsTrue(meld.ValidateMeld(), "Meld with exactly three natural cards should be valid.");
        }

        // Test that a meld containing wild cards but with less than four natural cards is invalid.
        [Test]
        public void ValidateMeld_WithWildCard_ButLessThanFourNaturalCards_ReturnsFalse()
        {
            Meld meld = new Meld("TeamA");
            meld.AddCard(new Card(Suit.Spades, Rank.Ace));   
            meld.AddCard(new Card(Suit.Hearts, Rank.Ace));   
            meld.AddCard(new Card(Suit.Diamonds, Rank.Two));       
            Assert.IsFalse(meld.ValidateMeld(), "Meld using wild cards must contain at least four natural cards.");
        }

        // Test that a meld containing wild cards with at least four natural cards is valid.
        [Test]
        public void ValidateMeld_WithWildCardAndAtLeastFourNaturalCards_ReturnsTrue()
        {
            Meld meld = new Meld("TeamA");
            meld.AddCard(new Card(Suit.Spades, Rank.Ace));
            meld.AddCard(new Card(Suit.Hearts, Rank.Ace)); 
            meld.AddCard(new Card(Suit.Clubs, Rank.Ace));   
            meld.AddCard(new Card(Suit.Diamonds, Rank.Two));
            meld.AddCard(new Card(Suit.Spades, Rank.Ace)); 
            Assert.IsTrue(meld.ValidateMeld(), "Meld with wild cards and four natural cards should be valid.");
        }

        // Test that adding a card of rank Three throws an exception.
        [Test]
        public void AddCard_DisallowsRankThree_ThrowsException()
        {
            Meld meld = new Meld("TeamA");
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                meld.AddCard(new Card(Suit.Spades, Rank.Three));
            });
            Assert.AreEqual("Cannot add Threes to a meld.", ex.Message);
        }

        // Test that adding a natural card with a rank different from the established meld rank throws an exception.
        [Test]
        public void AddCard_MismatchedNaturalCard_ThrowsException()
        {
            Meld meld = new Meld("TeamA");
            meld.AddCard(new Card(Suit.Spades, Rank.Ace));
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                meld.AddCard(new Card(Suit.Hearts, Rank.King));
            });
            Assert.AreEqual("Natural card rank must match the meld rank Ace.", ex.Message);
        }

        // Test that adding more than 7 cards causes an exception.
        [Test]
        public void AddCard_ExceedingSevenCards_ThrowsException()
        {
            Meld meld = new Meld("TeamA");
            
            meld.AddCard(new Card(Suit.Spades, Rank.Ace));
            meld.AddCard(new Card(Suit.Hearts, Rank.Ace));
            meld.AddCard(new Card(Suit.Clubs, Rank.Ace));
            meld.AddCard(new Card(Suit.Diamonds, Rank.Ace));
            meld.AddCard(new Card(Suit.Spades, Rank.Ace));
            meld.AddCard(new Card(Suit.Hearts, Rank.Ace));
            meld.AddCard(new Card(Suit.Clubs, Rank.Ace));
            
            //eighth card
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                meld.AddCard(new Card(Suit.Diamonds, Rank.Ace));
            });
            Assert.AreEqual("Meld is full (closed pile/book).", ex.Message);
        }

        // Test that the meld is closed when there are seven cards
        [Test]
        public void IsClosed_WhenSevenCards_ReturnsTrue()
        {
            Meld meld = new Meld("TeamA");
            meld.AddCard(new Card(Suit.Spades, Rank.Ace));
            meld.AddCard(new Card(Suit.Hearts, Rank.Ace));
            meld.AddCard(new Card(Suit.Clubs, Rank.Ace));
            meld.AddCard(new Card(Suit.Diamonds, Rank.Ace));
            meld.AddCard(new Card(Suit.Spades, Rank.Ace));
            meld.AddCard(new Card(Suit.Hearts, Rank.Ace));
            meld.AddCard(new Card(Suit.Clubs, Rank.Ace));
            Assert.IsTrue(meld.IsClosed, "Meld should be marked as closed when it contains 7 cards.");
        }
}
