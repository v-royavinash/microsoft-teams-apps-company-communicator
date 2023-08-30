// <copyright file="ScheduleSendFunctionTest.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// </copyright>
namespace Microsoft.Teams.Apps.CompanyCommunicator.Send.Func.Test
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Bot.Schema;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Teams.Apps.CompanyCommunicator.Common.Repositories.NotificationData;
    using Microsoft.Teams.Apps.CompanyCommunicator.Common.Repositories.SentNotificationData;
    using Microsoft.Teams.Apps.CompanyCommunicator.Common.Resources;
    using Microsoft.Teams.Apps.CompanyCommunicator.Common.Services.MessageQueues.DataQueue;
    using Microsoft.Teams.Apps.CompanyCommunicator.Common.Services.MessageQueues.PrepareToSendQueue;
    using Microsoft.Teams.Apps.CompanyCommunicator.Common.Services.MessageQueues.SendQueue;
    using Microsoft.Teams.Apps.CompanyCommunicator.Common.Services.Teams;
    using Microsoft.Teams.Apps.CompanyCommunicator.Send.Func.Services;
    using Moq;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    /// Schedule SendFunction test class.
    /// </summary>
    public class ScheduleSendFunctionTest
    {
        private readonly Mock<INotificationDataRepository> notificationDataRepository = new Mock<INotificationDataRepository>();
        private readonly Mock<ISentNotificationDataRepository> sentNotificationDataRepository = new Mock<ISentNotificationDataRepository>();
        private readonly Mock<IPrepareToSendQueue> prepareToSendQueue = new Mock<IPrepareToSendQueue>();
        private readonly Mock<IDataQueue> dataQueue = new Mock<IDataQueue>();
        private readonly double forceCompleteMessageDelayInSeconds = 86400;
        private readonly Mock<ILogger> logger = new Mock<ILogger>();

        /// <summary>
        /// Constructor tests.
        /// </summary>
        [Fact]
        public void ScheduleSendFunctionConstructorTest()
        {
            // Arrange
            Action action1 = () => new ScheduleSendFunction(null /*notificationDataRepository*/, this.sentNotificationDataRepository.Object, this.prepareToSendQueue.Object, this.dataQueue.Object);
            Action action2 = () => new ScheduleSendFunction(this.notificationDataRepository.Object, null /*sentNotificationDataRepository*/, this.prepareToSendQueue.Object, this.dataQueue.Object);
            Action action3 = () => new ScheduleSendFunction(this.notificationDataRepository.Object, this.sentNotificationDataRepository.Object, null /*prepareToSendQueue*/, this.dataQueue.Object);
            Action action4 = () => new ScheduleSendFunction(this.notificationDataRepository.Object, this.sentNotificationDataRepository.Object, this.prepareToSendQueue.Object, null /*dataQueue*/);

            // Act and Assert
            action1.Should().Throw<ArgumentNullException>("notificationDataRepository is null.");
            action2.Should().Throw<ArgumentNullException>("sentNotificationDataRepository is null.");
            action3.Should().Throw<ArgumentNullException>("prepareToSendQueue is null.");
            action4.Should().Throw<ArgumentNullException>("dataQueue is null.");
        }
    }
}