using KikisCom.Server.Models.ApiModels.Kiki;
using KikisCom.Server.Services.Interfaces;
using KikisCom.Server.WorkClasses;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
using KikisCom.Domain.Models;

namespace KikisCom.Server.Services
{
    public class MailService : IMailService
    {
        public async Task SendEmailRegister(string to, string login, string password, Mail smtp)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Kiki", smtp.FromMail));
                message.To.Add(new MailboxAddress("KikiReportUser", to));
                message.Subject = "Создан аккаунт";

                var builder = new BodyBuilder();
                builder.HtmlBody =
                "<!DOCTYPE html>\r\n" +
                "<html>\r\n" +
                "<head>\r\n" +
                "    <title>Email</title>\r\n" +
                "    <style>\r\n" +
                "        body, html {\r\n" +
                "            margin: 0;\r\n" +
                "            padding: 0;\r\n" +
                "            width: 100%;\r\n" +
                "            height: 100%;\r\n" +
                "            font-family: Arial, sans-serif;\r\n" +
                "        }\r\n" +
                "        .container {\r\n" +
                "            width: 40%;\r\n" +
                "            margin: 0 auto;\r\n" +
                "            padding: 20px;\r\n" +
                "            min-width: 400px;\r\n" +
                "            box-sizing: border-box;\r\n" +
                "            border-radius: 30px;\r\n" +
                "            background-color: #f9f9f9;\r\n" +
                "        }\r\n" +
                "        h1 {\r\n" +
                "            text-align: center;\r\n" +
                "            font-weight: bold;\r\n" +
                "        }\r\n" +
                "        p {\r\n" +
                "            margin: 10px 0;\r\n" +
                "            text-align: justify;\r\n" +
                "        }\r\n" +
                "        .button {\r\n" +
                "            display: block;\r\n" +
                "            width: 40%;\r\n" +
                "            padding: 10px 20px;\r\n" +
                "            margin: 20px auto;\r\n" +
                "            background-color: #007bff;\r\n" +
                "            color: white;\r\n" +
                "            text-align: center;\r\n" +
                "            border-radius: 5px;\r\n" +
                "            text-decoration: none;\r\n" +
                "        }\r\n" +
                "        .center {\r\n" +
                "            text-align: center;\r\n" +
                "            font-weight: bold;\r\n" +
                "        }\r\n" +
                "    </style>\r\n" +
                "</head>\r\n" +
                "<body>\r\n" +
                "<div class=\"container\">\r\n" +
                "    <h1>Создание аккаунта</h1>\r\n" +
                "    <p>Аккаунт успешно создан</p>\r\n" +
                "    <p>Используйте логин и пароль для входа в систему</p>\r\n" +
                "    <a href=\"http://185.133.40.6/\" class=\"button\">Перейти</a>\r\n" +
                "    <p>Или перейдите по <a href=\"http://185.133.40.6/\">ссылке</a> и введите логин и пароль</p>\r\n" +
                "    <p class=\"center\">" + login + "</p>\r\n" +
                "    <p class=\"center\">" + password + "</p>\r\n" +
                "</div>\r\n" +
                "</body>\r\n" +
                "</html>";
                message.Body = builder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(smtp.SMTPPatch, int.Parse(smtp.SMTPPort), false);
                    await client.AuthenticateAsync(smtp.FromMail, smtp.SMTPPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                WriteLog.Info($"Registration message was sended to user, addres {to}");
            }
            catch (Exception ex)
            {
                WriteLog.Error($"MailService.SendEmailRegister error, message: {ex.Message}");
            }
        }

        public static async Task SendEmailNewPassword(string to, string login, string password, Mail smtp)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Kiki", smtp.FromMail));
                message.To.Add(new MailboxAddress("KikiReportUser", to));
                message.Subject = "Изменен пароль";

                var builder = new BodyBuilder();
                builder.HtmlBody =
                "<!DOCTYPE html>\r\n" +
                "<html>\r\n" +
                "<head>\r\n" +
                "    <title>Email</title>\r\n" +
                "    <style>\r\n" +
                "        body, html {\r\n" +
                "            margin: 0;\r\n" +
                "            padding: 0;\r\n" +
                "            width: 100%;\r\n" +
                "            height: 100%;\r\n" +
                "            font-family: Arial, sans-serif;\r\n" +
                "        }\r\n" +
                "        .container {\r\n" +
                "            width: 40%;\r\n" +
                "            margin: 0 auto;\r\n" +
                "            padding: 20px;\r\n" +
                "            min-width: 400px;\r\n" +
                "            box-sizing: border-box;\r\n" +
                "            border-radius: 30px;\r\n" +
                "            background-color: #f9f9f9;\r\n" +
                "        }\r\n" +
                "        h1 {\r\n" +
                "            text-align: center;\r\n" +
                "            font-weight: bold;\r\n" +
                "        }\r\n" +
                "        p {\r\n" +
                "            margin: 10px 0;\r\n" +
                "            text-align: justify;\r\n" +
                "        }\r\n" +
                "        .button {\r\n" +
                "            display: block;\r\n" +
                "            width: 40%;\r\n" +
                "            padding: 10px 20px;\r\n" +
                "            margin: 20px auto;\r\n" +
                "            background-color: #007bff;\r\n" +
                "            color: white;\r\n" +
                "            text-align: center;\r\n" +
                "            border-radius: 5px;\r\n" +
                "            text-decoration: none;\r\n" +
                "        }\r\n" +
                "        .center {\r\n" +
                "            text-align: center;\r\n" +
                "            font-weight: bold;\r\n" +
                "        }\r\n" +
                "    </style>\r\n" +
                "</head>\r\n" +
                "<body>\r\n" +
                "<div class=\"container\">\r\n" +
                "    <h1>Изменение пароля</h1>\r\n" +
                "    <p>Пароль успешно изменен</p>\r\n" +
                "    <p>Используйте логин и пароль для входа в систему</p>\r\n" +
                "    <a href=\"http://185.133.40.6/\" class=\"button\">Перейти</a>\r\n" +
                "    <p>Или перейдите по <a href=\"http://185.133.40.6/\">ссылке</a> и введите логин и пароль</p>\r\n" +
                "    <p class=\"center\">" + login + "</p>\r\n" +
                "    <p class=\"center\">" + password + "</p>\r\n" +
                "</div>\r\n" +
                "</body>\r\n" +
                "</html>";
                message.Body = builder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(smtp.SMTPPatch, int.Parse(smtp.SMTPPort), false);
                    await client.AuthenticateAsync(smtp.FromMail, smtp.SMTPPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                WriteLog.Info($"Password change message was sended to user, address {to}");

            }
            catch (Exception ex)
            {
                WriteLog.Error($"MailService.SendEmailNewPassword error, message: {ex.Message}");
            }
        }
    }
}
