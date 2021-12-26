using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineValve
{
	[Serializable]
	public class ParamException : Exception
	{
		public ParamException()
		{
		}

		public ParamException(string message) : base(message)
		{
		}

		public ParamException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public ParamException(List<Exception> exceptions)
		{
			string message = "Parameter errors: \n";
			message += string.Join(";\n", exceptions);
			message += '.';
			new ParamException(message);
		}
	}
}
