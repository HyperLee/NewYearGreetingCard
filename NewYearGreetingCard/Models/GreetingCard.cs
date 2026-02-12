namespace NewYearGreetingCard.Models;

/// <summary>
/// 賀卡記錄，包含風格資訊、CSS 類別名稱與搭配的祝賀詞列表。
/// <example>
/// <code>
/// var card = new GreetingCard(
///     Id: 1,
///     StyleName: "寫實風",
///     Style: CardStyle.Realistic,
///     Description: "以寫實手法描繪駿馬奔騰的姿態，搭配漸層色彩與光影效果",
///     CssClass: "card-style-realistic",
///     Messages:
///     [
///         new GreetingMessage(1, "馬到成功，新年快樂！", "馬年專屬"),
///         new GreetingMessage(2, "龍馬精神，萬事如意！", "馬年專屬")
///     ]);
/// </code>
/// </example>
/// </summary>
/// <param name="Id">賀卡唯一識別碼</param>
/// <param name="StyleName">風格顯示名稱（繁體中文）</param>
/// <param name="Style">風格列舉值</param>
/// <param name="Description">賀卡描述文字</param>
/// <param name="CssClass">CSS 類別名稱，用於套用對應的視覺樣式</param>
/// <param name="Messages">搭配的祝賀詞列表</param>
public record GreetingCard(
    int Id,
    string StyleName,
    CardStyle Style,
    string Description,
    string CssClass,
    IReadOnlyList<GreetingMessage> Messages);
