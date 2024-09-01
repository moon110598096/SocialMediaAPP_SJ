# SocialMediaAPP_SJ
## 專案資訊
### 語言:C#(.net8)
### DB:MySQL 8.4.0

## 已實作功能
### User
註冊(含帳號密碼格式與重覆檢查)
登入
登出

### Post
新增文章
刪除文章
修改文章
取得單篇文章
取得所有文章

### Comment
新增留言
刪除留言

## 資料夾結構
### Controllers: 處理API及其邏輯
### Model: 放置Entity
### Data: DB Context
### DB: DDL檔

## DB資訊
DDL檔放置於DB資料夾中，DB連線資訊於appsettings.json中設定

## API資訊
### 說明
使用Post及Comment相關API前請使用User login取得JWT授權，透過Swagger UI中Authorize按鈕進行認證
