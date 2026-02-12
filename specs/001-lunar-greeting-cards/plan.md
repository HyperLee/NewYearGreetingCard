# 實作計畫：農曆馬年賀卡網站

**Branch**: `001-lunar-greeting-cards` | **Date**: 2026-02-12 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `/specs/001-lunar-greeting-cards/spec.md`

## 摘要

開發一個農曆新年馬年（2026 丙午年）賀卡範本與祝賀詞網站。使用 ASP.NET Core 10.0 Razor Pages 架構，提供至少 10 張不同風格的純 CSS/SVG 藝術賀卡，每張賀卡搭配祝賀詞並支援一鍵複製功能。資料以 C# 靜態集合定義，不使用任何資料庫。

## 技術背景

**語言/版本**: C# 14 / .NET 10.0
**主要相依性**: ASP.NET Core 10.0、Bootstrap 5、jQuery 3.x、jQuery Validation
**儲存**: N/A（C# 靜態資料集合，無資料庫）
**測試**: xUnit + Moq（單元測試）+ WebApplicationFactory（整合測試）
**目標平台**: 桌面瀏覽器（Chrome、Edge、Firefox、Safari）
**專案類型**: Web（單一 Razor Pages 專案）
**效能目標**: FCP < 1.5 秒、LCP < 2.5 秒
**約束**: 僅桌面瀏覽器、不需行動裝置適配、不使用任何資料庫軟體與架構
**規模/範圍**: 10+ 張賀卡、50+ 句不重複祝賀詞、5+ 種藝術風格

## 憲章檢查

*閘門：Phase 0 研究前必須通過。Phase 1 設計後重新檢查。*

### 初始檢查（Phase 0 前）

| 原則 | 狀態 | 說明 |
|------|------|------|
| I. 程式碼品質至上 | ✅ 通過 | C# 14 最新功能、PascalCase 命名、XML 文件註解、檔案範圍命名空間 |
| II. 測試優先開發 | ✅ 通過 | xUnit + Moq + WebApplicationFactory，TDD 紅-綠-重構流程 |
| III. 使用者體驗一致性 | ✅ 通過 | Bootstrap 5 統一設計語言、賀卡新年主題一致性、導航直覺操作 |
| IV. 效能與延展性 | ✅ 通過 | 純 CSS/SVG 無外部圖片、MapStaticAssets 靜態資源最佳化、FCP < 1.5s |
| V. 可觀察性與監控 | ✅ 通過 | 內建 ILogger 結構化日誌，可選 Serilog 升級 |
| VI. 安全優先 | ✅ 通過 | Razor 引擎 HTML 編碼防 XSS、Anti-Forgery Token、HTTPS Only |

### 特殊豁免

| 項目 | 理由 |
|------|------|
| 無資料庫 | 賀卡與祝賀詞為靜態內容，C# 靜態集合即可滿足需求，無需資料存取層，使用者明確要求不使用任何 DB 軟體與架構 |
| 不需行動裝置適配 | FR-013 明確指定僅需電腦版瀏覽器 |
| 無使用者帳號/登入 | 純靜態內容展示網站，無個人化功能需求 |
| 無 FluentValidation | 無使用者輸入表單，不需要複雜驗證機制 |

### 設計後重新檢查（Phase 1 後）

| 原則 | 狀態 | 說明 |
|------|------|------|
| I. 程式碼品質至上 | ✅ 通過 | Models/Services/Data 三層架構清晰分離，介面抽象化支援測試 |
| II. 測試優先開發 | ✅ 通過 | Service 層透過 `IGreetingCardService` 介面可 Mock 測試，頁面透過 WebApplicationFactory 整合測試 |
| III. 使用者體驗一致性 | ✅ 通過 | 共用 `_CardNavigation` Partial View 確保導航一致性，統一賀卡詳情模板 |
| IV. 效能與延展性 | ✅ 通過 | 靜態資料無 I/O 延遲，CSS/SVG 內嵌無額外 HTTP 請求 |
| V. 可觀察性與監控 | ✅ 通過 | ILogger 注入 PageModel，記錄頁面存取與錯誤 |
| VI. 安全優先 | ✅ 通過 | 無使用者輸入風險降至最低，Razor 自動 HTML 編碼，HTTPS 預設啟用 |

## 專案結構

### 文件（本功能）

```text
specs/001-lunar-greeting-cards/
├── plan.md              # 本檔案（/speckit.plan 輸出）
├── research.md          # Phase 0 輸出（/speckit.plan）
├── data-model.md        # Phase 1 輸出（/speckit.plan）
├── quickstart.md        # Phase 1 輸出（/speckit.plan）
├── contracts/           # Phase 1 輸出（/speckit.plan）
│   └── page-routes.md   # 頁面路由定義
└── tasks.md             # Phase 2 輸出（/speckit.tasks — 非本命令建立）
```

### 原始碼（專案根目錄）

```text
NewYearGreetingCard/
├── Program.cs                          # 應用程式進入點
├── NewYearGreetingCard.csproj          # 專案設定
├── appsettings.json                    # 應用程式設定
├── appsettings.Development.json        # 開發環境設定
├── Models/                             # 資料模型
│   ├── GreetingCard.cs                 # 賀卡模型
│   ├── GreetingMessage.cs              # 祝賀詞模型
│   └── CardStyle.cs                    # 賀卡風格列舉
├── Data/                               # 靜態資料
│   └── GreetingCardData.cs             # 賀卡與祝賀詞靜態集合
├── Services/                           # 服務層
│   ├── IGreetingCardService.cs         # 賀卡服務介面
│   └── GreetingCardService.cs          # 賀卡服務實作
├── Pages/                              # Razor Pages
│   ├── _ViewImports.cshtml             # 全域匯入
│   ├── _ViewStart.cshtml               # 預設佈局
│   ├── Index.cshtml                    # 首頁（重導至第一張賀卡或總覽）
│   ├── Index.cshtml.cs                 # 首頁 PageModel
│   ├── Cards/                          # 賀卡頁面資料夾
│   │   ├── Index.cshtml                # 賀卡總覽頁
│   │   ├── Index.cshtml.cs             # 賀卡總覽 PageModel
│   │   ├── Detail.cshtml               # 賀卡詳情頁（共用模板）
│   │   └── Detail.cshtml.cs            # 賀卡詳情 PageModel
│   ├── Shared/                         # 共用元件
│   │   ├── _Layout.cshtml              # 主佈局（含頂部導航列）
│   │   ├── _Layout.cshtml.css          # 佈局 CSS Isolation
│   │   ├── _CardNavigation.cshtml      # 賀卡導航 Partial View
│   │   └── _GreetingMessages.cshtml    # 祝賀詞列表 Partial View
│   └── Error.cshtml                    # 錯誤頁面
├── wwwroot/
│   ├── css/
│   │   ├── site.css                    # 全域樣式（農曆新年主題色系）
│   │   └── cards/
│   │       └── card-styles.css         # 各風格賀卡 CSS/SVG 藝術
│   ├── js/
│   │   └── site.js                     # 全域腳本（含剪貼簿複製功能）
│   └── lib/                            # 第三方函式庫
│       ├── bootstrap/
│       ├── jquery/
│       ├── jquery-validation/
│       └── jquery-validation-unobtrusive/
└── Properties/
    └── launchSettings.json

NewYearGreetingCard.Tests/              # 測試專案
├── NewYearGreetingCard.Tests.csproj
├── Unit/                               # 單元測試
│   ├── Models/
│   │   └── GreetingCardTests.cs
│   └── Services/
│       └── GreetingCardServiceTests.cs
├── Integration/                        # 整合測試
│   ├── Pages/
│   │   ├── IndexPageTests.cs
│   │   └── CardDetailPageTests.cs
│   └── TestWebApplicationFactory.cs
└── TestData/
    └── GreetingCardTestData.cs
```

**結構決策**: 採用單一 Razor Pages 專案結構。由於不需要 API 和資料庫，前後端合併於同一專案中，使用 `Models/`、`Data/`、`Services/` 三層分離架構。賀卡頁面使用動態路由 (`/Cards/Detail?id=N`) 搭配共用 Razor 模板，避免為每張賀卡建立獨立 `.cshtml` 檔案。測試專案獨立於主專案之外。

## 複雜度追蹤

> 無憲章違規需要辯護。本專案架構簡潔，完全符合憲章所有原則。
