# Copilot Instructions for Ordenpedido

## Project Overview
- **Ordenpedido** is a multi-project .NET Web Forms solution for managing purchase orders, invoices, users, and related business logic.
- The architecture is layered:
  - `Entities/`: POCO classes for business objects (e.g., `OrdenPedido`, `Proveedores`, `Usuarios`).
  - `DAL/`: Data Access Layer, handles database operations and helpers (e.g., `OrdenPedidoDAL.cs`, `DBHelpers.cs`).
  - `BLL/`: Business Logic Layer, orchestrates business rules and workflows (e.g., `OrdenPedidoBLL.cs`).
  - `Web/`: ASP.NET Web Forms UI, with pages, themes, and resources.
  - `WS/`: Web Services for integration (e.g., `OPInsert.asmx`).

## Key Patterns & Conventions
- **Naming**: Classes and files are named by function and layer (e.g., `OrdenPedidoDAL` for DAL, `OrdenPedidoBLL` for BLL).
- **Data Flow**: UI (Web) → BLL → DAL → DB. Entities are passed between layers.
- **Web Services**: Exposed via `WS/` as `.asmx` endpoints for external integration.
- **Security**: Handled in `SecurityBLL.cs` and `SecurityDAL.cs`.
- **Reports**: Crystal Reports files and related pages are in `Archivos de copia de seguridad de Crystal Reports/` and `Web/Reportes/`.

## Developer Workflows
- **Build**: Open `OP.sln` in Visual Studio. Build the solution (F6 or Build menu).
- **Run/Debug**: Start from `Web/` project (set as startup). Use IIS Express or local IIS.
- **Database**: Connection strings are in `Web/Web.config` and `WS/Web.config`.
- **Web Services**: Test via browser or tools like Postman at `/WS/OPInsert.asmx`.
- **DLLs**: Shared libraries are built into `bin/` folders per project.

## Project-Specific Notes
- **No automated tests**: Testing is manual or via UI/web service calls.
- **Legacy Web Forms**: Uses `.aspx` pages, code-behind, and designer files.
- **External dependencies**: Crystal Reports, AjaxControlToolkit, and custom DLLs in `Web/bin/`.
- **Themes/Styles**: Located in `Web/App_Themes/`.
- **Do not move**: Entity, DAL, and BLL classes between projects; maintain separation.

## Examples
- To add a new business rule: Implement in `BLL/`, update `DAL/` if new data access is needed, and expose via `Web/` or `WS/` as appropriate.
- To add a new web page: Create `.aspx`, `.aspx.cs`, and `.aspx.designer.cs` in `Web/`.
- To expose new integration: Add `.asmx` in `WS/` and implement logic in BLL/DAL.

## References
- See `README.md` for high-level project info (currently minimal).
- Key files: `Entities/OrdenPedido.cs`, `DAL/OrdenPedidoDAL.cs`, `BLL/OrdenPedidoBLL.cs`, `Web/LogIn.aspx`, `WS/OPInsert.asmx`.

---
Update this file if project structure or conventions change. For questions, review the code in each layer and follow the established separation of concerns.
