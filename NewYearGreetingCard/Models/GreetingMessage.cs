namespace NewYearGreetingCard.Models;

/// <summary>
/// 祝賀詞記錄，包含祝福文字內容與分類。
/// <example>
/// <code>
/// var message = new GreetingMessage(1, "馬到成功，新年快樂！", "馬年專屬");
/// </code>
/// </example>
/// </summary>
/// <param name="Id">祝賀詞唯一識別碼</param>
/// <param name="Text">祝賀詞文字內容</param>
/// <param name="Category">祝賀詞分類（如「通用」、「馬年專屬」、「事業」、「健康」等）</param>
public record GreetingMessage(int Id, string Text, string Category);
