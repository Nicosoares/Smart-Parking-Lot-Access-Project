﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace ClassLibrary1
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;

    /// <summary>
    /// Extension methods for Plate.
    /// </summary>
    public static partial class PlateExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='plate'>
            /// </param>
            public static object CheckPlateAuthStatus(this IPlate operations, string plate)
            {
                return Task.Factory.StartNew(s => ((IPlate)s).CheckPlateAuthStatusAsync(plate), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='plate'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> CheckPlateAuthStatusAsync(this IPlate operations, string plate, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CheckPlateAuthStatusWithHttpMessagesAsync(plate, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='plate'>
            /// </param>
            /// <param name='userID'>
            /// </param>
            public static object AddPlateToUser(this IPlate operations, string plate, string userID)
            {
                return Task.Factory.StartNew(s => ((IPlate)s).AddPlateToUserAsync(plate, userID), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='plate'>
            /// </param>
            /// <param name='userID'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> AddPlateToUserAsync(this IPlate operations, string plate, string userID, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.AddPlateToUserWithHttpMessagesAsync(plate, userID, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
