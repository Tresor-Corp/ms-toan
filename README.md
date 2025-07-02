# JwtAuth Project

Chào mừng bạn đến với dự án JwtAuth! Đây là một dự án mẫu được xây dựng bằng ASP.NET Core với kiến trúc Clean Architecture, minh họa cách xây dựng một Web API với Entity Framework Core và JWT Authentication.

## Cấu trúc Dự án

Dự án được tổ chức thành bốn lớp chính để đảm bảo tính module hóa, khả năng mở rộng và dễ bảo trì:

* **JwtAuth.API:**
    * Lớp trình bày (Presentation Layer).
    * Chứa các API Controllers, cấu hình ứng dụng (như `Program.cs`, `appsettings.json`), và các dịch vụ liên quan đến HTTP.
* **JwtAuth.Application:**
    * Lớp ứng dụng (Application Layer).
    * Chứa logic nghiệp vụ chính của ứng dụng, Command, Query và Handler.
    * Implement MediatR pattern
* **JwtAuth.Core:**
    * Lớp lõi (Domain Layer).
    * Chứa các Domain Entities (mô hình dữ liệu), các Response Objects, Repository Interface và các giao diện cốt lõi khác định nghĩa nghiệp vụ của hệ thống.
* **JwtAuth.Infrastructure:**
    * Lớp hạ tầng (Infrastructure Layer).
    * Chứa các triển khai cụ thể của các giao diện được định nghĩa trong `JwtAuth.Core` và `JwtAuth.Application`.

## Công nghệ Sử dụng

* **.NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core** (ORM)
* **PostgreSQL** (Hệ quản trị cơ sở dữ liệu)
* **JWT (JSON Web Token)** cho Authentication
* **Swagger/OpenAPI** cho tài liệu API và thử nghiệm
* **MediatR**

## Bắt đầu

Để chạy dự án này, chúng ta sẽ sử dụng Docker Compose để khởi tạo cả ứng dụng API và cơ sở dữ liệu PostgreSQL trong các container.

### Yêu cầu Tiên quyết

* [.NET SDK 8](https://dotnet.microsoft.com/download)
* Hoặc
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (bao gồm Docker Engine và Docker Compose)

### Thiết lập Dự án

1.  **Clone repository:**
    ```bash
    git clone <URL_CỦA_REPOSITORY_CỦA_BẠN>
    cd JwtAuth # Hoặc tên thư mục gốc của dự án của bạn
    ```
2.  **Cấu hình chuỗi kết nối và JWT Settings:**
    Xem phần [Cấu hình Chuỗi kết nối và JWT Settings](#cấu-hình-chuỗi-kết-nối-và-jwt-settings) dưới đây.

3.  **Khởi động các dịch vụ bằng Docker Compose:**
    Từ thư mục gốc của dự án (nơi chứa file `docker-compose.yml`), chạy lệnh sau để build các Docker image, tạo và khởi động các container:
    ```bash
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
    ```
    * Lệnh này sẽ xây dựng lại các image nếu có thay đổi và chạy các container ở chế độ nền (`-d`).
    * Database PostgreSQL sẽ tự động được khởi tạo (nếu chưa có) và các Migrations sẽ được áp dụng khi ứng dụng API khởi động (giả sử bạn đã cấu hình `dbContext.Database.Migrate()` trong `Program.cs` của API).

4.  **Truy cập ứng dụng:**
    * API sẽ khả dụng tại `http://localhost:5000`.
    * Bạn có thể truy cập Swagger UI tại `http://localhost:5000/swagger/index.html` để xem tài liệu API và thử nghiệm các endpoint.

5.  **Đăng Nhập**
    * Email: `captain@email.com`
    * Password: `password`

## Cấu hình Chuỗi kết nối và JWT Settings

Khi chạy dự án với Docker Compose, chuỗi kết nối và các cài đặt JWT sẽ được cung cấp thông qua các biến môi trường trong file `docker-compose.override.yml`.

1.  Mở file `docker-compose.override.yml` trong thư mục gốc của dự án.

2.  Tìm phần `services`:

    ```yaml
    # docker-compose.override.yml
    services:
      db:
        container_name: db
        restart: always
        environment:
            POSTGRES_USER: postgres
            POSTGRES_PASSWORD: "12345"
        ports:
          - "5432:5432"
        volumes:
          - pgdata:/var/lib/postgresql/data # Lưu trữ dữ liệu DB

      jwtauth.api:
        environment:
          - ASPNETCORE_ENVIRONMENT=Development
          - ASPNETCORE_HTTP_PORTS=8080
          - "ConnectionStrings__Connection=Host=db;Port=5432;Database=JwtAuth;Username=postgres;Password=12345"
          - "Authentication__Key=6CBxzdYcEgNDrRhMbDpkBF7e4d4Kib46dwL9ZE5egiL0iL5Y3dzREUBSUYVUwUkN6CBxzdYcEgNDrRhMbDpkBF7e4d4Kib46dwL9ZE5egiL0iL5Y3dzREUBSUYVUwUkN"
          - "Authentication__Issuer=https://localhost:5000"
          - "Authentication__Audience=https://localhost:5000"
          - "Authentication__ExpirationMinutes=60"
        ports:
          - "5000:8080"
        depends_on:
          - db
    ```

3.  **Chỉnh sửa các giá trị môi trường theo nhu cầu của bạn:**

    * **Trong `db` service:**
        * `POSTGRES_USER`: Tên người dùng cho PostgreSQL.
        * `POSTGRES_PASSWORD`: Mật khẩu cho người dùng PostgreSQL.
        * **Lưu ý:** Các giá trị này (Database, Username, Password) cần khớp với phần `ConnectionStrings__Connection` của `jwtauth.api` service.

    * **Trong `jwtauth.api` service:**
        * `ConnectionStrings__Connection`:
            * `Host=db`: Điều này rất quan trọng! Khi chạy trong Docker Compose network, `db` là hostname của container PostgreSQL.
            * `Port=5432`: Cổng mặc định của PostgreSQL.
            * `Database=JwtAuth`, `Username=postgres`, `Password=12345`: Các giá trị này phải khớp với các biến môi trường `POSTGRES_DB`, `POSTGRES_USER`, `POSTGRES_PASSWORD` trong `db` service.
        * `Authentication__Key`, `Authentication__Issuer`, `Authentication__Audience`, `Authentication__ExpirationMinutes`: Các cài đặt này khớp với phần `Authentication` trong `appsettings.json` của bạn.

    **Đảm bảo rằng các giá trị trong chuỗi kết nối của `jwtauth.api` (ví dụ: Username, Password) khớp chính xác với các biến môi trường của `db` service.**

## JWT Authentication

Dự án này sử dụng JWT (JSON Web Token) cho việc xác thực người dùng.

### Cấu hình JWT Settings

Cấu hình JWT được định nghĩa thông qua biến môi trường trong `docker-compose.override.yml` như đã mô tả ở trên.

* **`Key` (Secret):** Chuỗi bí mật này được sử dụng để ký và xác minh JWT token. **Hãy thay thế giá trị mặc định bằng một chuỗi dài, ngẫu nhiên và an toàn hơn trước khi triển khai lên môi trường Production.** Tuyệt đối không để lộ khóa này.
* **`Issuer`:** Tổ chức hoặc server phát hành token. (Thường là URL của API của bạn).
* **`Audience`:** Đối tượng mà token này được tạo ra để sử dụng. (Thường là URL của ứng dụng client sẽ sử dụng API này).
* **`ExpirationMinutes`:** Thời gian tồn tại của token (tính bằng phút) kể từ thời điểm nó được tạo.

### Sử dụng JWT Token

1.  **Đăng nhập:** Gửi yêu cầu `POST` đến endpoint `/api/auth/login` (hoặc tương tự) với `username` và `password`.
    ```json
    // Body của request
    {
      "email": "captain@email.com",
      "password": "password"
    }
    ```
    Server sẽ trả về một JWT token nếu thông tin đăng nhập hợp lệ.

2.  **Truy cập các API Protected:** Để truy cập các endpoint yêu cầu xác thực (được đánh dấu bằng `[Authorize]`), bạn cần đính kèm token nhận được vào header của yêu cầu với định dạng `Authorization: Bearer <your_jwt_token>`.

    **Ví dụ sử dụng cURL:**
    ```bash
    curl -X 'GET' \
      'http://localhost:5000/api/user/profile' \
      -H 'accept: text/plain' \
      -H 'Authorization: Bearer <DÁN_TOKEN_CỦA_BẠN_VÀO_ĐÂY>'
    ```

## Đóng góp

Nếu bạn muốn đóng góp vào dự án này, vui lòng fork repository và tạo một pull request.

## Giấy phép

Dự án này được cấp phép theo Giấy phép MIT. Xem file `LICENSE` để biết thêm chi tiết.
