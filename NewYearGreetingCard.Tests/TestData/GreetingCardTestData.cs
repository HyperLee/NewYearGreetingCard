namespace NewYearGreetingCard.Tests.TestData;

/// <summary>
/// 測試用的賀卡識別碼資料。
/// </summary>
public static class GreetingCardTestData
{
    /// <summary>
    /// 有效的賀卡識別碼清單。
    /// </summary>
    public static readonly IReadOnlyList<int> ValidCardIds = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

    /// <summary>
    /// 無效的賀卡識別碼，用於驗證找不到資料的情境。
    /// </summary>
    public const int InvalidCardId = 999;
}
