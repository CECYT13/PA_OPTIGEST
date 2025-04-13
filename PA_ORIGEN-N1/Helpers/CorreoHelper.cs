using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace PA_ORIGEN_N1.Helpers
{
    public class CorreoHelper
    {
        private readonly string _apiKey;

        public CorreoHelper(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task EnviarVerificacionAsync(string correoDestino, string nombre, string token, string urlBase)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("no-reply@optigest.com", "OPTIGEST");
            var subject = "Verifica tu cuenta";
            var to = new EmailAddress(correoDestino, nombre);

            var link = $"{urlBase}/Acceso/VerificarCorreo?token={token}";

            var htmlContent = $@"
                <h3>Hola {nombre},</h3>
                <p>Gracias por registrarte en <strong>OPTIGEST</strong>.</p>
                <p>Para activar tu cuenta, haz clic en el siguiente enlace:</p>
                <p><a href='{link}' style='background:#4CAF50;color:white;padding:10px 20px;text-decoration:none;'>Verificar Cuenta</a></p>
                <p>Si no solicitaste esto, puedes ignorar el mensaje.</p>
            ";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}



