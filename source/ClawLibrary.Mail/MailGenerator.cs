using System;
using System.Collections.Generic;
using System.IO;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.MediaStorage;
using ClawLibrary.Mail.Models;
using MimeKit;
using MimeKit.Utils;

namespace ClawLibrary.Mail
{
    public class MailGenerator : IMailGenerator
    {
        private readonly IMailDataService _dataService;
        private readonly IMediaStorageAppService _mediaStorageService;
        private readonly MailConfig _config;

        public MailGenerator(IMailDataService dataService, IMediaStorageAppService mediaStorageService,
            MailConfig config)
        {
            _dataService = dataService;
            _mediaStorageService = mediaStorageService;
            _config = config;
        }

        public MimeMessage PrepareEmailFromTemplate(Email email)
        {
            var content = ReadTemplate(email.EmailTemplatePath);
            content = ReplaceVariables(content, email.ElementsToReplace);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(email.SenderName, _config.SenderEmail));
            message.To.Add(new MailboxAddress(email.ReceiverName, email.ReceiverEmail));
            message.Subject = email.EmailSubject;
            var bodyBuilder = new BodyBuilder();

            content = InsertImages(bodyBuilder, content, email.Images);

            bodyBuilder.HtmlBody = content;
            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }

        private string InsertImages(BodyBuilder bodyBuilder, string content, Dictionary<string, string> images)
        {
            foreach (var image in images)
            {
                content = InsertImage(bodyBuilder, image.Value, image.Key, content);
            }
            return content;
        }

        private string InsertImage(BodyBuilder bodyBuilder, string imagePath, string imageTag, string content)
        {
            try
            {
                var imageBytes = _mediaStorageService.GetMedia(imagePath);
                Stream stream = new MemoryStream(imageBytes);

                var image = bodyBuilder.LinkedResources.Add(imageTag, stream);
                image.ContentId = MimeUtils.GenerateMessageId();
                content = content.Replace($"{{{imageTag}}}", image.ContentId);
            }
            catch (BusinessException e)
            {
                if (!Equals(e.ErrorCode, (Enum)ErrorCode.FileNotFound))
                {
                    throw;
                }
            }
            return content;
        }

        private string ReadTemplate(string name)
        {
            var template = _dataService.GetByName(name);
            return template.Result;
        }

        private string ReplaceVariables(string content, Dictionary<string, string> elementsToReplace)
        {
            foreach (var element in elementsToReplace)
            {
                content = content.Replace($"{{{element.Key}}}", element.Value);
            }
            return content;
        }

        public MimeMessage PrepareEmail(UserResetPasswordEmail emailDetails)
        {
            var emailSubject = $"ClawLibrary - {_config.PasswordResetEmailSubject}";
            var emailTemplate = _config.PasswordResetTemplate;

            var elementsToReplace = new Dictionary<string, string>()
            {
                {"receiverName", emailDetails.ReceiverName},
                {"passwordResetKey", emailDetails.PasswordResetKey},
                {"contactEmail", emailDetails.ContactEmail ?? _config.SenderEmail},
                {"baseUrl", _config.BaseUrl}
            };
            var images = new Dictionary<string, string>()
            {
                {"banner", _config.BannerFileId},
                {"facebook", _config.FacebookLogoFileId},
                {"twitter", _config.TwitterLogoFileId}
            };

            var email = new Email()
            {
                SenderName = emailDetails.SenderName,
                ReceiverName = emailDetails.ReceiverName,
                ReceiverEmail = emailDetails.ReceiverEmail,
                EmailSubject = emailSubject,
                EmailTemplatePath = emailTemplate,
                ElementsToReplace = elementsToReplace,
                Images = images
            };

            var message = PrepareEmailFromTemplate(email);

            return message;
        }

        public MimeMessage PrepareEmail(VerificationEmail emailDetails)
        {
            var emailSubject = $"ClawLibrary - {_config.AccountVerificationEmailSubject}";
            var emailTemplate = _config.AccountVerificationTemplate;

            var elementsToReplace = new Dictionary<string, string>()
            {
                {"receiverName", emailDetails.ReceiverName},
                {"userKey", emailDetails.CustomerKey},
                {"orgName", emailDetails.SenderName},
                {"contactEmail", emailDetails.ContactEmail ?? _config.SenderEmail},
                {"baseUrl", _config.BaseUrl}
            };
            var images = new Dictionary<string, string>()
            {
                {"banner", _config.BannerFileId},
                {"facebook", _config.FacebookLogoFileId},
                {"twitter", _config.TwitterLogoFileId}
            };

            var email = new Email()
            {
                SenderName = emailDetails.SenderName,
                ReceiverName = emailDetails.ReceiverName,
                ReceiverEmail = emailDetails.ReceiverEmail,
                EmailSubject = emailSubject,
                EmailTemplatePath = emailTemplate,
                ElementsToReplace = elementsToReplace,
                Images = images
            };

            var message = PrepareEmailFromTemplate(email);

            return message;
        }
    }
}
