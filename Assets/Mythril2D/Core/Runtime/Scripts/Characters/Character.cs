using UnityEngine;

namespace Gyvr.Mythril2D
{
    public abstract class Character<CharacterSheetDerivation> : CharacterBase
        where CharacterSheetDerivation : CharacterSheet
    {
        [Header("角色设置")] [SerializeField] protected CharacterSheetDerivation m_sheet = null;

        public override CharacterSheet characterSheet => m_sheet;
    }
}