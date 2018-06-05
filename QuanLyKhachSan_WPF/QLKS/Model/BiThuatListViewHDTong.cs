using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKS.Model
{
    public class BiThuatListViewHDTong
    {
        public ObservableCollection<string> BiThuat { get; set; }

        public BiThuatListViewHDTong()
        {
            BiThuat = new ObservableCollection<string>();
        }
    }
}
