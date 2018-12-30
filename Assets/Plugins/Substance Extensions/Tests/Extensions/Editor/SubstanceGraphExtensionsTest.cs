using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Substance.Game;

namespace SubstanceExtensions.Tests
{
    public class SubstanceGraphExtensionsTest
    {
        [Test(Description="Checks that an SubstanceExtensionsTestDataCollection instance exists in the project.")]
        public void SubstanceExtensionsTestDataIsNotNull()
        {
            Assert.NotNull(SubstanceExtensionsTestData.Instance);
        }

        [Test(Description="Checks that SetOutputSize(int) functions properly.")]
        public void SetOutputSizeTest()
        {
            Substance.Game.Substance subs = SubstanceExtensionsTestData.Instance.TestSubstance;
            SubstanceGraph graph = subs.graphs[0];

            int cachedSize = graph.GetInputVector2Int(SubstanceGraphExtensions.OutputSizeParameter)[0];
            int newSize = cachedSize == 9 ? 10 : 9;

            graph.SetOutputSize(newSize);
            graph.QueueForRender();

            Substance.Game.Substance.RenderSubstancesSync();

            int width = graph.GetGeneratedTextures()[0].width;

            Debug.Log(string.Format("{0} | {1} | {2} - {3}", width, (newSize == 10 ? 1024 : 512), cachedSize, newSize));

            Assert.AreEqual(width, (newSize == 10 ? 1024 : 512));

            graph.SetOutputSize(cachedSize);
            graph.QueueForRender();

            Substance.Game.Substance.RenderSubstancesSync();
        }

        [Test(Description="Checks that SetOutputSize(SubstanceOutputSize) functions properly.")]
        public void SetOutputSizeEnumTest()
        {
            Substance.Game.Substance subs = SubstanceExtensionsTestData.Instance.TestSubstance;
            SubstanceGraph graph = subs.graphs[0];

            int cachedSize = graph.GetInputVector2Int(SubstanceGraphExtensions.OutputSizeParameter)[0];
            SubstanceOutputSize newSize = cachedSize == 9 ? SubstanceOutputSize._1024 : SubstanceOutputSize._512;

            Debug.Log(graph.GetGeneratedTextures()[0].width);

            graph.SetOutputSize(newSize);

            //NativeFunctions.cppProcessOutputQueue();
            graph.QueueForRender();
            Substance.Game.Substance.RenderSubstancesSync();

            int width = graph.GetGeneratedTextures()[0].width;


            Debug.Log(graph.GetGeneratedTextures()[0].width);

            Assert.AreEqual(width, (newSize == SubstanceOutputSize._1024 ? 1024 : 512));
            
            graph.SetOutputSize(cachedSize);

            //NativeFunctions.cppProcessOutputQueue();
            graph.QueueForRender();
            Substance.Game.Substance.RenderSubstancesSync();
        }
    }
}