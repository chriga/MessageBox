﻿// -----------------------------------------------------------------------
// <copyright file="MessageBoxViewModel.cs" company="">
//
// The MIT License (MIT)
// 
// Copyright (c) 2016 Christoph Gattnar
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
// CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// </copyright>
// -----------------------------------------------------------------------

namespace Gat.Controls
{
	using System;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Input;
	using System.Windows.Media;
	using Gat.Controls.Framework;

	public class MessageBoxViewModel : INotifyPropertyChanged
	{
		#region Fields

		private ImageSource _Image;
		private string _Message;
		private string _Caption;
		private string _Ok;
		private string _Yes;
		private string _No;
		private string _Cancel;
		private bool _OkVisibility;
		private bool _YesVisibility;
		private bool _NoVisibility;
		private bool _CancelVisibility;
		private bool _IsOkDefault;
		private bool _IsYesDefault;
		private bool _IsNoDefault;
		private bool _IsCancelDefault;

		private const string _DefaultOk = "Ok";
		private const string _DefaultCancel = "Cancel";
		private const string _DefaultYes = "Yes";
		private const string _DefaultNo = "No";

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler<MessageBoxEventArgs> ShowMessageBoxEventHandler;
		public event EventHandler<MessageBoxEventArgs> CloseMessageBoxEventHandler;
		private Controls.MessageBoxButton _MessageBoxButton;
		private MessageBoxPosition _Position;
		private Window _Owner;

		#endregion

		#region Constructors

		public MessageBoxViewModel()
		{
			Ok = _DefaultOk;
			Yes = _DefaultYes;
			No = _DefaultNo;
			Cancel = _DefaultCancel;

			OkVisibility = true;
			YesVisibility = false;
			NoVisibility = false;
			CancelVisibility = false;

			IsOkDefault = true;
			IsYesDefault = false;
			IsNoDefault = false;
			IsCancelDefault = false;

			OkCommand = new RelayCommand(param => OnOkClicked());
			YesCommand = new RelayCommand(param => OnYesClicked());
			NoCommand = new RelayCommand(param => OnNoClicked());
			CancelCommand = new RelayCommand(param => OnCancelClicked());

			Caption = string.Empty;
			Result = MessageBoxResult.Cancel;
		}

		#endregion

		#region Properties

		public MessageBoxPosition Position
		{
			get
			{
				return _Position;
			}
			set
			{
				if (_Position != value)
				{
					_Position = value;
					OnPropertyChanged("Position");
				}
			}
		}

		public System.Windows.Window Owner
		{
			get
			{
				return _Owner;
			}
			set
			{
				if (_Owner != value)
				{
					_Owner = value;
					OnPropertyChanged("Owner");
				}
			}
		}

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The message box image.</value>
		public ImageSource Image
		{
			get
			{
				return _Image;
			}
			set
			{
				if(_Image != value)
				{
					_Image = value;
					OnPropertyChanged("Image");
				}
			}
		}

		/// <summary>
		/// Gets or sets the message box message.
		/// </summary>
		/// <value>The message.</value>
		public string Message
		{
			get
			{
				return _Message;
			}
			set
			{
				if(_Message != value)
				{
					_Message = value;
					OnPropertyChanged("Message");
				}
			}
		}

		/// <summary>
		/// Gets or sets the message box caption.
		/// </summary>
		/// <value>The caption.</value>
		public string Caption
		{
			get
			{
				return _Caption;
			}
			set
			{
				if(_Caption != value)
				{
					_Caption = value;
					OnPropertyChanged("Caption");
				}
			}
		}

		/// <summary>
		/// Gets or sets the Ok button content.
		/// </summary>
		/// <value>The Ok button content.</value>
		public string Ok
		{
			get
			{
				return _Ok;
			}
			set
			{
				_Ok = value;
				OnPropertyChanged("Ok");
			}
		}

		/// <summary>
		/// Gets or sets the Yes button content.
		/// </summary>
		/// <value>The Yes button content.</value>
		public string Yes
		{
			get
			{
				return _Yes;
			}
			set
			{
				_Yes = value;
				OnPropertyChanged("Yes");
			}
		}

		/// <summary>
		/// Gets or sets the No button content.
		/// </summary>
		/// <value>The No button content.</value>
		public string No
		{
			get
			{
				return _No;
			}
			set
			{
				_No = value;
				OnPropertyChanged("No");
			}
		}

		/// <summary>
		/// Gets or sets the Cancel button content.
		/// </summary>
		/// <value>The Cancel button content.</value>
		public string Cancel
		{
			get
			{
				return _Cancel;
			}
			set
			{
				_Cancel = value;
				OnPropertyChanged("Cancel");
			}
		}

		public MessageBoxButton MessageBoxButton
		{
			get
			{
				return _MessageBoxButton;
			}
			set
			{
				_MessageBoxButton = value;

				switch(value)
				{
					case MessageBoxButton.Ok:
						OkVisibility = true;
						IsOkDefault = true;
						YesVisibility = false;
						IsYesDefault = false;
						NoVisibility = false;
						IsNoDefault = false;
						CancelVisibility = false;
						IsCancelDefault = false;
						Ok = _DefaultOk;
						break;
					case MessageBoxButton.OkCancel:
						OkVisibility = true;
						IsOkDefault = true;
						YesVisibility = false;
						IsYesDefault = false;
						NoVisibility = false;
						IsNoDefault = false;
						CancelVisibility = true;
						IsCancelDefault = false;
						Ok = _DefaultOk;
						Cancel = _DefaultCancel;
						break;
					case MessageBoxButton.YesNo:
						OkVisibility = false;
						IsOkDefault = false;
						YesVisibility = true;
						IsYesDefault = true;
						NoVisibility = true;
						IsNoDefault = false;
						CancelVisibility = false;
						IsCancelDefault = false;
						Yes = _DefaultYes;
						No = _DefaultNo;
						break;
					case MessageBoxButton.YesNoCancel:
						OkVisibility = false;
						IsOkDefault = false;
						YesVisibility = true;
						IsYesDefault = true;
						NoVisibility = true;
						IsNoDefault = false;
						CancelVisibility = true;
						IsCancelDefault = false;
						Yes = _DefaultYes;
						No = _DefaultNo;
						Cancel = _DefaultCancel;
						break;
					default:
						break;
				}
			}
		}

		/// <summary>
		/// Gets or sets the visibility of the Ok button.
		/// </summary>
		/// <value>The visibility of the Ok button.</value>
		public bool OkVisibility
		{
			get
			{
				return _OkVisibility;
			}
			set
			{
				_OkVisibility = value;
				OnPropertyChanged("OkVisibility");
			}
		}

		/// <summary>
		/// Gets or sets the visibility of the Yes button.
		/// </summary>
		/// <value>The visibility of the Yes button.</value>
		public bool YesVisibility
		{
			get
			{
				return _YesVisibility;
			}
			set
			{
				_YesVisibility = value;
				OnPropertyChanged("YesVisibility");
			}
		}

		/// <summary>
		/// Gets or sets the visibility of the No button.
		/// </summary>
		/// <value>The visibility of the No button.</value>
		public bool NoVisibility
		{
			get
			{
				return _NoVisibility;
			}
			set
			{
				_NoVisibility = value;
				OnPropertyChanged("NoVisibility");
			}
		}

		/// <summary>
		/// Gets or sets the visibility of the Cancel button.
		/// </summary>
		/// <value>The visibility of the Cancel button.</value>
		public bool CancelVisibility
		{
			get
			{
				return _CancelVisibility;
			}
			set
			{
				_CancelVisibility = value;
				OnPropertyChanged("CancelVisibility");
			}
		}

		public bool IsOkDefault
		{
			get
			{
				return _IsOkDefault;
			}
			set
			{
				_IsOkDefault = value;
				OnPropertyChanged("IsOkDefault");
			}
		}

		public bool IsYesDefault
		{
			get
			{
				return _IsYesDefault;
			}
			set
			{
				_IsYesDefault = value;
				OnPropertyChanged("IsYesDefault");
			}
		}

		public bool IsNoDefault
		{
			get
			{
				return _IsNoDefault;
			}
			set
			{
				_IsNoDefault = value;
				OnPropertyChanged("IsNoDefault");
			}
		}

		public bool IsCancelDefault
		{
			get
			{
				return _IsCancelDefault;
			}
			set
			{
				_IsCancelDefault = value;
				OnPropertyChanged("IsCancelDefault");
			}
		}

		public ICommand OkCommand
		{
			get;
			private set;
		}

		public ICommand YesCommand
		{
			get;
			private set;
		}

		public ICommand NoCommand
		{
			get;
			private set;
		}

		public ICommand CancelCommand
		{
			get;
			private set;
		}

		public MessageBoxResult Result
		{
			get;
			private set;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Called when a property value has changed.
		/// </summary>
		/// <param name="propertyName">The name of the property that has changed.</param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if(handler != null)
			{
				PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
				handler(this, e);
			}
		}

		private void OnOkClicked()
		{
			Result = MessageBoxResult.Ok;
			Close();
		}

		private void OnYesClicked()
		{
			Result = MessageBoxResult.Yes;
			Close();
		}

		private void OnNoClicked()
		{
			Result = MessageBoxResult.No;
			Close();
		}

		private void OnCancelClicked()
		{
			Result = MessageBoxResult.Cancel;
			Close();
		}

		private void Close()
		{
			if(CloseMessageBoxEventHandler != null)
			{
				CloseMessageBoxEventHandler(this, new MessageBoxEventArgs());
			}
		}

		public void SetDefaultButton(MessageBoxResult defaultButton)
		{
			switch(defaultButton)
			{
				case MessageBoxResult.Ok:
					IsOkDefault = true;
					IsYesDefault = false;
					IsNoDefault = false;
					IsCancelDefault = false;
					break;
				case MessageBoxResult.Cancel:
					IsOkDefault = false;
					IsYesDefault = false;
					IsNoDefault = false;
					IsCancelDefault = true;
					break;
				case MessageBoxResult.Yes:
					IsOkDefault = false;
					IsYesDefault = true;
					IsNoDefault = false;
					IsCancelDefault = false;
					break;
				case MessageBoxResult.No:
					IsOkDefault = false;
					IsYesDefault = false;
					IsNoDefault = true;
					IsCancelDefault = false;
					break;
				default:
					break;
			}
		}

		public MessageBoxResult Show()
		{
			if(ShowMessageBoxEventHandler != null)
			{
				ShowMessageBoxEventHandler(this, new MessageBoxEventArgs() { Caption = Caption, Position = Position, Owner = Owner });
			}
			return Result;
		}

		public MessageBoxResult Show(string message)
		{
			Message = message;
			return Show();
		}

		public MessageBoxResult Show(string message, string caption)
		{
			Message = message;
			Caption = caption;
			return Show();
		}

		public MessageBoxResult Show(string message, ImageSource image)
		{
			Message = message;
			Image = image;
			return Show();
		}

		public MessageBoxResult Show(string message, MessageBoxButton button)
		{
			Message = message;
			MessageBoxButton = button;
			return Show();
		}

		public MessageBoxResult Show(string message, string caption, MessageBoxButton button)
		{
			Message = message;
			Caption = caption;
			MessageBoxButton = button;
			return Show();
		}

		public MessageBoxResult Show(string message, string caption, ImageSource image)
		{
			Message = message;
			Caption = caption;
			Image = image;
			return Show();
		}

		public MessageBoxResult Show(string message, ImageSource image, MessageBoxButton button)
		{
			Message = message;
			Image = image;
			MessageBoxButton = button;
			return Show();
		}

		public MessageBoxResult Show(string message, string caption, MessageBoxButton button, ImageSource image)
		{
			Message = message;
			Caption = caption;
			MessageBoxButton = button;
			Image = image;
			return Show();
		}

		#endregion
	}
}
