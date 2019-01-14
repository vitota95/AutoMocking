// <copyright company="Roche Diagnostics AG">
// Copyright (c) Roche Diagnostics AG. All rights reserved.
// </copyright>

using Microsoft.Practices.Unity;

using Moq;

namespace Roche.LabCore.Utilities.UnitTesting
{
    /// <summary>
    /// Provides extension methods for the <see cref="IUnityContainer"/> interface that
    /// allow register and resolve mocks.
    /// </summary>
    public static class UnityExtensions
    {
        /// <summary>
        /// Allows registering a mock in the <see cref="IUnityContainer"/>
        /// </summary>
        /// <typeparam name="T"> The type that we are mocking. </typeparam>
        /// <param name="container"> The <see cref="IUnityContainer"/> </param>
        /// <param name="mockBehavior"> Optional mock behavior, by default loose</param>
        /// <returns> The registered mock. </returns>
        public static Mock<T> RegisterMock<T>(this IUnityContainer container, MockBehavior mockBehavior = MockBehavior.Loose) where T : class
        {
            var mock = new Mock<T>(mockBehavior);

            container.RegisterInstance<Mock<T>>(mock);
            container.RegisterInstance<T>(mock.Object);

            return mock;
        }

        /// <summary>
        /// Simplifies the syntax to add the auto mock feature to the <see cref="IUnityContainer"/>.
        /// </summary>
        /// <param name="container"> The unity container. </param>
        /// <returns> The extended container. </returns>
        public static IUnityContainer WithAutoMocking(this IUnityContainer container)
        {
            return container.AddNewExtension<AutoMockingContainerExtension>();
        }
    }
}
