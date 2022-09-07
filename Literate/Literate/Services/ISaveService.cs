using System;
using System.Collections.Generic;
using System.Text;

namespace Literate.Services
{
	public interface ISaveService
	{
		void SaveFile(string fileName, byte[] data);
	}
}
