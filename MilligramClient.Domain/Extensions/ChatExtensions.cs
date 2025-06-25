using MilligramClient.Domain.Dtos;
using MilligramClient.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MilligramClient.Domain.Extensions;

public static class ChatExtensions
{
    public static ChatDto ToDto (this ChatModel chat)
    {
        return new ChatDto
        {
            Name = chat.Name,
        };
    }
}
