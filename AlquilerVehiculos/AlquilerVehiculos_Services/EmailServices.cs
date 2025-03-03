using AlquilerVehiculos_Entity;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;



namespace AlquilerVehiculos_Services
{
    
    public static class EmailServices
    {
        private static string _host = "smtp.gmail.com";
        private static int _puerto = 587;
        private static string nombre_envio = "Alquiler Vehículos";
        private static string _correo = "2c.bonilla.jairo0@gmail.com";
        private static string _clave = "mjuasrpjorjitvoq";//mjua srpj orji tvoq

        public static bool Enviar(Correo correo)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(nombre_envio, _correo));
                email.To.Add(MailboxAddress.Parse(correo.Para));
                email.Subject = correo.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = correo.Contenido
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_host, _puerto, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_correo, _clave); 
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                return false;
            }
        }

    }
}
