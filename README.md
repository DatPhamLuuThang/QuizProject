<code>*.</code> Được xây dựng trên Jetbrains Rider với phiên bản .NET 8.0

# --- Hướng dẫn sinh Database và cấu hình các Model ---

<code>1.</code> Mở thư mục "FrontEnd" trên CMD hoặc Terminal

<code>2.</code> "dotnet ef migrations add mfr_migration" => sinh Migrations

<code>3.</code> "dotnet ef database update" => sinh Database dựa trên Migrations

<code>4.</code> "dotnet ef dbcontext optimize -o ../DataBase/Context" => tối ưu các Model

<code>!.</code> Chạy lại bước 4 khi Model hoặc DbContext thay đổi

# --- Hướng dẫn đang ký các Service ---

<code>1.</code> Mở "ServiceRegister.cs" ở project "Services"

<code>2.</code> Ở hàm "GetAllServices()"

<code>3.</code> Thêm "new ServiceRegisterModel(typeof(<code><./Interface></code>), typeof(<code><./Implement></code>))"

# --- Chú thích ---

<ul>
<li><code>(*.) : Ghi chú</code></li>
<li><code>(!.) : Cảnh báo/Lưu ý</code></li>
<li><code>(<.Tham số/>) : Tham số</code></li>
<li><code>(=>) : Mục đích, kết quả đạt được,...</code></li>
</ul>