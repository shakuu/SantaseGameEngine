namespace Santase.Logic.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using Santase.Logic.Cards;
    using System.Linq;

    [TestFixture]
    public class ExtendedDeckTests
    {
        [Test]
        public void DeckContructor_ShouldReturnAnObjectWhichIsNotNull()
        {
            var newDeckObject = new Cards.Deck();

            Assert.NotNull(newDeckObject);
        }

        [Test]
        public void DeckContructor_ShouldReturnADeckObject()
        {
            var newDeckObject = new Deck();

            var expected = typeof(Deck);
            var actual = typeof(Deck);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeckContructor_ShouldReturnADeckWithTwentyFourCards()
        {
            var expectedNumberOfCards = 24;

            var newDeckObject = new Deck();
            var actualNumberOfCards = newDeckObject.CardsLeft;

            Assert.AreEqual(expectedNumberOfCards, actualNumberOfCards);
        }

        [Test]
        public void DeckContructor_ShouldInitializeTrumpCard()
        {
            var newDeckObject = new Deck();

            var actual = newDeckObject.TrumpCard;

            Assert.NotNull(actual);
        }

        [Test]
        public void DeckConstructor_TrumpCardShouldBeEqualToTheLastCardInTheDeckWhichIsOnIndexZero()
        {
            var newDeckObject = new Deck();
            Card expectedCard = null;

            for (int i = 0; i < 24; i++)
            {
                expectedCard = newDeckObject.GetNextCard();
            }

            Assert.AreEqual(expectedCard, newDeckObject.TrumpCard);
        }

        [Test]
        public void DeckConstructor_DeckMustHaveOneCardOfEachTypeSuitPair()
        {
            var availableCards = new List<Card>();
            var newDeckObject = new Deck();
            var deckSize = 24;

            var cardIsDuplicate = false;
            for (int i = 0; i < deckSize; i++)
            {
                var nextCard = newDeckObject.GetNextCard();

                cardIsDuplicate = availableCards.Any(card => card.Suit == nextCard.Suit && card.Type == nextCard.Type);

                if (cardIsDuplicate)
                {
                    break;
                }
                else
                {
                    availableCards.Add(nextCard);
                }
            }

            Assert.IsFalse(cardIsDuplicate);
        }

        [Test]
        public void GetNextCard_ShouldThrow_IfAllCardsAreDrawn()
        {
            var newDeckObject = new Deck();

            var deckSize = 24;
            for (int i = 0; i < deckSize; i++)
            {
                newDeckObject.GetNextCard();
            }

            Assert.Throws<InternalGameException>(() => newDeckObject.GetNextCard());
        }

        [Test]
        public void GetNextCard_ShouldNotThrow_IfTwentyFourCardsAreDrawn()
        {
            var newDeckObject = new Deck();

            var deckSize = 24;
            for (int i = 0; i < deckSize - 1; i++)
            {
                newDeckObject.GetNextCard();
            }

            Assert.DoesNotThrow(() => newDeckObject.GetNextCard());
        }

        [Test]
        [TestCase(CardSuit.Club, CardType.Ace)]
        [TestCase(CardSuit.Diamond, CardType.Nine)]
        [TestCase(CardSuit.Heart, CardType.Queen)]
        [TestCase(CardSuit.Spade, CardType.King)]
        public void ChangeTrumpCard_NewCardShouldBeTheLastOneInTheDeckAtIndexZero(CardSuit suit, CardType type)
        {
            var newDeckObject = new Deck();
            var expectedCard = new Card(suit, type);

            newDeckObject.ChangeTrumpCard(expectedCard);

            var deckSize = 24;
            for (int i = 0; i < deckSize - 1; i++)
            {
                newDeckObject.GetNextCard();
            }

            var actualLastCard = newDeckObject.GetNextCard();

            Assert.AreSame(expectedCard, actualLastCard);
        }
    }
}
