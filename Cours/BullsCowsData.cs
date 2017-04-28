using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CowLevel
{
	public class BullsCowsData
	{
		public List<string> LoadData(string _path)
		{
			string[] _datas = File.ReadAllLines(_path);
			return _datas.ToList();
		}
	}
}