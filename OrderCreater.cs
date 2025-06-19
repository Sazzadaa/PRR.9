using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppWithoutRefactoring
{
    internal class OrderCreater
    {
        public OrderCreater(string customerName, List<string> items, string paymentMethod) 
        {
            void ValidateOrderData()
            {
                if (string.IsNullOrWhiteSpace(customerName))
                {
                    throw new ArgumentException("Данные заказчика указаны некорректно!", nameof(customerName));
                }

                if (items == null || items.Count == 0)
                {
                    throw new ArgumentException("Не выбраны позиции для заказа!", nameof(items));
                }
            }
            // Логика обработки заказа
            decimal total = 0;

            foreach (string item in items)
            {
                switch (item)
                {
                    case "Ноутбук":
                        total += 1200;
                        break;
                    case "Мышь":
                        total += 25;
                        break;
                    case "Клавиатура":
                        total += 50;
                        break;
                    case "Камера":
                        total += 500;
                        break;
                    case "Колонки":
                        total += 150;
                        break;
                }
            }
            void ApplyDiscount()
            {
                const decimal discountRate = 0.9m; 
                const int minItemsForDiscount = 2;

                if (items.Count > minItemsForDiscount)
                {
                    total *= discountRate;
                }
            }
            void ProcessPayment()
            {
                switch (paymentMethod)
                {
                    case "По карте":
                        ShowCreditCardPayment(total);
                        break;
                    case "PayPal":
                        ShowPayPalPayment(total);
                        break;
                    default:
                        throw new ArgumentException("Неподдерживаемый способ оплаты.", nameof(paymentMethod));
                }
            }

            void ShowCreditCardPayment(decimal amount)
            {
                MessageBox.Show($"Обработка платежа по кредитной карте: {amount:C}");
            }

            void ShowPayPalPayment(decimal amount)
            {
                Console.WriteLine($"Обработка PayPal платежа: {amount:C}");
            }

            void LogOrder()
            {
                string logEntry = $"Order processed for {customerName} at {DateTime.Now}";
                File.WriteAllText(@".\logs\order.txt", logEntry);
            }
            void SendOrderNotification()
            {
                const string smtpHost = "smtp.gmail.com";
                const int smtpPort = 587;
                const string senderEmail = "lomovala@gmail.com";
                const string senderName = "LomovaLA";
                const string recipientEmail = "lomovala@gmail.com";

                using (SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort))
                {
                    MailAddress fromAddress = new MailAddress(senderEmail, senderName);
                    MailAddress toAddress = new MailAddress(recipientEmail);

                    using (MailMessage mailMessage = new MailMessage(fromAddress, toAddress))
                    {
                        mailMessage.Subject = "Новый заказ";
                        mailMessage.Body = $"<h2>Новый заказ для {customerName} общей стоимостью {total:C}.</h2>";
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);
                    }
                }
            }
        }
    }
}
