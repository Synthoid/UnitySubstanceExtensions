using UnityEngine;
using Substance.Game;

namespace SubstanceExtensions.Tests
{
    [CreateAssetMenu(fileName="Substance Extensions Test Data", menuName="Substance/Extensions/Test Data")]
    public class SubstanceExtensionsTestDataCollection : ScriptableObject
	{
        [SerializeField]
        private Substance.Game.Substance testSubstance;

        public Substance.Game.Substance TestSubstance
        {
            get { return testSubstance; }
        }
    }
}