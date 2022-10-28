using System.Net;
using System.Runtime.Serialization;

namespace FxCryptApp.Common.ApplicationExceptions;

public class NotFoundException : BaseApplicationException
{
	public NotFoundException(HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(statusCode)
	{
	}

	public NotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(message, statusCode)
	{
	}

	public NotFoundException(string message, Exception innerException, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(message, innerException, statusCode)
	{
	}

	protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
