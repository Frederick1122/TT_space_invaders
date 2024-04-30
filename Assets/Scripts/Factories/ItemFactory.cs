using Configs;
using Ships;

namespace Factories
{
    public class ItemFactory : Factory//<Item, ItemConfig>
    {
        protected override string _secondPrefabPath => "Item";
    }
}