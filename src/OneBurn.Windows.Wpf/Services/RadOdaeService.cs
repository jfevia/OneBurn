using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OneBurn.Core;
using OneBurn.Windows.Shell.Messaging;
using OneBurn.Windows.Shell.Services;
using OneBurn.Windows.Wpf.Properties;
using RocketDivision.StarBurnX;
using DriveInfo = RocketDivision.StarBurnX.DriveInfo;

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

        /// <summary>
        ///     Gets the drive write modes asynchronous.ly
        /// </summary>
        /// <param name="driveInfo">The drive information.</param>
        /// <returns>The drive write modes.</returns>
        public async Task<IEnumerable<STARBURN_WRITE_MODE>> GetDriveWriteModesAsync(DriveInfo driveInfo)
        {
            return await Task.Run(() =>
            {
                var writeModes = new List<STARBURN_WRITE_MODE>();
                try
                {
                    var supportedWriteModes = (STARBURN_WRITE_MODE) driveInfo.SupportedModesWrite;
                    if (supportedWriteModes.HasFlag(STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_TAO))
                        writeModes.Add(STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_TAO);

                    if (supportedWriteModes.HasFlag(STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_SAO))
                        writeModes.Add(STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_SAO);

                    if (supportedWriteModes.HasFlag(STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_DAO_16))
                        writeModes.Add(STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_DAO_16);

                    if (supportedWriteModes.HasFlag(STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_DAO_96))
                        writeModes.Add(STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_DAO_96);
                }
                catch (Exception ex)
                {
                    MessagingService.Instance.Send(new ExceptionMessage(ex));
                }

                return writeModes;
            });
        }

        /// <summary>
        ///     Burns the specified file path with the drive.
        /// </summary>
        /// <param name="drive">The drive.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="writeSpeed">The write speed.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="test">if set to <see langword="true" /> [test].</param>
        /// <returns>The task,</returns>
        public async Task Burn(Drive drive, string filePath, IDriveSpeed writeSpeed, STARBURN_WRITE_MODE writeMode, bool test)
        {
            await Task.Run(() =>
            {
                try
                {
                    var imageBurner = new ImageBurner();
                    imageBurner.Drive = drive;
                    imageBurner.ImageFileName = filePath;
                    imageBurner.WriteSpeed = writeSpeed.Speed;
                    imageBurner.Mode = writeMode;
                    imageBurner.Burn(test);
                }
                catch (Exception ex)
                {
                    MessagingService.Instance.Send(new ExceptionMessage(ex));
                }
            });
        }
    }
}