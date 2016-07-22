namespace Santase.Logic.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using Santase.Logic.Cards;
    using Santase.Logic.Players;
    using Santase.Logic.PlayerActionValidate;
    using Santase.Logic.RoundStates;

    [TestFixture]
    public class ExtendedPlayerActionValidatorTests
    {
        [Test]
        public void IsValid_shouldReturnFalse_IfPassedANullValue()
        {
            var playerActionValidator = new PlayerActionValidator();

            var deck = new Deck();

            // First Round CANNOT announce.
            var stateManager = new StateManager();
            var context = new PlayerTurnContext(
                new StartRoundState(stateManager),
                deck.TrumpCard,
                deck.CardsLeft,
                30,
                30);

            var testPlayerHand = new List<Card>()
            {
                new Card(CardSuit.Club, CardType.Jack),
                new Card(CardSuit.Diamond, CardType.Nine)
            };

            var playerActionCard = new Card(CardSuit.Diamond, CardType.Jack);

            var actualOutcome = playerActionValidator.IsValid(
                null,
                context,
                testPlayerHand);

            Assert.IsFalse(actualOutcome);
        }
    }
}
