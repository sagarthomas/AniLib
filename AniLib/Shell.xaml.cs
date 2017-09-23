using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AniLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		public Frame AppFrame { get { return Content; } }
        public MainPage()
        {
            this.InitializeComponent();
        }

		private void Option1Button_Checked(object sender, RoutedEventArgs e) {
			AppFrame.Navigate(typeof(AnimeCollection));
		}
		private void Option2Button_Checked(object sender, RoutedEventArgs e) {

		}

		private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
			NavigationPane.IsPaneOpen = !NavigationPane.IsPaneOpen;
			ResizeOptions();
		}

		private void ResizeOptions() {
			//Calculate width of nav pane
			var width = NavigationPane.CompactPaneLength;
			if (NavigationPane.IsPaneOpen)
				width = NavigationPane.OpenPaneLength;

			//change width of all controls in nav pane
			HamburgerButton.Width = width;
			foreach (var option in NavigationMenu.Children) {
				var radioButton = (option as RadioButton);
				if (radioButton != null)
					radioButton.Width = width;
			}
		}
		private void Shell_KeyDown(object sender, KeyRoutedEventArgs e) {
			FocusNavigationDirection direction = FocusNavigationDirection.None;

			switch(e.Key) {

				case Windows.System.VirtualKey.Space:
				case Windows.System.VirtualKey.Enter: {
						var control = FocusManager.GetFocusedElement() as Control;
						var option = control as RadioButton;
						if(option != null) {
							var automation = new RadioButtonAutomationPeer(option);
							automation.Select();
						}
					}
					return;
				// Otherwise. find next focusable element in apprpriate direction

				case Windows.System.VirtualKey.Left:
				case Windows.System.VirtualKey.GamepadDPadLeft:
				case Windows.System.VirtualKey.GamepadLeftThumbstickLeft:
				case Windows.System.VirtualKey.NavigationLeft:
					direction = FocusNavigationDirection.Left;
					break;

				case Windows.System.VirtualKey.Right:
				case Windows.System.VirtualKey.GamepadDPadRight:
				case Windows.System.VirtualKey.GamepadLeftThumbstickRight:
				case Windows.System.VirtualKey.NavigationRight:
					direction = FocusNavigationDirection.Right;
					break;

				case Windows.System.VirtualKey.Up:
				case Windows.System.VirtualKey.GamepadDPadUp:
				case Windows.System.VirtualKey.GamepadLeftThumbstickUp:
				case Windows.System.VirtualKey.NavigationUp:
					direction = FocusNavigationDirection.Up;
					break;

				case Windows.System.VirtualKey.Down:
				case Windows.System.VirtualKey.GamepadDPadDown:
				case Windows.System.VirtualKey.GamepadLeftThumbstickDown:
				case Windows.System.VirtualKey.NavigationDown:
					direction = FocusNavigationDirection.Down;
					break;
			}
			if(direction != FocusNavigationDirection.None) {
				var control = FocusManager.FindNextFocusableElement(direction) as Control;
				if (control != null) {
					control.Focus(FocusState.Programmatic);
					e.Handled = true;
				}

			}
		}
    }
}
