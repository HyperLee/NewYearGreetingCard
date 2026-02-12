using Microsoft.Extensions.Logging;

using Moq;

using NewYearGreetingCard.Models;
using NewYearGreetingCard.Services;
using NewYearGreetingCard.Tests.TestData;

using Xunit;

namespace NewYearGreetingCard.Tests.Unit.Services;

/// <summary>
/// GreetingCardService 單元測試。
/// </summary>
public sealed class GreetingCardServiceTests
{
    private readonly GreetingCardService _service;

    public GreetingCardServiceTests()
    {
        Mock<ILogger<GreetingCardService>> logger = new();
        _service = new GreetingCardService(logger.Object);
    }

    [Fact]
    public void GetAllCards_ReturnsAtLeast10Cards()
    {
        IReadOnlyList<GreetingCard> cards = _service.GetAllCards();

        Assert.NotNull(cards);
        Assert.True(cards.Count >= 10, $"Expected at least 10 cards, but got {cards.Count}.");
    }

    [Fact]
    public void GetAllCards_ReturnsDistinctStyles()
    {
        IReadOnlyList<GreetingCard> cards = _service.GetAllCards();

        int distinctStyleCount = cards.Select(c => c.Style).Distinct().Count();
        Assert.Equal(cards.Count, distinctStyleCount);
    }

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public void GetCardById_ExistingId_ReturnsCard(int id)
    {
        GreetingCard? card = _service.GetCardById(id);

        Assert.NotNull(card);
        Assert.Equal(id, card.Id);
        Assert.False(string.IsNullOrWhiteSpace(card.StyleName));
        Assert.False(string.IsNullOrWhiteSpace(card.CssClass));
    }

    [Fact]
    public void GetCardById_NonExistingId_ReturnsNull()
    {
        GreetingCard? card = _service.GetCardById(GreetingCardTestData.InvalidCardId);

        Assert.Null(card);
    }

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public void GetMessagesByCardId_ValidId_ReturnsAtLeast5Messages(int cardId)
    {
        IReadOnlyList<GreetingMessage> messages = _service.GetMessagesByCardId(cardId);

        Assert.NotNull(messages);
        Assert.True(messages.Count >= 5, $"Expected at least 5 messages for card {cardId}, but got {messages.Count}.");
    }

    [Fact]
    public void GetMessagesByCardId_InvalidId_ReturnsEmptyCollection()
    {
        IReadOnlyList<GreetingMessage> messages = _service.GetMessagesByCardId(GreetingCardTestData.InvalidCardId);

        Assert.NotNull(messages);
        Assert.Empty(messages);
    }

    public static TheoryData<int> ValidCardIds
    {
        get
        {
            TheoryData<int> data = new();
            foreach (int id in GreetingCardTestData.ValidCardIds)
            {
                data.Add(id);
            }

            return data;
        }
    }
}
