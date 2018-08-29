using System.Windows;

namespace OneBurn.Windows.Wpf.Shell
{
    public partial class MainMenu
    {
        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(MainMenu), new FrameworkPropertyMetadata { BindsTwoWayByDefault = true });

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:OneBurn.Windows.Wpf.Shell.MainMenu" /> class.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }
    }
}