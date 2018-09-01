using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RocketDivision.StarBurnX;

namespace OneBurn.Windows.Wpf.Services
{
    internal interface IOdaeService
    {
        /// <summary>
        ///     Gets the drives asynchronously.
        /// </summary>
        /// <returns>The drives.</returns>
        Task<IEnumerable<Drive>> GetDrivesAsync();

        /// <summary>
        ///     Gets the drive write speeds asynchronous.
        /// </summary>
        /// <param name="driveInfo">The drive information.</param>
        /// <returns>The drive write speeds.</returns>
        Task<IEnumerable<DriveSpeed>> GetDriveWriteSpeedsAsync(IDriveInfo driveInfo);

        /// <summary>
        ///     Determines whether [is SPTD driver supported asynchronous] [the specified version].
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>
        ///     <see langword="true" /> if [is SPTD driver supported asynchronous] [the specified version]; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        bool IsSptdDriverSupportedAsync(out Version version);
    }
}