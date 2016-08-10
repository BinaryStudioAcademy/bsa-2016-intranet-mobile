using System;
namespace IntranetMobile.Core
{
	public interface ILogger
	{
		void Error(Exception e);

		void Error(string message);

		void Error(string message, Exception e);

		void Info(string message);
	}
}

