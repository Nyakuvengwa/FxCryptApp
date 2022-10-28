using System.Net;
using System.Runtime.Serialization;

namespace FxCryptApp.Common.ApplicationExceptions;

[Serializable]
public class BaseApplicationException : Exception
{
	public HttpStatusCode StatusCode { get;}
	public BaseApplicationException(HttpStatusCode statusCode)
	{
		StatusCode = statusCode;
	}

	public BaseApplicationException(string message, HttpStatusCode statusCode) : base(message)
	{
		StatusCode = statusCode;
	}

	public BaseApplicationException(string message, Exception innerException, HttpStatusCode statusCode) : base(message, innerException)
	{
		StatusCode = statusCode;
	}

	protected BaseApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}


}