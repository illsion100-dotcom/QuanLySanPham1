using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Data;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("ShoesStoreConnection")
    ?? throw new InvalidOperationException("Không tìm thấy chuỗi kết nối 'ShoesStoreConnection' trong appsettings.json.");

builder.Services.AddDbContext<ShoesStoreDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Trường này không được để trống.");
    options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(field => $"Thiếu giá trị cho trường {field}.");
    options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Thiếu giá trị bắt buộc.");
    options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "Thiếu dữ liệu gửi lên máy chủ.");
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, field) => $"Giá trị '{value}' không hợp lệ cho trường {field}.");
    options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(value => $"Giá trị '{value}' không hợp lệ.");
    options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(field => $"Giá trị nhập cho trường {field} không hợp lệ.");
    options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "Giá trị nhập không hợp lệ.");
    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(value => $"Giá trị '{value}' không hợp lệ.");
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(field => $"Trường {field} phải là số.");
    options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "Giá trị phải là số.");
});

var app = builder.Build();

CultureInfo vietnameseCulture = new CultureInfo("vi-VN");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(vietnameseCulture),
    SupportedCultures = new List<CultureInfo> { vietnameseCulture },
    SupportedUICultures = new List<CultureInfo> { vietnameseCulture }
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
