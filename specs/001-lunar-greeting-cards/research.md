# 研究報告：農曆馬年賀卡網站

**Feature**: 001-lunar-greeting-cards
**Date**: 2026-02-12
**Status**: 已完成

## 研究項目

### R-001: 純 CSS/SVG 賀卡藝術實作方式

**決策**: 使用純 CSS 搭配 inline SVG 繪製賀卡圖案

**理由**:
- 純 CSS/SVG 無需外部圖片檔案，減少 HTTP 請求，提升載入速度
- SVG 為向量格式，在任何解析度下都保持清晰
- CSS 動畫可提供簡潔的視覺效果（如漸層、陰影）
- 完全前端渲染，與 ASP.NET Core 靜態資源管線完美整合
- 符合規格要求：「賀卡圖案將完全透過 CSS 樣式與 SVG 向量圖形繪製」

**備選方案**:
- Canvas 2D 繪圖：互動性強但 SEO 不友善、無法選取文字，對靜態賀卡過度複雜
- WebGL 3D 渲染：效能要求高、開發成本大，不符合賀卡簡潔需求
- Base64 內嵌圖片：增加 HTML 大小、不易維護、失去向量優勢

**實作指引**:
- 每種賀卡風格使用獨立的 CSS class（如 `.card-realistic`、`.card-cute`、`.card-scifi`）
- SVG 元素直接內嵌於 Razor Page HTML 中，便於動態控制顏色與尺寸
- 共用 CSS 變數定義新年主題色彩（`--cny-red: #E53935`、`--cny-gold: #FFD700`）
- 馬的圖案使用 SVG `<path>` 元素繪製，搭配 CSS `transform` 調整尺寸

### R-002: 剪貼簿複製 API 與備援機制

**決策**: 優先使用 `navigator.clipboard.writeText()` API，備援使用 `document.execCommand('copy')`

**理由**:
- `navigator.clipboard` 是現代瀏覽器標準 API，Chrome 66+、Firefox 63+、Edge 79+ 皆支援
- 需要 HTTPS 環境（生產環境已符合）或 localhost（開發環境）
- 備援機制確保舊版瀏覽器相容性

**備選方案**:
- 僅使用 `navigator.clipboard`：不支援舊版瀏覽器
- 第三方函式庫（clipboard.js）：增加相依性，對此簡單功能過度設計
- Flash 方案：已淘汰

**實作指引**:
```javascript
async function copyToClipboard(text, feedbackElement) {
    try {
        if (navigator.clipboard && window.isSecureContext) {
            await navigator.clipboard.writeText(text);
            showFeedback(feedbackElement, '已複製！');
        } else {
            fallbackCopy(text, feedbackElement);
        }
    } catch (err) {
        fallbackCopy(text, feedbackElement);
    }
}

function fallbackCopy(text, feedbackElement) {
    const textarea = document.createElement('textarea');
    textarea.value = text;
    textarea.style.position = 'fixed';
    textarea.style.opacity = '0';
    document.body.appendChild(textarea);
    textarea.select();
    try {
        document.execCommand('copy');
        showFeedback(feedbackElement, '已複製！');
    } catch (err) {
        showFeedback(feedbackElement, '請按 Ctrl+C 複製');
    }
    document.body.removeChild(textarea);
}
```

### R-003: 賀卡頁面導航模式

**決策**: 頂部導航列（Navbar）+ 動態路由 (`/Cards/Detail?id=N`)

**理由**:
- Bootstrap 5 Navbar 提供現成的響應式導航元件
- 所有賀卡在導航列可見，使用者可直接跳轉任意賀卡
- 動態路由透過單一 `Detail.cshtml` 模板渲染所有賀卡，避免重複程式碼
- 符合 FR-007：「系統 MUST 提供頂部或側邊導航列」
- 符合 FR-008：「每張賀卡擁有獨立 URL」

**備選方案**:
- 獨立 Razor Page per 賀卡：10+ 個重複頁面檔案，維護成本高
- 側邊導航列：佔用主要內容空間，不適合以圖案為主的賀卡展示
- Tab 切換：不產生獨立 URL，不利於分享

**實作指引**:
- `_CardNavigation.cshtml` Partial View 渲染導航列中的賀卡連結
- 使用 `asp-route-id` Tag Helper 產生路由連結
- 當前賀卡在導航列中高亮顯示（`active` class）
- 導航列文字顯示賀卡風格名稱（如「寫實風」、「可愛風」）

### R-004: C# 靜態資料架構

**決策**: 使用 `static readonly` 集合搭配服務層介面抽象

**理由**:
- 靜態資料在應用程式啟動時載入記憶體，存取速度最快
- 透過 `IGreetingCardService` 介面抽象化資料存取，支援單元測試 Mock
- 使用相依性注入註冊服務，符合憲章原則
- 不需要資料庫：使用者明確要求不使用任何 DB 軟體與架構
- 編譯期即可驗證資料正確性

**備選方案**:
- JSON 設定檔：需要反序列化、執行期才能發現錯誤
- 嵌入式資源檔：增加讀取複雜度，對靜態文字資料過度設計
- 直接在 PageModel 中定義資料：違反關注點分離原則

**實作指引**:
```csharp
// Data/GreetingCardData.cs
public static class GreetingCardData
{
    public static readonly IReadOnlyList<GreetingCard> Cards = [
        new GreetingCard(1, "寫實風", CardStyle.Realistic, [
            new GreetingMessage(1, "馬到成功，新年快樂！"),
            new GreetingMessage(2, "龍馬精神，萬事如意！"),
            // ...
        ]),
        // ... 10+ cards
    ];
}

// Services/IGreetingCardService.cs
public interface IGreetingCardService
{
    IReadOnlyList<GreetingCard> GetAllCards();
    GreetingCard? GetCardById(int id);
    IReadOnlyList<GreetingMessage> GetMessagesByCardId(int cardId);
}
```

### R-005: CSS 隔離策略

**決策**: 全域樣式 (`site.css`) + 賀卡專用樣式檔 (`card-styles.css`) + 佈局 CSS Isolation

**理由**:
- 全域 `site.css` 定義新年主題色系、字型、共用元件樣式
- 獨立 `card-styles.css` 集中管理所有賀卡風格的 CSS/SVG 藝術，便於維護
- `_Layout.cshtml.css` 使用 Razor Pages CSS Isolation 隔離佈局樣式
- 避免過度細分檔案（10 張賀卡各自一個 CSS 檔會增加 HTTP 請求）

**備選方案**:
- 所有樣式放在 `site.css`：單一檔案過大、職責不清
- 每張賀卡使用 CSS Isolation：Partial View 不支援 CSS Isolation
- CSS Module / PostCSS：需要建構工具，增加開發複雜度

**實作指引**:
- `site.css`：全域 CSS 變數、Bootstrap 覆寫、共用元件（如複製按鈕、回饋提示）
- `card-styles.css`：每種風格以 `.card-style-{name}` 命名空間隔離
- SVG 內嵌於 HTML 中，CSS 透過 class 選擇器控制填色與動畫

### R-006: 10 種賀卡風格設計方向

**決策**: 選定以下 10 種藝術風格，涵蓋至少 5 種不同類型

**理由**:
- 涵蓋傳統與現代風格，滿足不同使用者偏好
- 每種風格的 CSS/SVG 差異化足夠明顯
- 符合 FR-010：「至少 5 種以上不同的藝術風格」

**風格清單**:

| 編號 | 風格名稱 | 藝術類型 | CSS/SVG 技術重點 |
|------|----------|----------|------------------|
| 1 | 寫實風 | 寫實 | SVG 漸層、陰影效果、細緻線條 |
| 2 | 可愛風 | 可愛 | 圓潤造型、粉彩配色、簡筆畫馬 |
| 3 | 科幻風 | 科幻 | 霓虹光效、未來感線條、暗色背景 |
| 4 | 水墨風 | 水墨畫 | 墨漬效果、留白構圖、淡雅配色 |
| 5 | 剪紙風 | 剪紙藝術 | 紅底白紋、對稱圖案、鏤空效果 |
| 6 | 古典風 | 古典 | 書法字體、印章元素、宣紙紋理 |
| 7 | 極簡風 | 現代極簡 | 幾何線條馬、大量留白、單色調 |
| 8 | 年畫風 | 民俗 | 滿版構圖、鮮豔色彩、傳統年畫紋樣 |
| 9 | 插畫風 | 手繪插畫 | 手繪線條感、溫暖色調、故事性構圖 |
| 10 | 普普風 | 普普藝術 | 波點、粗黑描邊、高對比撞色 |

### R-007: ASP.NET Core 10.0 靜態資源最佳實踐

**決策**: 使用 `MapStaticAssets()` + `WithStaticAssets()` 管線

**理由**:
- ASP.NET Core 10.0 內建的靜態資源管線提供自動壓縮、指紋、快取控制
- 已在現有 `Program.cs` 中配置，無需額外設定
- 搭配 `<link>` 和 `<script>` Tag Helper 自動產生帶指紋的 URL

**實作指引**:
- 確保 `app.MapStaticAssets()` 在路由設定前呼叫
- 確保 `app.MapRazorPages().WithStaticAssets()` 正確鏈式呼叫
- CSS/JS 檔案放置於 `wwwroot/` 下，自動受管線管理

## 結論

所有技術決策已確認，無 NEEDS CLARIFICATION 項目。本專案技術複雜度低，主要工作量在於：

1. **CSS/SVG 藝術設計**（10 種風格的馬年賀卡圖案）— 佔整體工作量約 40%
2. **靜態資料定義**（50+ 句祝賀詞編排）— 約 15%
3. **頁面與導航實作**（Razor Pages + Bootstrap 導航）— 約 20%
4. **剪貼簿複製功能**（JavaScript + 備援機制）— 約 10%
5. **測試撰寫**（單元 + 整合測試）— 約 15%
