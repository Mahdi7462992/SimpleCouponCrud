مستندات پروژه Simple Coupon CRUD & Validation API
در این پروژه از الگوهای معماری و توسعه نرم‌افزار زیر استفاده شده است:

Repository Pattern
Dependency Injection
DTO Pattern
FluentValidation
Service Layer Pattern
Entity Framework Core


روش اجرا
1. دریافت پروژه
ابتدا سورس پروژه را Clone کنید:
git clone <RepositoryUrl>
یا فایل فشرده پروژه را دانلود و Extract نمایید.

2. بازیابی پکیج‌ها
در پوشه پروژه دستور زیر را اجرا کنید:
dotnet restore

3. اجرای پروژه
برای اجرای پروژه:
dotnet run
یا از طریق Visual Studio کلید F5 را فشار دهید.



نمونه درخواست‌ها
1. ایجاد کوپن جدید

Endpoint
POST /api/coupons
Request Body
{
  "code": "SUMMER25",
  "discountType": 0,
  "value": 25,
  "minPurchaseAmount": 100,
  "expirationDate": "2026-12-31T23:59:59",
  "isActive": true
}
پاسخ موفق
{
  "isSuccess": true,
  "message": "SuccessfullOperation"
}


2. حذف کوپن
Endpoint
DELETE /api/coupons/{id}
نمونه درخواست
DELETE /api/coupons/9f55f438-c33e-46ea-a2c2-4b7ebf6933de
پاسخ موفق
{
  "isSuccess": true,
  "message": "SuccessfullOperation"
}


3. اعتبارسنجی کوپن
Endpoint
POST /api/coupons/validate
Request Body
{
  "couponCode": "SUMMER25",
  "purchaseAmount": 500
}
پاسخ موفق
{
  "isSuccess": true,
  "data": {
    "discountAmount": 125,
    "finalAmount": 375
  }
}



توضیحات معماری
برای جداسازی منطق دسترسی به داده از Repository Pattern استفاده شده است.
تمامی قوانین کسب‌وکار (Business Logic) در Service Layer پیاده‌سازی شده‌اند.
برای اعتبارسنجی ورودی‌ها از FluentValidation استفاده شده است.
جهت جلوگیری از وابستگی مستقیم لایه‌ها از Dependency Injection استفاده شده است.
برای تبادل داده بین لایه‌ها از DTO استفاده شده است.
مدیریت خطاها در Service Layer با استفاده از Try/Catch انجام شده است.