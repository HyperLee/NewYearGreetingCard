# 快速入門指南：農曆馬年賀卡網站

**Feature**: 001-lunar-greeting-cards
**Date**: 2026-02-12

## 先決條件

- .NET 10.0 SDK（[下載連結](https://dotnet.microsoft.com/download/dotnet/10.0)）
- 支援的 IDE：Visual Studio 2022 17.x+ 或 VS Code + C# Dev Kit
- Git

## 取得原始碼

```bash
git clone https://github.com/HyperLee/NewYearGreetingCard.git
cd NewYearGreetingCard
git checkout 001-lunar-greeting-cards
```

## 建構與執行

### 方法一：命令列

```bash
# 還原相依套件並建構
dotnet build NewYearGreetingCard/NewYearGreetingCard.csproj

# 執行應用程式
dotnet run --project NewYearGreetingCard/NewYearGreetingCard.csproj
```

應用程式將在 `https://localhost:5001` 或 `http://localhost:5000` 啟動。

### 方法二：VS Code 工作

使用已設定的 VS Code 工作：
- **建構**: 執行 `build` task（`Ctrl+Shift+B`）
- **執行**: 執行 `run (dotnet run)` task

## 執行測試

```bash
# 執行所有測試
dotnet test NewYearGreetingCard.Tests/NewYearGreetingCard.Tests.csproj

# 執行特定測試類別
dotnet test --filter "ClassName=GreetingCardServiceTests"

# 執行並產生覆蓋率報告
dotnet test --collect:"XPlat Code Coverage"
```

## 專案結構概覽

```text
PostCard/
├── PostCard.slnx                       # 方案檔
├── NewYearGreetingCard/                # 主專案
│   ├── Models/                         # 資料模型（GreetingCard, GreetingMessage, CardStyle）
│   ├── Data/                           # 靜態資料集合（GreetingCardData）
│   ├── Services/                       # 服務層（IGreetingCardService）
│   ├── Pages/                          # Razor Pages
│   │   ├── Cards/                      # 賀卡頁面
│   │   └── Shared/                     # 共用元件
│   └── wwwroot/                        # 靜態資源（CSS/JS/lib）
├── NewYearGreetingCard.Tests/          # 測試專案
│   ├── Unit/                           # 單元測試
│   └── Integration/                    # 整合測試
└── specs/                              # 規格文件
```

## 主要頁面路由

| 路由 | 說明 |
|------|------|
| `/` | 首頁 — 歡迎頁面與年度資訊 |
| `/Cards` | 賀卡總覽 — 所有賀卡導航 |
| `/Cards/Detail?id={N}` | 賀卡詳情 — 單張賀卡與祝賀詞 |

## 新增賀卡的步驟

1. 在 `Models/CardStyle.cs` 中新增列舉值
2. 在 `Data/GreetingCardData.cs` 中新增賀卡資料與祝賀詞
3. 在 `wwwroot/css/cards/card-styles.css` 中新增對應風格的 CSS/SVG 藝術
4. 執行測試確認新賀卡正確載入

## 技術堆疊摘要

| 項目 | 技術 |
|------|------|
| 框架 | ASP.NET Core 10.0 |
| 語言 | C# 14 |
| 前端 | Razor Pages + Bootstrap 5 + jQuery |
| 資料 | C# 靜態集合（無資料庫） |
| 測試 | xUnit + Moq + WebApplicationFactory |
| 靜態資源 | MapStaticAssets + WithStaticAssets |

## 常見問題

### Q: 為什麼不使用資料庫？
A: 賀卡與祝賀詞為固定的靜態內容，C# 靜態集合提供最佳效能且編譯期即可驗證資料正確性，無需資料庫的額外複雜度。

### Q: 如何修改賀卡的視覺樣式？
A: 編輯 `wwwroot/css/cards/card-styles.css` 中對應風格的 CSS class（如 `.card-style-realistic`），SVG 圖案在 `Pages/Cards/Detail.cshtml` 中修改。

### Q: 如何新增祝賀詞？
A: 在 `Data/GreetingCardData.cs` 的 `Cards` 靜態集合中，找到對應賀卡的 `Messages` 列表新增 `GreetingMessage` 記錄。
