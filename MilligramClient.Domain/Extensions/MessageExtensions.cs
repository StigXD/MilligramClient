using MilligramClient.Domain.Dtos;
using MilligramClient.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilligramClient.Domain.Extensions;
public static class MessageExtensions
{
    public static MessageDto ToDto (this MessageModel message)
    {
        return new MessageDto
        {
            Text = message.Text,
            UserNickname = message.Sender,
            CreationTime = message.Timestamp,
            LastChangeTime = message.Timestamp
        };
    }
}
