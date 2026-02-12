using NewYearGreetingCard.Data;
using NewYearGreetingCard.Models;

namespace NewYearGreetingCard.Services;

/// <summary>
/// 賀卡服務實作，從靜態資料集中提供查詢功能。
/// </summary>
public sealed class GreetingCardService : IGreetingCardService
{
    private readonly ILogger<GreetingCardService> _logger;

    /// <summary>
    /// 初始化賀卡服務。
    /// </summary>
    /// <param name="logger">記錄查詢與診斷資訊的記錄器。</param>
    public GreetingCardService(ILogger<GreetingCardService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public IReadOnlyList<GreetingCard> GetAllCards()
    {
        _logger.LogInformation("取得所有賀卡資料，共 {CardCount} 張。", GreetingCardData.Cards.Count);
        return GreetingCardData.Cards;
    }

    /// <inheritdoc />
    public GreetingCard? GetCardById(int id)
    {
        GreetingCard? card = GreetingCardData.Cards.FirstOrDefault(candidate => candidate.Id == id);

        if (card is null)
        {
            _logger.LogWarning("查無賀卡資料，Id={CardId}。", id);
            return null;
        }

        _logger.LogInformation("取得賀卡資料，Id={CardId}，風格={StyleName}。", id, card.StyleName);
        return card;
    }

    /// <inheritdoc />
    public IReadOnlyList<GreetingMessage> GetMessagesByCardId(int cardId)
    {
        GreetingCard? card = GetCardById(cardId);
        return card?.Messages ?? Array.Empty<GreetingMessage>();
    }
}
