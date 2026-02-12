using NewYearGreetingCard.Models;

namespace NewYearGreetingCard.Services;

/// <summary>
/// 提供賀卡與祝賀詞的查詢服務。
/// </summary>
public interface IGreetingCardService
{
    /// <summary>
    /// 取得所有賀卡。
    /// </summary>
    /// <returns>賀卡清單。</returns>
    IReadOnlyList<GreetingCard> GetAllCards();

    /// <summary>
    /// 依據賀卡識別碼取得對應賀卡。
    /// </summary>
    /// <param name="id">賀卡識別碼。</param>
    /// <returns>找到的賀卡，若不存在則為 <c>null</c>。</returns>
    GreetingCard? GetCardById(int id);

    /// <summary>
    /// 依據賀卡識別碼取得其祝賀詞清單。
    /// </summary>
    /// <param name="cardId">賀卡識別碼。</param>
    /// <returns>祝賀詞清單，若賀卡不存在則回傳空集合。</returns>
    IReadOnlyList<GreetingMessage> GetMessagesByCardId(int cardId);
}
