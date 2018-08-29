﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using OneBurn.Windows.Shell;
using OneBurn.Windows.Shell.Burn;
using OneBurn.Windows.Wpf.Properties;

namespace OneBurn.Windows.Wpf.Burn
{
    internal sealed class BurnOptionsViewModel : BurnOptionsViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BurnOptionsViewModel" /> class.
        /// </summary>
        public BurnOptionsViewModel()
        {
            Title = "Burn Options";
        }

        /// <inheritdoc />
        /// <summary>
        ///     Loads the data asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            ViewModels = new ObservableCollection<ContextViewModelBase>
            {
                new BurnImageViewModel(),
                new BurnFilesAndFoldersViewModel(),
                new CreateImageFromMediaViewModel(),
                new CreateImageFromFilesAndFoldersViewModel(),
            };

            //await OnSetCurrentViewModel(ViewModels.FirstOrDefault());
        }

        /// <summary>
        ///     Called when [set current view model].
        /// </summary>
        /// <param name="obj">The object.</param>
        private async Task OnSetCurrentViewModel(object obj)
        {
            var retryCount = Settings.Default.GeneralNetworkRetryCount;
            do
            {
                try
                {
                    if (retryCount > 0)
                        retryCount--;

                    CurrentViewModel = (ContextViewModelBase) obj;
                    await CurrentViewModel.InitializeAsync();
                    break;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    await Task.Delay(Settings.Default.GeneralNetworkTimeSpanBetweenRetries);
                }
            } while (retryCount != 0);
        }
    }
}