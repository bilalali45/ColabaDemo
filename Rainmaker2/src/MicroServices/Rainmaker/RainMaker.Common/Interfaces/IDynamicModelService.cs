using System.Collections;
using System.Linq;

namespace RainMaker.Common.Interfaces
{
    public interface IDynamicModelService
    {
        IEnumerable GetList(DynamicListCommand command, string displayField, string searchtext);
        IEnumerable GetListByValue(DynamicListCommand command, string valueField, string[] values);
        IEnumerable GetList(DynamicListCommand command);
    }
}
