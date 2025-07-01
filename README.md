# DemoAPI Project

Chào mừng bạn đến với dự án DemoAPI! Đây là một dự án mẫu được xây dựng bằng ASP.NET Core với kiến trúc phân lớp (Layered Architecture) hoặc kiến trúc Clean Architecture, minh họa cách xây dựng một Web API với Entity Framework Core và JWT Authentication.

## Cấu trúc Dự án

Dự án được tổ chức thành bốn lớp chính để đảm bảo tính module hóa, khả năng mở rộng và dễ bảo trì:

* **Demo.API:**
    * Lớp trình bày (Presentation Layer).
    * Chứa các API Controllers, cấu hình ứng dụng (như `Program.cs`, `appsettings.json`), và các dịch vụ liên quan đến HTTP.
* **Demo.Application:**
    * Lớp ứng dụng (Application Layer).
    * Chứa logic nghiệp vụ chính của ứng dụng, Command, Query và Handler.
    * Implement MediatR pattern
* **Demo.Core:**
    * Lớp lõi (Domain Layer).
    * Chứa các Domain Entities (mô hình dữ liệu), các Value Objects, Repository Interface và các giao diện cốt lõi khác định nghĩa nghiệp vụ của hệ thống.
* **Demo.Infrastructure:**
    * Lớp hạ tầng (Infrastructure Layer).
    * Chứa các triển khai cụ thể của các giao diện được định nghĩa trong `Demo.Core` và `Demo.Application`.

## Công nghệ Sử dụng

* **.NET 8
* **ASP.NET Core Web API**
* **Entity Framework Core** (ORM)
* **PostgreSQL** (Hệ quản trị cơ sở dữ liệu)
* **JWT (JSON Web Token)** cho Authentication
* **Swagger/OpenAPI** cho tài liệu API và thử nghiệm
* **MediatR

## Bắt đầu

Để chạy dự án này trên máy cục bộ của bạn, hãy làm theo các bước sau:

### Yêu cầu Tiên quyết

* [.NET SDK](https://dotnet.microsoft.com/download) (Phiên bản phù hợp với dự án)
* [Visual Studio](https://visualstudio.microsoft.com/vs/community/) (Khuyên dùng) hoặc [Visual Studio Code](https://code.visualstudio.com/) với C# Extension.
* [PostgreSQL Server](https://www.postgresql.org/download/) đang chạy và có thể truy cập được.

### Thiết lập Dự án

1.  **Clone repository:**
    ```bash
    git clone <URL_CỦA_REPOSITORY_CỦA_BẠN>
    cd DemoAPI
    ```
2.  **Cập nhật các gói NuGet:**
    Mở dự án trong Visual Studio và chạy `Build Solution` để khôi phục các gói NuGet. Hoặc chạy lệnh sau trong thư mục gốc của solution:
    ```bash
    dotnet restore
    ```
3.  **Cấu hình chuỗi kết nối (ConnectionString):**
    Xem phần [Cấu hình ConnectionString](#cấu-hình-connectionstring) dưới đây.

4.  **Chạy ứng dụng:**
    * Trong Visual Studio, nhấn `F5` hoặc nút `Start` để chạy dự án `Demo.API`.
    * Hoặc từ Terminal/Command Prompt trong thư mục `Demo.API`:
        ```bash
        dotnet run
        ```
    Ứng dụng sẽ khởi chạy, trên `http://localhost:5000` hoặc `https://localhost:5001`. Swagger UI sẽ tự động mở trong trình duyệt của bạn (nếu cấu hình).
    Database sẽ tự tạo ngay khi run project nên hãy làm theo các bước tại [Cấu hình ConnectionString](#cấu-hình-connectionstring) để có thể kết nối thành công database.
5. **Đăng Nhập**
   * Email: captain@email.com
   * Password: password
   * Hoặc Bạn có thể tạo account mới thông qua Register API

## Cấu hình ConnectionString

Để kết nối ứng dụng với cơ sở dữ liệu PostgreSQL của bạn, bạn cần thay đổi chuỗi kết nối trong file `appsettings.json` của project `Demo.API`.

1.  Mở file `appsettings.json` trong thư mục `Demo.API`.

2.  Tìm phần `ConnectionStrings`:

    ```json
    // Demo.API/appsettings.json
    {
      // ... các cấu hình khác
      "ConnectionStrings": {
        "Connection": "Host=localhost;Port=5432;Database=Demo;Username=postgres;Password=12345"
      }
    }
    ```

3.  **Chỉnh sửa giá trị của `Connection`:**

    * `Host`: Địa chỉ IP hoặc tên máy chủ nơi PostgreSQL server của bạn đang chạy. (Ví dụ: `localhost` nếu chạy trên cùng máy, hoặc IP máy chủ).
    * `Port`: Cổng mà PostgreSQL server đang lắng nghe. (Mặc định là `5432`).
    * `Database`: Tên của cơ sở dữ liệu mà bạn muốn ứng dụng kết nối tới. (Ví dụ: `Demo`).
    * `Username`: Tên người dùng để truy cập cơ sở dữ liệu PostgreSQL.
    * `Password`: Mật khẩu của người dùng đó.

    **Ví dụ sau khi thay đổi:**

    ```json
    // Demo.API/appsettings.json
    {
      // ...
      "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=5432;Database=Demo;Username=postgres;Password=12345"
      },
      // ...
    }
    ```
## JWT Authentication

Dự án này sử dụng JWT (JSON Web Token) cho việc xác thực người dùng.

### Cấu hình JWT Settings

Cấu hình JWT được định nghĩa trong phần `"Authentication"` của `appsettings.json` (hoặc `JwtSettings` tùy theo cách bạn đặt tên trong `Program.cs`):

```json
// Demo.API/appsettings.json
{
  // ...
  "Authentication": {
    "Key": "6CBxzdYcEgNDrRhMbDpkBF7e4d4Kib46dwL9ZE5egiL0iL5Y3dzREUBSUYVUwUkN6CBxzdYcEgNDrRhMbDpkBF7e4d4Kib46dwL9ZE5egiL0iL5Y3dzREUBSUYVUwUkN",
    "Issuer": "https://localhost:5000",
    "Audience": "https://localhost:5000",
    "ExpirationMinutes": 60
  },
  // ...
}
