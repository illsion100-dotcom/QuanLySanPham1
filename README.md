# Shoes Store Management – ASP.NET Core MVC (.NET 10) + EF Core 10

## Chạy nhanh
1. Mở `ShoesStore.sln` bằng **Visual Studio 2026** (workload *ASP.NET and web development* + *Data storage and processing*).
2. Visual Studio tự restore 3 gói NuGet: `Microsoft.EntityFrameworkCore.SqlServer`, `.Tools`, `.Design` (10.0.x).
3. Kiểm tra chuỗi kết nối `ShoesStoreConnection` trong `appsettings.json` (mặc định LocalDB `(localdb)\MSSQLLocalDB`).
4. Tạo CSDL – chọn **một** cách:
   - **Code First:** Tools → NuGet Package Manager → Package Manager Console:
     ```
     Add-Migration InitialCreate
     Update-Database
     ```
   - **Script SQL:** chạy `Database/ShoesStoreDb.sql` trong SSMS / SQL Server Object Explorer.
5. Nhấn **Ctrl + F5**.

Dữ liệu mẫu: 5 danh mục + 10 sản phẩm giày (tự nạp bằng `HasData` hoặc có trong script SQL).

## Dùng Visual Studio 2022 (.NET 8)
- `ShoesStore.csproj`: đổi `net10.0` → `net8.0`, các gói `10.0.*` → `8.0.*`.
- `Program.cs`: xóa dòng `app.MapStaticAssets();` và đoạn `.WithStaticAssets()`.

## Chức năng
- Sản phẩm: danh sách có phân trang, tìm theo tên/thương hiệu, lọc theo danh mục và khoảng giá, sắp xếp, thêm (có validation + upload ảnh), sửa, xóa (trang xác nhận), xem chi tiết.
- Danh mục: danh sách + tìm kiếm, thêm, sửa, xóa (chặn xóa khi còn sản phẩm), xem sản phẩm theo danh mục.
- Thống kê: tổng số sản phẩm, tổng số lượng tồn, tổng giá trị tồn kho, bảng + biểu đồ Chart.js theo danh mục, danh sách sắp hết hàng.
