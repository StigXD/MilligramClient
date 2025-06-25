using MilligramClient.Domain.Dtos;
using MilligramClient.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilligramClient.Domain.Extensions;

public static class ContactExtensions
{
    public static ContactDto ToDto (this ContactModel contact)
    {
        return new ContactDto
        {
            Name = contact.Name,
            
        };
    }
}
