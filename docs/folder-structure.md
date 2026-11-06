# Ramsha platform — folder structure

Text version of [`folder-structure.png`](folder-structure.png). Counts are files per folder, read from disk.

```text
Ramsha.sln                                            React UI + .NET API — حل واحد
.vscode/                                              launch + tasks                                     2 ملف

src/                                                  جذر الحل (Solution)
├── Core/                                             طبقة الأعمال (Domain + Application)
│   ├── Ramsha.Domain/                                Entities, ValueObjects, Enums, Events            139 ملف
│   │   ├── Products/                                 Entities(17), Enums, Events, Services             39 ملف
│   │   ├── Inventory/                                Entities(5), Enums, Events, Services              22 ملف
│   │   ├── Suppliers/                                Entities(11), Enums                               18 ملف
│   │   ├── Orders/                                   Entities(5), Enums, Events                        10 ملف
│   │   ├── Baskets/  Customers/                      Entities                                           6 ملف
│   │   ├── Common/                                   Address, Price, Currency, DiscountService         11 ملف
│   │   ├── Repositories/                             20 واجهة IRepository                              21 ملف
│   │   └── Settings/                                 JWT, Stripe, Identity, DeliveryFee                 6 ملف
│   └── Ramsha.Application/                           CQRS: Features, Dtos, Services                   316 ملف
│       ├── Features/                                 16 ميزة × Commands / Queries                     216 ملف
│       │   ├── Products/  Suppliers/                 Commands, Queries, Handlers                       91 ملف
│       │   ├── Orders/  Inventory/                   Commands, Queries, EventHandlers                  37 ملف
│       │   ├── Baskets/  Account/                    Commands, Queries                                 24 ملف
│       │   └── Brands, Catalog, Tags...              10 ميزات أخرى                                     64 ملف
│       ├── Dtos/                                     17 مجلد Dto حسب الميزة                            57 ملف
│       ├── Services/                                 Price, Variant, Basket, Currency...               13 ملف
│       ├── Wrappers/                                 PagedResponse, FilterParams, Error                 8 ملف
│       └── Extensions/  Exceptions/                  Validation, DI registration                       12 ملف

├── Infrastructure/                                   طبقة البنية التحتية
│   ├── Ramsha.Persistence/                           DbContext, Repositories, Migrations               43 ملف
│   │   ├── Contexts/                                 ApplicationDbContext, UnitOfWork                   9 ملف
│   │   ├── Repositories/                             20 تنفيذ Repository                               20 ملف
│   │   ├── Migrations/                               Initial + ModelSnapshot                            3 ملف
│   │   └── Seeds/  Helpers/  Services/               EF seeding + conversions                           8 ملف
│   ├── Ramsha.Identity/                              ASP.NET Identity + JWT                            29 ملف
│   │   ├── Models/  Services/                        Users, Roles, Tokens                              13 ملف
│   │   ├── Contexts/  Migrations/                    IdentityDbContext                                  5 ملف
│   │   └── Seeds/  Providers/  Common/               Default roles + claims                             7 ملف
│   ├── Ramsha.FileStorage/                           Firebase, Supabase, Google Drive                   9 ملف
│   ├── Ramsha.Mail/                                  EmailService + settings                            4 ملف
│   ├── Ramsha.CacheService/                          Redis cache wrapper                                4 ملف
│   └── Ramsha.BackgroundJobs/                        Cron jobs + executor                               5 ملف

├── Presentation/                                     طبقة العرض (Web API)
│   └── Ramsha.Startup/                               Program.cs, DI, Controllers                       49 ملف
│       ├── Program.cs                                composition root
│       ├── Controllers/v1/                           21 Controller                                     24 ملف
│       ├── Infrastructure/                           Middlewares, Filters, Extensions                  11 ملف
│       │   ├── Middlewares/                          ErrorHandlerMiddleware                             1 ملف
│       │   ├── Filters/                              ApiResultFilter, ValidationFilter                  2 ملف
│       │   └── Extensions/                           Swagger, Migrations, BaseResult                    4 ملف
│       └── appsettings*.json                         Firebase, Development

├── Ramsha.Api/                                       ApiModule (module glue)                            2 ملف
├── Ramsha.Contracts/                                 BackgroundJob / Basket / Email DTOs               15 ملف
├── Ramsha.Shared/                                    Roles, SharedModule, AppCorn                       4 ملف
├── Ramsha.PaymentService/                            Stripe integration                                 3 ملف
├── Ramsha.Ui/                                        React + Vite + MUI — واجهة المتجر ولوحة التحكم   335 ملف
│   ├── public/                                       أيقونات، صور، خطوط                                30 ملف
│   └── src/                                          main.tsx, assets                                 294 ملف
│       ├── app/                                      البنية التحتية للواجهة                           166 ملف
│       │   ├── api/                                  client.ts, Request.ts, services/                  21 ملف
│       │   ├── components/                           AppDialog, AppTable, ui, icons, maps              54 ملف
│       │   ├── hooks/                                react-query hooks لكل ميزة                        28 ملف
│       │   ├── layout/                               header, footer, sidebar, dashboard, auth          19 ملف
│       │   ├── models/                               TypeScript models                                 19 ملف
│       │   └── utils, router, store, providers       أدوات، مسارات، حالة                               18 ملف
│       └── features/                                 18 ميزة (شاشات)                                  125 ملف
│           ├── suppliers/                            Products, Variants, supplies                      24 ملف
│           ├── products/                             variants, skeletons                               22 ملف
│           ├── catalog/  account/                    المتجر و تسجيل الدخول                             28 ملف
│           └── admin, orders, checkout...            12 ميزة أخرى                                      51 ملف
└── test/Ramsha.Application.UnitTest/                 xUnit + Mocks + DataGenerators                     9 ملف
```
