using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CowLevel.Data
{
	public class BCData
	{
		public List<string> LoadData(string _path)
		{
			string[] _datas = File.ReadAllLines(_path);
			return _datas.ToList();
		}
	}
}