using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Models;

namespace RealEstateWeb.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser> 

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Quy định kiểu decimal cho Price để tránh cảnh báo EF
            modelBuilder.Entity<Property>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Seed data
            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    Id = 1,
                    Title = "Golden Urban House",
                    Description = "Nhà phố đẹp, trung tâm thành phố, nội thất cao cấp.",
                    Price = 12345000m,
                    Address = "123 Street, New York, USA",
                    Size = 1000,
                    Bed = 3,
                    Bath = 2,
                    Status = "For Sale",
                    Type = "Apartment",
                    ImageUrl = "/img/property-1.jpg"
                },
                new Property
                {
                    Id = 2,
                    Title = "Luxury Villa",
                    Description = "Biệt thự sang trọng, view sông, đầy đủ tiện nghi.",
                    Price = 8500000m,
                    Address = "456 Avenue, Los Angeles, USA",
                    Size = 2500,
                    Bed = 5,
                    Bath = 4,
                    Status = "For Rent",
                    Type = "Villa",
                    ImageUrl = "/img/property-2.jpg"
                },
                new Property
                {
                    Id = 3,
                    Title = "Modern Office Space",
                    Description = "Văn phòng hiện đại nằm tại khu business district.",
                    Price = 4500000m,
                    Address = "789 Business Rd, Chicago, USA",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Sale",
                    Type = "Office",
                    ImageUrl = "/img/property-3.jpg"
                },
                new Property
                {
                    Id = 4,
                    Title = " Vinhomes Royal Island Vũ Yên",
                    Description = "Vinhomes Royal Island Vũ Yên cung cấp đa dạng các loại hình biệt thự. Từ những căn biệt thự đơn lập, biệt thự song lập sang trọng đến những căn biệt thự liền kề, shophouse hiện đại tất cả đều sở hữu thiết kế tinh tế, tỉ mỉ và đa công năng được trải rộng trên mọi phân khu hòn đảo. Giá bán biệt thự Vinhomes Royal Island dao động từ 16 tỷ đến 50 tỷ đồng, phù hợp với khả năng tài chính của nhiều đối tượng khách hàng.",
                    Price = 16000000000000m,
                    Address = "thuộc Đảo Vũ Yên, Huyện Thủy Nguyên và Quận Hải An, Thành phố Hải Phòng.",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Rent",
                    Type = "Office",
                    ImageUrl = "/img/property-4.jpg"
                },
                new Property
                {
                    Id = 5,
                    Title = " The Rivus Elie Saab",
                    Description = "Dinh thự này được thiết kế dành riêng cho tầng lớp siêu giàu hưởng thụ. Đây là một dinh thự đầu tiên chuẩn quốc tế với đầy đủ tiện ích và nội thất cực kỳ sang trọng, đạt đẳng cấp thế giới, được phát triển và mở bán tại Việt Nam bởi 2 bàn tay lớn kiến tạo là chủ đầu tư Masterise Homes cùng Elie Saab Interior Design.",
                    Price = 300000000000000m,
                    Address = "Thuộc đại đô thị Vinhomes Grand Park Quận 9, HCM",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Sale",
                    Type = "Office",
                    ImageUrl = "/img/property-5.jpg"
                },
                new Property
                {
                    Id = 6,
                    Title = " Ecopark Nhơn Trạch",
                    Description = "Ecopark Nhơn Trạch bao gồm 369 căn biệt thự đơn lập và song lập được thiết kế phong cách hiện đại giao thoa hài hoà với thiên nhiên xanh và sông thanh bình, thiết kế với diện tích đa dạng từ 160m2 đến 600m2, bao gồm 3 tầng cao. Những căn biệt thự Ecopark Nhơn Trạch trước khi về tay gia chủ sẽ được bàn giao hoàn thiện mặt ngoài và xây thô bên trong, giúp khách hàng dễ dàng tân trang lại ngôi nhà theo sở thích của mình. Giá bán cho mỗi căn biệt thự thường dao động từ 26-58 tỷ đồng/căn",
                    Price = 26000000000000m,
                    Address = "Ấp Bến Cò, thị trấn Đại Phước, thuộc địa phận huyện Nhơn Trạch, tỉnh Đồng Nai.",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Rent",
                    Type = "Office",
                    ImageUrl = "/img/property-6.jpg"
                },
                new Property
                {
                    Id = 7,
                    Title = " Ecopark Hưng Yên",
                    Description = "Ecopark Hưng Yên cũng là dự án thuộc quyền sở hữu của nhà sáng lập Ecopark. Ecopark Hưng Yên được quy hoạch đầu tư theo trình tự bài bản, chia ra làm 9 giai đoạn phát triển hình thành như: Palm Springs, Aqua Bay, Park River, Education Hub, Ecopark Grand, Ecopark CBD, Dragon Islands, Golf EPGA, Sân Golf 18 lỗ đẳng cấp,với tổng vốn đầu tư cho việc xây dựng tới 800 triệu USD. Công trình này được khởi công xây dựng từ năm 2009 và dự kiến hoàn thành vào năm 2029. Biệt thự Ecopark Hưng Yên gây ấn tượng bởi thiết kế độc đáo với đa dạng phong cách lấy cảm hứng thiết kế như làng cổ Hà Lan, cảnh quan Châu Âu, vườn tùng nhiệt đới, Thung lũng mùa xuân…",
                    Price = 50000000000000m,
                    Address = "Ecopark Hưng Yên sở hữu vị trí lý tưởng tại huyện Văn Giang, tỉnh Hưng Yên, phía Đông Nam Hà Nội, cách trung tâm Thủ đô chỉ 12,8 km. Vị trí đắc địa này không chỉ mang ý nghĩa phong thủy “nhân vượng – gia an” mà còn thuận tiện kết nối với các khu vực trọng điểm nhờ hệ thống giao thông đồng bộ, bao gồm các trục đường huyết mạch như cao tốc Hà Nội – Hải Phòng, quốc lộ 5 và tuyến vành đai 3.",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Sale",
                    Type = "Office",
                    ImageUrl = "/img/property-7.jpg"
                },
                new Property
                {
                    Id = 8,
                    Title = "Sun Hà Nam ",
                    Description = "Tiện ích đa dạng và đẳng cấp: như khu vui chơi giải trí Sunworld rộng 19 ha, khu hành chính mới 28 ha, hệ thống trường học, bệnh viện, trung tâm thương mại và khu thể thao. Những tiện ích này đáp ứng đầy đủ nhu cầu sống, làm việc và giải trí tối đa chỉ trong vài bước chân của cư dân",
                    Price = 79000000000000m,
                    Address = "trung tâm huyện Kim Bảng, tỉnh Hà Nam",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Rent",
                    Type = "Office",
                    ImageUrl = "/img/property-8.jpg"
                },
                new Property
                {
                    Id = 9,
                    Title = " Ixora Hồ Tràm",
                    Description = "Một thiên đường nghỉ dưỡng với mặt trước là biển, sau lưng là núi gọi tên Ixora Hồ Tràm, bao gồm 109 căn biệt thự biển 2 tầng với diện tích đất từ 296m2 đến 559m2, đều được bố trí hồ bơi và sân vườn riêng, thiết kế thông minh với độ dốc lý tưởng giúp các căn biệt thự có tầm nhìn biển tuyệt mỹ huyền dịu.",
                    Price = 18000000000000m,
                    Address = "Điểm ấn tượng đầu tiên khi nói đến vị trí của dự án Ixora Ho Tram by Fusion chính là vị thế “tựa sơn hướng thủy”, với một bên là dải biển dài nguyên sơ của Hồ Tràm, một bên là mảng xanh bạt ngàn của rừng nguyên sinh Phước Bửu – Bình Châu có diện tích tự nhiên hơn 10.000 ha. Đây sẽ là nơi du khách không chỉ tận hưởng những làn gió biển tươi mát mà còn có thể cảm nhận rõ ràng một không gian ngập tràn mảng xanh, một bầu không khí trong lành và thuần khiết.",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Sale",
                    Type = "Office",
                    ImageUrl = "/img/property-9.jpg"
                },
                new Property
                {
                    Id = 10,
                    Title = " Banyan Tree Huế",
                    Description = "Theo Robb Report – một tạp chí uy tín hàng đầu của giới thượng lưu đã bình chọn Laguna Lăng Cô là một trong trong 4 khu nghỉ dưỡng đẳng cấp bậc nhất Việt Nam bên cạnh Amanoi, InterContinental Da Nang Sun Peninsula Resort, JW Marriott Phu Quoc Emerald Bay. Điều đặc biệt, Laguna Lăng Cô là dự án duy nhất trong top 4 có kinh doanh bất động sản nghỉ dưỡng với khu biệt thự biển Banyan Tree Residences và khu biệt thự liên kế Laguna Park",
                    Price = 40000000000000m,
                    Address = "Huế.",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Rent",
                    Type = "Office",
                    ImageUrl = "/img/property-10.jpg"
                },
                new Property
                {
                    Id = 11,
                    Title = " Arbora Đà Nẵng ",
                    Description = "Tại The Residences at Arbora, mỗi khoảnh khắc đều trở thành một kiệt tác, được tô điểm bởi những tiện ích đẳng cấp và tinh tế. Nơi đây không chỉ là điểm đến lý tưởng cho giới xa xỉ nghỉ dưỡng mà còn là thiên đường điểm đến cho con người tìm thấy niềm vui mới, tái tạo năng lượng, vun đắp cảm xúc và khơi dậy những giác quan.",
                    Price = 124000000000000m,
                    Address = "KM số 0 đường Thanh niên ven biển, KP Viêm Đông, P. Điện Ngọc, Thị xã Điện Bàn, Quảng Nam.",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Sale",
                    Type = "Office",
                    ImageUrl = "/img/property-11.jpg"
                },
                new Property
                {
                    Id = 12,
                    Title = " InterContinental Sun Peninsula Đà Nẵng",
                    Description = "Tách biệt với khu vực chính của khu nghỉ dưỡng, biệt thự Sun Peninsula Residence là không gian nghỉ dưỡng riêng tư và yên bình tuyệt đối, tựa như một ốc đảo biệt lập, giúp du khách hòa mình vào thiên nhiên hoang sơ, thụ hưởng bầu không khí trong lành và thư giãn tâm hồn. Chỉ cách vài bước chân là du khách đã có thể đặt chân lên bờ cát trắng mịn màng, tận hưởng làn nước biển xanh ngọc bích và cảm nhận sự mát lạnh của gió biển.",
                    Price = 26000000000000m,
                    Address = "Đà Nẵng.",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Rent",
                    Type = "Office",
                    ImageUrl = "/img/property-12.jpg"
                },
                new Property
                {
                    Id = 13,
                    Title = " The Villas Vinhomes Central Park",
                    Description = "Khởi công xây dựng vào năm 2014, đến năm 2017, 88 căn biệt thự tại The Villas Vinhomes Central Park đã được bàn giao cho cư dân, đánh dấu sự hoàn thiện của một kiệt tác kiến trúc ven sông. Khu biệt thự sở hữu 2 loại hình chính là biệt thự đơn lập và biệt thự song lập, diện tích đa dạng từ 223-700m2 với 3 mặt thoáng.Mỗi căn biệt thự đều được thiết kế theo phong cách tân cổ điển, cảm hứng từ hình ảnh “New York thu nhỏ” giữa lòng Sài Gòn, cùng những chiếc mái vát màu khói thanh lịch theo đường nét tinh tế, tỉ mỉ, hệ thống cửa sổ rộng rãi giúp đón trọn ánh sáng tự nhiên và tầm nhìn khoáng đạt ra sông Sài Gòn phồn hoa, đặc biệt rất phù hợp cho những gia chủ ưu tiên lối thiết kế tối giản và thanh lịch. Biệt thự Vinhomes Central Park có giá từ 130 tỷ đồng.",
                    Price = 26000000000000m-130000000000000m,
                    Address = "208 Nguyễn Hữu Cảnh, Phường 22, Quận Bình Thạnh, TP. Hồ Chí Minh",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Sale",
                    Type = "Office",
                    ImageUrl = "/img/property-13.jpg"
                },
                new Property
                {
                    Id = 14,
                    Title = " Holm Villas Thảo Điền",
                    Description = "Điểm nhấn đặc biệt của Holm Villa Thảo Điền chính là khuôn viên biệt thự được bao phủ toàn bộ mảng xanh, lý do duy nhất vì chủ đầu tư muốn đem đến một môi trường sống với không gian trong lành và cảm giác thư thái cho gia chủ sau mỗi ồn ào náo nhiệt hàng ngày. Có thể nói Holm Thảo Điền là nơi rất đáng sống và hiếm một dự án có thể sở hữu được những ưu điểm tương tự như vậy tại TP Hồ Chí Minh. Giá giao động mỗi căn từ 50-131 tỷ",
                    Price = 50000000000000m,
                    Address = "TP.HCM",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Rent",
                    Type = "Office",
                    ImageUrl = "/img/property-14.jpg"
                },
                new Property
                {
                    Id = 15,
                    Title = " The Victoria Vinhomes Golden River",
                    Description = "Lấy cảm hứng từ những thành phố ven sông sôi động trên thế giới, Vinhomes Golden River sở hữu kiến trúc nguy nga và tinh tế. Các biệt thự còn được thiết kế hài hòa với cảnh quan thiên nhiên, tạo nên một tổng thể đẹp mắt và ấn tượng. Nội thất của các biệt thự cũng được nhập khẩu từ những thương hiệu uy tín trên thế giới, phục vụ nhu cầu sống đẳng cấp và tiện nghi cho gia chủ. Với 54 căn biệt thự diện tích từ 225 m2 – 275 m2 – 475 m2 có giá dao động 154-500 tỷ",
                    Price = 500000000000000m,
                    Address = "số 2, đường Tôn Đức Thắng, phường Bến Nghé, quận 1, thành phố Hồ Chí Minh.",
                    Size = 1500,
                    Bed = 0,
                    Bath = 2,
                    Status = "For Sale",
                    Type = "Office",
                    ImageUrl = "/img/property-15.jpg"
                }



            );
        }
    }
}
