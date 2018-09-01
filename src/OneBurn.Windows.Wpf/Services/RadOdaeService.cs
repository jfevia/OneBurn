using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OneBurn.Core;
using OneBurn.Windows.Shell.Messaging;
using OneBurn.Windows.Shell.Services;
using OneBurn.Windows.Wpf.Properties;
using RocketDivision.StarBurnX;

namespace OneBurn.Windows.Wpf.Services
{
    internal class RadOdaeService : IOdaeService
    {
        private StarBurnX _starBurn;

        /// <summary>
        ///     Initializes the <see cref="RadOdaeService" /> class.
        /// </summary>
        static RadOdaeService()
        {
            if (Instance == null)
                Instance = new RadOdaeService();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RadOdaeService" /> class.
        /// </summary>
        public RadOdaeService()
        {
            Initialize();
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static RadOdaeService Instance { get; }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            _starBurn = new StarBurnX();

            var isSPTDSupported = _starBurn.CheckSPTDDriver(out _, out _);
            var keyFilePath = Settings.Default.OpticalDriveAuthoringEngineKeyFilePath.ExpandSpecialFolders();
            var key = File.ReadAllText(keyFilePath);
            _starBurn.InitializeEx(key, isSPTDSupported ? STARBURN_TRANSPORT.STARBURN_TRANSPORT_SPTD : STARBURN_TRANSPORT.STARBURN_TRANSPORT_SPTI);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Determines whether [is SPTD driver supported] [the specified version].
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>
        ///     <see langword="true" /> if [is SPTD driver supported] [the specified version]; otherwise, <see langword="false" />.
        /// </returns>
        public bool IsSptdDriverSupportedAsync(out Version version)
        {
            var result = _starBurn.CheckSPTDDriver(out var majorVersion, out var minorVersion);
            version = result ? default(Version) : new Version(majorVersion, minorVersion);
            return result;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the drives asynchronously.
        /// </summary>
        /// <returns>The drives.</returns>
        public async Task<IEnumerable<Drive>> GetDrivesAsync()
        {
            return await Task.Run(() =>
            {
                var drives = new List<Drive>();
                try
                {
                    foreach (Drive drive in _starBurn.Drives)
                        drives.Add(drive);
                }
                catch (Exception ex)
                {
                    MessagingService.Instance.Send(new ExceptionMessage(ex));
                }

                return drives;
            });
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the drive write speeds asynchronously.
        /// </summary>
        /// <param name="driveInfo">The drive information.</param>
        /// <returns>The drive write speeds.</returns>
        public async Task<IEnumerable<DriveSpeed>> GetDriveWriteSpeedsAsync(IDriveInfo driveInfo)
        {
            return await Task.Run(() =>
            {
                var driveSpeeds = new List<DriveSpeed>();
                try
                {
                    foreach (DriveSpeed driveSpeed in driveInfo.SupportedWriteSpeeds)
                        driveSpeeds.Add(driveSpeed);
                }
                catch (Exception ex)
                {
                    MessagingService.Instance.Send(new ExceptionMessage(ex));
                }

                return driveSpeeds;
            });
        }
    }
}