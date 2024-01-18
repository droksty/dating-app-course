namespace API.DTOs
{
	/// <summary>
	/// Encapsulates the response to be sent to the client in case of an exception.
	/// </summary>
	public class ErrorDTO
	{
        public int StatusCode { get; set; }
		public string Message { get; set; }
        public string Details { get; set; }

		public ErrorDTO(int statusCode, string message, string details)
		{
			StatusCode = statusCode;
			Message = message;
			Details = details;
		}
	}
}
