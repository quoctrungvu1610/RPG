using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        //IEnumerator khac voi IEnumerable la IEnumerable de su dung hon trong for each loop
        IEnumerable<float>GetAdditiveModifier(Stat stat);
    }
}
