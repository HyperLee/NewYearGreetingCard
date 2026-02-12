# 頁面路由定義：農曆馬年賀卡網站

**Feature**: 001-lunar-greeting-cards
**Date**: 2026-02-12
**Status**: 已完成

## 概述

本專案為純伺服器端渲染的 Razor Pages 網站，不提供 REST API。所有互動透過頁面導航與前端 JavaScript 完成。

## 頁面路由

### 首頁

| 項目 | 值 |
|------|-----|
| **路由** | `/` |
| **Razor Page** | `Pages/Index.cshtml` |
| **HTTP 方法** | GET |
| **說明** | 網站首頁，顯示歡迎資訊與年度標示，並導引至賀卡瀏覽 |
| **回應** | HTML 頁面 |

**頁面內容**:
- 網站標題：農曆馬年賀卡
- 年度資訊：西元 2026 年、農曆丙午年、生肖馬年（FR-011）
- 導航至賀卡總覽或第一張賀卡的入口

---

### 賀卡總覽

| 項目 | 值 |
|------|-----|
| **路由** | `/Cards` |
| **Razor Page** | `Pages/Cards/Index.cshtml` |
| **HTTP 方法** | GET |
| **說明** | 顯示所有賀卡的概覽導航，列出風格名稱與小預覽 |
| **回應** | HTML 頁面 |

**頁面內容**:
- 所有賀卡的風格名稱清單
- 每張賀卡的縮圖預覽連結
- 點擊任一賀卡跳轉至詳情頁

---

### 賀卡詳情

| 項目 | 值 |
|------|-----|
| **路由** | `/Cards/Detail?id={cardId}` 或 `/Cards/Detail/{cardId}` |
| **Razor Page** | `Pages/Cards/Detail.cshtml` |
| **HTTP 方法** | GET |
| **參數** | `id` (int) — 賀卡唯一識別碼 |
| **說明** | 顯示單張賀卡的完整內容，包含 CSS/SVG 藝術圖案與祝賀詞列表 |
| **回應** | HTML 頁面 |
| **錯誤處理** | `id` 不存在時重導至 `/Cards` |

**頁面內容**:
- 賀卡 CSS/SVG 藝術圖案（以圖案為主、文字為輔）
- 祝賀詞列表（≥ 5 句）
- 每句祝賀詞旁的複製按鈕
- 前一張/下一張賀卡的導航按鈕
- 頂部導航列中高亮當前賀卡

**前端互動**:
- 點擊祝賀詞或複製按鈕 → 呼叫 `navigator.clipboard.writeText()` 複製文字
- 複製成功 → 顯示「已複製！」視覺回饋提示
- 複製失敗（API 不可用） → 自動選取文字 + 顯示「請按 Ctrl+C 複製」

---

### 隱私權政策

| 項目 | 值 |
|------|-----|
| **路由** | `/Privacy` |
| **Razor Page** | `Pages/Privacy.cshtml` |
| **HTTP 方法** | GET |
| **說明** | 隱私權政策頁面（ASP.NET Core 預設） |
| **回應** | HTML 頁面 |

---

### 錯誤頁面

| 項目 | 值 |
|------|-----|
| **路由** | `/Error` |
| **Razor Page** | `Pages/Error.cshtml` |
| **HTTP 方法** | GET |
| **說明** | 全域錯誤處理頁面 |
| **回應** | HTML 頁面 |

## 共用元件（Partial Views）

### _CardNavigation

- **用途**: 賀卡導航列，嵌入於 `_Layout.cshtml` 或賀卡詳情頁
- **資料來源**: `IGreetingCardService.GetAllCards()` 取得所有賀卡清單
- **Model**: `IReadOnlyList<GreetingCard>`
- **呈現**: 頂部導航列中的下拉選單或水平連結列表

### _GreetingMessages

- **用途**: 祝賀詞列表，嵌入於賀卡詳情頁
- **資料來源**: 當前賀卡的 `Messages` 屬性
- **Model**: `IReadOnlyList<GreetingMessage>`
- **呈現**: 卡片式列表，每句祝賀詞附帶複製按鈕

## 導航流程

```text
[首頁 /] ──────────────────────► [賀卡總覽 /Cards]
    │                                   │
    │                                   ├── 賀卡 1 ─► [詳情 /Cards/Detail?id=1]
    │                                   ├── 賀卡 2 ─► [詳情 /Cards/Detail?id=2]
    │                                   ├── ...
    │                                   └── 賀卡 10 ─► [詳情 /Cards/Detail?id=10]
    │
    └── 頂部導航列（所有頁面可見）
         ├── 首頁
         ├── 賀卡（下拉選單列出所有風格）
         └── 隱私權
```
