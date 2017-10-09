using System;
using System.Text;
using System.Text.RegularExpressions;
using OpenPop.Pop3;
using OpenQA.Selenium;

namespace Autotests.Tests
{
    public class GetEmailBasePage : GetConstantBasePage
    {
        public string GetTokenEmail(string email, string pass)
        {
            var idUser = GetUserIdByName(email);
            using (Pop3Client pop3Client = new Pop3Client())
            {
                try
                {
                    if (pop3Client.Connected)
                        pop3Client.Disconnect();
                    pop3Client.Connect("pop.yandex.ru", 995, true);
                    pop3Client.Authenticate(email, pass);
                }
                catch (Exception ex)
                {
                    throw new NoSuchElementException("Ошибка при подключении к ящику: " + ex.Message);
                }
                int count = pop3Client.GetMessageCount();
                if (count > 0)
                {
                    for (var i = 1; i < count; i= i + 1)
                    {
                        var emaiFrom = pop3Client.GetMessage(i).ToMailMessage().From;
                        if (emaiFrom.ToString() == "notify@ddelivery.ru")
                        {
                            if (pop3Client.GetMessage(i).ToMailMessage().Subject == "Запрос на смену пароля")
                            {
                                var messageBit = pop3Client.GetMessage(i).RawMessage;
                                var message = Encoding.UTF8.GetString(messageBit);
                                Regex reg = new Regex("change_password/(.*?):" + idUser, RegexOptions.IgnoreCase);
                                foreach (Match mtch in reg.Matches(message))
                                {
                                    if (mtch.Success)
                                    {
                                        string nameToken = mtch.ToString();
                                        return nameToken.Replace("change_password/", "").Replace(":" + idUser, "");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
