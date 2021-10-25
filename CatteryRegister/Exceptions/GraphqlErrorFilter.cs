using HotChocolate;
using Microsoft.Extensions.Logging;

namespace CatteryRegister.Exceptions
{
    public class GraphQlErrorFilter : IErrorFilter
    {
        private ILogger _logger;

        public GraphQlErrorFilter(ILogger<GraphQlErrorFilter> logger)
        {
            _logger = logger;
        }

        public IError OnError(IError error)
        {
            if (error.Exception is BusinessException domainException)
            {
                return ErrorBuilder.New()
                    .SetMessage(domainException.Message)
                    .SetCode(domainException.Code).SetPath(error.Path).Build();
            }
            return error;
        }
    }
}