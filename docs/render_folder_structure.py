#!/usr/bin/env python3
"""
Renders the Ramsha platform folder-structure diagram to a PNG: dark background,
monospace tree, with a file-count annotation on every module (counts are read
from disk, so the diagram never goes stale).

Requirements
------------
  * ImageMagick (`convert`)
  * python3 with `arabic-reshaper` + `python-bidi` (Arabic annotations are
    shaped/reordered so a plain monospace font renders joined glyphs)

Usage
-----
    python3 docs/render_folder_structure.py                # docs/folder-structure.png
    python3 docs/render_folder_structure.py out.png 21     # custom path + point size
    python3 docs/render_folder_structure.py --md           # also refresh the text
                                                           # version (folder-structure.md)

Rows are drawn one by one with `convert -draw` (not a single multi-line `label:`)
so the output size is not bound by ImageMagick's per-label limits.
"""
import os
import subprocess
import sys

import arabic_reshaper
from bidi.algorithm import get_display

REPO = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

FONT = "DejaVu-Sans-Mono"
BG = "#1e1e1e"
FG = "#d6d6d6"
DIM_FG = "#9c9c9c"      # descriptions / secondary rows
ANN_FG = "#c9c9c9"      # file-count badges
GAP = 5          # blank columns between the name column and the description
PAD_X = 44
PAD_Y = 32
LINE = 1.22      # line height as a multiple of the point size (keeps │ joined)
V, S = "│   ", "    "


# ────────────────────────────────────────────────────────────── helpers ──
ARABIC = lambda s: any("\u0600" <= ch <= "\u06ff" for ch in s)


def shaped(text):
    """Shape + bidi-reorder a whole cell (Arabic runs need visual order and
    joined presentation forms when drawn with a non-shaping font)."""
    if text and ARABIC(text):
        # base direction LTR so numbers stay before their Arabic unit
        return get_display(arabic_reshaper.reshape(text), base_dir="L")
    return text


def node(name, desc="", path=None, children=(), dim=False, gap=False, absolute=False):
    return dict(name=name, desc=desc, path=path, children=list(children),
                dim=dim, gap=gap, absolute=absolute)


def count_files(paths):
    """Number of files under one or more repo-relative paths (None = no badge)."""
    if paths is None:
        return None
    if isinstance(paths, str):
        paths = [paths]
    total = 0
    for p in paths:
        for _, _, files in os.walk(os.path.join(REPO, p)):
            total += len(files)
    return total


def count_label(n):
    return f"{n} ملف"


# ─────────────────────────────────────────────────────── the tree spec ──
def tree():
    return [
        node("Ramsha.sln", "React UI + .NET API — حل واحد", absolute=True),
        node(".vscode/", "launch + tasks", ".vscode", dim=True, gap=True, absolute=True),

        node("src/", "جذر الحل (Solution)", absolute=True, children=[
            node("Core/", "طبقة الأعمال (Domain + Application)", children=[
                node("Ramsha.Domain/", "Entities, ValueObjects, Enums, Events", "Ramsha.Domain", [
                    node("Products/", "Entities(17), Enums, Events, Services", "Ramsha.Domain/Products"),
                    node("Inventory/", "Entities(5), Enums, Events, Services", "Ramsha.Domain/Inventory"),
                    node("Suppliers/", "Entities(11), Enums", "Ramsha.Domain/Suppliers"),
                    node("Orders/", "Entities(5), Enums, Events", "Ramsha.Domain/Orders"),
                    node("Baskets/  Customers/", "Entities",
                         ["Ramsha.Domain/Baskets", "Ramsha.Domain/Customers"]),
                    node("Common/", "Address, Price, Currency, DiscountService", "Ramsha.Domain/Common"),
                    node("Repositories/", "20 واجهة IRepository", "Ramsha.Domain/Repositories"),
                    node("Settings/", "JWT, Stripe, Identity, DeliveryFee", "Ramsha.Domain/Settings"),
                ]),
                node("Ramsha.Application/", "CQRS: Features, Dtos, Services", "Ramsha.Application", [
                    node("Features/", "16 ميزة × Commands / Queries", "Ramsha.Application/Features", [
                        node("Products/  Suppliers/", "Commands, Queries, Handlers",
                             ["Ramsha.Application/Features/Products",
                              "Ramsha.Application/Features/Suppliers"]),
                        node("Orders/  Inventory/", "Commands, Queries, EventHandlers",
                             ["Ramsha.Application/Features/Orders",
                              "Ramsha.Application/Features/Inventory"]),
                        node("Baskets/  Account/", "Commands, Queries",
                             ["Ramsha.Application/Features/Baskets",
                              "Ramsha.Application/Features/Account"]),
                        node("Brands, Catalog, Tags...", "10 ميزات أخرى",
                             ["Ramsha.Application/Features/" + d for d in
                              ("Brands", "Catalog", "Common", "Customers", "DeliveryAgents",
                               "Payments", "Statistics", "Supplies", "Tags", "Admin")]),
                    ]),
                    node("Dtos/", "17 مجلد Dto حسب الميزة", "Ramsha.Application/Dtos"),
                    node("Services/", "Price, Variant, Basket, Currency...", "Ramsha.Application/Services"),
                    node("Wrappers/", "PagedResponse, FilterParams, Error", "Ramsha.Application/Wrappers"),
                    node("Extensions/  Exceptions/", "Validation, DI registration",
                         ["Ramsha.Application/Extensions", "Ramsha.Application/Exceptions"]),
                ]),
            ], gap=True),

            node("Infrastructure/", "طبقة البنية التحتية", children=[
                node("Ramsha.Persistence/", "DbContext, Repositories, Migrations", "Ramsha.Persistence", [
                    node("Contexts/", "ApplicationDbContext, UnitOfWork", "Ramsha.Persistence/Contexts"),
                    node("Repositories/", "20 تنفيذ Repository", "Ramsha.Persistence/Repositories"),
                    node("Migrations/", "Initial + ModelSnapshot", "Ramsha.Persistence/Migrations"),
                    node("Seeds/  Helpers/  Services/", "EF seeding + conversions",
                         ["Ramsha.Persistence/Seeds", "Ramsha.Persistence/Helpers",
                          "Ramsha.Persistence/Services"]),
                ]),
                node("Ramsha.Identity/", "ASP.NET Identity + JWT", "Ramsha.Identity", [
                    node("Models/  Services/", "Users, Roles, Tokens",
                         ["Ramsha.Identity/Models", "Ramsha.Identity/Services"]),
                    node("Contexts/  Migrations/", "IdentityDbContext",
                         ["Ramsha.Identity/Contexts", "Ramsha.Identity/Migrations"]),
                    node("Seeds/  Providers/  Common/", "Default roles + claims",
                         ["Ramsha.Identity/Seeds", "Ramsha.Identity/Providers",
                          "Ramsha.Identity/Common"]),
                ]),
                node("Ramsha.FileStorage/", "Firebase, Supabase, Google Drive", "Ramsha.FileStorage"),
                node("Ramsha.Mail/", "EmailService + settings", "Ramsha.Mail"),
                node("Ramsha.CacheService/", "Redis cache wrapper", "Ramsha.CacheService"),
                node("Ramsha.BackgroundJobs/", "Cron jobs + executor", "Ramsha.BackgroundJobs"),
            ], gap=True),

            node("Presentation/", "طبقة العرض (Web API)", children=[
                node("Ramsha.Startup/", "Program.cs, DI, Controllers", "Ramsha.Startup", [
                    node("Program.cs", "composition root"),
                    node("Controllers/v1/", "21 Controller", "Ramsha.Startup/Controllers/v1"),
                    node("Infrastructure/", "Middlewares, Filters, Extensions",
                         "Ramsha.Startup/Infrastructure", [
                             node("Middlewares/", "ErrorHandlerMiddleware",
                                  "Ramsha.Startup/Infrastructure/Middlewares", dim=True),
                             node("Filters/", "ApiResultFilter, ValidationFilter",
                                  "Ramsha.Startup/Infrastructure/Filters", dim=True),
                             node("Extensions/", "Swagger, Migrations, BaseResult",
                                  "Ramsha.Startup/Infrastructure/Extensions", dim=True),
                         ]),
                    node("appsettings*.json", "Firebase, Development", dim=True),
                ]),
            ], gap=True),

            node("Ramsha.Api/", "ApiModule (module glue)", "Ramsha.Api"),
            node("Ramsha.Contracts/", "BackgroundJob / Basket / Email DTOs", "Ramsha.Contracts"),
            node("Ramsha.Shared/", "Roles, SharedModule, AppCorn", "Ramsha.Shared"),
            node("Ramsha.PaymentService/", "Stripe integration", "Ramsha.PaymentService"),
            node("Ramsha.Ui/", "React + Vite + MUI — واجهة المتجر ولوحة التحكم", "Ramsha.Ui", [
                node("public/", "أيقونات، صور، خطوط", "Ramsha.Ui/public"),
                node("src/", "main.tsx, assets", "Ramsha.Ui/src", [
                    node("app/", "البنية التحتية للواجهة", "Ramsha.Ui/src/app", [
                        node("api/", "client.ts, Request.ts, services/", "Ramsha.Ui/src/app/api"),
                        node("components/", "AppDialog, AppTable, ui, icons, maps",
                             "Ramsha.Ui/src/app/components"),
                        node("hooks/", "react-query hooks لكل ميزة", "Ramsha.Ui/src/app/hooks"),
                        node("layout/", "header, footer, sidebar, dashboard, auth",
                             "Ramsha.Ui/src/app/layout"),
                        node("models/", "TypeScript models", "Ramsha.Ui/src/app/models"),
                        node("utils, router, store, providers", "أدوات، مسارات، حالة",
                             ["Ramsha.Ui/src/app/utils", "Ramsha.Ui/src/app/router",
                              "Ramsha.Ui/src/app/store", "Ramsha.Ui/src/app/providers"]),
                    ]),
                    node("features/", "18 ميزة (شاشات)", "Ramsha.Ui/src/features", [
                        node("suppliers/", "Products, Variants, supplies", "Ramsha.Ui/src/features/suppliers"),
                        node("products/", "variants, skeletons", "Ramsha.Ui/src/features/products"),
                        node("catalog/  account/", "المتجر و تسجيل الدخول",
                             ["Ramsha.Ui/src/features/catalog", "Ramsha.Ui/src/features/account"]),
                        node("admin, orders, checkout...", "12 ميزة أخرى",
                             ["Ramsha.Ui/src/features/" + d for d in
                              ("admin", "orders", "checkout", "basket", "categories", "common",
                               "contact", "deliveryAgents", "home", "inventory", "profile",
                               "search", "supplies", "about")]),
                    ]),
                ]),
            ]),
            node("test/Ramsha.Application.UnitTest/", "xUnit + Mocks + DataGenerators",
                 "Ramsha.Application.UnitTest"),
        ]),
    ]


# ────────────────────────────────────────────────────────── flattening ──
def flatten(nodes, anc=(), out=None):
    """Turn the nested spec into render rows, computing ├── / └── connectors."""
    out = [] if out is None else out
    for i, n in enumerate(nodes):
        last = i == len(nodes) - 1
        out.append(dict(node=n, last=last, absolute=n["absolute"], anc=list(anc)))
        if n["children"]:
            # an absolute row is drawn at column 0, so its children start there too
            step = "" if n["absolute"] else (S if last else V)
            flatten(n["children"], list(anc) + [step], out)
        if n["gap"]:
            out.append(dict(blank=True))
    return out


def row_prefix(r):
    if r["absolute"]:
        return ""
    return "".join(r["anc"]) + ("└── " if r["last"] else "├── ")


def layout():
    """Render rows: left column text, description, file-count annotation."""
    items = []
    for r in flatten(tree()):
        if r.get("blank"):
            items.append(dict(blank=True))
            continue
        n = r["node"]
        n_files = count_files(n["path"])
        items.append(dict(
            left=row_prefix(r) + n["name"],
            desc=n["desc"],
            ann=count_label(n_files) if n_files is not None else "",
            dim=n["dim"],
        ))
    return items


# ─────────────────────────────────────────────────────────────── text ──
def write_markdown(path):
    """The same tree as plain text inside a Markdown file."""
    items = layout()
    body = [i for i in items if not i.get("blank")]
    name_w = max(len(i["left"]) for i in body) + 2
    desc_w = max(len(i["desc"]) for i in body) + 2

    lines = ["# Ramsha platform — folder structure", "",
             "Text version of [`folder-structure.png`](folder-structure.png). "
             "Counts are files per folder, read from disk.", "", "```text"]
    for i in items:
        if i.get("blank"):
            lines.append("")
            continue
        line = i["left"].ljust(name_w) + " " * GAP + i["desc"].ljust(desc_w)
        if i["ann"]:
            line += i["ann"].rjust(8)
        lines.append(line.rstrip())
    lines += ["```", ""]

    with open(path, "w", encoding="utf-8") as fh:
        fh.write("\n".join(lines))
    print(f"wrote {path}")


# ─────────────────────────────────────────────────────────────── png ──
def draw(out_path, size):
    items = []
    for i in layout():
        if i.get("blank"):
            items.append(dict(blank=True))
            continue
        items.append(dict(left=shaped(i["left"]), desc=shaped(i["desc"]),
                          ann=shaped(i["ann"]), dim=i["dim"]))

    body = [i for i in items if not i.get("blank")]
    name_w = max(len(i["left"]) for i in body) + 2
    desc_w = max(len(i["desc"]) for i in body) + 2
    ann_w = max(len(i["ann"]) for i in body)

    adv = float(subprocess.run(
        ["convert", "-font", FONT, "-pointsize", str(size), "label:" + "0" * 20,
         "-format", "%w", "info:"],
        capture_output=True, text=True, check=True).stdout) / 20.0

    line_h = size * LINE
    width = int(PAD_X * 2 + adv * (name_w + GAP + desc_w + ann_w)) + 2
    height = int(PAD_Y * 2 + line_h * (len(items) + 1))
    desc_x = int(PAD_X + adv * (name_w + GAP))

    main, dim, fade, ann = [], [], [], []
    y = PAD_Y + size * 1.1
    for i in items:
        if not i.get("blank"):
            (dim if i["dim"] else main).append(f"text {PAD_X},{y:.0f} '{i['left']}'")
            if i["desc"]:
                fade.append(f"text {desc_x},{y:.0f} '{i['desc']}'")
            if i["ann"]:
                x = int(width - PAD_X - adv * len(i["ann"]))
                ann.append(f"text {x},{y:.0f} '{i['ann']}'")
        y += line_h

    cmd = ["convert", "-size", f"{width}x{height}", "xc:" + BG,
           "-font", FONT, "-pointsize", str(size)]
    for fill, draws in ((FG, main), (DIM_FG, dim), (DIM_FG, fade), (ANN_FG, ann)):
        if draws:
            cmd += ["-fill", fill, "-draw", " ".join(draws)]
    cmd += ["-depth", "8", "-colorspace", "sRGB", out_path]
    subprocess.run(cmd, check=True)
    print(f"wrote {out_path}  ({width}x{height})")


def main():
    want_md = "--md" in sys.argv
    args = [a for a in sys.argv[1:] if a != "--md"]
    out_path = args[0] if args else os.path.join(REPO, "docs/folder-structure.png")
    size = int(args[1]) if len(args) > 1 else 21

    draw(out_path, size)
    if want_md:
        write_markdown(os.path.join(os.path.dirname(os.path.abspath(out_path)),
                                    "folder-structure.md"))


if __name__ == "__main__":
    main()
