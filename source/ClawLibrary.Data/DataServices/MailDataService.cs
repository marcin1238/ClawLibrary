using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClawLibrary.Data.DataServices
{
    public class MailDataService : IMailDataService
    {
        private readonly DatabaseContext _context;

        public MailDataService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<string> GetByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var mail = await _context.EmailTemplate
                    .FirstOrDefaultAsync(x => x.Name.ToString().ToLower().Equals(name.ToLower()));
                return mail.Content;
            }
            throw new BusinessException(ErrorCode.InvalidValue, "Mail template name is null or empty");
        }
    }
}
