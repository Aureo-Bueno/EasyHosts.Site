using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Easy.Hosts.Site.App_Start
{
    public class Functions
    {
        public static string HashText(string text, string nameHash)
        {
            HashAlgorithm algoritmo = HashAlgorithm.Create(nameHash);
            if (algoritmo == null)
            {
                throw new ArgumentException("Nome de hash incorreto", "nomeHash");
            }
            byte[] hash = algoritmo.ComputeHash(Encoding.UTF8.GetBytes(text));
            return Convert.ToBase64String(hash);
        }

        public static string CodeBookigSort()
        {
            Random codeBooking = new Random();
            int result = codeBooking.Next(999999);
            return result.ToString();
        }

        public static string SendEmail(string emailRecipient, string subject, string bodymessage)
        {
            try
            {
                //Cria o endereço de email do remetente
                MailAddress inemail = new MailAddress("Easy Hosts <hostseasy@gmail.com>");
                //Cria o endereço de email do destinatário -->
                MailAddress foremail = new MailAddress(emailRecipient);
                MailMessage message = new MailMessage(inemail, foremail);
                message.IsBodyHtml = true;
                //Assunto do email
                message.Subject = subject;
                //Conteúdo do email
                message.Body = bodymessage;
                //Prioridade E-mail
                message.Priority = MailPriority.Normal;
                //Cria o objeto que envia o e-mail
                SmtpClient client = new SmtpClient();
                //Envia o email
                client.Send(message);
                return "success|E-mail enviado com sucesso";
            }
            catch { return "error|Erro ao enviar e-mail"; }
        }

        public static decimal QuantityDaysBooking(DateTime dateCheckin, DateTime dateCheckout, decimal valueBooking)
        {
            int days = dateCheckout.Day - dateCheckin.Day;

            return days * valueBooking;
        }

    }
}