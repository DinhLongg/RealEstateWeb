using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RealEstateWeb.Services
{
    // Implements Microsoft built-in IEmailSender
    public class DevEmailSender : IEmailSender
    {
        private readonly IWebHostEnvironment _env;

        public DevEmailSender(IWebHostEnvironment env)
        {
            _env = env;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var dir = Path.Combine(_env.WebRootPath, "emails");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid()}.html";
            var filePath = Path.Combine(dir, fileName);

            var content = $@"
                <h2>{subject}</h2>
                <p>To: {email}</p>
                <hr/>
                {htmlMessage}
            ";

            File.WriteAllText(filePath, content);

            return Task.CompletedTask;
        }
    }
}
