using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.Requests.Abstract
{
    internal interface IRequest
    {
        HttpMethod Method { get; }
        string MethodName { get; }

        HttpContent? ToHttpContent();
    }
}
