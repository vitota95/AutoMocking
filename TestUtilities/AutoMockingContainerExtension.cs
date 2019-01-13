// <copyright company="Roche Diagnostics AG">
// Copyright (c) Roche Diagnostics AG. All rights reserved.
// </copyright>

using System;

using Moq;

using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Roche.LabCore.Utilities.UnitTesting
{
    /// <summary>
    /// Extension class to be able to add auto mocking to the unity container. This will help
    /// in the creation of system under tests saving development time and improving maintainability.
    /// </summary>
    public class AutoMockingContainerExtension : UnityContainerExtension
    {
        /// <inheritdoc />
        protected override void Initialize()
        {
            var strategy = new AutoMockingBuilderStrategy(this.Container);

            this.Context.Strategies.Add(strategy, UnityBuildStage.PreCreation);
        }

        private class AutoMockingBuilderStrategy : BuilderStrategy
        {
            private readonly IUnityContainer container;

            public AutoMockingBuilderStrategy(IUnityContainer container)
            {
                this.container = container;
            }

            public override void PreBuildUp(IBuilderContext context)
            {
                var key = context.OriginalBuildKey;

                if (key.Type.IsInterface && !this.container.IsRegistered(key.Type))
                {
                    context.Existing = CreateDynamicMock(key.Type);
                }
            }

            private static object CreateDynamicMock(Type type)
            {
                var genericMockType = typeof(Mock<>).MakeGenericType(type);
                var mock = (Mock)Activator.CreateInstance(genericMockType);
                return mock.Object;
            }
        }
    }
}
