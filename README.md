<div align="center">

# 🐴 農曆馬年賀卡網站

[![.NET](https://img.shields.io/badge/.NET-10.0-512bd4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=flat-square&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-yellow?style=flat-square)](LICENSE)

**西元 2026 年 · 農曆丙午年 · 生肖馬年**

精選 10 張不同風格的純 CSS/SVG 馬年賀卡，搭配祝賀詞一鍵複製，方便在社交媒體或訊息中使用。

[功能特色](#功能特色) · [快速開始](#快速開始) · [專案結構](#專案結構) · [技術架構](#技術架構)

</div>

## 功能特色

- **10 張多元風格賀卡** — 寫實、可愛、科幻、水墨、剪紙、古典、極簡、年畫、插畫、普普，每張賀卡均以馬為主角搭配農曆新年傳統元素
- **純 CSS/SVG 藝術** — 所有賀卡圖案皆透過 CSS 樣式與內嵌 SVG 繪製，不依賴外部圖片
- **祝賀詞一鍵複製** — 每張賀卡搭配至少 5 句祝賀詞，點擊即可複製到剪貼簿，並即時顯示視覺回饋
- **直覺導航體驗** — 頂部導航列列出所有賀卡風格，可快速跳轉至任意賀卡頁面
- **獨立 URL** — 每張賀卡擁有獨立頁面路由，方便直接分享連結

## 快速開始

### 必備工具

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Git](https://git-scm.com/downloads)

### 安裝與執行

```bash
# 複製專案
git clone https://github.com/HyperLee/NewYearGreetingCard.git
cd NewYearGreetingCard

# 還原相依套件並啟動
dotnet run --project NewYearGreetingCard/NewYearGreetingCard.csproj
```

應用程式啟動後，開啟瀏覽器前往 `http://localhost:5069` 即可瀏覽賀卡。

> [!TIP]
> 若要使用 HTTPS，請前往 `https://localhost:7224`。

### 執行測試

```bash
dotnet test
```

## 專案結構

```
NewYearGreetingCard/          # 主應用程式 (ASP.NET Core Razor Pages)
├── Models/                   # 資料模型 (GreetingCard, CardStyle, GreetingMessage)
├── Services/                 # 服務層 (IGreetingCardService)
├── Data/                     # 靜態賀卡資料集
├── Pages/                    # Razor Pages
│   ├── Cards/                #   賀卡總覽與詳情頁面
│   └── Shared/               #   共用版面配置與元件
└── wwwroot/                  # 靜態資源 (CSS, JS)

NewYearGreetingCard.Tests/    # 測試專案 (xUnit)
├── Unit/                     # 單元測試
├── Integration/              # 整合測試
└── TestData/                 # 測試資料

specs/                        # 功能規格文件
```

## 技術架構

| 層級 | 技術 |
|------|------|
| 框架 | ASP.NET Core 10 (Razor Pages) |
| 語言 | C# 14 |
| 前端 | Bootstrap 5 + 自訂 CSS/SVG |
| 測試 | xUnit + Moq + Microsoft.AspNetCore.Mvc.Testing |
| 目標框架 | .NET 10.0 |

### 架構概覽

```
使用者瀏覽器
    │
    ▼
Razor Pages (路由 + 頁面渲染)
    │
    ▼
GreetingCardService (查詢賀卡 / 祝賀詞)
    │
    ▼
靜態資料集 (C# 記憶體內集合)
```

應用程式採用簡潔的分層架構：Razor Pages 負責路由與 UI 渲染，透過相依性注入取得 `IGreetingCardService` 查詢賀卡與祝賀詞資料。所有賀卡與祝賀詞資料以 C# 靜態集合定義，無需資料庫。

## 賀卡風格預覽

| # | 風格 | 描述 |
|---|------|------|
| 1 | 寫實風 | 漸層色彩與光影效果 |
| 2 | 可愛風 | 圓潤造型與粉彩配色 |
| 3 | 科幻風 | 霓虹光效與未來感線條 |
| 4 | 水墨風 | 墨漬效果與留白構圖 |
| 5 | 剪紙風 | 紅底白紋與鏤空效果 |
| 6 | 古典風 | 書法字體與印章元素 |
| 7 | 極簡風 | 幾何線條與大量留白 |
| 8 | 年畫風 | 滿版構圖與鮮豔色彩 |
| 9 | 插畫風 | 手繪線條與溫暖色調 |
| 10 | 普普風 | 波點與高對比撞色 |
