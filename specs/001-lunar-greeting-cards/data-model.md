# 資料模型：農曆馬年賀卡網站

**Feature**: 001-lunar-greeting-cards
**Date**: 2026-02-12
**Status**: 已完成

## 實體概覽

本系統包含 3 個核心實體，均以 C# 靜態集合定義，不使用任何資料庫。

```text
┌─────────────────┐       ┌─────────────────────┐
│   CardStyle     │       │   GreetingMessage    │
│   (列舉)        │       │   (Record)           │
│─────────────────│       │─────────────────────│
│ Realistic       │       │ Id: int              │
│ Cute            │       │ Text: string         │
│ SciFi           │  1..* │ Category: string     │
│ InkWash         │◄──────│                      │
│ PaperCut        │       └──────────┬───────────┘
│ Classical       │                  │ *
│ Minimalist      │                  │
│ FolkArt         │            ┌─────┴─────┐
│ Illustration    │            │  多對多    │
│ PopArt          │            │  (通用/專屬)│
└────────┬────────┘            └─────┬─────┘
         │ 1                         │ *
         │                  ┌────────┴──────────┐
         └─────────────────►│   GreetingCard     │
                            │   (Record)         │
                            │────────────────────│
                            │ Id: int            │
                            │ StyleName: string  │
                            │ Style: CardStyle   │
                            │ Description: string│
                            │ CssClass: string   │
                            │ Messages: List<..> │
                            └────────────────────┘
```

## 實體定義

### CardStyle（賀卡風格列舉）

代表賀卡的藝術風格分類，用於區分不同賀卡的視覺呈現。

```csharp
/// <summary>
/// 賀卡藝術風格列舉，定義 10 種不同的視覺風格。
/// </summary>
public enum CardStyle
{
    /// <summary>寫實風格，使用漸層與陰影效果</summary>
    Realistic,

    /// <summary>可愛風格，圓潤造型與粉彩配色</summary>
    Cute,

    /// <summary>科幻風格，霓虹光效與未來感線條</summary>
    SciFi,

    /// <summary>水墨風格，墨漬效果與留白構圖</summary>
    InkWash,

    /// <summary>剪紙風格，紅底白紋與鏤空效果</summary>
    PaperCut,

    /// <summary>古典風格，書法字體與印章元素</summary>
    Classical,

    /// <summary>極簡風格，幾何線條與大量留白</summary>
    Minimalist,

    /// <summary>年畫風格，滿版構圖與鮮豔色彩</summary>
    FolkArt,

    /// <summary>插畫風格，手繪線條與溫暖色調</summary>
    Illustration,

    /// <summary>普普風格，波點與高對比撞色</summary>
    PopArt
}
```

**欄位說明**: 共 10 個列舉值，對應 10 種不同藝術風格（滿足 FR-010 要求的至少 5 種）。

### GreetingMessage（祝賀詞）

代表搭配賀卡使用的祝福短語。每句祝賀詞可被獨立複製。

```csharp
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
```

**欄位說明**:

| 欄位 | 型別 | 必填 | 說明 |
|------|------|------|------|
| Id | int | ✅ | 唯一識別碼，從 1 開始遞增 |
| Text | string | ✅ | 祝賀詞文字內容，如「馬到成功，新年快樂！」 |
| Category | string | ✅ | 分類標籤，用於區分通用/專屬祝賀詞 |

**驗證規則**:
- `Id` 必須 > 0
- `Text` 不得為空白或 null
- `Category` 不得為空白或 null

**資料量**: 全站至少 50 句不重複祝賀詞（SC-005）

### GreetingCard（賀卡）

代表一張完整的農曆新年賀卡，包含風格資訊與搭配的祝賀詞列表。

```csharp
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
///     Messages: [
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
```

**欄位說明**:

| 欄位 | 型別 | 必填 | 說明 |
|------|------|------|------|
| Id | int | ✅ | 唯一識別碼，從 1 開始遞增 |
| StyleName | string | ✅ | 繁體中文風格名稱（如「寫實風」） |
| Style | CardStyle | ✅ | 對應的風格列舉值 |
| Description | string | ✅ | 賀卡的文字描述，說明視覺呈現 |
| CssClass | string | ✅ | CSS 類別名稱（如 `card-style-realistic`） |
| Messages | IReadOnlyList&lt;GreetingMessage&gt; | ✅ | 搭配的祝賀詞列表（≥ 5 句） |

**驗證規則**:
- `Id` 必須 > 0
- `StyleName` 不得為空白或 null
- `CssClass` 格式必須為 `card-style-{name}`
- `Messages` 至少包含 5 個元素（FR-003）
- `Messages` 中至少 2 句為該風格專屬（非通用分類）

**資料量**: 至少 10 張賀卡（FR-001）

## 關聯關係

### 賀卡 ↔ 祝賀詞（組合關係，允許共用）

- 每張賀卡**包含**至少 5 句祝賀詞
- 通用祝賀詞（Category = "通用"）可出現在多張賀卡中
- 每張賀卡至少有 2 句專屬祝賀詞（Category ≠ "通用"）
- 關係在 `GreetingCardData.cs` 靜態集合中定義

### 賀卡 → 風格（一對一）

- 每張賀卡對應恰好一種 `CardStyle`
- 每種 `CardStyle` 對應恰好一張賀卡

## 狀態轉換

本系統為純靜態展示，實體無狀態轉換。

## 祝賀詞分類規劃

| 分類 | 說明 | 範例 |
|------|------|------|
| 通用 | 農曆新年通用祝福語 | 新年快樂、恭喜發財、萬事如意 |
| 馬年專屬 | 與馬年生肖相關 | 馬到成功、龍馬精神、一馬當先 |
| 事業 | 事業學業祝福 | 步步高升、事業有成、前程似錦 |
| 健康 | 健康平安祝福 | 身體健康、平安喜樂、福壽康寧 |
| 財運 | 財運祝福 | 財源廣進、黃金萬兩、招財進寶 |
| 家庭 | 家庭和樂祝福 | 闔家歡樂、家和萬事興、天倫之樂 |
