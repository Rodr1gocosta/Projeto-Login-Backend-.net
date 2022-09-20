using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Sistema_Login.Models;
using System;

namespace Sistema_Login.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal void EnviarEmail(string[] destinatario, string assunto, int usuarioId, string code)
        {
            Mensagem mensagem = new Mensagem(destinatario, assunto, usuarioId, code);
            var mensagemDeEmail = CriaCorpoDoEmail(mensagem);
            Enviar(mensagemDeEmail);
        }

        private MimeMessage CriaCorpoDoEmail(Mensagem mensagem)
        {
            var mensagemDeEmail = new MimeMessage();
            mensagemDeEmail.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From")));
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            mensagemDeEmail.Subject = mensagem.Assunto;
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };
            return mensagemDeEmail;
        }

        private void Enviar(MimeMessage mensagemDeEmail)
        {
            using(var cliente = new SmtpClient())
            {
                try
                {
                    cliente.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"), true);
                    cliente.AuthenticationMechanisms.Remove("XOUATH2");
                    cliente.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password"));
                    cliente.Send(mensagemDeEmail);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    cliente.Disconnect(true);
                    cliente.Dispose();
                }
            }
        }
    }
}
